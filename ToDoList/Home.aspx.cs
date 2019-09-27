using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDoList
{
    public partial class Home : System.Web.UI.Page
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Page load method, loads the default task list for the logged in users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Label lblLoginID = (Label)Master.FindControl("lblLoginID");
                lblLoginID.Text = Convert.ToString(Session["sessionLoggedinUser"]);
                Label lblLastLoginTime = (Label)Master.FindControl("lblLastLoginTime");
                lblLastLoginTime.Text = Convert.ToString(Session["sessionLoggedinTime"]);
                lblmainpageheader.Text = Convert.ToString(Session["sessionLoggedinUser"]);
                if (!Page.IsPostBack)
                {
                    PopulateTaskGrid(Convert.ToString(Session["sessionLoggedinUser"]));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error at Page_Load method - " + ex.Message);
            }
        }

        /// <summary>
        /// Reusable grid binding code
        /// </summary>
        /// <param name="userId"></param>
        public void PopulateTaskGrid(string userId)
        {
            try
            {
                gvUserTasks.DataSource = DataLayer.DataOperations.RetrieveUserTasks(userId);
                gvUserTasks.DataBind();
            }
            catch (Exception ex)
            {
                log.Error("Error at PopulateTaskGrid method - " + ex.Message);
            }
        }

        /// <summary>
        /// Delete the selected task for the logged in user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDeleteTask_Click(object sender, EventArgs e)
        {
            try
            {
                int taskID = Convert.ToInt32(((Label)((GridViewRow)((Control)sender).NamingContainer).FindControl("lbltaskid")).Text);
                string taskName = ((TextBox)((GridViewRow)((Control)sender).NamingContainer).FindControl("lblTaskName")).Text;
                DataLayer.DataOperations.DeleteTask(taskID);
                PopulateTaskGrid(Convert.ToString(Session["sessionLoggedinUser"]));
                MsgBox("Task deleted - " + taskName, this.Page, this);
            }
            catch (Exception ex)
            {
                log.Error("Error at lnkDeleteTask_Click method - " + ex.Message);
            }
        }

        /// <summary>
        /// Add  task for the logged in user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            try
            {
                string taskName = txtNewTaskName.Text.Trim();
                string taskDesc = txtNewTaskDesc.Text.Trim();
                DataLayer.DataOperations.AddTask(taskName, taskDesc, Convert.ToString(Session["sessionLoggedinUser"]));
                PopulateTaskGrid(Convert.ToString(Session["sessionLoggedinUser"]));
                txtNewTaskName.Text = string.Empty;
                txtNewTaskDesc.Text = string.Empty;
                MsgBox("New task added - " + taskName, this.Page, this);
            }
            catch (Exception ex)
            {
                log.Error("Error at btnAddTask_Click method - " + ex.Message);
            }
        }

        /// <summary>
        /// Edit the selected task for the logged in user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditTask_Click(object sender, EventArgs e)
        {
            try
            {
                string taskName = txtEditTaskName.Text.Trim();
                string taskDesc = txtEditTaskDesc.Text.Trim();
                int taskid = Convert.ToInt32(txtEditTaskID.Text.Trim());
                DataLayer.DataOperations.EditTask(taskName, taskDesc, taskid);
                PopulateTaskGrid(Convert.ToString(Session["sessionLoggedinUser"]));
                txtEditTaskName.Text = string.Empty;
                txtEditTaskDesc.Text = string.Empty;
                txtEditTaskID.Text = string.Empty;
                MsgBox("Task details updated - " + taskName, this.Page, this);
            }
            catch (Exception ex)
            {
                log.Error("Error at btnAddTask_Click method - " + ex.Message);
            }
        }

        /// <summary>
        /// Change the task status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbxTaskCompleted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int taskID = Convert.ToInt32(((Label)((GridViewRow)((Control)sender).NamingContainer).FindControl("lbltaskid")).Text);
                string taskName = ((TextBox)((GridViewRow)((Control)sender).NamingContainer).FindControl("lblTaskName")).Text;
                int checkedStatus = 0;
                if (((CheckBox)((GridViewRow)((Control)sender).NamingContainer).FindControl("ckbxTaskCompleted")).Checked)
                { 
                    checkedStatus = 1; 
                }
                DataLayer.DataOperations.UpdateTaskStatus(taskID, checkedStatus);
                PopulateTaskGrid(Convert.ToString(Session["sessionLoggedinUser"]));
                MsgBox("Status updated for - " + taskName, this.Page, this);
            }
            catch (Exception ex)
            {
                log.Error("Error at ckbxTaskCompleted_CheckedChanged method - " + ex.Message);
            }
        }

        /// <summary>
        /// Status update message box after every action
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="pg"></param>
        /// <param name="obj"></param>
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}