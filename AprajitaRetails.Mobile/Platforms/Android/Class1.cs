using Javax.Net.Ssl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace AprajitaRetails.Mobile.Platforms.Android
{
    //internal sealed class CustomAndroidMessageHandler : AndroidMessageHandler
    //{
    //    protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
    //        => new CustomHostnameVerifier();

    //    private sealed class CustomHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    //    {
    //        public bool Verify(string hostname, ISSLSession session)
    //            => HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session)
    //                || (hostname == "152.67.78.183" && session.PeerPrincipal.Name == "CN=152.67.78.183");
    //    }
    //}
}
