<%@ Page Title="Registratie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registratie.aspx.cs" Inherits="TTEC.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/BsValidation.js"></script>
    <script src="js/jquery-3.7.1.js"></script>
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
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="formcontainer" id="form needs-validation">
        <h1>Registreren</h1>
        <label for="TxtVoornaam">Voornaam: </label>
        <asp:TextBox CssClass="form-control" ID="TxtVoornaam" runat="server" placeholder="Je Voornaam." required="true"></asp:TextBox>
        <label for="TxtAchternaam">Achternaam: </label>
        <asp:TextBox CssClass="form-control" ID="TxtAchternaam" runat="server" placeholder="Je Achternaam." required="true"></asp:TextBox>
        <label>Bent u leerkracht op: </label>
        <label>Campus Zenit</label>
        <asp:CheckBox runat="server" ID="CheckZenit" />
        <label>Campus Boomgaard</label>
        <asp:CheckBox runat="server" ID="CheckBoomgaard" />
        <label for="TxtEmail">E-mail:</label>
        <asp:TextBox CssClass="form-control" ID="TxtEmail" runat="server" placeholder="Uw E-mailadres." required="true" onkeypress="return disableSpace(event)"></asp:TextBox>
        <asp:Button ID="BtnRegistreer" runat="server" Text="Registreer" CssClass="btn btn-primary" OnClick="BtnRegistreer_Click" />
        <asp:Label ID="LblRegistratieMessage" runat="server" CssClass="text-danger" Visible="true" Style="display: block;" />
    </div>
</asp:Content>
