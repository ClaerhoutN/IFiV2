using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFiV2.Common.Http
{
    public class AddQueryStringArgumentHandler(string _key, string _value) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(request.RequestUri);

            if (string.IsNullOrEmpty(uriBuilder.Query))
            {
                uriBuilder.Query = $"{_key}={_value}";
            }
            else
                uriBuilder.Query = $"{uriBuilder.Query}&{_key}={_value}";
            request.RequestUri = uriBuilder.Uri;
            return base.SendAsync(request, cancellationToken);
        }
    }
}
