using CorpseLib.DataNotation;
using CorpseLib;

namespace ForewarnedPlugin.EvidenceGame.Core
{
    public class Entity(string name, string[] evidences)
    {
        public class DataSerializer : ADataSerializer<Entity>
        {
            protected override OperationResult<Entity> Deserialize(DataObject reader) => new("Not implemented", string.Empty);

            protected override void Serialize(Entity obj, DataObject writer)
            {
                writer["name"] = obj.m_Name;
                writer["is_found"] = obj.m_IsFound;
                writer["is_ruled_out"] = obj.m_IsRuledOut;
            }
        }

        private readonly HashSet<string> m_Evidences = [..evidences];
        private readonly string m_Name = name;
        private bool m_IsFound = false;
        private bool m_IsRuledOut = false;

        public string Name => m_Name;
        public bool IsFound => m_IsFound;
        public bool IsRuledOut => m_IsRuledOut;

        public void SetFound(bool found)
        {
            m_IsFound = found;
            if (found)
                m_IsRuledOut = false;
        }

        public void SetRuledOut(bool ruledOut)
        {
            m_IsRuledOut = ruledOut;
            if (ruledOut)
                m_IsFound = false;
        }

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
