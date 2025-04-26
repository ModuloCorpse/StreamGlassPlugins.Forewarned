using CorpseLib.Web.API;
using ForewarnedPlugin.Action;
using ForewarnedPlugin.Endpoint;
using StreamGlass.Core;
using StreamGlass.Core.API.Overlay;
using StreamGlass.Core.Plugin;

namespace ForewarnedPlugin
{
    public class ForewarnedPlugin : APlugin, IAPIPlugin, IOverlayPlugin
    {
        public static class Canals
        {
            public static readonly string EVIDENCES = "forewarned_evidences";
            public static readonly string RESET = "forewarned_reset";
            public static readonly string RULE_OUT = "forewarned_rule_out";
        }

        private readonly Core m_Core = new();
        private readonly Overlay m_Overlay = new();

        public ForewarnedPlugin() : base("Forewarned")
        {
            m_Overlay.AddRootAssemblyResource("forewarned.html", "ForewarnedPlugin.overlay.forewarned.html");
            m_Overlay.AddAssemblyResource("forewarned.css", "ForewarnedPlugin.overlay.forewarned.css");
            m_Overlay.AddAssemblyResource("forewarned.js", "ForewarnedPlugin.overlay.forewarned.js");
            m_Overlay.AddAssemblyResource("assets/cross.png", "ForewarnedPlugin.overlay.assets.cross.png");
            m_Overlay.AddAssemblyResource("assets/destruction.png", "ForewarnedPlugin.overlay.assets.destruction.png");
            m_Overlay.AddAssemblyResource("assets/disturbed_tombs.png", "ForewarnedPlugin.overlay.assets.disturbed_tombs.png");
            m_Overlay.AddAssemblyResource("assets/electronic_disturbance.png", "ForewarnedPlugin.overlay.assets.electronic_disturbance.png");
            m_Overlay.AddAssemblyResource("assets/extinguish_flames.png", "ForewarnedPlugin.overlay.assets.extinguish_flames.png");
            m_Overlay.AddAssemblyResource("assets/footsteps.png", "ForewarnedPlugin.overlay.assets.footsteps.png");
            m_Overlay.AddAssemblyResource("assets/magnetic_distortion.png", "ForewarnedPlugin.overlay.assets.magnetic_distortion.png");
            m_Overlay.AddAssemblyResource("assets/metallic_signature.png", "ForewarnedPlugin.overlay.assets.metallic_signature.png");
            m_Overlay.AddAssemblyResource("assets/radar_detection.png", "ForewarnedPlugin.overlay.assets.radar_detection.png");
            m_Overlay.AddAssemblyResource("assets/radioactivity.png", "ForewarnedPlugin.overlay.assets.radioactivity.png");
            m_Overlay.AddAssemblyResource("assets/reanimation.png", "ForewarnedPlugin.overlay.assets.reanimation.png");
            m_Overlay.AddAssemblyResource("assets/tremors.png", "ForewarnedPlugin.overlay.assets.tremors.png");
            m_Overlay.AddAssemblyResource("assets/vocal_response.png", "ForewarnedPlugin.overlay.assets.vocal_response.png");
        }

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

        public Overlay[] GetOverlays() => [ m_Overlay ];

        public AEndpoint[] GetEndpoints() => [
            new ForewarnedEvidencesEndpoint(m_Core),
            new ForewarnedRuleOutEvidencesEndpoint(m_Core)
        ];

        protected override void OnUnload() { }
    }
}
