using System;
using System.Net;

namespace WorldCapitalsAndWeather.Services
{
    public class CarnivalProxy : IWebProxy
    {
        private readonly Uri _proxyUri;
        private readonly string[] _bypassHosts;
        private readonly bool _enabled;

        public CarnivalProxy()
        {
            Credentials = new NetworkCredential("_leadcapture_test", "1e@dc@pt", "");
            _enabled = true;
            _proxyUri = new Uri("http://proxy.carnival.com:8080");
        }

        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination) => _proxyUri;

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
}
