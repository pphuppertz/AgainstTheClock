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
    public partial class AgainstTheClockForm : Form
    {
        private Activity currentActivity;
        private List<Activity> activeActivities;

        public AgainstTheClockForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadActivities();
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            AddActivity();
        }

        private void AddActivity()
        {
            frmActivity myForm = new frmActivity();
            myForm.Text = "Add activity";

            if (myForm.ShowDialog() == DialogResult.OK)
            {
                Activity activity = new Activity
                {
                    ActivityName = myForm.txtActivityName.Text,
                    LinkToCard = myForm.txtLinkToCard.Text,
                    Notes = myForm.txtNotes.Text,
                    IsActive = myForm.chkIsActive.Checked
                };
                activity.Save();
                LoadActivities();
            }
        }

        private void LoadActivities()
        {
            cmbActivities.Items.Clear();
            activeActivities = Activity.GetActiveActivities();
            foreach (var a in activeActivities)
            {
                cmbActivities.Items.Add(a.ActivityName);
            }
        }

        private void LoadCurrentActivity(int index)
        {
            currentActivity = activeActivities[index];
        }

        private void cmbActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbActivities.SelectedIndex > -1)
            {
                LoadCurrentActivity(cmbActivities.SelectedIndex);
            }
        }
    }
}
