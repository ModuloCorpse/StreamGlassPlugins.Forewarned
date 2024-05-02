using CorpseLib.Actions;
using StreamGlass.Core;

namespace ForewarnedPlugin.Action
{
    public class ForwarnedEvidenceAction(Core core) : AStreamGlassAction(ms_Definition)
    {
        private readonly static ActionDefinition ms_Definition = new ActionDefinition("ForewarnedEvidence", "Toggle an evidence")
            .AddArgument<string>("evidence", "Evidence to toggle");
        public override bool AllowRemoteCall => true;

        private readonly Core m_Core = core;

        public override object?[] Call(object?[] args)
        {
            m_Core.EvidenceToggle((string)args[0]!);
            return [];
        }
    }
}
