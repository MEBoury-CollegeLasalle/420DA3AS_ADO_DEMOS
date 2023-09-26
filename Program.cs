using Microsoft.Data.SqlClient;
using System.Diagnostics;
using TestPatates.DataAccess;

namespace TestPatates;

internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.

        try {
            Demo1DAO dao = new Demo1DAO();

            List<Demo1DTO> list = new List<Demo1DTO>();

            Demo1DTO dto = new Demo1DTO();
            dto.Name = "courge";
            dto.Description = "C'est une courge!";

            Demo1DTO dto2 = new Demo1DTO();
            dto2.Name = "celeri";
            dto2.Description = "C'est pas mal juste de l'eau.";

            list.Add(dto);
            list.Add(dto2);

            dao.InsertMany(list);

            /*
            Demo1DTO? monObjet = dao.GetById(1);
            Debug.WriteLine(monObjet);

            monObjet.Name = "Une carotte";
            monObjet.Description = "Cest maintenant une carotte!";

            dao.Update(monObjet);
            */

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        } catch (Exception ex) {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        } finally {
            DatabaseConnectionService.Shutdown();
        }

    }
}