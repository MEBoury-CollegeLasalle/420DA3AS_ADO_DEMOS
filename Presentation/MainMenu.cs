using TestPatates.Business;

namespace TestPatates;

public partial class MainMenu : Form {
    private readonly App application;

    public MainMenu(App app) {
        this.application = app;
        this.InitializeComponent();
    }

    private void ManageDemo1Button_Click(object sender, EventArgs e) {
        // appel de fonction dans la classe métier
        this.application.OpenDemo1ManagementWindow();
    }

    private void QuitButton_Click(object sender, EventArgs e) {
        // appel de fonction dans la classe métier
        App.Stop();
    }
}
