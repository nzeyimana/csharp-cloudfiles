using System;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.DeleteContainerSpecs
{
    [TestFixture]
    public class when_deleting_a_container_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteContainer(null,  "containername");
        }
    }

    [TestFixture]
    public class when_deleting_a_container_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteContainer("", "containername");
        }
    }



    [TestFixture]
    public class when_deleting_a_container_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteContainer("http://storageurl", null);
        }
    }

    [TestFixture]
    public class when_deleting_a_container_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteContainer("http://storageUrl", "");
        }
    }

    [TestFixture]
    public class when_deleting_a_container
    {
        private DeleteContainer deleteContainer;
        private Mock<ICloudFilesRequest> _mockrequest;
        [SetUp]
        public void setup()
        {
            deleteContainer = new DeleteContainer("http://storageurl", "containername");
            _mockrequest = new Mock<ICloudFilesRequest>();
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
           
            Assert.That(deleteContainer.CreateUri().ToString(), Is.EqualTo("http://storageurl/containername"));
        }

        [Test]
        public void should_have_a_http_delete_method()
        {
            deleteContainer.Apply(_mockrequest.Object);
            _mockrequest.VerifySet(x => x.Method = "DELETE");
        }

     
    }
}