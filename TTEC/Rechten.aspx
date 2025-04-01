<%@ Page Title="" Language="C#" MasterPageFile="~/Beheerder.master" AutoEventWireup="true" CodeBehind="Rechten.aspx.cs" Inherits="TTEC.Rechten" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/RechtenStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="form-container">
        <label>Selecteer een gebruiker: </label>
        <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
        </asp:DropDownList>
        <div class="user-details">
            <div class="detail-row">
                <asp:Label ID="lblGebruikersnaam" runat="server" Text="Gebruikersnaam:"></asp:Label>
                <asp:Label ID="lblGebruikersnaamValue" runat="server"></asp:Label>
            </div>
            <div class="detail-row">
                <asp:Label ID="lblVoornaam" runat="server" Text="Voornaam:"></asp:Label>
                <asp:Label ID="lblVoornaamValue" runat="server"></asp:Label>
            </div>
            <div class="detail-row">
                <asp:Label ID="lblAchternaam" runat="server" Text="Achternaam:"></asp:Label>
                <asp:Label ID="lblAchternaamValue" runat="server"></asp:Label>
            </div>
            <div class="detail-row">
                <asp:Label ID="lblRol" runat="server" Text="Rol:"></asp:Label>
                <asp:DropDownList ID="ddlRollen" runat="server"></asp:DropDownList>
            </div>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Bewaren" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-positive"></asp:Label>
    </div>
</asp:Content>
