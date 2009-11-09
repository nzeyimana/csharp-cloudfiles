using System;
using System.Net;
using com.mosso.cloudfiles.domain;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.response;
using com.mosso.cloudfiles.exceptions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.integration.tests.domain.PutContainerSpecs
{
    [TestFixture]
    public class When_creating_a_container : TestBase
    {
        [Test]
        [ExpectedException(typeof (ContainerNameException))]
        public void Should_throw_exception_if_container_name_greater_than_256_characters()
        {
            new CreateContainer(storageUrl, Constants.BadContainerName);
            Assert.Fail("Should fail due to container name exceeding 64 characters");
        }

        [Test]
        [ExpectedException(typeof(ContainerNameException))]
        public void Should_throw_exception_if_container_name_contains_a_slash()
        {
            new CreateContainer(storageUrl, Constants.BadContainerNameWithSlash);
            Assert.Fail("Should fail due to container name having a slash");
        }

        [Test]
        [ExpectedException(typeof(ContainerNameException))]
        public void Should_throw_exception_if_container_name_contains_a_question_mark()
        {
            new CreateContainer(storageUrl,  Constants.BadContainerNameWithQuestionMark);
            Assert.Fail("Should fail due to container name having a question mark");
        }

        [Test]
        public void Should_return_created_status_when_the_container_does_not_exist()
        {
            
            CreateContainer createContainer = new CreateContainer(storageUrl, Constants.CONTAINER_NAME);

            IResponse response = new GenerateRequestByType( ).Submit(createContainer, authToken);
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.Created));

            DeleteContainer(storageUrl, Constants.CONTAINER_NAME);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Should_throw_an_exception_when_the_storage_url_is_null()
        {
            new CreateContainer(null, "a");
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Should_throw_an_exception_when_the_container_name_is_null()
        {
            new CreateContainer("a", null);
        }


        [Test]
        public void Should_return_accepted_status_when_the_container_already_exists()
        {
            CreateContainer createContainer = new CreateContainer(storageUrl,  Constants.CONTAINER_NAME);

            IResponse response = new GenerateRequestByType().Submit(createContainer, authToken);
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.Created));

            createContainer = new CreateContainer(storageUrl, Constants.CONTAINER_NAME);

            response = new GenerateRequestByType().Submit(createContainer, authToken);
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.Accepted));

            DeleteContainer(storageUrl, Constants.CONTAINER_NAME);
        }
		
        private void DeleteContainer(string storageUri, string containerName)
        {
            DeleteContainer deleteContainer = new DeleteContainer(storageUri,  containerName);

            IResponse response = new GenerateRequestByType().Submit(deleteContainer, authToken);
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.NoContent));
        }
    }
}