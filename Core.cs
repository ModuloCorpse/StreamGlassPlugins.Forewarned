using CorpseLib.DataNotation;
using CorpseLib.Json;
using CorpseLib.Web.API;
using StreamGlass.Core;

namespace ForewarnedPlugin
{
    public class Core
    {
        private readonly Dictionary<string, WebsocketReference> m_WebsocketClients = [];
        //TODO Add Mejai rule out
        private readonly Dictionary<string, Evidence> m_Evidences = [];
        private readonly List<Mejai> m_Mejais = [];
        private readonly List<Mejai> m_PossibleMejais = [];

        public Core()
        {
            AddEvidence("destruction", "forewarned/assets/destruction.png");
            AddEvidence("disturbed_tombs", "forewarned/assets/disturbed_tombs.png");
            AddEvidence("electronic_disturbance", "forewarned/assets/electronic_disturbance.png");
            AddEvidence("extinguish_flames", "forewarned/assets/extinguish_flames.png");
            AddEvidence("footsteps", "forewarned/assets/footsteps.png");
            AddEvidence("magnetic_distortion", "forewarned/assets/magnetic_distortion.png");
            AddEvidence("metallic_signature", "forewarned/assets/metallic_signature.png");
            AddEvidence("radar_detection", "forewarned/assets/radar_detection.png");
            AddEvidence("radioactivity", "forewarned/assets/radioactivity.png");
            AddEvidence("reanimation", "forewarned/assets/reanimation.png");
            AddEvidence("tremors", "forewarned/assets/tremors.png");
            AddEvidence("vocal_response", "forewarned/assets/vocal_response.png");

            AddMejai("Necreph", "radioactivity", "vocal_response", "extinguish_flames", "metallic_signature", "magnetic_distortion", "destruction", "disturbed_tombs", "electronic_disturbance");
            AddMejai("Rathos", "footsteps", "radar_detection", "reanimation", "extinguish_flames", "metallic_signature", "magnetic_distortion", "disturbed_tombs", "electronic_disturbance");
            AddMejai("Dekan", "footsteps", "radar_detection", "reanimation", "radioactivity", "tremors", "vocal_response", "destruction", "disturbed_tombs");
            AddMejai("Ouphris", "footsteps", "reanimation", "radioactivity", "tremors", "vocal_response", "extinguish_flames", "metallic_signature", "magnetic_distortion");
            AddMejai("Talgor", "radar_detection", "reanimation", "radioactivity", "tremors", "extinguish_flames", "magnetic_distortion", "destruction", "electronic_disturbance");
            AddMejai("Ataimon", "footsteps", "radar_detection", "tremors", "vocal_response", "metallic_signature", "destruction", "disturbed_tombs", "electronic_disturbance");
            AddMejai("Ptahmes", "footsteps", "radar_detection", "reanimation", "tremors", "vocal_response", "extinguish_flames", "destruction", "electronic_disturbance");
        }

        private void AddMejai(string name, params string[] evidences) => m_Mejais.Add(new(name, evidences));
        private void AddEvidence(string evidence, string path) => m_Evidences[evidence] = new Evidence(evidence, path);

        private void UpdateClients(DataObject data)
        {
            foreach (WebsocketReference client in m_WebsocketClients.Values)
                client.Send(JsonParser.NetStr(data));
        }

        internal void ClientConnect(WebsocketReference client)
        {
            m_WebsocketClients[client.ClientID] = client;
            client.Send(JsonParser.NetStr(new DataObject()
            {
                { "type", "welcome" },
                { "evidences", m_Evidences.Values.ToArray() },
                { "mejais", m_PossibleMejais.ToArray() }
            }));
        }

        internal void ClientDisconnect(WebsocketReference client) => m_WebsocketClients.Remove(client.ClientID);

        private void UpdateMejais()
        {
            m_PossibleMejais.Clear();
            Evidence[] evidences = [..m_Evidences.Values];
            foreach (Mejai mejai in m_Mejais)
            {
                if (mejai.Validate(evidences))
                    m_PossibleMejais.Add(mejai);
            }
        }

        public void EvidenceToggle(string evidenceName)
        {
            if (m_Evidences.TryGetValue(evidenceName, out Evidence? evidence))
            {
                evidence!.SetFound(!evidence.IsFound);
                UpdateMejais();
                UpdateClients(new DataObject()
                {
                    { "type", "evidence" },
                    { "evidence", evidence },
                    { "mejais", m_PossibleMejais }
                });
            }
        }

        public void RuleOutToggle(string evidenceName)
        {
            if (m_Evidences.TryGetValue(evidenceName, out Evidence? evidence))
            {
                evidence!.SetRuledOut(!evidence.IsRuledOut);
                UpdateMejais();
                UpdateClients(new DataObject()
                {
                    { "type", "evidence" },
                    { "evidence", evidence },
                    { "mejais", m_PossibleMejais }
                });
            }
        }

        public void Reset()
        {
            foreach (var pair in m_Evidences)
            {
                Evidence evidence = pair.Value;
                evidence.SetFound(false);
                evidence.SetRuledOut(false);
            }
            UpdateMejais();
            UpdateClients(new DataObject() { { "type", "reset" } });
            StreamGlassActions.Call("TimerStop", "forewarned");
        }
    }
}
