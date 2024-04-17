using CorpseLib.Actions;

namespace ForewarnedPlugin.Action
{
    public class ForwarnedResetAction(Core core) : AAction(ms_Definition)
    {
        private readonly static ActionDefinition ms_Definition = new("ForewarnedReset");

        private readonly Core m_Core = core;

        public override object?[] Call(object?[] args)
        {
            m_Core.Reset();
            return [];
        }
    }
}
