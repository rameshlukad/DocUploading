<%@ Page Title="" Language="C#" MasterPageFile="~/CustomMasterPage.Master" AutoEventWireup="true" CodeBehind="regularEmailUpload.aspx.cs" Inherits="DocUploading.regularEmailUpload" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <title>Xlsx Upload</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap4.min.css" />
    <script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="container py-3">
        <h2 class="text-center">Upload Regular Email</h2>
        <div class="card">
            <h2 class="text-left" style="color: blue">.xls/.xlsx.csv file types supported only</h2>

            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <%-- <asp:Label ID="LabelImport" runat="server"></asp:Label>--%>
          <%--  <div class="card-body">
                <button style="margin-bottom: 10px;" type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
                    <i class="fa fa-plus-circle"></i>Import file
                </button>

            </div>--%>


            <div class="input-group">
                <div class="custom-file">
                    <asp:FileUpload ID="FileUpload1" CssClass="custom-file-input" runat="server" />
                    <label class="custom-file-label"></label>
                </div>
                <label id="filename"></label>
                <div class="input-group-append">
                    <asp:Button ID="Button3" runat="server" CssClass="btn btn-outline-primary" Text="Upload" OnClick="btnUpload_Click" />

                </div>
            </div>


        </div>
    </div>


</asp:Content>