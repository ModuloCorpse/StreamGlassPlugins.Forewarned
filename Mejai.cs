namespace ForewarnedPlugin
{
    public class Mejai(string name)
    {
        private readonly HashSet<string> m_Evidences = [];
        private readonly string m_Name = name;

        public string Name => m_Name;

        public bool HaveEvidences(string evidence) => m_Evidences.Contains(evidence);
    }
}
