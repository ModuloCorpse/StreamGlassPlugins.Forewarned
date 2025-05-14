using CorpseLib.DataNotation;
using CorpseLib;

namespace ForewarnedPlugin.EvidenceGame.Core
{
    public class Evidence(string name, string assetPath)
    {
        public class DataSerializer : ADataSerializer<Evidence>
        {
            protected override OperationResult<Evidence> Deserialize(DataObject reader) => new("Not implemented", string.Empty);

            protected override void Serialize(Evidence obj, DataObject writer)
            {
                writer["id"] = obj.m_Name;
                writer["path"] = obj.m_AssetPath;
                writer["is_found"] = obj.m_IsFound;
                writer["is_ruled_out"] = obj.m_IsRuledOut;
            }
        }

        private readonly string m_Name = name;
        private readonly string m_AssetPath = assetPath;
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
    }
}
