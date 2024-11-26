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
</asp:Content>
