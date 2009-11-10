using System;
using com.mosso.cloudfiles.domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace com.mosso.cloudfiles.unit.tests.domain.UserCredentialsSpecs
{
    [TestFixture]
    public class When_creating_usercredentials_with_auth_url
    {
        private UserCredentials userCreds;
        private ProxyCredentials proxyCredentials;
        private Uri authUrl;

        [SetUp]
        public void SetUp()
        {
            authUrl = new Uri(Constants.AUTH_URL);

            proxyCredentials = new ProxyCredentials(Constants.PROXY_ADDRESS, Constants.PROXY_USERNAME, Constants.PROXY_PASSWORD, Constants.PROXY_DOMAIN);

            userCreds = new UserCredentials(
                authUrl,
                Constants.CREDENTIALS_USER_NAME,
                Constants.CREDENTIALS_PASSWORD,
                Constants.CREDENTIALS_CLOUD_VERSION,
                Constants.CREDENTIALS_ACCOUNT_NAME,
                proxyCredentials
                );
        }

        [Test]
        public void Should_have_username()
        {
            Assert.That(userCreds.Username, Is.EqualTo(Constants.CREDENTIALS_USER_NAME));
        }

        [Test]
        public void Should_have_password()
        {
            Assert.That(userCreds.Api_access_key, Is.EqualTo(Constants.CREDENTIALS_PASSWORD));
        }

        [Test]
        public void Should_have_auth_url()
        {
            Assert.That(userCreds.AuthUrl, Is.EqualTo(authUrl));
        }

        [Test]
        public void Should_have_cloud_version()
        {
            Assert.That(userCreds.Cloudversion, Is.EqualTo(Constants.CREDENTIALS_CLOUD_VERSION));
        }

        [Test]
        public void Should_have_account_name()
        {
            Assert.That(userCreds.AccountName, Is.EqualTo(Constants.CREDENTIALS_ACCOUNT_NAME));
        }

        [Test]
        public void Should_have_proxy_user_name_when_proxy_information_is_set()
        {
            Assert.That(userCreds.ProxyCredentials.ProxyUsername, Is.EqualTo(Constants.PROXY_USERNAME));
        }

        [Test]
        public void Should_have_proxy_password_when_proxy_information_is_set()
        {
            Assert.That(userCreds.ProxyCredentials.ProxyPassword, Is.EqualTo(Constants.PROXY_PASSWORD));
        }

        [Test]
        public void Should_have_proxy_address_when_proxy_information_is_set()
        {
            Assert.That(userCreds.ProxyCredentials.ProxyAddress, Is.EqualTo(Constants.PROXY_ADDRESS));
        }

        [Test]
        public void Should_have_proxy_domain_when_proxy_information_is_set()
        {
            Assert.That(userCreds.ProxyCredentials.ProxyDomain, Is.EqualTo(Constants.PROXY_DOMAIN));
        }
    }

    [TestFixture]
    public class When_creating_user_credentials_without_auth_url
    {
        private UserCredentials userCreds;
        private ProxyCredentials proxyCredentials;

        [SetUp]
        public void Setup()
        {
            proxyCredentials = new ProxyCredentials(Constants.PROXY_ADDRESS, Constants.PROXY_USERNAME, Constants.PROXY_PASSWORD, Constants.PROXY_DOMAIN);

            userCreds = new UserCredentials(
                Constants.CREDENTIALS_USER_NAME,
                Constants.CREDENTIALS_PASSWORD,
                proxyCredentials
                );
        }

        [Test]
        public void Should_default_auth_url_to_mosso_api_url()
        {
            Assert.That(userCreds.AuthUrl.ToString(), Is.EqualTo(utils.Constants.MOSSO_AUTH_URL));
        }
    }
}