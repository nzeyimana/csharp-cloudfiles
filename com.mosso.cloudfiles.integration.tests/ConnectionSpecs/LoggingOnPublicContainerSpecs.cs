using System;
using NUnit.Framework;

namespace com.mosso.cloudfiles.integration.tests.ConnectionSpecs
{
    [TestFixture]
    public class LoggingOnPublicContainerSpecs : TestBase
    {
        [Test]
        public void should_add_logging_to_public_container()
        {
            try
            {
                connection.CreateContainer(Constants.CONTAINER_NAME);
                connection.SetLoggingOnPublicContainer(Constants.CONTAINER_NAME, true);
                var container = connection.GetContainers();
            }
            catch (Exception ex)
            {
                var ex1 = ex;
            }
            finally
            {

                connection.DeleteContainer(Constants.CONTAINER_NAME);
            }

        }
    }
}
