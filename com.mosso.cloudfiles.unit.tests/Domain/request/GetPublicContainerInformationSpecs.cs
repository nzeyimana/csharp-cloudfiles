using System;
using com.mosso.cloudfiles.domain.request;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.GetPublicContainerInformationSpecs
{
    [TestFixture]
    public class when_getting_information_of_a_public_container_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetPublicContainerInformation(null, "containername");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_public_container_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetPublicContainerInformation("", "containername");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_public_container_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetPublicContainerInformation("http://storageurl", null);
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_public_container_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetPublicContainerInformation("http://storageurl", "");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_public_container
    {
        private GetPublicContainerInformation getPublicContainerInformation;

        [SetUp]
        public void setup()
        {
            getPublicContainerInformation = new GetPublicContainerInformation("http://storageurl", "containername");
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(getPublicContainerInformation.CreateUri().ToString(), Is.EqualTo("http://storageurl/containername?enabled_only=true"));
        }

        [Test]
        public void should_have_a_http_head_method()
        {
            Asserts.AssertMethod(getPublicContainerInformation, "HEAD");
         
        }

       
    }
}