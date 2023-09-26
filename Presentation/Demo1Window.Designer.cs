namespace TestPatates.Presentation;

partial class Demo1Window {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        demo1GridView = new DataGridView();
        cancelButton = new Button();
        saveChangesButton = new Button();
        ((System.ComponentModel.ISupportInitialize) demo1GridView).BeginInit();
        this.SuspendLayout();
        // 
        // demo1GridView
        // 
        demo1GridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        demo1GridView.Location = new Point(12, 12);
        demo1GridView.Name = "demo1GridView";
        demo1GridView.RowHeadersWidth = 51;
        demo1GridView.RowTemplate.Height = 29;
        demo1GridView.Size = new Size(776, 376);
        demo1GridView.TabIndex = 0;
        // 
        // cancelButton
        // 
        cancelButton.Location = new Point(658, 401);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(130, 37);
        cancelButton.TabIndex = 1;
        cancelButton.Text = "Annuler";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += this.CloseButton_Click;
        // 
        // saveChangesButton
        // 
        saveChangesButton.Location = new Point(522, 401);
        saveChangesButton.Name = "saveChangesButton";
        saveChangesButton.Size = new Size(130, 37);
        saveChangesButton.TabIndex = 2;
        saveChangesButton.Text = "Sauvegarder";
        saveChangesButton.UseVisualStyleBackColor = true;
        saveChangesButton.Click += this.SaveChangesButton_Click;
        // 
        // Demo1View
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(800, 450);
        this.Controls.Add(saveChangesButton);
        this.Controls.Add(cancelButton);
        this.Controls.Add(demo1GridView);
        this.Name = "Demo1View";
        this.Text = "Demo1View";
        ((System.ComponentModel.ISupportInitialize) demo1GridView).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private DataGridView demo1GridView;
    private Button cancelButton;
    private Button saveChangesButton;
}