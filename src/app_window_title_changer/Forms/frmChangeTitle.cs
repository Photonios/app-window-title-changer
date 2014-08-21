using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_window_title_changer
{
    public partial class frmChangeTitle : Form
    {
        public Window Window { get; private set; }

        public string Title
        {
            get
            {
                return this.txtWindowTitle.Text;
            }
        }

        public frmChangeTitle(Window window)
        {
            this.Window = window;

            InitializeComponent();            
        }

        private void frmChangeTitle_Load(object sender, EventArgs e)
        {
            this.txtWindowTitle.Text = this.Window.MainWindowTitle ?? string.Empty;
        }

        private void txtWindowTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
                return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmChangeTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Escape)
                return;

            e.Handled = true;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;            
            this.Close();
        }
    }
}
