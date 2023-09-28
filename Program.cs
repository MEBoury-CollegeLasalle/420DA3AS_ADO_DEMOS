namespace TestPatates;

internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        new TestPatates.Business.App().Start();

    }
}