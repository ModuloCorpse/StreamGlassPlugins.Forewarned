using StreamGlass.Core;

namespace ForewarnedPlugin
{
    public class Core
    {
        //TODO Add Mejai rule out
        private readonly HashSet<string> m_Evidences = [];
        private readonly HashSet<string> m_RuledOutEvidences = [];
        private readonly List<Mejai> m_Mejais = [];
        private readonly List<Mejai> m_PossibleMejais = [];

        public string[] Evidences => [..m_Evidences];

        private void ValidateMejai(Mejai mejai)
        {
            foreach (string ruledOutEvidence in m_RuledOutEvidences)
            {
                if (mejai.HaveEvidences(ruledOutEvidence))
                    return;
            }
            foreach (string evidence in m_Evidences)
            {
                if (!mejai.HaveEvidences(evidence))
                    return;
            }
            m_PossibleMejais.Add(mejai);
        }

        private void UpdateMejais()
        {
            m_PossibleMejais.Clear();
            foreach (Mejai mejai in m_Mejais)
                ValidateMejai(mejai);
            //TODO Canal update
        }

        public void EvidenceToggle(string evidence)
        {
            m_RuledOutEvidences.Remove(evidence);
            if (!m_Evidences.Remove(evidence))
                m_Evidences.Add(evidence);
            UpdateMejais();
            StreamGlassCanals.Emit(ForewarnedPlugin.Canals.EVIDENCES, evidence);
        }

        public void RuleOutToggle(string evidence)
        {
            m_Evidences.Remove(evidence);
            if (!m_RuledOutEvidences.Remove(evidence))
                m_RuledOutEvidences.Add(evidence);
            UpdateMejais();
            StreamGlassCanals.Emit(ForewarnedPlugin.Canals.RULE_OUT, evidence);
        }

        public void Reset()
        {
            m_Evidences.Clear();
            m_RuledOutEvidences.Clear();
            StreamGlassCanals.Trigger(ForewarnedPlugin.Canals.RESET);
            StreamGlassActions.Call("TimerStop", "forewarned");
        }
    }
}
