using System.Xml;
using com.mosso.cloudfiles.domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.integration.tests.ConnectionSpecs
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
                connection.MarkContainerAsPublic(Constants.CONTAINER_NAME);
                connection.SetDetailsOnPublicContainer(Constants.CONTAINER_NAME, false, 7200, "Mozilla", "testdomain.com");
                connection.PutStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemName);
                account = connection.GetAccountInformation();

            }
            finally
            {
                connection.DeleteStorageItem(Constants.CONTAINER_NAME, Constants.StorageItemName);
                connection.MarkContainerAsPrivate(Constants.CONTAINER_NAME);
                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }
        }


    }

    [TestFixture]
    public class When_getting_serialized_account_information_for_an_account_in_json_format_and_container_exists : SharedTestBase
    {
        private string jsonReturnValue;

        protected override void SetUp()
        {

            connection.CreateContainer(Constants.CONTAINER_NAME);
            connection.MarkContainerAsPublic(Constants.CONTAINER_NAME);
            connection.SetDetailsOnPublicContainer(Constants.CONTAINER_NAME, false, 7200,  "testdomain.com","Mozilla");

            try
            {
              
                jsonReturnValue = connection.GetPublicAccountInformationJSON();
            }
            finally
            {
                
                connection.MarkContainerAsPrivate(Constants.CONTAINER_NAME);
                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }
        }


        [Test]
        public void should_get_user_agent_acl()
        {


            StringAssert.Contains("\"referrer_acl\":\"testdomain.com\"", jsonReturnValue);



        }
        [Test]
        public void should_get__referrer_acl()
        {


            StringAssert.Contains("\"useragent_acl\":\"Mozilla\"",jsonReturnValue );
        }
    }

    [TestFixture]
    public class When_getting_serialized_account_information_for_an_account_in_xml_format_and_container_exists : SharedTestBase
    {
        private XmlDocument xmlReturnValue;


        protected override void SetUp()
        {
            connection.CreateContainer(Constants.CONTAINER_NAME);
            connection.MarkContainerAsPublic(Constants.CONTAINER_NAME);
            connection.SetDetailsOnPublicContainer(Constants.CONTAINER_NAME, false, 7200, "testdomain.com", "Mozilla");

            try
            {
              
                xmlReturnValue = connection.GetPublicAccountInformationXML();

            }
            finally
            {
                
                connection.MarkContainerAsPrivate(Constants.CONTAINER_NAME);
                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }

        }


        [Test]
        public void should_get_user_agent_acl()
        {


            StringAssert.Contains("<referrer_acl>testdomain.com</referrer_acl>", xmlReturnValue.InnerXml);



        }
        [Test]
        public void should_get__referrer_acl()
        {


            StringAssert.Contains("<useragent_acl>Mozilla</useragent_acl>", xmlReturnValue.InnerXml);
        }
    }
}