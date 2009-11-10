using System;
using System.Xml;
using com.mosso.cloudfiles.domain;
using com.mosso.cloudfiles.exceptions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;

namespace com.mosso.cloudfiles.integration.tests.ConnectionSpecs.GetContainerInformationSpecs
{
    [TestFixture]
    public class When_requesting_information_on_a_container_using_connection : TestBase
    {
        [Test]
        public void Should_return_container_information_when_the_container_exists()
        {

            string containerName = Guid.NewGuid().ToString();
            try
            {
                connection.CreateContainer(containerName);
                Container containerInformation = connection.GetContainerInformation(containerName);

                Assert.That(containerInformation.Name, Is.EqualTo(containerName));
                Assert.That(containerInformation.ByteCount, Is.EqualTo(0));
                Assert.That(containerInformation.ObjectCount, Is.EqualTo(0));
                Assert.That(containerInformation.CdnUri, Is.EqualTo(""));
            }
            finally
            {
                connection.DeleteContainer(containerName);
            }
        }

        [Test]
        [ExpectedException(typeof (ContainerNotFoundException))]
        public void Should_throw_an_exception_when_the_container_does_not_exist()
        {
            
            connection.GetContainerInformation(Constants.CONTAINER_NAME);
        }
    }

    [TestFixture]
    public class When_getting_serialized_container_information_for_a_container_in_json_format_and_objects_exist : SharedTestBase
    {
		private string jsonReturnValue;
	 
		protected override void  SetUp()
		{
			  connection.CreateContainer(Constants.CONTAINER_NAME);

            try
            {
                connection.PutStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemNameJpg);
                jsonReturnValue = connection.GetContainerInformationJson(Constants.CONTAINER_NAME);
              
            }
            finally
            {
                connection.DeleteStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemNameJpg);
                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }
		}
        [Test]
        public void Should_get_serialized_json_format()
        {
              var expectedSubString = "[{\"name\": \"" + Constants.StorageItemNameJpg + "\", \"hash\": \"b44a59383b3123a747d139bd0e71d2df\", \"bytes\": 105542, \"content_type\": \"image\\u002fjpeg\", \"last_modified\": \"" + String.Format("{0:yyyy-MM}", DateTime.Now);

               Assert.That(jsonReturnValue.IndexOf(expectedSubString) == 0, Is.True);
          
        }
    }

    [TestFixture]
    public class When_getting_serialized_container_information_for_a_container_in_xml_format_and_objects_exist : SharedTestBase
    {
		private XmlDocument xmlReturnValue;
		 
		protected override void  SetUp()
        {
		
			 connection.CreateContainer(Constants.CONTAINER_NAME);

            try
            {
				var dict = new Dictionary<string,string>();
				dict.Add("X-User-Agent-ACL", "Mozilla");
				dict.Add("X-Referrer-ACL", "testdomain.com");
                connection.PutStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemNameJpg, dict );
                xmlReturnValue = connection.GetContainerInformationXml(Constants.CONTAINER_NAME);
               
            }
            finally
            {
                connection.DeleteStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemNameJpg);
                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }
		
		}
		[Test]
		public void should_have_serialized_xml()
		{
		
		  		var expectedSubString = "<container name=\"" + Constants.CONTAINER_NAME + "\"><object><name>" + Constants.StorageItemNameJpg + "</name><hash>b44a59383b3123a747d139bd0e71d2df</hash><bytes>105542</bytes><content_type>image/jpeg</content_type><last_modified>" + String.Format("{0:yyyy-MM}", DateTime.Now);

                Assert.That(xmlReturnValue.InnerXml.IndexOf(expectedSubString) > -1, Is.True);
		
		}

    }
	 
}