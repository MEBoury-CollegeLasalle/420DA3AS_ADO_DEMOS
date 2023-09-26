namespace TestPatates;

partial class MainMenu {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        manageDemo1Button = new Button();
        quitButton = new Button();
        this.SuspendLayout();
        // 
        // manageDemo1Button
        // 
        manageDemo1Button.Location = new Point(12, 91);
        manageDemo1Button.Name = "manageDemo1Button";
        manageDemo1Button.Size = new Size(358, 43);
        manageDemo1Button.TabIndex = 0;
        manageDemo1Button.Text = "Gestion des Demo1";
        manageDemo1Button.UseVisualStyleBackColor = true;
        manageDemo1Button.Click += this.ManageDemo1Button_Click;
        // 
        // quitButton
        // 
        quitButton.Location = new Point(12, 173);
        quitButton.Name = "quitButton";
        quitButton.Size = new Size(358, 43);
        quitButton.TabIndex = 1;
        quitButton.Text = "Quitter";
        quitButton.UseVisualStyleBackColor = true;
        quitButton.Click += this.QuitButton_Click;
        // 
        // MainMenu
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(382, 353);
        this.Controls.Add(quitButton);
        this.Controls.Add(manageDemo1Button);
        this.Name = "MainMenu";
        this.Text = "Menu Principal";
        this.ResumeLayout(false);
    }

    #endregion

    private Button manageDemo1Button;
    private Button quitButton;
}
