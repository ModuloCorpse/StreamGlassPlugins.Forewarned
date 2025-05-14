using ForewarnedPlugin.EvidenceGame.Core;
using StreamGlass.Core;
using StreamGlass.Core.API.Overlay;
using StreamGlass.Core.Plugin;

namespace ForewarnedPlugin
{
    public class ForewarnedPlugin : BasePlugin
    {
        private void AddEvidence(string evidence)
        {
            Overlay.AddAssemblyResource($"assets/{evidence}.png", $"ForewarnedPlugin.overlay.assets.{evidence}.png");
            Core.AddEvidence(evidence, $"assets/{evidence}.png");
        }

        public ForewarnedPlugin() : base("Forewarned")
        {
            AddEvidence("destruction");
            AddEvidence("disturbed_tombs");
            AddEvidence("electronic_disturbance");
            AddEvidence("extinguish_flames");
            AddEvidence("footsteps");
            AddEvidence("magnetic_distortion");
            AddEvidence("metallic_signature");
            AddEvidence("radar_detection");
            AddEvidence("radioactivity");
            AddEvidence("reanimation");
            AddEvidence("tremors");
            AddEvidence("vocal_response");

            Core.AddEntity("Necreph", "radioactivity", "vocal_response", "extinguish_flames", "metallic_signature", "magnetic_distortion", "destruction", "disturbed_tombs", "electronic_disturbance");
            Core.AddEntity("Rathos", "footsteps", "radar_detection", "reanimation", "extinguish_flames", "metallic_signature", "magnetic_distortion", "disturbed_tombs", "electronic_disturbance");
            Core.AddEntity("Dekan", "footsteps", "radar_detection", "reanimation", "radioactivity", "tremors", "vocal_response", "destruction", "disturbed_tombs");
            Core.AddEntity("Ouphris", "footsteps", "reanimation", "radioactivity", "tremors", "vocal_response", "extinguish_flames", "metallic_signature", "magnetic_distortion");
            Core.AddEntity("Talgor", "radar_detection", "reanimation", "radioactivity", "tremors", "extinguish_flames", "magnetic_distortion", "destruction", "electronic_disturbance");
            Core.AddEntity("Ataimon", "footsteps", "radar_detection", "tremors", "vocal_response", "metallic_signature", "destruction", "disturbed_tombs", "electronic_disturbance");
            Core.AddEntity("Ptahmes", "footsteps", "radar_detection", "reanimation", "tremors", "vocal_response", "extinguish_flames", "destruction", "electronic_disturbance");

        }

        protected override PluginInfo GeneratePluginInfo() => new("1.0.0-beta", "ModuloCorpse<https://www.twitch.tv/chaporon_>");

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
    }
}
