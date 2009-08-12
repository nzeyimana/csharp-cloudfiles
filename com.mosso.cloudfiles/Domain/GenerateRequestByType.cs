using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using com.mosso.cloudfiles.domain.request.Interfaces;
using com.mosso.cloudfiles.domain.response.Interfaces;
using com.mosso.cloudfiles.utils;

namespace com.mosso.cloudfiles.domain.request
{
   
    public class GenerateRequestByType
    {
        private readonly IRequestFactory _factory;
       
        public GenerateRequestByType():this(new RequestFactory() ){}
        public GenerateRequestByType(IRequestFactory factory)
        {
            _factory = factory;
        }
        private ICloudFilesRequest commonSubmit(IAddToWebRequest requesttype)
        {
            var cfrequest = _factory.Create(requesttype.CreateUri());
            requesttype.Apply(cfrequest);
            return cfrequest;
        }
        public ICloudFilesResponse Submit (IAddToWebRequest requesttype,  string authtoken)
        {
            var cfrequest = commonSubmit(requesttype);
            cfrequest.Headers.Add(Constants.X_AUTH_TOKEN, HttpUtility.UrlEncode(authtoken));
            var response = new ResponseFactory().Create(cfrequest);
            return response;
        }
        public ICloudFilesResponse Submit(IAddToWebRequest requesttype)
        {
            var response = new ResponseFactory().Create(commonSubmit(requesttype));
            return response;
        }


        public ICloudFilesResponse Submit(IAddToWebRequest requesttype, string authtoken, ProxyCredentials credentials)
        {
            var cfrequest = _factory.Create(requesttype.CreateUri(),credentials);
            requesttype.Apply(cfrequest);
            cfrequest.Headers.Add(Constants.X_AUTH_TOKEN, HttpUtility.UrlEncode(authtoken));
            var response = new ResponseFactory().Create(cfrequest);
            return response;
        }
    }
}