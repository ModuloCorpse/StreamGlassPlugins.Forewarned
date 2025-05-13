using CorpseLib.Web;
using CorpseLib.Web.API;
using StreamGlass.Core.API.Overlay;
using System.Reflection;
using Path = CorpseLib.Web.Http.Path;

namespace ForewarnedPlugin.overlay
{
    public class ForewarnedRootEndpoint(Core core) : AssemblyResource(Assembly.GetExecutingAssembly(), "ForewarnedPlugin.overlay.forewarned.html", MIME.TEXT.HTML)
    {
        private readonly Core m_Core = core;
        protected override void OnClientRegistered(Path path, WebsocketReference wsReference) => m_Core.ClientConnect(wsReference);
        protected override void OnClientUnregistered(Path path, WebsocketReference wsReference) => m_Core.ClientDisconnect(wsReference);
    }
}
