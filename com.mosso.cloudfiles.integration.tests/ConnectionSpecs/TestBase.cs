using System.Net;
using com.mosso.cloudfiles.domain;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.response;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.integration.tests
{
    public class TestBase
    {
        protected string storageUrl;
        protected string authToken;
        protected IConnection connection;

        [SetUp]
        public void SetUpBase()
        {
            var request = new GetAuthentication(new UserCredentials(Credentials.USERNAME, Credentials.API_KEY));
            var cfrequest = new CloudFilesRequest((HttpWebRequest) WebRequest.Create(request.CreateUri()));
            request.Apply(cfrequest);
            var response =
                new ResponseFactory().Create(cfrequest);
            
            storageUrl = response.Headers[Constants.XStorageUrl];
            authToken = response.Headers[Constants.XAuthToken];
            Assert.That(authToken.Length, Is.EqualTo(36));
            connection = new Connection(new UserCredentials(Credentials.USERNAME, Credentials.API_KEY));
            SetUp();
        }


        protected virtual void SetUp()
        {
        }
    }
    public class SharedTestBase
    {
        protected string storageUrl;
        protected string authToken;
        protected IConnection connection;

        [TestFixtureSetUp]
        public void SetUpBase()
        {
            var request = new GetAuthentication(new UserCredentials(Credentials.USERNAME, Credentials.API_KEY));
            var cfrequest = new CloudFilesRequest((HttpWebRequest)WebRequest.Create(request.CreateUri()));
            request.Apply(cfrequest);
            var response =
                new ResponseFactory().Create(cfrequest);

            storageUrl = response.Headers[Constants.XStorageUrl];
            authToken = response.Headers[Constants.XAuthToken];
            Assert.That(authToken.Length, Is.EqualTo(36));
            connection = new Connection(new UserCredentials(Credentials.USERNAME, Credentials.API_KEY));
            SetUp();
        }


        protected virtual void SetUp()
        {
        }
    }
}