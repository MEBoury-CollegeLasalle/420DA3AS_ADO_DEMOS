using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPatates.DataAccess;
internal class Demo2DAO {
    public static readonly string tableName = "Demo2";
    public static readonly string selectByIdQuery = "SELECT * FROM {0} WHERE Id = @idParam;";


    public static Demo2DTO GetById(int Id) {

        SqlCommand command = DatabaseConnectionService.GetConnection().CreateCommand();
        command.CommandText = String.Format(selectByIdQuery, tableName);

        SqlParameter idParameter = command.CreateParameter();
        idParameter.ParameterName = "@idParam";
        idParameter.DbType = DbType.Int32;
        idParameter.Value = Id;
        command.Parameters.Add(idParameter);

        using (DatabaseConnectionService.GetConnection()) {
            if (DatabaseConnectionService.GetConnection().State != ConnectionState.Open) {
                DatabaseConnectionService.GetConnection().Open();
            }

            using (SqlDataReader reader = command.ExecuteReader()) {

                Demo2DTO result = new Demo2DTO();
                if (reader.Read()) {
                    // résultat trouvé, créons instance DTO
                    result.Id = reader.GetInt32(0);
                    result.Name = reader.GetString(1);
                    result.Description = reader.GetValue(2) == DBNull.Value ? null : reader.GetString(2);
                    result.ObjetDemo1Id = reader.GetInt32(3);

                    // TODO: exemple de eager loading (pas un vrai TODO)
                    result.ObjetDemo1 = Demo1DAO.GetById(result.ObjetDemo1Id);

                } else {
                    // pas de résultat trouvé, retournons null
                    throw new Exception($"Pas de résultat trouvé dans la base de données pour Id#{Id}.");

                }

                if (reader.Read()) {
                    throw new Exception("Someone messed up bad the database. He pays the donuts.");
                }

                return result;
            }
        }
    }

    public void Insert(Demo2DTO dto) {

    }

    public void Update(Demo2DTO dto) {

    }

    public void Delete(Demo2DTO dto) {

    }

}
