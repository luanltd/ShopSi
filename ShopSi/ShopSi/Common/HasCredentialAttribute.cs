using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;


namespace ShopSi
{
    public class HasCredentialAttribute: AuthorizeAttribute
    {
        public string RoleID { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (Common.CommonLogin)HttpContext.Current.Session[Common.CommonConstant.user_session];
            if (session == null)
            {
                return false;
            }
            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName);
            if (privilegeLevels.Contains(this.RoleID) || session.GroupID==CommonUser.USER_ADMIN)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<string> GetCredentialByLoggedInUser(string userName)
        {

            var credentails = (List<string>)HttpContext.Current.Session[Common.CommonConstant.session_credential];
            return credentails;
        }

    }
}