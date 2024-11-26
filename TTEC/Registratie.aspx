<%@ Page Title="" Language="C#" MasterPageFile="~/Bezoeker.master" AutoEventWireup="true" CodeBehind="Registratie.aspx.cs" Inherits="TTEC.Registratie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div id="rf-header">
        Registreren
    </div>
    <div id="form needs-validation">
        <div class="form-group">
            <label for="TxtVoornaam">Voornaam</label>
            <asp:TextBox ID="TxtVoornaam" runat="server" placeholder="Je Voornaam." required=""></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TxtAchternaam">Achternaam</label>
            <asp:TextBox ID="TxtAchternaam" runat="server" placeholder="Je Achternaam." required=""></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Bent u leerkracht op:</label>
            <div class="checkbox-container">
                <label>
                    Campus Zenit
        <asp:CheckBox runat="server" ID="CheckZenit" required="" />
                </label>
            </div>
            <div class="form-group">
                <label>
                    Campus Boomgaard
        <asp:CheckBox runat="server" ID="CheckBoomgaard" required="" />
                </label>
                <asp:Label ID="LblErrorMessage" runat="server" ForeColor="Red" />
            </div>
        </div>
        <div class="form-group">
            <label for="TxtEmail">E-mail:</label>
            <asp:TextBox ID="TxtEmail" runat="server" placeholder="Uw E-mailadres." TextMode="Email" required="" pattern="[a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,}$" onkeypress="return disableSpace(event)"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="BtnRegistreer" runat="server" Text="Registreer" OnClick="BtnRegistreer_Click"  />
        </div>
        <asp:Label ID="LblRegistratieMessage" runat="server" />
    </div>
</div>
</asp:Content>
