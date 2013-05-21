namespace SwissKip.Web
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Web;

    using DapperExtensions;

    using SwissKip.Web.Core;

    public static class Current
    {
        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        public static int UserId
        {
            get
            {
                try
                {
                    return Int32.Parse(Context.User.Identity.Name);
                }
                catch
                {
                    return 1;
                }
            }
        }

        public static User User
        {
            get
            {
                return Connection.Get<User>(UserId);
            }
        }

        public static DbConnection Connection
        {
            get
            {
                var connection = Context.Items["Connection"] as DbConnection; ;

                if (connection == null)
                {
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    connection.Open();
                    Context.Items["Connection"] = connection;
                }

                return connection;
            }
        }

        public static void DisposeConnection()
        {
            var connection = Context.Items["Connection"] as DbConnection;

            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection = null;
                }
                Context.Items["Connection"] = null;
            }
        }
    }
}