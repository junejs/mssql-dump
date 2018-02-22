using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mssql_dump
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }

        public new void Show()
        {
            base.Show();
            this.Refresh();
        }

        public void SetMsg(string msg)
        {
            msgLabel.Text = msg;
            Refresh();
        }
    }
}
