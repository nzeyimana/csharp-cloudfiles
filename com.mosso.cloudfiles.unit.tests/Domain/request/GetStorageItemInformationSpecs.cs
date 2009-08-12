using System;
using com.mosso.cloudfiles.domain.request;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.GetStorageItemInformationSpecs
{
    [TestFixture]
    public class when_getting_information_of_a_storage_item_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetStorageItemInformation(null, "containername", "storageitemname");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_storage_item_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetStorageItemInformation("", "containername", "storageitemname");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_storage_item_and_container_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetStorageItemInformation("http://storageurl", null, "storageitemname");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_storage_item_and_container_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetStorageItemInformation("http://storageurl", "", "storageitemname");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_storage_item_and_storage_item_name_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetStorageItemInformation("http://storageurl", "containername", null);
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_storage_item_and_storage_item_name_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetStorageItemInformation("http://storageurl", "containername", "");
        }
    }

    [TestFixture]
    public class when_getting_information_of_a_storage_item
    {
        private GetStorageItemInformation getStorageItemInformation;

        [SetUp]
        public void setup()
        {
            getStorageItemInformation = new GetStorageItemInformation("http://storageurl", "containername", "storageitemname");
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(getStorageItemInformation.CreateUri().ToString(), Is.EqualTo("http://storageurl/containername/storageitemname"));
        }

        [Test]
        public void should_have_a_http_head_method()
        {
            Asserts.AssertMethod(getStorageItemInformation, "HEAD");
  
        }

       
    }

}