using System;
using com.mosso.cloudfiles.exceptions;
using com.mosso.cloudfiles.utils;

namespace com.mosso.cloudfiles.domain.request
{
    public class SetLoggingToContainerRequest : BaseRequest
    {

        public SetLoggingToContainerRequest(string publiccontainer, string AuthToken, string cdnManagmentUrl, bool loggingenabled)
        {
            if (String.IsNullOrEmpty(publiccontainer))
                throw new ArgumentNullException();

            if (!ContainerNameValidator.Validate(publiccontainer)) throw new ContainerNameException();

            Uri = new Uri(cdnManagmentUrl + "/" + publiccontainer.Encode());
            Method = "PUT";
            string enabled = "False";
            if (loggingenabled)
                enabled = "True";
            headers.Add("X-Log-Retention", enabled);
            AddAuthTokenToHeaders(AuthToken);
        }
    }
}