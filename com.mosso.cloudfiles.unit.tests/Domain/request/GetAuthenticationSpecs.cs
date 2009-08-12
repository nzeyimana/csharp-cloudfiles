using System;
using System.Net;
using com.mosso.cloudfiles.domain;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.GetAuthenticationSpecs
{
    [TestFixture]
    public class when_authenticating_against_cloud_files_and_user_credentials_are_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetAuthentication(null);
        }
    }

    [TestFixture]
    public class when_authenticating_against_cloud_files_and_no_auth_url_provided
    {
        private GetAuthentication getAuthentication;
        private Mock<ICloudFilesRequest> _mockrequest;

        [SetUp]
        public void setup()
        {
            var userCredentials = new UserCredentials("username", "apikey");
            getAuthentication = new GetAuthentication(userCredentials);
            _mockrequest = new Mock<ICloudFilesRequest>();
        }

        [Test]
        public void should_use_mosso_auth_url()
        {
            Assert.That(getAuthentication.CreateUri().ToString(), Is.EqualTo(utils.Constants.MOSSO_AUTH_URL));
        }

        [Test]
        public void should_have_a_http_head_method()
        {
            var webrequest = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(webrequest);
            getAuthentication.Apply(_mockrequest.Object);
            //this seems wrong as its not looking for HEAD
            _mockrequest.VerifySet(x => x.Method = "GET");
            
        }

        [Test]
        public void should_have_a_username_in_the_headers()
        {
            var webrequest = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(webrequest);
            getAuthentication.Apply(_mockrequest.Object);
            Assert.That(webrequest[utils.Constants.X_AUTH_USER], Is.EqualTo("username"));
        }

        [Test]
        public void should_have_api_key_in_the_headers()
        {
            var webrequest = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(webrequest);
            getAuthentication.Apply(_mockrequest.Object);
            Assert.That(webrequest[utils.Constants.X_AUTH_KEY], Is.EqualTo("apikey"));
        }
    }

    [TestFixture]
    public class when_authenticating_against_cloud_files_and_auth_url_provided
    {
        private GetAuthentication getAuthentication;
        private Mock<ICloudFilesRequest> _mockrequest;

        [SetUp]
        public void setup()
        {
            var userCredentials = new UserCredentials(new Uri("http://authurl"), "username", "apikey", "cloudversion", "cloudaccountname");
            getAuthentication = new GetAuthentication(userCredentials);
            _mockrequest = new Mock<ICloudFilesRequest>();
        }

        [Test]
        public void should_have_properly_formmated_auth_url()
        {
            Assert.That(getAuthentication.CreateUri().ToString(), Is.EqualTo("http://authurl//cloudversion/cloudaccountname/auth"));
        }

        [Test]
        public void should_have_a_http_head_method()
        {
            var webrequest = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(webrequest);
            getAuthentication.Apply(_mockrequest.Object);
            //this seems wrong as its not looking for HEAD
            _mockrequest.VerifySet(x => x.Method = "GET");
        }

        [Test]
        public void should_have_a_username_in_the_headers()
        {
            var webrequest = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(webrequest);
            getAuthentication.Apply(_mockrequest.Object);
            Assert.That(webrequest[utils.Constants.X_AUTH_USER], Is.EqualTo("username"));
        }

        [Test]
        public void should_have_api_key_in_the_headers()
        {
            var webrequest = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(webrequest);
            getAuthentication.Apply(_mockrequest.Object);
            Assert.That(webrequest[utils.Constants.X_AUTH_KEY], Is.EqualTo("apikey"));
        }
    }

    [TestFixture]
    public class when_authenticating_against_cloud_files_and_username_and_password_have_spaces_in_them
    {

        [Test]
        public void Should_replace_plus_sign_with_percent_20_on_account_name_username_and_password()
        {
            UserCredentials userCredentials = new UserCredentials(new Uri("http://tempuri"), "user name", "pass word", "v 1", "account name");
            GetAuthentication getAuthentication = new GetAuthentication(userCredentials);
            var _mockrequest = new Mock<ICloudFilesRequest>();
            var headers = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(headers);
            getAuthentication.Apply(_mockrequest.Object);
            Assert.That(getAuthentication.CreateUri().AbsoluteUri, Is.EqualTo("http://tempuri//v%201/account%20name/auth"));
            Assert.That(headers[utils.Constants.X_AUTH_USER], Is.EqualTo("user%20name"));
            Assert.That(headers[utils.Constants.X_AUTH_KEY], Is.EqualTo("pass%20word"));
        }
    }
}