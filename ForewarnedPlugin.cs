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
            public static readonly string RULE_OUT = "forewarned_rule_out";
        }

        private readonly Core m_Core = new();

        public ForewarnedPlugin() : base("Forewarned") { }

        protected override PluginInfo GeneratePluginInfo() => new("1.0.0-beta", "ModuloCorpse<https://www.twitch.tv/chaporon_>");

        protected override void OnLoad()
        {
            StreamGlassCanals.NewCanal<string>(Canals.EVIDENCES);
            StreamGlassCanals.NewCanal<string>(Canals.RULE_OUT);
            StreamGlassCanals.NewCanal(Canals.RESET);

            StreamGlassActions.AddAction(new ForwarnedEvidenceAction(m_Core));
            StreamGlassActions.AddAction(new ForwarnedResetAction(m_Core));
            StreamGlassActions.AddAction(new ForwarnedRuleOutEvidenceAction(m_Core));
        }

        protected override void OnInit()
        {
            StreamGlassActions.Call("TimerCreate", new Dictionary<string, object?>
            {
                { "duration", 180 },
                { "id", "forewarned3minutes" },
                { "family", "forewarned" },
                { "string_source", "timer_forewarned" }
            });
            StreamGlassActions.Call("TimerCreate", new Dictionary<string, object?>
            {
                { "duration", 360 },
                { "id", "forewarned6minutes" },
                { "family", "forewarned" },
                { "string_source", "timer_forewarned" }
            });
            StreamGlassActions.Call("TimerCreate", new Dictionary<string, object?>
            {
                { "duration", 540 },
                { "id", "forewarned9minutes" },
                { "family", "forewarned" },
                { "string_source", "timer_forewarned" }
            });
            StreamGlassActions.Call("TimerCreate", new Dictionary<string, object?>
            {
                { "duration", 720 },
                { "id", "forewarned12minutes" },
                { "family", "forewarned" },
                { "string_source", "timer_forewarned" }
            });
            StreamGlassActions.Call("TimerCreate", new Dictionary<string, object?>
            {
                { "duration", 900 },
                { "id", "forewarned15minutes" },
                { "family", "forewarned" },
                { "string_source", "timer_forewarned" }
            });
        }

        public AEndpoint[] GetEndpoints() => [
            new ForewarnedEvidencesEndpoint(m_Core),
            new ForewarnedRuleOutEvidencesEndpoint(m_Core)
        ];

        protected override void OnUnload() { }
    }
}
