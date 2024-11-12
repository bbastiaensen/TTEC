<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registratie.aspx.cs" Inherits="TTEC.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/BsValidation.js"></script>
    <script src="js/jquery-3.7.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/RegistratieStyle.css" rel="stylesheet" />
        <script type="text/javascript">
            $(document).ready(function () {
                $("#TxtEmail").keydown(function (event) {
                    if (event.keyCode == 32) {
                        event.preventDefault();
                    }
                });
            });
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentplaceholder1" runat="server">
    <div id="mainform">
        <div>
            Registreren
        </div>
        <div id="form">
            <div class="form-group">
                <label for="TxtVoornaam">Voornaam</label>
                <asp:TextBox ID="TxtVoornaam" runat="server" placeholder="Je Voornaam." required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TxtAchternaam">Achternaam</label>
                <asp:TextBox ID="TxtAchternaam" runat="server" placeholder="Je Achternaam." required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ChckLeerkracht">Bent u leerkracht op Campus zenit en of Campus boomgaard.</label>
                <asp:CheckBox Text="Campus zenit" runat="server" required />
                <asp:CheckBox Text="Campus boomgaard" runat="server" required />
            </div>
            <div>
                <label for="TxtEmail">E-mail:</label>
                <asp:TextBox ID="TxtEmail" runat="server" placeholder="Uw E-mailadres." TextMode="Email" required></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
