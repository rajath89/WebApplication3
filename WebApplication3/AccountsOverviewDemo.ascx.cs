using ge_core.Base;
using ge_core.Coretypes;
using ge_core.DataClass;
using ge_core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class AccountsOverviewDemo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTestData();
                DisplayAccountGroups();
            }
        }

        private void LoadTestData()
        {
            var obj = Util.GetConfigObj();

            var cache = SimpleCache<Session>.Instance;
            var sessionId = Guid.NewGuid().ToString();
            if (obj != null && cache != null && cache.Get(Constants.SESSION_OBJ) == null)
            {
                var sessionObj = new Session { SessionId = sessionId, User = obj.User, Site = obj.CurrentSite };
                cache.Set(Constants.SESSION_OBJ, sessionObj, TimeSpan.FromMinutes(10));
            }


            var acctContext = new ge_core.Context.AccountContext();

            var accountGroups = acctContext.GetAccountGroups();

            Session["AccountGroups"] = accountGroups;
        }

        private void DisplayAccountGroups()
        {
            var accountGroups = Session["AccountGroups"] as List<AccountGroup>;
            if (accountGroups == null) return;

            decimal totalBalance = 0;

            foreach (var group in accountGroups)
            {
                var groupBalance = group.Accounts.Sum(a => a.Balance);
                totalBalance += groupBalance;
                var groupHeaderHtml = $@"
            <div class='section-header'>{group.GroupName} - Total: ${groupBalance:N2}</div>";

                accountContainer.InnerHtml += groupHeaderHtml;

                foreach (var account in group.Accounts)
                {
                    var accountHtml = $@"
                <div class='account-item'>
                    <div class='row'>
                        <div class='col-md-6'>
                            <div class='account-name'>{account.AccountDisplayName}</div>
                        </div>
                        <div class='col-md-3 text-right'>
                            <div class='account-action'>Account number : {account.AccountNumber} <i class='fas fa-angle-down'></i></div>
                        </div>
                        <div class='col-md-3 text-right'>
                            <div class='account-balance'>${account.Balance:N2}</div>
                        </div>
                    </div>
                </div>";
                    accountContainer.InnerHtml += accountHtml;
                }
            }

            var totalBalanceHtml = $@"
                <div class='section-header text-right'>
                    Total Balance of All Groups: ${totalBalance:N2}
                </div>";

            totalBalanceContainer.InnerHtml = totalBalanceHtml;
        }
    }
}