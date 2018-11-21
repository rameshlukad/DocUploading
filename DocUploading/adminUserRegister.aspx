<%@ Page Title="" Language="C#" MasterPageFile="~/CustomMasterPage.Master" AutoEventWireup="true" CodeBehind="adminUserRegister.aspx.cs" Inherits="DocUploading.adminUserRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <div class="jumbotron">
          <div class="form-group">
            <label for="">Enter user Name </label> 
            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter user Name"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="">Enter user email </label> 
            <asp:TextBox ID="entEmail" runat="server" CssClass="form-control" placeholder="Enter user email"></asp:TextBox>
        </div>
        <div class="form-group">
            <Label for="">Enter password</Label><br />
            <asp:TextBox ID="entPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnAdminUserRegister" runat="server" Text="Login" class="btn btn-success" OnClick="btnAdminUserRegister_Click" />
        </div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
    </div> 



</asp:Content>
