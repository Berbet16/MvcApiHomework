using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class LoggerForSQLServer : ILogger
    {
        string connectionString = "Server=.;Initial catalog=deneme;Integrated Security=True";
        SqlConnection connection;
        SqlCommand command;
        List<Log> logList;

        public IEnumerable<Log> ReadAll()
        {
            using (connection = new SqlConnection(connectionString))
            {
                logList = new List<Log>();

                connection.Open();
                String commandText = "SELECT [Id],[Method],[Path],[Query],[CreatedTime] FROM [dbo].[Logs]";
                command = new SqlCommand(commandText, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Log log = new Log();
                    log.Id = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                        log.Method = reader.GetString(1);
                    if (!reader.IsDBNull(2))
                        log.Path = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        log.Query = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        log.CreatedTime = reader.GetDateTime(4);    

                    logList.Add(log);
                }

            }

            return logList;
        }

        public IEnumerable<Log> Search(string query)
        {
            using (connection = new SqlConnection(connectionString))
            {
                logList = new List<Log>();

                connection.Open();
                String commandText = "SELECT [Id],[Method],[Path],[Query],[CreatedTime] FROM [dbo].[Logs]" +
                    " WHERE Method = @Query OR Path LIKE '%' + @Query + '%' OR Query LIKE '%' + @Query + '%'";

                command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("Query", query);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Log log = new Log();
                    log.Id = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                        log.Method = reader.GetString(1);
                    if (!reader.IsDBNull(2))
                        log.Path = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        log.Query = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        log.CreatedTime = reader.GetDateTime(4);

                    logList.Add(log);
                }

            }

            return logList;
        }

        public void Write(Log log)
        {
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String commandText = "INSERT INTO [dbo].[Logs] ([Method],[Path],[Query],[CreatedTime])" +
                                     "  VALUES(@Method, @Path, @Query, @CreatedTime)";
                command =new SqlCommand(commandText, connection);

                command.Parameters.AddWithValue("Method", log.Method);
                command.Parameters.AddWithValue("Path", log.Path);
                command.Parameters.AddWithValue("Query", log.Query);
                command.Parameters.AddWithValue("CreatedTime", log.CreatedTime);

                command.ExecuteNonQuery();

            }

        }

    }
}
