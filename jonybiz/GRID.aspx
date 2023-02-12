<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GRID.aspx.cs" Inherits="jonybiz.GRID" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <link href="Css.css" rel="stylesheet" />
    <br />
    <h1 class="a"> Svaka kupovina kurseva</h1>
    <br />
    <br />
    <div class="form-group">  
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered"></asp:GridView>
    </div>
    <a href="index.aspx">KUPOVINA KURSA</a>
</asp:Content>
