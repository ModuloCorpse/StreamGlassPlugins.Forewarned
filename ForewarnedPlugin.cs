using CorpseLib.Web.API;
using ForewarnedPlugin.Action;
using ForewarnedPlugin.Endpoint;
using StreamGlass.Core;
using StreamGlass.Core.Plugin;

namespace ForewarnedPlugin
{
    public class ForewarnedPlugin : APlugin, IAPIPlugin
    {
        public static class Canals
        {
            public static readonly string EVIDENCES = "forewarned_evidences";
            public static readonly string RESET = "forewarned_reset";
        }

        private readonly Core m_Core = new();

        public ForewarnedPlugin() : base("Forewarned") { }

        protected override PluginInfo GeneratePluginInfo() => new("1.0.0-beta", "ModuloCorpse<https://www.twitch.tv/chaporon_>");

        protected override void OnLoad()
        {
            StreamGlassCanals.NewCanal<string>(Canals.EVIDENCES);
            StreamGlassCanals.NewCanal(Canals.RESET);

            StreamGlassActions.AddAction(new ForwarnedEvidenceAction(m_Core), false, false, true);
            StreamGlassActions.AddAction(new ForwarnedResetAction(m_Core), false, false, true);
        }

        protected override void OnInit() { }

        public AEndpoint[] GetEndpoints() => [
            new ForewarnedEvidencesEndpoint(m_Core)
        ];

        protected override void OnUnload() { }
    }
}
