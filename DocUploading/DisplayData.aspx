<%@ Page Title="" Language="C#" MasterPageFile="~/CustomMasterPage.Master" AutoEventWireup="true" CodeBehind="DisplayData.aspx.cs" Inherits="DocUploading.DisplayData" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#GridView1").prepend
                (
                $("<thead></thead>").append
                    (
                    $(this).find("tr:first")
                    )
                ).dataTable();
        });
    </script>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-3">
        <h2 class="text-center ">Uploaded Detials</h2>
        <div class="card-body">
            <asp:GridView ID="GridView1" HeaderStyle-CssClass="bg-primary text-white" ShowHeaderWhenEmpty="true" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered``">
                <EmptyDataTemplate>
                    <div class="text-center">No record found</div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="autoid" />
                    <asp:BoundField HeaderText="First Name" DataField="first_name" />
                    <asp:BoundField HeaderText="Last Name" DataField="last_name" />
                    <asp:BoundField HeaderText="Designation" DataField="designation" />
                    <asp:BoundField HeaderText="Email" DataField="email" />
                    <asp:BoundField HeaderText="Phone" DataField="phone" />
                    <asp:BoundField HeaderText="Organization" DataField="organization" />
                    <asp:BoundField HeaderText="City" DataField="city" />
                    <asp:BoundField HeaderText="Country" DataField="country" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
