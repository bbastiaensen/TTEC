<%@ Page Title="" Language="C#" MasterPageFile="~/Beheerder.master" AutoEventWireup="true" CodeBehind="Organisaties.aspx.cs" Inherits="TTEC.Organisaties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <h2>OrganisatieBeheer</h2>

    <label>Selecteer een organisatietype:</label>
    <asp:DropDownList ID="ddlOrganisaties" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrganisaties_SelectedIndexChanged" CssClass="form-select mb-3" />
    <label>Organisatie:</label>
    <asp:TextBox ID="txtOrganisatieNaam" runat="server" CssClass="form-control mb-3" placeholder="Naam:" />

    <asp:Button ID="btnNieuw" runat="server" Text="Nieuw" CssClass="btn btn-secondary me-2" OnClick="btnNieuw_Click" />
    <asp:Button ID="btnBewaren" runat="server" Text="Bewaren" CssClass="btn btn-primary me-2" OnClick="btnBewaren_Click" />
    <asp:Button ID="btnVerwijderen" runat="server" Text="Verwijderen" CssClass="btn btn-danger" OnClick="btnVerwijderen_Click" />

    <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 d-block" />
</asp:Content>
