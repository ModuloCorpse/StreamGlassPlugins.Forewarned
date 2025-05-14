using CorpseLib.Web;
using CorpseLib.Web.API;
using System.Reflection;

namespace ForewarnedPlugin.EvidenceGame.Core.Endpoint
{
    public class EvidenceGameRootEndpoint(Core core) : AssemblyResource(true, Assembly.GetExecutingAssembly(), "ForewarnedPlugin.EvidenceGame.Core.overlay.index.html", MIME.TEXT.HTML)
    {
        private readonly Core m_Core = core;
        protected override void OnClientRegistered(WebsocketReference wsReference) => m_Core.ClientConnect(wsReference);
        protected override void OnClientUnregistered(WebsocketReference wsReference) => m_Core.ClientDisconnect(wsReference);
    }
}
