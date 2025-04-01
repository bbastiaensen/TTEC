<%@ Page Title="Aanmelden" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="TTEC.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <div class="LoginContainer">
        <div>LOGIN</div>
        <div>Gebruikersnaam</div>
        <div><asp:TextBox class="form-control" ID="TxtUsername" runat="server"></asp:TextBox></div>
        <div>Wachtwoord</div>
        <div><asp:TextBox type="password" class="form-control" ID="TxtPassword" runat="server"></asp:TextBox></div>
        <div><asp:Button ID="SubmitLogin" runat="server" Text="Button" OnClick="SubmitLogin_Click" /></div>
        <div><asp:Label ID="LblMessage" runat="server" Text=""></asp:Label></div>
    </div>
</asp:Content>
