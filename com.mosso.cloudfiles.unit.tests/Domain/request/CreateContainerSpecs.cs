using System;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.CreateContainerSpecs
{
    [TestFixture]
    public class when_creating_a_container_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new CreateContainer(null, "containername");        
        }
    }

    [TestFixture]
    public class when_creating_a_container_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new CreateContainer("", "containername");
        }
    }

   



    [TestFixture]
    public class when_creating_a_container_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new CreateContainer("http://storageurl", null);
        }
    }

    [TestFixture]
    public class when_creating_a_container_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new CreateContainer("http://storageUrl", "");
        }
    }

    [TestFixture]
    public class when_creating_a_container
    {
        private CreateContainer createContainer;

        [SetUp]
        public void setup()
        {
            createContainer = new CreateContainer("http://storageurl", "containername");    
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(createContainer.CreateUri().ToString(), Is.EqualTo("http://storageurl/containername"));
        }

        [Test]
        public void should_have_a_http_put_method()
        {
            var mock = new Mock<ICloudFilesRequest>();
            createContainer.Apply(mock.Object);
            mock.VerifySet(x=>x.Method="PUT");
        }

       
    }
}