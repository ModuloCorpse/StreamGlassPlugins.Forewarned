using CorpseLib.DataNotation;
using CorpseLib.Web.API;
using ForewarnedPlugin.EvidenceGame.Core.Action;
using ForewarnedPlugin.EvidenceGame.Core.Endpoint;
using StreamGlass.Core;
using StreamGlass.Core.API.Overlay;
using StreamGlass.Core.Plugin;

namespace ForewarnedPlugin.EvidenceGame.Core
{
    public abstract class BasePlugin : APlugin, IAPIPlugin, IOverlayPlugin
    {
        private readonly Core m_Core = new();
        private readonly Overlay m_Overlay = new(string.Empty);

        protected Core Core => m_Core;
        protected Overlay Overlay => m_Overlay;

        static BasePlugin()
        {
            DataHelper.RegisterSerializer(new Entity.DataSerializer());
            DataHelper.RegisterSerializer(new Evidence.DataSerializer());
        }

        protected BasePlugin(string name) : base(name)
        {
            m_Overlay.AddRootResource($"{name}.html", new EvidenceGameRootEndpoint(m_Core));
            m_Overlay.AddAssemblyResource($"stylesheet.css", "ForewarnedPlugin.EvidenceGame.Core.overlay.stylesheet.css");
            m_Overlay.AddAssemblyResource($"script.js", "ForewarnedPlugin.EvidenceGame.Core.overlay.script.js");
            m_Overlay.AddAssemblyResource("assets/cross.png", "ForewarnedPlugin.EvidenceGame.Core.overlay.assets.cross.png");
        }

        protected override void OnLoad()
        {
            StreamGlassActions.AddAction(new EvidenceAction(m_Core));
            StreamGlassActions.AddAction(new ResetAction(m_Core));
            StreamGlassActions.AddAction(new RuleOutEvidenceAction(m_Core));
        }

        public Overlay[] GetOverlays() => [ m_Overlay ];

        public Dictionary<CorpseLib.Web.Http.Path, AEndpoint> GetEndpoints() => new() {
            { new("/evidences"), new EvidencesEndpoint(m_Core) }
        };

        protected override void OnUnload() { }
    }
}
