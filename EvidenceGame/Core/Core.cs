using CorpseLib.DataNotation;
using CorpseLib.Json;
using CorpseLib.Web.API;
using StreamGlass.Core;

namespace ForewarnedPlugin.EvidenceGame.Core
{
    public class Core
    {
        private readonly Dictionary<string, WebsocketReference> m_WebsocketClients = [];
        //TODO Add Mejai rule out
        private readonly Dictionary<string, Evidence> m_Evidences = [];
        private readonly List<Entity> m_Mejais = [];
        private readonly List<Entity> m_PossibleMejais = [];

        public Core()
        {
        }

        public void AddEntity(string name, params string[] evidences) => m_Mejais.Add(new(name, evidences));
        public void AddEvidence(string evidence, string path) => m_Evidences[evidence] = new Evidence(evidence, path);

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
                { "entities", m_PossibleMejais.ToArray() }
            }));
        }

        internal void ClientDisconnect(WebsocketReference client) => m_WebsocketClients.Remove(client.ClientID);

        private void UpdateEntities()
        {
            m_PossibleMejais.Clear();
            Evidence[] evidences = [..m_Evidences.Values];
            foreach (Entity mejai in m_Mejais)
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
                UpdateEntities();
                UpdateClients(new DataObject()
                {
                    { "type", "evidence" },
                    { "evidence", evidence },
                    { "entities", m_PossibleMejais }
                });
            }
        }

        public void RuleOutToggle(string evidenceName)
        {
            if (m_Evidences.TryGetValue(evidenceName, out Evidence? evidence))
            {
                evidence!.SetRuledOut(!evidence.IsRuledOut);
                UpdateEntities();
                UpdateClients(new DataObject()
                {
                    { "type", "evidence" },
                    { "evidence", evidence },
                    { "entities", m_PossibleMejais }
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
            UpdateEntities();
            UpdateClients(new DataObject() { { "type", "reset" } });
            StreamGlassActions.Call("TimerStop", "forewarned");
        }
    }
}
