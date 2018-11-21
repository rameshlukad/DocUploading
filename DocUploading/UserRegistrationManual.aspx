<%@ Page Title="" Language="C#" MasterPageFile="~/CustomMasterPage.Master" AutoEventWireup="true" CodeBehind="UserRegistrationManual.aspx.cs" Inherits="DocUploading.UserRegistrationManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>User menual registration </h3>
        <br />
         <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="XX-Small"></asp:Label>
        <div class="form-group">
            <label for="">Enter First Name</label>
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter First Name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="First name required" ControlToValidate="txtFirstName" Style="color: red"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <label for="">Enter Last Name</label>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name"></asp:TextBox>

        </div>

        <div class="form-group">
            <label for="">Enter Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Email required" ControlToValidate="txtEmail" Style="color: red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator" Style="color: red" runat="server"
                ErrorMessage="Enter Email" ForeColor="Red" ControlToValidate="txtEmail"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>


        </div>

        <div class="form-group">
            <label for="">Enter Title</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter Title"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="">Enter mobile number</label>
            <asp:TextBox ID="txtMobNumber" runat="server" CssClass="form-control" placeholder="Enter mobile number"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="">Enter Phone number</label>
            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Enter Phone number"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="">Enter Organization</label>
            <asp:TextBox ID="txtOrganization" runat="server" CssClass="form-control" placeholder="Enter Organization"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-success" OnClick="btnSubmit_Click" />
        </div>
        <div></div>

    </div>


</asp:Content>
