using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using dziennik_asp_mvc.Models;
using dziennik_asp_mvc.Helpers;

namespace dziennik_asp_mvc.Controllers
{
    public class AccountController : Controller
    {
        public DziennikMembershipProvider MembershipService { get; set; }
        public DziennikRoleProvider AuthorizationService { get; set; }

        protected override void Initialize(RequestContext requestContext)       // niestety nie udało się wstrzykiwać ponieważ obiekty są tworozne przez platforme 
        {
            if (MembershipService == null)
                MembershipService = new DziennikMembershipProvider();
            if (AuthorizationService == null)
                AuthorizationService = new DziennikRoleProvider();

            base.Initialize(requestContext);
        }

        public ActionResult Login()
        {
            if (Request.IsAuthenticated && (User.IsInRole("Admin"))) // Jesli ktoś jest zalogowany i chce sie ponownie zalogować
                return RedirectToRoute("Admin_default", new
                {
                    controller = "Profile",
                    action = "Profile"
                });                                          

            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //if (AuthorizationService.IsUserInRole(model.UserName, "Admin"))
                        //{ // Jeśli zalogował się admin to przenieś do jego View
                        //    return RedirectToRoute("Admin_default", new
                        //    {
                        //        controller = "Profile",
                        //        action = "Profile"
                        //    });
                        //}
                        return RedirectToAction("List", "Grades");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login lub hasło jest błędne");
                }

            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }


        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
