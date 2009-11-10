using System;
using System.Xml;
using com.mosso.cloudfiles.domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.integration.tests.ConnectionSpecs.GetAccountInformationSpecs
{
    [TestFixture]
    public class When_retrieving_account_information_from_a_container_using_connection : SharedTestBase
    {
        private AccountInformation account;

        protected override void SetUp()
        {
            try
            {
                connection.CreateContainer(Constants.CONTAINER_NAME);
              
                connection.PutStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemName);

                 account = connection.GetAccountInformation();

            }
            finally
            {
                connection.DeleteStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemName);
          
                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }
        }

        [Test]
        public void Should_return_the_size_and_quantity_of_items_in_the_account()
        {
            Assert.That(account, Is.Not.Null);
            Assert.That(account.BytesUsed, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public class When_getting_serialized_account_information_for_an_account_in_json_format_and_container_exists : SharedTestBase
    {
        private string jsonReturnValue;

        protected override void SetUp()
        {

            connection.CreateContainer(Constants.CONTAINER_NAME);
          
          

            try
            {
                connection.PutStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemNameJpg);
                 jsonReturnValue = connection.GetAccountInformationJson();
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

            string expectedSubString = "[{\"name\": \"" + Constants.CONTAINER_NAME + "\", \"count\": 1, \"bytes\": 105542}]";
            Assert.That(jsonReturnValue, Is.EqualTo(expectedSubString));
        }
    }

    [TestFixture]
    public class When_getting_serialized_account_information_for_an_account_in_xml_format_and_container_exists : TestBase
    {
        private XmlDocument xmlReturnValue;
      

         protected override void SetUp()
         {
                 connection.CreateContainer(Constants.CONTAINER_NAME);
             
               

            try
            {
                connection.PutStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemNameJpg);
                 xmlReturnValue = connection.GetAccountInformationXml();

            }
            finally
            {
                connection.DeleteStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemNameJpg);
               
                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }
            
         }

        [Test]
        public void Should_get_serialized_xml_format()
        {
            
        
                string expectedSubString = "<container><name>" + Constants.CONTAINER_NAME + "</name><count>1</count><bytes>105542</bytes></container>";
                Assert.That(xmlReturnValue.InnerXml.IndexOf(expectedSubString) > 0, Is.True);
        }
     
    }
}