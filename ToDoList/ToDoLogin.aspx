<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ToDoLogin.aspx.cs" Inherits="ToDoList.ToDoLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validatetext()
        {
            var usridval = $('#<%= txtusr.ClientID %>').val().length;
            var pwdval = $('#<%= txtpwd.ClientID %>').val().length;
            if (usridval > 0 && pwdval > 0)
                return true;
            else
            {
                alert("Userid & Password fields cannot be left blank.");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel-heading h4 text-primary text-center">Login</div>
    <div class="card card-body" style="padding-left:35%;">
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-10">
                        <asp:textbox class="form-control textboxlength" id="txtusr" placeholder="Enter Username" runat="server" textmode="SingleLine" MaxLength="12"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-10">
                        <asp:textbox class="form-control textboxlength" id="txtpwd" placeholder="Enter Password" runat="server" textmode="Password" MaxLength="12"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:label cssclass="label label-danger" id="lblmsg" runat="server" ForeColor="#c82333"></asp:label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:button cssclass="btn btn-success textboxlength" id="btnLogin" OnClientClick="return validatetext();" onclick="btnLogin_Click" runat="server" text="Log in"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
