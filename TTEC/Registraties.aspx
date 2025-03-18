<%@ Page Title="Registraties" Language="C#" MasterPageFile="~/Beheerder.master" AutoEventWireup="true" CodeBehind="Registraties.aspx.cs" Inherits="TTEC.Registraties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.7.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="container">
        <h2>In afwachting van goedkeuring</h2>
        <div class="form-group">
            <asp:Label ID="LblRegistratieMessage" runat="server" CssClass="text-danger" Visible="true" Style="display: block;" />
        </div>
        <asp:GridView ID="gvRegistraties" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnSelectedIndexChanged="gvRegistraties_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Gebruikersnaam" HeaderText="Gebruikersnaam" Visible="false" />
                <asp:BoundField DataField="Voornaam" HeaderText="Voornaam" />
                <asp:BoundField DataField="Achternaam" HeaderText="Achternaam" />
                <asp:BoundField DataField="Gebruikersnaam" HeaderText="E-mail" />

                <asp:TemplateField HeaderText="Actie">
                    <ItemTemplate>
                        <div class="btn-group" role="group">
                            <asp:Button ID="btnGoedkeuren" runat="server" Text="Goedkeuren" CssClass="btn btn-success" OnClick="BtnGoedkeuren_Click"/>
                            <asp:Button ID="btnAfkeuren" runat="server" Text="Afkeuren" CssClass="btn btn-danger"
                                CommandName="Afkeuren" CommandArgument='<%# Eval("Gebruikersnaam") %>' OnClick="BtnAfkeuren_Click" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
