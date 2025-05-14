using CorpseLib.Actions;
using StreamGlass.Core;

namespace ForewarnedPlugin.EvidenceGame.Core.Action
{
    public class ResetAction(Core core) : AStreamGlassAction(ms_Definition)
    {
        private readonly static ActionDefinition ms_Definition = new("ForewarnedReset", "Reset all evidences and timers");
        public override bool AllowRemoteCall => true;

        private readonly Core m_Core = core;

        public override object?[] Call(object?[] args)
        {
            m_Core.Reset();
            return [];
        }
    }
}
