<%@ Page Title="" Language="C#" MasterPageFile="~/CustomMasterPage.Master" AutoEventWireup="true" CodeBehind="CSVupload.aspx.cs" Inherits="DocUploading.CSVupload" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
   
        <div class="container py-3">
            <h2 class="text-center">Import CSV File</h2>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>

            <div class="card">            
                <div class="card-body">
                    <button style="margin-bottom: 10px;" type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
                        <i class="fa fa-plus-circle"></i>Import CSV
                    </button>
                    <div class="modal fade" id="myModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Import CSV File</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Choose excel file</label>
                                                <div class="input-group">
                                                    <div class="custom-file">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="custom-file-input"/> 
                                                        <label class="custom-file-label"></label>
                                                    </div>
                                                    <label id="filename"></label>
                                                    <div class="input-group-append">
                                                         <asp:Button ID="btnCSV" runat="server" Text="Upload" CssClass="btn btn-outline-primary" OnClick="btnCSV_Click" /> 
                                                    </div>
                                                </div>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
 
</asp:Content>
