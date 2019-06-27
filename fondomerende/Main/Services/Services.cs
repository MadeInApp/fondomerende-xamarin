using Flurl.Http;
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using fondomerende.Main.Manager;

namespace fondomerende.Main.Services
{
    static class Services
    {
        private static bool test = true;
        
        private readonly static string protocollo = "http://";
        private readonly static string basePath = "fondomerende.madeinapp.net/api";
        private readonly static string authkey = "MEt085D5zxZXK7FES6qMHOrBbuzGPGwBlYzt1cwAJux";
        private static string token = UserManager.Instance.token;
        private readonly static string content = "application/x-www-form-urlencoded; param=value;charset=UTF-8";
        private readonly static string basePathTest = "192.168.0.175:8888";
        private readonly static string authkeyTest = "metticiquellochetipare";

        public static string GetAuthKey()
        {
            if (test) return authkeyTest;
            else return authkey;
        }
        public static string Concatenazione()
        {
            if (test)
            {
                return protocollo + basePathTest;
            }
            return protocollo + basePath;
        }
        public static string LoginUrlRequest()
        {
            string app = Concatenazione();

            Cookie authKey = new Cookie("auth-key", Services.authkey);

            if(test) app.WithCookie("auth-key", authkey)
                        .WithHeader("Content-Type", content);

            else app.WithCookie("auth-key", authkeyTest)
                    .WithHeader("Content-Type", content);


            return app;
        }


        public static string GenericUrlRequest()
        {
            string app = Concatenazione();
            Cookie authKey = new Cookie("auth-key", Services.authkey);
            Cookie token = new Cookie("user-token", Services.token);

            app.WithCookies(new { authKey, token })
               .WithHeader("Content-Type", content);

            return app;
        }
    }
}
