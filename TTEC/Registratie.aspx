<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registratie.aspx.cs" Inherits="TTEC.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/BsValidation.js"></script>
    <script src="js/jquery-3.7.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/RegistratieStyle.css" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css?family=Ubuntu' rel='stylesheet'>
    <script>
        function disableSpace(event) {
            if (event.key === " ") {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentplaceholder1" runat="server">
    <div id="mainform">
        <div id="rf-header">
            Registreren
        </div>
        <div id="form">
            <div class="form-group">
                <label for="TxtVoornaam">Voornaam</label>
                <asp:TextBox ID="TxtVoornaam" runat="server" placeholder="Je Voornaam." required=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TxtAchternaam">Achternaam</label>
                <asp:TextBox ID="TxtAchternaam" runat="server" placeholder="Je Achternaam." required=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ChckLeerkracht">Bent u leerkracht op.</label>
                <asp:CheckBox Text="Campus zenit" runat="server" required="" />
                <asp:CheckBox Text="Campus boomgaard" runat="server" required="" />
            </div>
            <div class="form-group">
                <label for="TxtEmail">E-mail:</label>
                <asp:TextBox ID="TxtEmail" runat="server" placeholder="Uw E-mailadres." TextMode="Email" onkeypress="return disableSpace(event)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="BtnRegistreer" runat="server" Text="Registreer" />
            </div>
        </div>
    </div>
</asp:Content>
