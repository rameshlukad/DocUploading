<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="DocUploading.AdminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <div class="form-group">
            <label for="">Enter user email </label> 
            <asp:TextBox ID="entEmail" runat="server" CssClass="form-control" placeholder="Enter user email"></asp:TextBox>
        </div>
        <div class="form-group">
            <Label for="">Enter password</Label><br />
            <asp:TextBox ID="entPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnAdminLogin" runat="server" Text="Login" class="btn btn-success" OnClick="btnAdminLogin_Click" />
        </div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
    </div> 


</asp:Content>
