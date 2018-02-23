namespace mssql_dump
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dumpButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dbNameComboBox = new System.Windows.Forms.ComboBox();
            this.includeDataCheckBox = new System.Windows.Forms.CheckBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.schemaDataSeperateCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dumpButton
            // 
            this.dumpButton.Location = new System.Drawing.Point(12, 295);
            this.dumpButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dumpButton.Name = "dumpButton";
            this.dumpButton.Size = new System.Drawing.Size(87, 30);
            this.dumpButton.TabIndex = 0;
            this.dumpButton.Text = "Dump";
            this.dumpButton.Click += new System.EventHandler(this.dumpButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server:";
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(79, 22);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(278, 23);
            this.serverTextBox.TabIndex = 0;
            this.serverTextBox.Text = ".\\sql";
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.Location = new System.Drawing.Point(79, 51);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(278, 23);
            this.userIdTextBox.TabIndex = 1;
            this.userIdTextBox.Text = "sa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "UserID:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(79, 80);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(278, 23);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.Text = "Hinac0m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "DbName:";
            // 
            // dbNameComboBox
            // 
            this.dbNameComboBox.FormattingEnabled = true;
            this.dbNameComboBox.Location = new System.Drawing.Point(79, 109);
            this.dbNameComboBox.Name = "dbNameComboBox";
            this.dbNameComboBox.Size = new System.Drawing.Size(278, 25);
            this.dbNameComboBox.TabIndex = 3;
            this.dbNameComboBox.Click += new System.EventHandler(this.dbNameComboBox_Click);
            // 
            // includeDataCheckBox
            // 
            this.includeDataCheckBox.AutoSize = true;
            this.includeDataCheckBox.Checked = true;
            this.includeDataCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeDataCheckBox.Location = new System.Drawing.Point(6, 22);
            this.includeDataCheckBox.Name = "includeDataCheckBox";
            this.includeDataCheckBox.Size = new System.Drawing.Size(99, 21);
            this.includeDataCheckBox.TabIndex = 0;
            this.includeDataCheckBox.Text = "Include data";
            this.includeDataCheckBox.UseVisualStyleBackColor = true;
            this.includeDataCheckBox.CheckedChanged += new System.EventHandler(this.includeDataCheckBox_CheckedChanged);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // schemaDataSeperateCheckBox
            // 
            this.schemaDataSeperateCheckBox.AutoSize = true;
            this.schemaDataSeperateCheckBox.Location = new System.Drawing.Point(6, 66);
            this.schemaDataSeperateCheckBox.Name = "schemaDataSeperateCheckBox";
            this.schemaDataSeperateCheckBox.Size = new System.Drawing.Size(183, 21);
            this.schemaDataSeperateCheckBox.TabIndex = 1;
            this.schemaDataSeperateCheckBox.Text = "Seperate schema and data";
            this.schemaDataSeperateCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serverTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dbNameComboBox);
            this.groupBox1.Controls.Add(this.userIdTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.passwordTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 146);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database info";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.includeDataCheckBox);
            this.groupBox2.Controls.Add(this.schemaDataSeperateCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 124);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(340, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "- Script name: [DbName]-schema.sql, [DbName]-data.sql";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "- Script name: [DbName].sql";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 337);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dumpButton);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(406, 376);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mssql-dump";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button dumpButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dbNameComboBox;
        private System.Windows.Forms.CheckBox includeDataCheckBox;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.CheckBox schemaDataSeperateCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

