using CorpseLib.Actions;

namespace ForewarnedPlugin.Action
{
    public class ForwarnedEvidenceAction(Core core) : AAction(ms_Definition)
    {
        private readonly static ActionDefinition ms_Definition = new ActionDefinition("ForewarnedEvidence")
            .AddArgument<string>("evidence");

        private readonly Core m_Core = core;

        public override object?[] Call(object?[] args)
        {
            m_Core.EvidenceToggle((string)args[0]!);
            return [];
        }
    }
}
