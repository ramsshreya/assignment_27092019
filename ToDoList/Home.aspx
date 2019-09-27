<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ToDoList.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validatetext() {
            var taskname = $('#<%= txtNewTaskName.ClientID %>').val().length;
            var taskdesc = $('#<%= txtNewTaskDesc.ClientID %>').val().length;
            if (taskname > 0 && taskdesc > 0)
                return true;
            else {
                alert("Task Name & Description fields cannot be left blank.");
                return false;
            }
        }
        function validateedittext() {
            var taskname = $('#<%= txtEditTaskName.ClientID %>').val().length;
            var taskdesc = $('#<%= txtEditTaskDesc.ClientID %>').val().length;
            if (taskname > 0 && taskdesc > 0)
                return true;
            else {
                alert("Task Name & Description fields cannot be left blank.");
                return false;
            }
        }
        function openedittextarea(existingName,existingDesc,existingid) {
            $('#<%= txtEditTaskName.ClientID %>').val(existingName);
            $('#<%= txtEditTaskDesc.ClientID %>').val(existingDesc);
            $('#<%= txtEditTaskID.ClientID %>').val(existingid);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <h4>
        To Do list for: <b><asp:Label runat="server" ID="lblmainpageheader"></asp:Label></b>
    </h4>
    <hr />
    <div class="table-responsive">
        <asp:GridView ID="gvUserTasks" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton class="btn btn-danger" ID="lnkDeleteTask" runat="server" Text="Delete" Visible='<%# Eval("isCompleted").ToString() == "0" ? true :false %>'
                        OnClientClick="return confirm('Are you sure to delete this task?');" OnClick="lnkDeleteTask_Click" />
                    <asp:Label ID="lbltaskid" runat="server" Text='<%# Eval("id") %>' Visible="false"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Panel runat="server" Visible='<%# Eval("isCompleted").ToString() == "0" ? true :false %>'>
                    <a class="btn btn-primary" data-toggle="collapse" href="#collapseEdit" role="button" aria-expanded="false"
                        aria-controls="collapseEditZone" onclick="openedittextarea('<%# Eval("taskName") %>','<%# Eval("taskDescription") %>','<%# Eval("id") %>');">Edit</a>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User Id" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblUserid" runat="server" Text='<%# Eval("userid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Completed?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="ckbxTaskCompleted" runat="server" AutoPostBack="true" OnCheckedChanged="ckbxTaskCompleted_CheckedChanged" 
                        Enabled='<%# Eval("isCompleted").ToString() == "0" ? true :false %>' Checked='<%# Eval("isCompleted").ToString() == "1" ? true :false %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Task Name" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="lblTaskName" runat="server" Text='<%# Eval("taskName") %>' TextMode="MultiLine" Enabled="false" Rows="3" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Task Description" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:TextBox ID="lblTaskDesc" runat="server" Text='<%# Eval("taskDescription") %>' TextMode="MultiLine" Enabled="false" Rows="3" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created On" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblTaskCreatedOn" runat="server" Text='<%# ToDoList.DataLayer.DataFormatter.FormattedDate(Eval("dateCreated")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Updated On" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblTaskModifiedOn" runat="server" Text='<%# ToDoList.DataLayer.DataFormatter.FormattedDate(Eval("dateUpdated")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
        <br />
        <p>
          <a class="btn btn-success" data-toggle="collapse" href="#collapseAdd" role="button" aria-expanded="false" aria-controls="collapseAddZone">Add Task</a>
        </p>
        <div class="collapse" id="collapseAdd">
          <div class="card card-body">
              Task Name: <asp:TextBox ID="txtNewTaskName" runat="server" placeholder="Enter Task Name" MaxLength="100"/>
              <br />
              Task Description: <asp:TextBox ID="txtNewTaskDesc" runat="server" TextMode="MultiLine" placeholder="Enter Task Description" MaxLength="500"/>
              <br />
              <table>
                  <tr>
                      <td>
                          <asp:button cssclass="btn btn-success btnlength" id="btnAddTask" OnClientClick="return validatetext();" OnClick="btnAddTask_Click" runat="server" text="Save"/>
                          <a class="btn btn-success btnlength" data-toggle="collapse" href="#collapseAdd" role="button" aria-expanded="false" aria-controls="collapseAddZone">Cancel</a>
                      </td>
                  </tr>
              </table>
          </div>
        </div>
        <br />
        <div class="collapse" id="collapseEdit">
          <div class="card card-body">
              <div style="display:none;"><asp:TextBox ID="txtEditTaskID" runat="server"/></div>
              Task Name: <asp:TextBox ID="txtEditTaskName" runat="server" placeholder="Enter Task Name" MaxLength="100"/>
              <br />
              Task Description: <asp:TextBox ID="txtEditTaskDesc" runat="server" TextMode="MultiLine" placeholder="Enter Task Description" MaxLength="500"/>
              <br />
              <table>
                  <tr>
                      <td>
                          <asp:button cssclass="btn btn-success btnlength" id="btnEditTask" OnClientClick="return validateedittext();" OnClick="btnEditTask_Click" runat="server" text="Save"/>
                          <a class="btn btn-success btnlength" data-toggle="collapse" href="#collapseEdit" role="button" aria-expanded="false" aria-controls="collapseEditZone">Cancel</a>
                      </td>
                  </tr>
              </table>
          </div>
        </div>
    </div>
</asp:Content>
