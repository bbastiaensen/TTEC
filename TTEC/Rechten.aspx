<%@ Page Title="" Language="C#" MasterPageFile="~/Beheerder.master" AutoEventWireup="true" CodeBehind="Rechten.aspx.cs" Inherits="TTEC.Rechten" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="form-container">
        <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
        </asp:DropDownList>
        <div class="user-details">
            <div class="detail-row">
                <asp:Label ID="lblUsername" runat="server" Text="Gebruikersnaam:"></asp:Label>
                <asp:Label ID="lblUsernameValue" runat="server"></asp:Label>
            </div>
            <div class="detail-row">
                <asp:Label ID="lblFirstName" runat="server" Text="Voornaam:"></asp:Label>
                <asp:Label ID="lblFirstNameValue" runat="server"></asp:Label>
            </div>
            <div class="detail-row">
                <asp:Label ID="lblLastName" runat="server" Text="Achternaam:"></asp:Label>
                <asp:Label ID="lblLastNameValue" runat="server"></asp:Label>
            </div>
            <div class="detail-row">
                <asp:Label ID="lblRole" runat="server" Text="Rol:"></asp:Label>
                <asp:DropDownList ID="ddlRoles" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Bewaren" OnClick="btnSave_Click" />
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
