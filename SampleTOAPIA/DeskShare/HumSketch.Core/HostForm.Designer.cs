namespace DistributedDesktop
{
    partial class HostForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupAddressField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupPortField = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Group Address";
            // 
            // groupAddressField
            // 
            this.groupAddressField.AcceptsTab = true;
            this.groupAddressField.Location = new System.Drawing.Point(108, 12);
            this.groupAddressField.Name = "groupAddressField";
            this.groupAddressField.Size = new System.Drawing.Size(184, 20);
            this.groupAddressField.TabIndex = 1;
            this.groupAddressField.Text = "234.5.7.15";
            this.groupAddressField.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // groupPortField
            // 
            this.groupPortField.AcceptsTab = true;
            this.groupPortField.Location = new System.Drawing.Point(108, 51);
            this.groupPortField.Name = "groupPortField";
            this.groupPortField.Size = new System.Drawing.Size(100, 20);
            this.groupPortField.TabIndex = 3;
            this.groupPortField.Text = "5004";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(133, 90);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // HostForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 124);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupPortField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupAddressField);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 160);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 160);
            this.Name = "HostForm";
            this.Text = "Group Address";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox groupAddressField;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox groupPortField;
        private System.Windows.Forms.Button OKButton;
    }
}