using Dapper;
using mssql_service;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace mssql_tool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IStoreService storeService = new StoreService();
            var dbInfo = storeService.Load<DbInfo>("dbinfo-userinput");
            if (dbInfo != null)
            {
                serverTextBox.Text = dbInfo.Server;
                userIdTextBox.Text = dbInfo.UserId;
                passwordTextBox.Text = dbInfo.Password;
                dbNameComboBox.Text = dbInfo.DbName;
            }
        }

        private void dumpButton_Click(object sender, EventArgs e)
        {
            var dbInfo = getDbInfo();
            try
            {
                var folderDiaglog = new FolderBrowserDialog();
                folderDiaglog.RootFolder = Environment.SpecialFolder.Desktop;
                var result = folderDiaglog.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    return;
                }

                string dir = folderDiaglog.SelectedPath;
                bool includeData = includeDataCheckBox.Checked;
                bool seperateSchemaAndData = schemaDataSeperateCheckBox.Checked;

                ScriptGenerateServiceWrapper.Generate(dbInfo, includeData, seperateSchemaAndData, dir);

                Process.Start(dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dump failed." + ex.Message);
            }

            IStoreService storeService = new StoreService();
            storeService.Save("dbinfo-userinput", dbInfo);
        }

        private void dbNameComboBox_Click(object sender, EventArgs e)
        {
            var dbInfo = getDbInfo();
            SqlConnectionStringBuilder ssb = new SqlConnectionStringBuilder();
            ssb.DataSource = dbInfo.Server;
            ssb.UserID = dbInfo.UserId;
            ssb.Password = dbInfo.Password;
            ssb.InitialCatalog = "master";

            using (IDbConnection conn = new SqlConnection(ssb.ConnectionString))
            {
                var result = conn.Query<string>("select name from sys.databases").ToList();
                if (dbNameComboBox.DataSource == null
                    || !result.SequenceEqual(dbNameComboBox.DataSource as List<string>))
                {
                    dbNameComboBox.DataSource = result;
                }
            }
        }

        private DbInfo getDbInfo()
        {
            return new DbInfo()
            {
                Server = serverTextBox.Text,
                UserId = userIdTextBox.Text,
                Password = passwordTextBox.Text,
                DbName = dbNameComboBox.Text
            };
        }

        private void includeDataCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool includeData = includeDataCheckBox.Checked;
            if (!includeData)
            {
                schemaDataSeperateCheckBox.Checked = false;
            }
            schemaDataSeperateCheckBox.Enabled = includeData;
        }
    }
}
