<%@ Page Title="Registraties" Language="C#" MasterPageFile="~/Beheerder.master" AutoEventWireup="true" CodeBehind="Registraties.aspx.cs" Inherits="TTEC.Registraties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.7.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="container">
        <h2>In afwachting van goedkeuring</h2>
        <asp:GridView ID="gvRegistraties" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="Voornaam" HeaderText="Voornaam" />
                <asp:BoundField DataField="Achternaam" HeaderText="Achternaam" />
                <asp:BoundField DataField="Gebruikersnaam" HeaderText="E-mail" />
                <asp:BoundField DataField="Campus" HeaderText="Campus" />
                <asp:TemplateField HeaderText="Actie">
                    <ItemTemplate>
                        <asp:Button ID="btnGoedkeuren" runat="server" Text="Goedkeuren" CssClass="btn btn-success"
                            CommandName="Goedkeuren" CommandArgument='<%# Eval("ID") %>' OnClick="BtnGoedkeuren_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
