using Microsoft.Data.SqlClient;

namespace TestPatates.DataAccess;
public class DatabaseConnectionService {
    public static readonly string connectionString = "Server=.\\SQL2022DEV;DAtabase=db_demos;TrustServerCertificate=true;Integrated Security=true";
    private static SqlConnection? CONNECTION;


    public static SqlConnection GetConnection() {
        CONNECTION ??= new SqlConnection(connectionString);
        return CONNECTION;
    }

    public static void Shutdown() {
        if (CONNECTION is not null) {
            CONNECTION.Close();
        }
    }
}
