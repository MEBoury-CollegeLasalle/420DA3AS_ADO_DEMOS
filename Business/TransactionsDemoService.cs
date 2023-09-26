using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPatates.DataAccess;

namespace TestPatates.Business;
internal class TransactionsDemoService {
    private Demo1DAO dao;

    public TransactionsDemoService() {
        this.dao = new Demo1DAO();
    }


    public void DoWithoutTransaction() {
        Demo1DTO dto = new Demo1DTO();
        dto.Name = "ZisIsATest";
        dto.Description = "Description";
        dao.Insert(dto);

        dao.Delete(dto);

    }

    public void DoTransactionDemo() {

        try {

            SqlTransaction transaction = DatabaseConnectionService.GetConnection().BeginTransaction();

            Demo1DTO dto = new Demo1DTO();
            dto.Name = "ZisIsATest";
            dto.Description = "Description";

            dao.Insert(dto, transaction);
            dto = dao.GetById(dto.Id, transaction);

            dto.Description = "Une meilleure description!";
            dao.Update(dto, transaction);

            dao.Delete(dto, transaction);

            transaction.Commit();

        } catch (Exception ex) {
            // NOTE: pas de rollback ici puisque je fais déjà le rollback dans les méthodes
            // du DAO. JE 'catch' quand même les exceptions pour pouvoir les gérer face à
            // l'utilisateur.

            // gestion poche de l'exception
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }


    }

}
