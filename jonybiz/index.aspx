<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="jonybiz.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Css.css" rel="stylesheet" />

    <h1>  KUPOVINA KURSEVA  </h1>
    <br />
    <br />
    <br />

     <h3 class="boja"> Biranje Kurseva  </h3>
    <br />
    <div class="form-group">
         
        <asp:Label ID="Label1" runat="server" Text="Puno ime:"></asp:Label><br />
        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="Email:"></asp:Label><br />
        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox><br />
        <asp:Label ID="Label3" runat="server" Text="Telefon:"></asp:Label><br />
        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox><br />

    </div>

    <h3 class="boja"> Biranje Kurseva  </h3>
    <br />
  

    <div class="form-group">

        <asp:Label ID="Label4" runat="server" Text="Kurs"></asp:Label><br />
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Width="24%" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Yoga</asp:ListItem>
            <asp:ListItem>C#</asp:ListItem>
            <asp:ListItem>Boxing</asp:ListItem>
        </asp:DropDownList><br />
        <asp:Label ID="Label5" runat="server" Text="Cena"></asp:Label><br />
        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox><br />

    </div>

    <asp:Button ID="Button1" runat="server" Text="PURCHASE"  CssClass="btn btn-bg-primary" OnClick="Button1_Click"/><br />

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Unesite Ime" EnableClientScript="false" ControlToValidate="TextBox1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Unesite Email" EnableClientScript="false" ControlToValidate="TextBox2" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Unesite Telefon" EnableClientScript="false" ControlToValidate="TextBox3" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Izaberite Kurs" EnableClientScript="false" ControlToValidate="DropDownList1" Display="None"></asp:RequiredFieldValidator>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" Font-Bold="true"/>

</asp:Content>
