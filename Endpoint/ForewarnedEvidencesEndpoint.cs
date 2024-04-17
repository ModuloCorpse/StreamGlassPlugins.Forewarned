using CorpseLib.Json;
using CorpseLib.Web;
using CorpseLib.Web.API;
using CorpseLib.Web.Http;

namespace ForewarnedPlugin.Endpoint
{
    public class ForewarnedEvidencesEndpoint(Core core) : AHTTPEndpoint("/evidences")
    {
        private readonly Core m_Core = core;

        protected override Response OnPostRequest(Request request)
        {
            if (request.HaveParameter("evidence") && request.HaveParameter("reset"))
                return new(400, "Bad Request", "Request have both evidence and reset parameter");

            if (request.HaveParameter("evidence"))
            {
                string evidence = request.GetParameter("evidence");
                if (!string.IsNullOrEmpty(evidence))
                {
                    m_Core.EvidenceToggle(evidence);
                    return new(200, "Ok");
                }
                return new(400, "Bad Request", "Missing evidence value in request parameters");
            }
            else if (request.HaveParameter("reset"))
            {
                m_Core.Reset();
                return new(200, "Ok");
            }
            else
                return new(400, "Bad Request", "Missing evidence or reset parameter");
        }

        protected override Response OnGetRequest(Request request) => new(200, "Ok", new JsonObject() { { "evidences", m_Core.Evidences } }.ToNetworkString(), MIME.APPLICATION.JSON);
    }
}
