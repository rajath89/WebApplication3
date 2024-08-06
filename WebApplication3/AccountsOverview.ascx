<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountsOverview.ascx.cs" Inherits="WebApplication3.AccountsOverview" %>
<div class="account-overview-container">
    <asp:DropDownList ID="ddlAccounts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccounts_SelectedIndexChanged"></asp:DropDownList>
    <asp:GridView ID="gvAccountDetails" runat="server" AutoGenerateColumns="true"></asp:GridView>
</div>

<div class="account-group-overview-container">
    <asp:DropDownList ID="ddlAccountGroups" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountGroups_SelectedIndexChanged"></asp:DropDownList>
    <asp:GridView ID="gvAccounts" runat="server" AutoGenerateColumns="true"></asp:GridView>
</div>

</div>
