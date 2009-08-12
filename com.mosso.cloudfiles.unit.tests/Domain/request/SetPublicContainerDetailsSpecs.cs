using System;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.SetPublicContainerDetailsSpecs
{
    [TestFixture]
    public class when_setting_public_container_details_and_cdn_management_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetPublicContainerDetails(null, "containername", true, -1);
        }
    }

    [TestFixture]
    public class when_setting_public_container_details_and_cdn_management_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetPublicContainerDetails("", "containername", true, -1);
        }
    }


    [TestFixture]
    public class when_setting_public_container_details_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetPublicContainerDetails("http://cdnmanagementurl", null, true, -1);
        }
    }

    [TestFixture]
    public class when_setting_public_container_details_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new SetPublicContainerDetails("http://cdnmanagementurl", "", true, -1);
        }
    }

    [TestFixture]
    public class when_setting_public_container_details_and_container_ttl_is_less_than_zero
    {
        [Test]
        public void should_throw_argument_null_exception()
        {
            var setPublicContainerDetails = new SetPublicContainerDetails("http://cdnmanagementurl", "containername", true, -1);
          
            Asserts.AssertHeaders(setPublicContainerDetails,utils.Constants.X_CDN_TTL,null );
            
        }
    }

    [TestFixture]
    public class when_setting_public_container_details
    {
        private SetPublicContainerDetails setPublicContainerDetails;

        [SetUp]
        public void setup()
        {
            setPublicContainerDetails = new SetPublicContainerDetails("http://cdnmanagementurl", "containername", true, 12345);
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(setPublicContainerDetails.CreateUri().ToString(), Is.EqualTo("http://cdnmanagementurl/containername"));
        }

        [Test]
        public void should_have_a_http_post_method()
        {
            var mock = new Mock<ICloudFilesRequest>();
            var headers = new WebHeaderCollection();
            mock.SetupGet(x => x.Headers).Returns(headers);
            setPublicContainerDetails.Apply(mock.Object);
            mock.VerifySet(x => x.Method = "POST");
            
           
        }


        [Test]
        public void should_have_cdn_enabled_in_the_headers()
        {
            Asserts.AssertHeaders(setPublicContainerDetails, utils.Constants.X_CDN_ENABLED, "True");
          
        }

        [Test]
        public void should_have_time_to_live_aka_ttl_in_the_headers()
        {
            Asserts.AssertHeaders(setPublicContainerDetails, utils.Constants.X_CDN_TTL, "12345");
         
        }
    }
}