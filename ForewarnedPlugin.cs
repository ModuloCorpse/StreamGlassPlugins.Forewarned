using CorpseLib.Json;
using CorpseLib.Web.API;
using StreamGlass.Core.Plugin;
using StreamGlass.Core.Settings;

namespace ForewarnedPlugin
{
    public class ForewarnedPlugin : APlugin
    {
        public ForewarnedPlugin() : base("forewarned") { }

        protected override PluginInfo GeneratePluginInfo() => new("1.0.0-beta", "ModuloCorpse<https://www.twitch.tv/chaporon_>");

        protected override void InitTranslation() { }

        protected override void InitSettings() { }

        protected override void InitPlugin() { }

        protected override void InitCommands() { }

        protected override void InitCanals() { }

        protected override AEndpoint[] GetEndpoints() => [
            new ForewarnedEndpoint()
        ];

        protected override void Unregister() { }

        protected override void Update(long deltaTime) { }

        protected override TabItemContent[] GetSettings() => [];

        protected override void TestPlugin() { }
    }
}
