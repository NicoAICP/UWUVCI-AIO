namespace UWUVCI_AIO
{
    partial class PathMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathMenu));
            this.BaseRomLabel = new System.Windows.Forms.Label();
            this.BaseRomTextBox = new System.Windows.Forms.TextBox();
            this.InjectionTextBox = new System.Windows.Forms.TextBox();
            this.InjectionLabel = new System.Windows.Forms.Label();
            this.BaseRomButton = new System.Windows.Forms.Button();
            this.InjectionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // BaseRomLabel
            //
            resources.ApplyResources(this.BaseRomLabel, "BaseRomLabel");
            this.BaseRomLabel.Name = "BaseRomLabel";
            //
            // BaseRomTextBox
            //
            resources.ApplyResources(this.BaseRomTextBox, "BaseRomTextBox");
            this.BaseRomTextBox.Name = "BaseRomTextBox";
            //
            // InjectionTextBox
            //
            resources.ApplyResources(this.InjectionTextBox, "InjectionTextBox");
            this.InjectionTextBox.Name = "InjectionTextBox";
            //
            // InjectionLabel
            //
            resources.ApplyResources(this.InjectionLabel, "InjectionLabel");
            this.InjectionLabel.Name = "InjectionLabel";
            //
            // BaseRomButton
            //
            resources.ApplyResources(this.BaseRomButton, "BaseRomButton");
            this.BaseRomButton.Name = "BaseRomButton";
            this.BaseRomButton.UseVisualStyleBackColor = true;
            this.BaseRomButton.Click += new System.EventHandler(this.BaseRomButton_Click);
            //
            // InjectionButton
            //
            resources.ApplyResources(this.InjectionButton, "InjectionButton");
            this.InjectionButton.Name = "InjectionButton";
            this.InjectionButton.UseVisualStyleBackColor = true;
            this.InjectionButton.Click += new System.EventHandler(this.InjectionButton_Click);
            //
            // PathMenu
            //
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InjectionButton);
            this.Controls.Add(this.BaseRomButton);
            this.Controls.Add(this.InjectionTextBox);
            this.Controls.Add(this.InjectionLabel);
            this.Controls.Add(this.BaseRomTextBox);
            this.Controls.Add(this.BaseRomLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PathMenu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label BaseRomLabel;
        private System.Windows.Forms.TextBox BaseRomTextBox;
        private System.Windows.Forms.TextBox InjectionTextBox;
        private System.Windows.Forms.Label InjectionLabel;
        private System.Windows.Forms.Button BaseRomButton;
        private System.Windows.Forms.Button InjectionButton;
    }
}
