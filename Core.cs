using StreamGlass.Core;

namespace ForewarnedPlugin
{
    public class Core
    {
        //TODO Add Mejai rule out
        private readonly HashSet<string> m_Evidences = [];

        public string[] Evidences => [..m_Evidences];

        public void EvidenceToggle(string evidence)
        {
            if (!m_Evidences.Remove(evidence))
                m_Evidences.Add(evidence);
            StreamGlassCanals.Emit(ForewarnedPlugin.Canals.EVIDENCES, evidence);
        }

        public void Reset()
        {
            m_Evidences.Clear();
            StreamGlassCanals.Trigger(ForewarnedPlugin.Canals.RESET);
            StreamGlassCLI.ExecuteCommand("TimerStop forewarned");
        }
    }
}
