<%@ Page Title="" Language="C#" MasterPageFile="~/Beheerder.master" AutoEventWireup="true" CodeBehind="Rollen.aspx.cs" Inherits="TTEC.Rollen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <h2>Rolbeheer</h2>

    <label>Selecteer een rol:</label>
    <asp:DropDownList ID="ddlRollen" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRollen_SelectedIndexChanged" CssClass="form-select mb-3" />
    <label>Naam:</label>
    <asp:TextBox ID="txtRolNaam" runat="server" CssClass="form-control mb-3" placeholder="Rolnaam" />

    <asp:Button ID="btnNieuw" runat="server" Text="Nieuw" CssClass="btn btn-secondary me-2" OnClick="btnNieuw_Click" />
    <asp:Button ID="btnBewaren" runat="server" Text="Bewaren" CssClass="btn btn-primary me-2" OnClick="btnBewaren_Click" />
    <asp:Button ID="btnVerwijderen" runat="server" Text="Verwijderen" CssClass="btn btn-danger" OnClick="btnVerwijderen_Click" />

    <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 d-block" />
</asp:Content>
