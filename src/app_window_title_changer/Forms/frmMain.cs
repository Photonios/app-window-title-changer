using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace app_window_title_changer
{   
    public partial class frmMain : Form
    {
        public List<Window> OpenWindows = new List<Window>();

        public frmMain()
        {
            InitializeComponent();
        } 

        private void UpdateWindowsList()
        {
            int selected_index = this.lstWindows.SelectedIndex;

            List<Window> window_list = new List<Window>();

            Process[] processes = Process.GetProcesses();
            foreach(Process proc in processes)
            {
                if(proc.MainWindowHandle == IntPtr.Zero)
                    continue;

                if(string.IsNullOrEmpty(proc.MainWindowTitle) || string.IsNullOrWhiteSpace(proc.MainWindowTitle))
                    continue;

                window_list.Add(
                    new Window(proc)
                );
            }

            this.OpenWindows = window_list;
            this.lstWindows.DataSource = this.OpenWindows;
            this.lstWindows.DisplayMember = "MainWindowTitle";

            this.lstWindows.SelectedIndex= selected_index;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.UpdateWindowsList();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            UpdateWindowsList();   
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            bool handeld = false;

            switch(e.KeyCode)
            {
                case Keys.F5:
                    this.UpdateWindowsList();   
                    handeld = true;      
                    break;

                case Keys.F2:
                    lstWindows_DoubleClick(null, EventArgs.Empty);
                    handeld = true;
                    break;  
            }

            e.Handled = handeld;
        }

        private void lstWindows_DoubleClick(object sender, EventArgs e)
        {
            if(lstWindows.SelectedIndex < 0)
                return;

            if(this.OpenWindows == null)
                return;

            Window selected_window = this.OpenWindows[lstWindows.SelectedIndex];

            frmChangeTitle dialog = new frmChangeTitle(selected_window);
            DialogResult result = dialog.ShowDialog();
            
            if(result != System.Windows.Forms.DialogResult.OK)
                return;

            if(string.IsNullOrEmpty(dialog.Title) || string.IsNullOrWhiteSpace(dialog.Title))
            {
                MessageBox.Show("Cannot change the title to an empty string.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if(!selected_window.SetTitle(dialog.Title))
            {
                MessageBox.Show("Unable to change the title of the specified window.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tmrRefreshWindowList_Tick(object sender, EventArgs e)
        {
            this.UpdateWindowsList();
        }
    }    
}
