using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgainstTheClock;

namespace AgainstTheClockApp
{
    public partial class frmActivityLogs : Form
    {
        public List<ActivityLog> activityLogs;
        public frmActivityLogs()
        {
            InitializeComponent();
        }

        private void frmActivityLogs_Load(object sender, EventArgs e)
        {

        }
    }
}
