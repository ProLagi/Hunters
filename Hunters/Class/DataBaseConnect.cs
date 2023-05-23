using System.Data.SqlClient;

namespace Hunters.Class
{
    internal class DataBaseConnect
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source = WIN-OKBE6BFC2QH; Initial Catalog = БД Охотники; Integrated Security = true");
        public void openConnection()
        {
            if(sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}
