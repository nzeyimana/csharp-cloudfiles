using System;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.MarkContainerAsPublicSpecs
{
    [TestFixture]
    public class when_marking_a_container_as_public_and_cdn_management_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new MarkContainerAsPublic(null,  "containername");
        }
    }

    [TestFixture]
    public class when_marking_a_container_as_public_and_cdn_management_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new MarkContainerAsPublic("", "containername");
        }
    }

  

    [TestFixture]
    public class when_marking_a_container_as_public_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new MarkContainerAsPublic("http://cdnmanagementurl", null);
        }
    }

    [TestFixture]
    public class when_marking_a_container_as_public_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new MarkContainerAsPublic("http://cdnmanagementurl", "");
        }
    }

    [TestFixture]
    public class when_marking_a_container_as_public
    {
        private MarkContainerAsPublic markContainerAsPublic;
        private Mock<ICloudFilesRequest> _mockrequest;

        [SetUp]
        public void setup()
        {
            markContainerAsPublic = new MarkContainerAsPublic("http://cdnmanagementurl",  "containername");
             _mockrequest = new Mock<ICloudFilesRequest>();
          
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(markContainerAsPublic.CreateUri().ToString(), Is.EqualTo("http://cdnmanagementurl/containername"));
        }

        [Test]
        public void should_have_a_http_put_method()
        {

            var request = _mockrequest.Object;
            markContainerAsPublic.Apply(request);
            _mockrequest.VerifySet(x=>x.Method="PUT");
            
        }

        [Test]
        public void should_have_an_auth_token_in_the_headers()
        {
            var request = _mockrequest.Object;
            markContainerAsPublic.Apply(request);
            var headers = new WebHeaderCollection();
            _mockrequest.SetupGet(x => x.Headers).Returns(headers);
            Assert.AreEqual("authtoken", headers[utils.Constants.X_AUTH_TOKEN]);
        
        }
    }
    
}