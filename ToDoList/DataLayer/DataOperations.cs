using System.Data.SQLite;
using System;
using System.Data;
using System.Configuration;

namespace ToDoList.DataLayer
{
    public class DataOperations
    {
        /// <summary>
        /// Create instance of SQLLite Database
        /// </summary>
        public static void CreateSqliteDB()
        {
            SQLiteConnection.CreateFile(AppDomain.CurrentDomain.BaseDirectory + ToDoList.Constants.Constants.databaseLocation);
        }

        /// <summary>
        /// Retrieve the sqlite connection
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection RetrieveDBConnection()
        {
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ToDoList.Constants.Constants.databaseLocation + ";Version=3;");
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            return dbConnection;
        }

        /// <summary>
        /// Used for generate the test users in the application
        /// </summary>
        public static void CreateTestUsers()
        {
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "create table tblUserBase (id INTEGER PRIMARY KEY, userid varchar(12) UNIQUE, password varchar(12), lastLogin datetime)";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
                sql = string.Empty;
                sql = "insert into tblUserBase (userid, password) values ('testuser_1', '" + PasswordFormatter.EncodePasswordToBase64("testuser111") + "');" +
                      "insert into tblUserBase (userid, password) values ('testuser_2', '" + PasswordFormatter.EncodePasswordToBase64("testuser222") + "');" +
                      "insert into tblUserBase (userid, password) values ('testuser_3', '" + PasswordFormatter.EncodePasswordToBase64("testuser333") + "');" +
                      "insert into tblUserBase (userid, password) values ('testuser_4', '" + PasswordFormatter.EncodePasswordToBase64("testuser444") + "');"; ;
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
                sql = "create table tblUserTask (id INTEGER PRIMARY KEY, userid varchar(12), taskName varchar(100) UNIQUE, taskDescription varchar(500), dateCreated datetime default current_timestamp, dateUpdated datetime default NULL, isCompleted int default 0, isactive int, FOREIGN KEY(userid) REFERENCES tblUserBase(userid))";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
                sql = "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_1','testuser_1_task_1', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_1','testuser_1_task_2', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_1','testuser_1_task_3', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_2','testuser_2_task_1', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_2','testuser_2_task_2', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_2','testuser_2_task_3', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_3','testuser_3_task_1', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_3','testuser_3_task_2', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_3','testuser_3_task_3', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_4','testuser_4_task_1', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_4','testuser_4_task_2', 'random task desc', 1);" +
                      "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('testuser_4','testuser_4_task_3', 'random task desc', 1);";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Validate the login credentials against the database
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool RetrieveUsers(string userid, string password)
        {
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "select count(userid) from tblUserBase where LOWER(userid) = LOWER('" + userid + "') and password = '" + password + "'";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    string returnValue = command.ExecuteScalar().ToString();
                    if (returnValue.ToString() != "0")
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Not used in application runtime, used for generate the test users in the application
        /// </summary>
        public static DataTable RetrieveTestUsers()
        {
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "select A.userid, A.password, A.lastLogin, B.taskCount FROM tblUserBase A LEFT JOIN (select userid,count(taskName) AS taskCount from tblUserTask where isactive = 1 group by userid) B ON A.userid = B.userid";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    using (SQLiteDataAdapter adp = new SQLiteDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve User tasks for particular user id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataTable RetrieveUserTasks(string userid)
        {
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "select id, userid, taskName, taskDescription, dateCreated, dateUpdated, isCompleted, isactive FROM tblUserTask where isactive = 1 and userid = '" + userid + "' order by dateUpdated desc,dateCreated desc";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    using (SQLiteDataAdapter adp = new SQLiteDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Update the lastlogin timestamp
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string UpdateLastLogin(string userid)
        {
            string currentTimeStamp = DateTime.Now.ToString();
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "update tblUserBase set lastLogin = current_timestamp where LOWER(userid) = LOWER('" + userid + "')";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return currentTimeStamp;
        }

        /// <summary>
        /// Delete the specified task
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public static string DeleteTask(int taskid)
        {
            string currentTimeStamp = DateTime.Now.ToString();
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "update tblUserTask set isactive = 0, dateUpdated = current_timestamp where id = " + taskid;
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return currentTimeStamp;
        }
        
        /// <summary>
        /// Save newly added task
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="taskDesc"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string AddTask(string taskName, string taskDesc, string userId)
        {
            string currentTimeStamp = DateTime.Now.ToString();
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "insert into tblUserTask (userid, taskName, taskDescription, isactive) values ('" + userId + "','" + taskName + "', '" + taskDesc + "', 1);";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return currentTimeStamp;
        }

        /// <summary>
        /// Save the edited Task
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="taskDesc"></param>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public static string EditTask(string taskName, string taskDesc, int taskid)
        {
            string currentTimeStamp = DateTime.Now.ToString();
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "update tblUserTask set taskName = '" + taskName + "', taskDescription = '" + taskDesc + "', dateUpdated = current_timestamp where id = " + taskid.ToString();
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return currentTimeStamp;
        }

        /// <summary>
        /// Delete the specified task
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public static string UpdateTaskStatus(int taskid, int checkedStatus)
        {
            string currentTimeStamp = DateTime.Now.ToString();
            using (SQLiteConnection dbConnection = RetrieveDBConnection())
            {
                string sql = "update tblUserTask set isCompleted = " + checkedStatus + ", dateUpdated = current_timestamp where id = " + taskid;
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            return currentTimeStamp;
        }
    }
}