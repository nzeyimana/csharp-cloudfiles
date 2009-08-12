using System;
using System.Net;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.DeleteStorageItemSpecs
{
    [TestFixture]
    public class when_deleting_a_storage_item_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteStorageItem(null,  "containername", "storageitemname");
        }
    }

    [TestFixture]
    public class when_deleting_a_storage_item_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteStorageItem("",  "containername", "storageitemname");
        }
    }

  

    [TestFixture]
    public class when_deleting_a_storage_item_and_storage_item_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteStorageItem("http://storageurl", "containername", null);
        }
    }

    [TestFixture]
    public class when_deleting_a_storage_item_and_storage_item_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteStorageItem("http://storageurl", "containername", "");
        }
    }

    [TestFixture]
    public class when_deleting_a_storage_item_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteStorageItem("http://storageurl", null, "storageitemname");
        }
    }

    [TestFixture]
    public class when_deleting_a_storage_item_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new DeleteStorageItem("http://storageurl", "", "storageitemname");
        }
    }

    [TestFixture]
    public class when_deleting_a_storage_item
    {
        private DeleteStorageItem deleteStorageItem;
        private Mock<ICloudFilesRequest> _mockrequest;

        [SetUp]
        public void setup()
        {
            deleteStorageItem = new DeleteStorageItem("http://storageurl", "containername", "storageitemname");
            _mockrequest = new Mock<ICloudFilesRequest>();
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(deleteStorageItem.CreateUri().ToString(), Is.EqualTo("http://storageurl/containername/storageitemname"));
        }

        [Test]
        public void should_have_a_http_delete_method()
        {
            _mockrequest.SetupGet(x => x.Headers).Returns(new WebHeaderCollection());
            deleteStorageItem.Apply(_mockrequest.Object);
            _mockrequest.VerifySet(x => x.Method = "DELETE");
          
        }

       
    }
}