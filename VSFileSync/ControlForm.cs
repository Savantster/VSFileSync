using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSFileSync.SupportClasses;
using EnvDTE;

namespace Raydude.VSFileSync
{
    public partial class ControlForm : Form
    {
        private MyDB m_oDB = null;
        private DTE m_DTE = null;

        public ControlForm(ref MyDB oDB, ref DTE oDTE)
        {
            InitializeComponent();

            m_oDB = oDB;
            m_DTE = oDTE;

        }

        private void ControlForm_Load(object sender, EventArgs e)
        {
            if (m_oDB == null || m_DTE == null)
            {
                MessageBox.Show("Can't configure Tracking/Sync tool; missing internal DB connection or Solution Event tracker..", "Error!");
                this.Close();
            }


        }

    }

    // used to seed the OLV so we know what we have in our DB and what we need
    // to add to the tracking/sync tool.
    private class clDBDataTracker
    {
        public string SolutionName;
        public string ProjectName;
        public string LocalPath;
        public string RemotePath;
        public bool bTracking;

    }
}
