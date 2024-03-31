using CorpseLib.Web.API;
using StreamGlass.Core.Plugin;

namespace ForewarnedPlugin
{
    public class ForewarnedPlugin : APlugin, IAPIPlugin
    {
        public ForewarnedPlugin() : base("forewarned") { }

        protected override PluginInfo GeneratePluginInfo() => new("1.0.0-beta", "ModuloCorpse<https://www.twitch.tv/chaporon_>");

        protected override void OnLoad() { }
        protected override void OnInit() { }

        public AEndpoint[] GetEndpoints() => [
            new ForewarnedEndpoint()
        ];

        protected override void OnUnload() { }
    }
}
