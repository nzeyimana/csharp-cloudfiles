using System;
using com.mosso.cloudfiles.domain.request;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.Domain.request.GetContainersSpecs
{
    [TestFixture]
    public class when_getting_list_of_containers_and_storage_url_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetContainers(null);
        }
    }

    [TestFixture]
    public class when_getting_list_of_containers_and_storage_url_is_emptry_string
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argument_null_exception()
        {
            new GetContainers("");
        }
    }

 
    [TestFixture]
    public class when_getting_list_of_containers
    {
        private GetContainers getContainers;

        [SetUp]
        public void setup()
        {
            getContainers = new GetContainers("http://storageurl");
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(getContainers.CreateUri().ToString(), Is.EqualTo("http://storageurl/"));
        }

        [Test]
        public void should_have_a_http_get_method()
        {
            Asserts.AssertMethod(getContainers, "GET");
            
        }

       
    }
}