using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPatates.DataAccess;
public class DatabaseConnectionService {
    public static readonly string connectionString = "Server=.\\SQL2022DEV;DAtabase=db_demos;TrustServerCertificate=true;Integrated Security=true";
    private static SqlConnection? CONNECTION;


    public static SqlConnection GetConnection() {
        if (CONNECTION == null) {
            CONNECTION = new SqlConnection(connectionString);
        }
        return CONNECTION;
    }

    public static void Shutdown() {
        if (CONNECTION is SqlConnection) {
            CONNECTION.Close();
        }
    }
}
