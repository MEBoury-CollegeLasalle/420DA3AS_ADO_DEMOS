using System.Data;
using TestPatates.Business;

namespace TestPatates.Presentation;
internal partial class Demo1Window : Form {
    private readonly Demo1Service demo1Service;

    public Demo1Window(Demo1Service service) {
        this.demo1Service = service;
        this.InitializeComponent();
    }

    public void OpenWithTable(DataTable dataTable) {
        this.demo1GridView.DataSource = dataTable;
        this.Show();

    }

    private void SaveChangesButton_Click(object sender, EventArgs e) {
        this.demo1Service.SaveChanges();
    }

    private void CloseButton_Click(object sender, EventArgs e) {
        if (this.demo1Service.CheckChangesBeforeClosing()) {
            // si fermeture de la fenêtre est OK (pas de changements non-sauvegardés ou
            // si utilisateur est prêt à les annuler), fermer la fenêtre.
            this.Close();
        }
    }
}
