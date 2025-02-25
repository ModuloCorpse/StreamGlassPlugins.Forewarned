using CorpseLib.DataNotation;
using CorpseLib.Json;
using CorpseLib.Web;
using CorpseLib.Web.API;
using CorpseLib.Web.Http;

namespace ForewarnedPlugin.Endpoint
{
    public class ForewarnedRuleOutEvidencesEndpoint(Core core) : AHTTPEndpoint("/rule_out")
    {
        private readonly Core m_Core = core;

        protected override Response OnPostRequest(Request request)
        {
            if (request.HaveParameter("evidence"))
            {
                string evidence = request.GetParameter("evidence");
                if (!string.IsNullOrEmpty(evidence))
                {
                    m_Core.RuleOutToggle(evidence);
                    return new(200, "Ok");
                }
                return new(400, "Bad Request", "Missing evidence value in request parameters");
            }
            else
                return new(400, "Bad Request", "Missing evidence parameter");
        }

        protected override Response OnGetRequest(Request request) => new(200, "Ok", JsonParser.NetStr(new DataObject() { { "evidences", m_Core.Evidences } }), MIME.APPLICATION.JSON);
    }
}
