using OpenUniversity.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Utility
{
    public class RepositoryFactory
    {
        public static bool DatabaseAccessPossible { get; private set; }
        private static bool _hasInit = false;
        public static IBaseRepository<T> GetRepository<T>() where T : class
        {
            if (!_hasInit)
            {
                DatabaseAccessPossible = false;
                string connectionString = ConfigurationManager.ConnectionStrings["OpenUniversityDBEntities"].ConnectionString.ToString();
                if (!string.IsNullOrEmpty(connectionString))
                {
                    string dbType = ConfigurationManager.AppSettings["databasetype"];
                    if (dbType.ToLower().Equals("mssql"))
                    {
                        SqlConnection cnn = new SqlConnection(connectionString);
                        try
                        {
                            cnn.Open();
                            DatabaseAccessPossible = true;
                        }
                        catch (Exception e)
                        {
                            DatabaseAccessPossible = false;
                        }
                        finally
                        {
                            cnn.Close();
                        }
                    }
                }
                _hasInit = true;
            }
            if (DatabaseAccessPossible)
            {
                return new DatabaseRepository<T>();
            }
            else
            {
                return new FileRepository<T>();
            }
        }
    }
}
