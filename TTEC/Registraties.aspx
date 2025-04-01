<%@ Page Title="Registraties" Language="C#" MasterPageFile="~/Beheerder.master" AutoEventWireup="true" CodeBehind="Registraties.aspx.cs" Inherits="TTEC.Registraties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/BsValidation.js"></script>
    <script src="js/jquery-3.7.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/RegistratiesStyle.css" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css?family=Ubuntu' rel='stylesheet'>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="container">
        <h2>In afwachting van goedkeuring</h2>
        <div class="form-group">
            <asp:Label ID="LblRegistratieMessage" runat="server" CssClass="text-danger" Visible="true" Style="display: block;" />
        </div>
        <div class="row">
            <asp:Repeater ID="rptRegistraties" runat="server">
                <ItemTemplate>
                    <div class="col-lg-4 col-md-6 col-sm-12">
                        <div class="card mb-4">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Achternaam") %> <%# Eval("Voornaam") %></h5>
                                <p class="card-text">E-mail: <%# Eval("Gebruikersnaam") %></p>
                                <p class="card-text">Campus: <%# Eval("Campus") %></p>
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnGoedkeuren" runat="server" Text="Goedkeuren" CssClass="btn btn-success"
                                        CommandName="Goedkeuren" CommandArgument='<%# Eval("Gebruikersnaam") %>' OnClick="BtnGoedkeuren_Click" />
                                    <asp:Button ID="btnAfkeuren" runat="server" Text="Afkeuren" CssClass="btn btn-danger"
                                        CommandName="Afkeuren" CommandArgument='<%# Eval("Gebruikersnaam") %>' OnClick="BtnAfkeuren_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
