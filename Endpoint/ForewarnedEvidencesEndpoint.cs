using CorpseLib.Web.API;
using CorpseLib.Web.Http;

namespace ForewarnedPlugin.Endpoint
{
    public class ForewarnedEvidencesEndpoint(Core core) : AHTTPEndpoint("/evidences")
    {
        private readonly Core m_Core = core;

        protected override Response OnPostRequest(Request request)
        {
            int requestCount = 0;
            bool haveEvidence = request.HaveParameter("evidence");
            if (haveEvidence)
                requestCount++;

            bool haveRuleOut = request.HaveParameter("rule_out");
            if (haveRuleOut)
                requestCount++;

            bool haveReset = request.HaveParameter("reset");
            if (haveReset)
                requestCount++;

            if (requestCount > 1)
                return new(400, "Bad Request", "Request have multiples request parameters");
            if (requestCount < 1)
                return new(400, "Bad Request", "Missing request parameter");

            if (haveEvidence)
            {
                string evidence = request.GetParameter("evidence");
                if (!string.IsNullOrEmpty(evidence))
                {
                    m_Core.EvidenceToggle(evidence);
                    return new(200, "Ok");
                }
                return new(400, "Bad Request", "Missing evidence value in request parameters");
            }
            else if (haveRuleOut)
            {
                string evidence = request.GetParameter("rule_out");
                if (!string.IsNullOrEmpty(evidence))
                {
                    m_Core.RuleOutToggle(evidence);
                    return new(200, "Ok");
                }
                return new(400, "Bad Request", "Missing evidence value in request parameters");
            }
            else //haveReset is true
            {
                m_Core.Reset();
                return new(200, "Ok");
            }
        }
    }
}
