using System.Data;
using System.Diagnostics;
using TestPatates.DataAccess;

namespace TestPatates.Business;
public class App {
    private readonly Demo1Service demo1Service;
    private readonly DataSet dataSet;
    private readonly MainMenu mainMenu;

    public App() {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // configuration initiale de l'application, création des services/fenêtres requises:
        // nouveau DataSet vide
        this.dataSet = new DataSet();
        // objet de fenêtre 'MainMenu'. Notez le passage de 'this' en argument
        // qui va permettre à la fenêtre d'accéder aux méthodes de l'instance de App
        this.mainMenu = new MainMenu(this);
        this.demo1Service = new Demo1Service(this.dataSet);
    }

    public void Start() {
        // démarrage de l'application
        try {
            System.Windows.Forms.Application.Run(this.mainMenu);

        } catch (Exception ex) {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        } finally {
            App.Stop();
        }
    }

    public static void Stop() {
        DatabaseConnectionService.Shutdown();
        Application.Exit();
    }

    public void OpenDemo1ManagementWindow() {
        // Appel de fonction métier dans service spécifique (cas d'utilisation: gerer les démo1)
        this.demo1Service.OpenDemo1View();
    }

}
