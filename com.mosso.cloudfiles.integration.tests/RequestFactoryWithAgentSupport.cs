using System;
using System.Net;
using System.Threading;
using com.mosso.cloudfiles.domain;
using com.mosso.cloudfiles.domain.request;
using com.mosso.cloudfiles.domain.request.Interfaces;

namespace com.mosso.cloudfiles.integration.tests
{
    public class RequestFactoryWithAgentSupport : IRequestFactory
    {
        private readonly string _useragent;

        public RequestFactoryWithAgentSupport(string useragent)
        {
            _useragent = useragent;
        }

        public ICloudFilesRequest Create(Uri uri)
        {
            var webreq = (HttpWebRequest) WebRequest.Create(uri);
            webreq.UserAgent = _useragent;
            return new CloudFilesRequest(webreq);
        }

        public ICloudFilesRequest Create(Uri uri, ProxyCredentials creds)
        {
            throw new NotImplementedException();
        }
    }
}