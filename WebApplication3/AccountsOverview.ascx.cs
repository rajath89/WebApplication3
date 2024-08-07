using ge_core.Base;
using ge_core.Coretypes;
using ge_core.DataClass;
using ge_core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class AccountsOverview : System.Web.UI.UserControl
    {
        private List<ge_core.Coretypes.Account> accounts;

        private List<AccountGroup> accountGroups;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTestData();
                BindAccountsToDropdown();
                BindAccountGroupsToDropdown();
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

            accounts = acctContext.GetAllAccounts();

            accountGroups = acctContext.GetAccountGroups();

            //foreach (var account in accts)
            //{
            //    var acct = new Account { AccountNumber = account.AccountNumber, AccountType = account.AccountType, ADX = account.ADX, Balance = account.Balance };

            //    accounts.Add(acct);

            //}

            //accounts = new List<Account>
            //{
            //    new Account { AccountNumber = "123456", ADX = "ADX001", Balance = 1000.50m, AccountType = "Savings" },
            //    new Account { AccountNumber = "789012", ADX = "ADX002", Balance = 2500.75m, AccountType = "Checking" },
            //    new Account { AccountNumber = "345678", ADX = "ADX003", Balance = 300.00m, AccountType = "Business" }
            //};

            Session["Accounts"] = accounts;
            Session["AccountGroups"] = accountGroups;
        }

        private void BindAccountsToDropdown()
        {
            ddlAccounts.DataSource = accounts;
            ddlAccounts.DataTextField = "AccountNumber";
            ddlAccounts.DataValueField = "AccountNumber";
            ddlAccounts.DataBind();
            ddlAccounts.Items.Insert(0, new ListItem("Select an account", ""));
        }

        private void BindAccountGroupsToDropdown()
        {
            ddlAccountGroups.DataSource = accountGroups;
            ddlAccountGroups.DataTextField = "GroupName";
            ddlAccountGroups.DataValueField = "GroupSequenceNumber";
            ddlAccountGroups.DataBind();
            ddlAccountGroups.Items.Insert(0, new ListItem("Select a group", ""));
        }

        protected void ddlAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAccountNumber = ddlAccounts.SelectedValue;
            if (!string.IsNullOrEmpty(selectedAccountNumber))
            {
                List<ge_core.Coretypes.Account> accounts = (List<ge_core.Coretypes.Account>)Session["Accounts"];
                ge_core.Coretypes.Account selectedAccount = accounts.Find(a => a.AccountNumber == selectedAccountNumber);
                gvAccountDetails.DataSource = new List<ge_core.Coretypes.Account> { selectedAccount };
                gvAccountDetails.DataBind();
            }
            else
            {
                gvAccountDetails.DataSource = null;
                gvAccountDetails.DataBind();
            }
        }

        protected void ddlAccountGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccountGroups.SelectedIndex > 0)
            {
                int selectedGroupSeqNum = int.Parse(ddlAccountGroups.SelectedValue);
                List<AccountGroup> accountGroups = (List<AccountGroup>)Session["AccountGroups"];
                AccountGroup selectedGroup = accountGroups.Find(g => g.GroupSequenceNumber == selectedGroupSeqNum);
                gvAccounts.DataSource = selectedGroup.Accounts;
                gvAccounts.DataBind();
            }
            else
            {
                gvAccounts.DataSource = null;
                gvAccounts.DataBind();
            }
        }

        //protected void ddlAccounts_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedAccountNumber = ddlAccounts.SelectedValue;
        //    if (!string.IsNullOrEmpty(selectedAccountNumber))
        //    {
        //        List<ge_core.Coretypes.Account> accounts = (List<ge_core.Coretypes.Account>)Session["Accounts"];
        //        ge_core.Coretypes.Account selectedAccount = accounts.Find(a => a.AccountNumber == selectedAccountNumber);
        //        gvAccountDetails.DataSource = new List<ge_core.Coretypes.Account> { selectedAccount };
        //        gvAccountDetails.DataBind();
        //    }
        //    else
        //    {
        //        gvAccountDetails.DataSource = null;
        //        gvAccountDetails.DataBind();
        //    }
        //}

        //protected void ddlAccountGroups_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cardContainer.Controls.Clear();
        //    if (ddlAccountGroups.SelectedIndex > 0)
        //    {
        //        int selectedGroupSeqNum = int.Parse(ddlAccountGroups.SelectedValue);
        //        List<AccountGroup> accountGroups = (List<AccountGroup>)Session["AccountGroups"];
        //        AccountGroup selectedGroup = accountGroups.Find(g => g.GroupSequenceNumber == selectedGroupSeqNum);

        //        foreach (var account in selectedGroup.Accounts)
        //        {
        //            var cardHtml = $@"
        //            <div class='card'>
        //                <div class='card-body'>
        //                    <h5 class='card-title'>{account.AccountNumber}</h5>
        //                    <p class='card-text'>Type: {account.AccountType}<br>ADX: {account.ADX}<br>Balance: {account.Balance:C}</p>
        //                </div>
        //                <div class='card-footer'>
        //                    <small class='text-muted'>Account Number: {account.AccountNumber}</small>
        //                </div>
        //            </div>";
        //            var literal = new Literal { Text = cardHtml };
        //            cardContainer.Controls.Add(literal);
        //        }
        //    }

        //}

        //----------------------------------------------------------------------------------

        //private void BindAccountGroupsToDropdown()
        //{
        //    ddlAccountGroups.Items.Clear();
        //    ddlAccountGroups.Items.Add(new ListItem("Select a group", ""));
        //    ddlAccountGroups.Items.Add(new ListItem("All Accounts", "AllAccounts"));
        //    ddlAccountGroups.Items.Add(new ListItem("All Account Groups", "AllAccountGroups"));

        //    foreach (var group in accountGroups)
        //    {
        //        ddlAccountGroups.Items.Add(new ListItem(group.GroupName, group.GroupSequenceNumber.ToString()));
        //    }
        //}

        //protected void ddlAccountGroups_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cardContainer.Controls.Clear();
        //    if (ddlAccountGroups.SelectedIndex > 0)
        //    {
        //        string selectedValue = ddlAccountGroups.SelectedValue;

        //        if (selectedValue == "AllAccounts")
        //        {
        //            //List<ge_core.Coretypes.Account> allAccounts = new List<ge_core.Coretypes.Account>();
        //            //foreach (var group in accountGroups)
        //            //{
        //            //    allAccounts.AddRange(group.Accounts);
        //            //}
        //            DisplayAccountsAsCards(null);
        //        }
        //        else if (selectedValue == "AllAccountGroups")
        //        {
        //            DisplayAllAccountGroups();
        //        }
        //        else
        //        {
        //            int selectedGroupSeqNum = int.Parse(selectedValue);
        //            AccountGroup selectedGroup = accountGroups.Find(g => g.GroupSequenceNumber == selectedGroupSeqNum);
        //            DisplayAccountsAsCards(selectedGroup.Accounts);
        //        }
        //    }
        //}

        //private void DisplayAccountsAsCards(ge_core.Coretypes.Account[] accounts)
        //{
        //    var accounts1 = (List<ge_core.Coretypes.Account>)Session["Accounts"];
        //    foreach (var account in accounts1)
        //    {
        //        var cardHtml = $@"
        //        <div class='card'>
        //            <div class='card-body'>
        //                <h5 class='card-title'>{account.AccountNumber}</h5>
        //                <p class='card-text'>Type: {account.AccountType}<br>ADX: {account.ADX}<br>Balance: {account.Balance:C}</p>
        //            </div>
        //            <div class='card-footer'>
        //                <small class='text-muted'>Account Number: {account.AccountNumber}</small>
        //            </div>
        //        </div>";
        //        var literal = new Literal { Text = cardHtml };
        //        cardContainer.Controls.Add(literal);
        //    }
        //}

        //private void DisplayAllAccountGroups()
        //{
        //    List<AccountGroup> accountGroups = (List<AccountGroup>)Session["AccountGroups"];
        //    foreach (var group in accountGroups)
        //    {
        //        foreach (var account in group.Accounts)
        //        {
        //            var cardHtml = $@"
        //            <div class='card'>
        //                <div class='card-body'>
        //                    <h5 class='card-title'>{account.AccountNumber}</h5>
        //                    <p class='card-text'>Type: {account.AccountType}<br>ADX: {account.ADX}<br>Balance: {account.Balance:C}</p>
        //                </div>
        //                <div class='card-footer'>
        //                    <small class='text-muted'>Group: {group.GroupName}<br>Account Number: {account.AccountNumber}</small>
        //                </div>
        //            </div>";
        //            var literal = new Literal { Text = cardHtml };
        //            cardContainer.Controls.Add(literal);
        //        }
        //    }
        //}
    }
}