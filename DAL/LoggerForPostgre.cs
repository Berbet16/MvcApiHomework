using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Devart.Data.PostgreSql;

namespace DAL
{
    public class LoggerForPostgre : ILogger
    {
        PgSqlConnection pgSqlConnection1;
        PgSqlCommand command;
        List<Log> logList;

        public IEnumerable<Log> ReadAll()
        {
            using (pgSqlConnection1 = new PgSqlConnection())
            {
                pgSqlConnection1.Host = "localhost";
                pgSqlConnection1.Port = 5432;
                pgSqlConnection1.UserId = "postgres";
                pgSqlConnection1.Password = "237233";
                pgSqlConnection1.Database = "Homework";

                logList = new List<Log>();

                pgSqlConnection1.Open();
                String commandText = "SELECT * FROM public.\"Logs\"";
                command = new PgSqlCommand(commandText, pgSqlConnection1);

                PgSqlDataReader reader = command.ExecuteReader();

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
            using (pgSqlConnection1 = new PgSqlConnection())
            {
                pgSqlConnection1.Host = "localhost";
                pgSqlConnection1.Port = 5432;
                pgSqlConnection1.UserId = "postgres";
                pgSqlConnection1.Password = "237233";
                pgSqlConnection1.Database = "Homework";

                logList = new List<Log>();

                pgSqlConnection1.Open();
                String commandText = "SELECT * FROM public.\"Logs\"" +
                    " WHERE \"Method\" = $1 OR \"Path\" LIKE '%' || $1 || '%' OR \"Query\" LIKE '%' || $1 || '%'";

                command = new PgSqlCommand(commandText, pgSqlConnection1);
                command.Parameters.AddWithValue("$1", query);


                PgSqlDataReader reader = command.ExecuteReader();

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
            using (pgSqlConnection1 = new PgSqlConnection())
            {
                pgSqlConnection1.Host = "localhost";
                pgSqlConnection1.Port = 5432;
                pgSqlConnection1.UserId = "postgres";
                pgSqlConnection1.Password = "237233";
                pgSqlConnection1.Database = "Homework";

                pgSqlConnection1.Open();
                String commandText = "INSERT INTO public.\"Logs\" (\"Method\", \"Path\", \"Query\", \"CreatedTime\") " +
                                     "  VALUES($1, $2, $3, $4)";
                command = new PgSqlCommand(commandText, pgSqlConnection1);

                command.Parameters.AddWithValue("$1", log.Method);
                command.Parameters.AddWithValue("$2", log.Path);
                command.Parameters.AddWithValue("$3", log.Query);
                command.Parameters.AddWithValue("$4", log.CreatedTime);

                command.ExecuteNonQuery();

            }

        }
    }
}
