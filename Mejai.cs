using CorpseLib.DataNotation;
using CorpseLib;

namespace ForewarnedPlugin
{
    public class Mejai(string name, string[] evidences)
    {
        public class DataSerializer : ADataSerializer<Mejai>
        {
            protected override OperationResult<Mejai> Deserialize(DataObject reader) => new("Not implemented", string.Empty);

            protected override void Serialize(Mejai obj, DataObject writer)
            {
                writer["name"] = obj.m_Name;
            }
        }

        private readonly HashSet<string> m_Evidences = [..evidences];
        private readonly string m_Name = name;

        public string Name => m_Name;

        public bool Validate(Evidence[] evidences)
        {
            foreach (Evidence evidence in evidences)
            {
                if (evidence.IsFound && !m_Evidences.Contains(evidence.Name))
                    return false;
                if (evidence.IsRuledOut && m_Evidences.Contains(evidence.Name))
                    return false;
            }
            return true;
        }
    }
}
