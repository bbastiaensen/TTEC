<%@ Page Title="Aanmelden" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="TTEC.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="mainform">
        <div id="rf-header">Aanmelden</div>
        <div class="form-group">
            <div class="form-group">
                <label for="TxtGebruikersnaam">Gebruikersnaam</label>
                <asp:TextBox CssClass="form-control" ID="TxtGebruikersnaam" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TxtWachtwoord">Wachtwoord</label>
                <asp:TextBox type="password" CssClass="form-control" ID="TxtPassword" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <div class="button-container">
                    <div>
                        <asp:Button ID="BtnAanmelden" runat="server" Text="Aanmelden" CssClass="btn btn-primary" OnClick="SubmitLogin_Click" />
                    </div>
                    <div>
                        <asp:Label ID="LblAanmeldMessage" CssClass="text-danger" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
