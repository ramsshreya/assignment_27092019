using System;

namespace ToDoList
{
    public partial class ToDoLogin : System.Web.UI.Page
    {
        static int loginAttempts = 1;
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Page Load method, Do nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Login method for the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try 
            {
                if (loginAttempts <= 3)
                {
                    bool user_existing = DataLayer.DataOperations.RetrieveUsers(txtusr.Text.Trim(), DataLayer.PasswordFormatter.EncodePasswordToBase64(txtpwd.Text.Trim()));
                    if (user_existing)
                    {
                        loginAttempts = 1;
                        string loginTimeStamp = DataLayer.DataOperations.UpdateLastLogin(txtusr.Text.Trim());
                        Session["sessionLoggedinUser"] = txtusr.Text.Trim();
                        Session["sessionLoggedinTime"] = loginTimeStamp;
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        lblmsg.Text = "Incorrect UserID / Password combination entered. '" + (3 - loginAttempts).ToString() + "' attempts remaining";
                        loginAttempts++;
                    }
                }
                else
                {
                    loginAttempts = 1;
                    lblmsg.Text = "Incorrect UserID / Password combination entered 3 times, kindly retry after sometime.";
                    Session.Abandon();
                    Response.Redirect("ToDoLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error at btnLogin_Click method - " + ex.Message);
            }
        }
    }
}