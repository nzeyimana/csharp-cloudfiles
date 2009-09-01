using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using com.mosso.cloudfiles.unit.tests.CustomMatchers;
using Moq;
using SpecMaker.Core;

namespace com.mosso.cloudfiles.unit.tests.Domain.request
{
    public class SetLoggingToContainerRequestSpec : BaseSpec
    {
        #region setup infoz
        private Mock<ICloudFilesRequest> requestmock;
        private WebHeaderCollection webheaders;
        private void SetupApply(bool isenabled)
        {
            var loggingtopublicontainer = new SetLoggingToContainerRequest("fakecontainer", "http://fake", isenabled);
            requestmock = new Mock<ICloudFilesRequest>();
            webheaders = new WebHeaderCollection();
            requestmock.SetupGet(x => x.Headers).Returns(webheaders);
            loggingtopublicontainer.Apply(requestmock.Object);
        }
        #endregion

        public void when_creating_the_uri()
        {
            const string container = "mycontainer";
            const string url = "http://myurl.com";

            var loggingtopublicontainer = new SetLoggingToContainerRequest(container, url, true);
            var uri = loggingtopublicontainer.CreateUri();

            should("use a management url as the base url", () => uri.StartsWith(url));
            should("put public container at end of url", () => uri.EndsWith(container));
        }
     
        public void when_logging_is_not_set()
        {
            SetupApply(false);

            should("set method to put", () => requestmock.VerifySet(x => x.Method = "PUT"));
            should("set X-Log-Retention to False", () => webheaders.KeyValueFor("X-Log-Retention").HasValueOf("False"));
        }
        public void when_logging_is_set()
        {

            SetupApply(true);

            should("set method to put", () => requestmock.VerifySet(x => x.Method = "PUT"));
            should("set X-Log-Retention to True", () => webheaders.KeyValueFor("X-Log-Retention").HasValueOf("True"));
        }
    }
}