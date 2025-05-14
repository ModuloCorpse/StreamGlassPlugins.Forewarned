using CorpseLib.Actions;
using StreamGlass.Core;

namespace ForewarnedPlugin.EvidenceGame.Core.Action
{
    public class RuleOutEvidenceAction(Core core) : AStreamGlassAction(ms_Definition)
    {
        private readonly static ActionDefinition ms_Definition = new ActionDefinition("ForewarnedRuleOut", "Toggle rule out an evidence")
            .AddArgument<string>("evidence", "Evidence to toggle rule out");
        public override bool AllowRemoteCall => true;

        private readonly Core m_Core = core;

        public override object?[] Call(object?[] args)
        {
            m_Core.RuleOutToggle((string)args[0]!);
            return [];
        }
    }
}
