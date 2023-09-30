using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Mobile
{
    using System.Net.Security;



    public class HttpsClientHandlerService
    {
        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
        var handler = new Xamarin.Android.Net.AndroidMessageHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert != null && cert.Issuer.Equals("CN=152.67.78.183"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = IsHttpsLocalhost
        };
        return handler;
#else
            throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if IOS
    public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
    {
        if (url.StartsWith("https://152.67.78.183"))
            return true;
        return false;
    }
#endif
    }



//    public static class DevHttpsConnectionHelperExtensions
//    {
//        /// <summary>
//        /// Configures HttpClient to use 152.67.78.183 or 10.0.2.2 and bypass certificate checking on Android.
//        /// </summary>
//        /// <param name="sslPort">Development server port</param>
//        /// <returns>The IServiceCollection</returns>
//        public static IServiceCollection AddDevHttpClient(this IServiceCollection services, int sslPort)
//        {
//            var devSslHelper = new DevHttpsConnectionHelper(sslPort);
//            var http = devSslHelper.HttpClient;
//            http.BaseAddress = new Uri(devSslHelper.DevServerRootUrl);
//            services.AddScoped(sp => http);
//            return services;
//        }
//    }
//    public class DevHttpsConnectionHelper
//    {
//        public DevHttpsConnectionHelper(int sslPort)
//        {
//            SslPort = sslPort;
//            DevServerRootUrl = FormattableString.Invariant($"https://{DevServerName}:{SslPort}");
//            LazyHttpClient = new Lazy<HttpClient>(() => new HttpClient(GetPlatformMessageHandler()));
//        }

//        public int SslPort { get; }

//        public string DevServerName =>
//#if WINDOWS
//            "152.67.78.183";
//#elif ANDROID
//        "152.67.78.183";
//#else
//        throw new PlatformNotSupportedException("Only Windows and Android currently supported.");
//#endif

//        public string DevServerRootUrl { get; }

//        private Lazy<HttpClient> LazyHttpClient;
//        public HttpClient HttpClient => LazyHttpClient.Value;

//        public HttpMessageHandler? GetPlatformMessageHandler()
//        {
//#if WINDOWS
//            return null;
//#elif ANDROID
//        var handler = new CustomAndroidMessageHandler();
//        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
//        {
//            if (cert != null && cert.Issuer.Equals("CN=152.67.78.183"))
//                return true;
//            return errors == SslPolicyErrors.None;
//        };
//        return handler;

//#else
//        throw new PlatformNotSupportedException("Only Windows and Android currently supported.");
//#endif
//        }

//#if ANDROID
//    internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
//    {
//        protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
//            => new CustomHostnameVerifier();

//        private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
//        {
//            public bool Verify(string? hostname, Javax.Net.Ssl.ISSLSession? session)
//            {
//                return
//                    Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session)
//                    || hostname == "152.67.78.183" && session.PeerPrincipal?.Name == "CN=152.67.78.183";
//            }
//        }
//    }
//#endif
//    }
}
