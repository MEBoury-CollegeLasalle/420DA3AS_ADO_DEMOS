using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TestPatates.DataAccess;
internal class Demo1DAO {
    
    public static readonly string tableName = "Demo1";
    public static readonly string selectByIdQuery = "SELECT * FROM {0} WHERE Id = @idParam;";
    public static readonly string updateQuery = "UPDATE {0} SET Name = @nameParam, Description = @descParam WHERE Id = @idParam;";
    public static readonly string insertQuery = "INSERT INTO {0} (Name, Description) VALUES (@nameParam, @descParam); SELECT CAST(SCOPE_IDENTITY() AS INT);";
    public static readonly string deleteQuery = "DELETE FROM {0} WHERE Id = @idParam;";



    public Demo1DTO GetById(int Id) {
        return this.GetById(Id, DatabaseConnectionService.GetConnection().BeginTransaction(), true);
    }

    public Demo1DTO GetById(int Id, SqlTransaction transaction) {
        return this.GetById(Id, transaction, false);
    }

    public Demo1DTO GetById(int Id, SqlTransaction transaction, bool doCommit) {

        try {

            SqlCommand command = transaction.Connection.CreateCommand();
            command.CommandText = String.Format(selectByIdQuery, tableName);

            SqlParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "@idParam";
            idParameter.DbType = DbType.Int32;
            idParameter.Value = Id;
            command.Parameters.Add(idParameter);

            if (transaction.Connection.State != ConnectionState.Open) {
                transaction.Connection.Open();
            }

            using (SqlDataReader reader = command.ExecuteReader()) {

                Demo1DTO result = new Demo1DTO();
                if (reader.Read()) {
                    // résultat trouvé, créons instance DTO
                    result.Id = reader.GetInt32(0);
                    result.Name = reader.GetString(1);
                    result.Description = reader.GetValue(2) == DBNull.Value ? null : reader.GetString(2);
                    result.DateCreated = reader.GetDateTime(3);

                } else {
                    // pas de résultat trouvé, retournons null
                    throw new Exception($"Pas de résultat trouvé dans la base de données pour Id#{Id}.");

                }

                if (reader.Read()) {
                    throw new Exception("Someone messed up bad the database. He pays the donuts.");
                }

                return result;
            }

        } catch (Exception ex) {
            transaction.Rollback();
            throw;
        }
    }



    public void Update(Demo1DTO dto) {
        this.Update(dto, DatabaseConnectionService.GetConnection().BeginTransaction(), true);
    }

    public void Update(Demo1DTO dto, SqlTransaction transaction) {
        this.Update(dto, transaction, false);
    }

    public void Update(Demo1DTO dto, SqlTransaction transaction, bool doCommit) {

        try {
            if (dto.Id < 1) {
                throw new Exception($"Integer id value of DTO is invalid; must be > 0. Value found: [{dto.Id}].");
            }

            SqlCommand command = transaction.Connection.CreateCommand();
            command.CommandText = String.Format(updateQuery, tableName);

            SqlParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@nameParam";
            nameParameter.DbType = DbType.String;
            nameParameter.Value = dto.Name;
            command.Parameters.Add(nameParameter);

            SqlParameter descriptionParameter = command.CreateParameter();
            descriptionParameter.ParameterName = "@descParam";
            descriptionParameter.DbType = DbType.String;
            descriptionParameter.Value = dto.Description;
            command.Parameters.Add(descriptionParameter);

            SqlParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "@idParam";
            idParameter.DbType = DbType.Int32;
            idParameter.Value = dto.Id;
            command.Parameters.Add(idParameter);

            if (transaction.Connection.State != ConnectionState.Open) {
                transaction.Connection.Open();
            }

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected < 1) {
                throw new Exception($"Row with id#{dto.Id} not found for update in database. No actions done.");
            } else if (rowsAffected > 1) {
                throw new Exception($"Danger Dwayne Robinson, multiple rows affected in database for id#{dto.Id}.");
            }

            Debug.WriteLine($"({rowsAffected}) rows affected!");

            if (doCommit) {
                transaction.Commit();
            }


        } catch (Exception ex) {
            transaction.Rollback();
            throw;
        }

    }



    #region Insert Methods

    public void Insert(Demo1DTO dto) {
        this.Insert(dto, DatabaseConnectionService.GetConnection().BeginTransaction(), false);
    }

    public void Insert(Demo1DTO dto, SqlTransaction transaction) {
        this.Insert(dto, transaction, false);
    }

    public void Insert(Demo1DTO dto, SqlTransaction transaction, bool doCommit) {
        try {
            if (dto.Id != 0) {
                throw new Exception("Cannot insert DTO with Id value already set.");
            }

            SqlCommand command = transaction.Connection.CreateCommand();
            command.CommandText = String.Format(insertQuery, tableName);
            command.Transaction = transaction;

            SqlParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@nameParam";
            nameParameter.DbType = DbType.String;
            nameParameter.Value = dto.Name;
            command.Parameters.Add(nameParameter);

            SqlParameter descriptionParameter = command.CreateParameter();
            descriptionParameter.ParameterName = "@descParam";
            descriptionParameter.DbType = DbType.String;
            descriptionParameter.Value = dto.Description;
            command.Parameters.Add(descriptionParameter);

            if (transaction.Connection.State != ConnectionState.Open) {
                transaction.Connection.Open();
            }

            dto.Id = (int) command.ExecuteScalar();

            if (doCommit) {
                transaction.Commit();
            }

        } catch (Exception ex) {
            transaction.Rollback();
            throw;
        }
    }

    #endregion


    #region Insert Many Methods

    public void InsertMany(List<Demo1DTO> list) {
        this.InsertMany(list, DatabaseConnectionService.GetConnection().BeginTransaction(), true);
    }

    public void InsertMany(List<Demo1DTO> list, SqlTransaction transaction) {
        this.InsertMany(list, transaction, false);
    }

    public void InsertMany(List<Demo1DTO> list, SqlTransaction transaction, bool doCommit) {

        try {

            SqlCommand command = transaction.Connection.CreateCommand();
            command.CommandText = String.Format(insertQuery, tableName);
            command.Transaction = transaction;

            if (transaction.Connection.State != ConnectionState.Open) {
                transaction.Connection.Open();
            }

            int maxTextLength = 0;
            foreach (Demo1DTO objet in list) {
                if (objet.Description is not null) {
                    if (objet.Description.Length > maxTextLength) {
                        maxTextLength = objet.Description.Length;
                    }
                }
            }

            SqlParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@nameParam";
            nameParameter.DbType = DbType.String;
            nameParameter.Size = 64;
            command.Parameters.Add(nameParameter);

            SqlParameter descriptionParameter = command.CreateParameter();
            descriptionParameter.ParameterName = "@descParam";
            descriptionParameter.DbType = DbType.String;
            descriptionParameter.Size = maxTextLength;
            command.Parameters.Add(descriptionParameter);

            command.Prepare();

            foreach (Demo1DTO dto in list) {
                if (dto.Id != 0) {
                    throw new Exception("Cannot insert DTO with Id value already set.");
                }
                command.Parameters[nameParameter.ParameterName].Value = dto.Name;
                command.Parameters[descriptionParameter.ParameterName].Value = dto.Description;
                dto.Id = (int) command.ExecuteScalar();
            }

            if (doCommit) {
                transaction.Commit();
            }

        } catch (Exception ex) {
            transaction.Rollback();
            throw;
        }

    }

    #endregion


    #region Delete Methods

    public void Delete(Demo1DTO dto) {
        this.Delete(dto, DatabaseConnectionService.GetConnection().BeginTransaction(), true);
    }

    public void Delete(Demo1DTO dto, SqlTransaction transaction) {
        this.Delete(dto, transaction, false);
    }

    public void Delete(Demo1DTO dto, SqlTransaction transaction, bool doCommit) {

        try {
            if (dto.Id < 1) {
                throw new Exception("Cannot delete DTO with no Id value set.");
            }

            SqlCommand command = transaction.Connection.CreateCommand();
            command.CommandText = String.Format(deleteQuery, tableName);
            command.Transaction = transaction;

            SqlParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "@idParam";
            idParameter.DbType = DbType.Int32;
            idParameter.Value = dto.Id;
            command.Parameters.Add(idParameter);

            if (transaction.Connection.State != ConnectionState.Open) {
                transaction.Connection.Open();
            }

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected < 1) {
                throw new Exception($"Row with id#{dto.Id} not found for delete in database. No actions done.");
            } else if (rowsAffected > 1) {
                throw new Exception($"Danger Dwayne Robinson, multiple rows affected in database for id#{dto.Id}.");
            }

            Debug.WriteLine($"({rowsAffected}) rows affected!");

            if (doCommit) {
                transaction.Commit();
            }

        } catch (Exception ex) {
            transaction.Rollback();
            throw;
        }

    }

    #endregion

}
