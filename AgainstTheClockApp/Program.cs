using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgainstTheClock;

namespace AgainstTheClockApp
{
    public class AgainstTheClockSysTrayApp : Form
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new AgainstTheClockSysTrayApp());
        }

        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;


        public AgainstTheClockSysTrayApp()
        {
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", OnExit);
            trayMenu.MenuItems.Add("Activities", OnEnter);
            trayMenu.MenuItems.Add("Active activity logs", OnEnter);


            trayIcon = new NotifyIcon();
            trayIcon.Text = "Against the Clock";
            trayIcon.Icon = new Icon(this.GetType(), "sport_stopwatch_puN_icon.ico");
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnEnter(object sender, EventArgs e)
        {            
            switch (((MenuItem)sender).Text)
            {                
                case "Active activity logs":
                    frmActivityLogs frm = new frmActivityLogs();
                    var activities = ActivityLog.GetUnfinishedActivitiesByUser(1);
                    frm.dgvActivityLogs.DataSource = activities;
                    frm.ShowDialog();
                    break;
                case "Activities":
                    AgainstTheClockForm againstTheClockForm = new AgainstTheClockForm();
                    againstTheClockForm.ShowDialog();
                    break;                                       
            }
            
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Release the icon resource.
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }

    }
}
