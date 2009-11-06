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
        private ICloudFilesResponse commonSubmit(IAddToWebRequest requesttype, Func<ICloudFilesRequest> requestfactory, string authtoken)
        {
            var cfrequest = requestfactory.Invoke();
			if(authtoken!=String.Empty)
				 cfrequest.Headers.Add(Constants.X_AUTH_TOKEN, HttpUtility.UrlEncode(authtoken));
            requesttype.Apply(cfrequest);
           	var response = new ResponseFactory().Create(cfrequest);
           	return response;
        }
        public ICloudFilesResponse Submit (IAddToWebRequest requesttype,  string authtoken)
        {
			return commonSubmit(requesttype, ()=>_factory.Create(requesttype.CreateUri()), authtoken);
        }
        public ICloudFilesResponse Submit(IAddToWebRequest requesttype)
        {
			return commonSubmit(requesttype,()=> _factory.Create(requesttype.CreateUri()), "");
        }

        public ICloudFilesResponse Submit(IAddToWebRequest requesttype, string authtoken, ProxyCredentials credentials)
        {
           return commonSubmit(requesttype, ()=> _factory.Create(requesttype.CreateUri(),credentials),authtoken );   
        }
    }
}