using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace HclaimProcessor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //private void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    HttpCookie decryptedCookie =
        //        Context.Request.Cookies[FormsAuthentication.FormsCookieName];

        //    FormsAuthenticationTicket ticket =
        //        FormsAuthentication.Decrypt(decryptedCookie.Value);

        //    var identity = new GenericIdentity(ticket.Name);
        //    var principal = new GenericPrincipal(identity, null);

        //    HttpContext.Current.User = principal;
        //    Thread.CurrentPrincipal = HttpContext.Current.User;
        //}
    }
}
