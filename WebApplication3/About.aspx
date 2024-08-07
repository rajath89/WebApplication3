<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication3.About" %>

<%@ Register TagPrefix="uc" TagName="AccountsOverviewDemo" Src="~/AccountsOverviewDemo.ascx" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<%--    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>
    </main>
     <div class="container mt-4">
        <div class="card">
            <div class="card-header">
                My Merrill Investment Accounts
                <span class="float-right">As of 01/09/2020 12:01 PM ET <a href="#" class="text-secondary">Customize</a></span>
            </div>
            <div class="card-body">
                <div id="accountContainer" runat="server"></div>
            </div>
        </div>
    </div>--%>
    <div class="container mt-4">
        <uc:AccountsOverviewDemo ID="AccountsOverviewDemo" runat="server" />
    </div>
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
</asp:Content>
