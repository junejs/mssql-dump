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
using System.Threading.Tasks;
using System.Threading;

namespace mssql_dump
{
    public partial class MainForm : Form
    {
        private SynchronizationContext _uiSyncContext;
        public MainForm()
        {
            InitializeComponent();
            _uiSyncContext = SynchronizationContext.Current;
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
            LoadingForm loadingForm = new LoadingForm();
            loadingForm.SetMsg("Generating script, please wait...");

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

            
                loadingForm.Show(this);

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
            finally
            {
                loadingForm.Close();
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

            Task.Factory.StartNew<List<string>>(() =>
            {
                using (IDbConnection conn = new SqlConnection(ssb.ConnectionString))
                {
                    try
                    {
                        return conn.Query<string>("select name from sys.databases").ToList();
                    }
                    catch
                    {
                        return null;
                    }
                }
            })
            .ContinueWith(t =>
            {
                _uiSyncContext.Post(state =>
                {
                    var dbNames = state as List<string>;
                    if (dbNames == null)
                    {
                        return;
                    }

                    dbNameComboBox.Items.Clear();
                    dbNameComboBox.Items.AddRange(dbNames.ToArray());
                }, t.Result);
            });
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
