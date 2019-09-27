using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDoList
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Page load method for the master page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            lblcurrenttime.Text = ToDoList.DataLayer.DataFormatter.FormattedDateTime(DateTime.Now);
            lblIP.Text = "IP: " + GetLocalIPAddress();
        }

        /// <summary>
        /// Logout from application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("ToDoLogin.aspx");
        }

        /// <summary>
        /// Get IP for the current machine
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error at GetLocalIPAddress method - " + ex.Message);
            }
            return localIP;
        }
    }
}