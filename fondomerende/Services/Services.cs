using Flurl.Http;
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using fondomerende.Manager;

namespace fondomerende.Services
{
    static class Services
    {
        private readonly static string protocollo = "http://";
        private readonly static string basePath = "192.168.0.175:8888/fondomerende/public/process-request.php";
        private readonly static string authkey = "metticiquellochetipare";
        private static string token = UserManager.Instance.token;
        private readonly static string content = "application/x-www-form-urlencoded; param=value;charset=UTF-8";

        public static string Concatenazione()
        {
            string app;
            app = protocollo + basePath;
            return app;
        }
        public static string LoginUrlRequest()
        {
            string app = Concatenazione();
            Cookie authKey = new Cookie("auth-key", Services.authkey);

            app.WithCookie("auth-key", "metticiquellochetipare")
               .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8");
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
