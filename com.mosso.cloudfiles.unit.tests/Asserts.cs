using System.Net;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;

namespace com.mosso.cloudfiles.unit.tests
{
    public class Asserts
    {
        public static void AssertMethod(IAddToWebRequest addtowebrequest, string method)
        {
            var _mockrequest = new Mock<ICloudFilesRequest>();
            addtowebrequest.Apply(_mockrequest.Object);
            _mockrequest.VerifySet(x => x.Method = method);
        }
        public static void AssertHeaders(IAddToWebRequest addToWebRequest, string headerkey, object headervalue)
        {
            var webresponse = new WebHeaderCollection();
            var _mockrequest = new Mock<ICloudFilesRequest>();
            _mockrequest.SetupGet(x => x.Headers).Returns(webresponse);
            addToWebRequest.Apply(_mockrequest.Object);
            Assert.AreEqual(webresponse[headerkey], headervalue);
        }
        public static Mock<ICloudFilesRequest> GetMock(IAddToWebRequest addtowebrequest)
        {
            var webresponse = new WebHeaderCollection();
            var _mockrequest = new Mock<ICloudFilesRequest>();
            _mockrequest.SetupGet(x => x.Headers).Returns(webresponse);
            addtowebrequest.Apply(_mockrequest.Object);
            return _mockrequest;
        }
    }
}