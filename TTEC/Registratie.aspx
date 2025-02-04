<%@ Page Title="Registratie" Language="C#" MasterPageFile="~/Bezoeker.master" AutoEventWireup="true" CodeBehind="Registratie.aspx.cs" Inherits="TTEC.WebForm1" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div id="mainform">
        <div id="rf-header">Registreren</div>
        <div id="form needs-validation">
            <div class="form-group">
                <label for="TxtVoornaam">Voornaam: </label>
                <asp:TextBox ID="TxtVoornaam" runat="server" placeholder="Je Voornaam." required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TxtAchternaam">Achternaam: </label>
                <asp:TextBox ID="TxtAchternaam" runat="server" placeholder="Je Achternaam." required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Bent u leerkracht op: </label>
                <div class="checkbox-container">
                    <label>Campus Zenit</label>
                    <asp:CheckBox runat="server" ID="CheckZenit" />
                </div>
                <div class="checkbox-container">
                    <label>Campus Boomgaard</label>
                    <asp:CheckBox runat="server" ID="CheckBoomgaard" />
                </div>
            </div>
            <div class="form-group">
                <label for="TxtEmail">E-mail:</label>
                <asp:TextBox ID="TxtEmail" runat="server" TextMode="Email" placeholder="Uw E-mailadres." required="true" onkeypress="return disableSpace(event)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="BtnRegistreer" runat="server" Text="Registreer" CssClass="btn btn-primary" OnClick="BtnRegistreer_Click" />
                <asp:Label ID="LblRegistratieMessage" runat="server" CssClass="text-danger" Visible="true" Style="display: block;" />
            </div>
        </div>
    </div>
</asp:Content>
