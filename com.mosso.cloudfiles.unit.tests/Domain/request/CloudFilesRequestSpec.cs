using System;
using SpecMaker.Core;
using SpecMaker.Core.Matchers;

namespace com.mosso.cloudfiles.unit.tests.Domain
{
    public class CloudFilesRequestSpec: BaseSpec
    {
        
        public void when_getting_response()
        {
            should("set timeout to global constant", RuleIs.Pending);
            should("set user agent to xyz", RuleIs.Pending);
            should("check for range headers", RuleIs.Pending);
            should("check for proxy credentials", RuleIs.Pending);

        }
    }
}