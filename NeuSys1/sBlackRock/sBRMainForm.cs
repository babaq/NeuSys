using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nSignal;
using NSCore;

namespace sBlackRock
{
    public partial class sBRMainForm : SignalModule
    {
        BRDevice BRDev;


        public sBRMainForm() : this(null)
        {
        }

        public sBRMainForm(NeuSysConsole console)
        {
            InitializeComponent();

            NeuSysConsole = console;
            BRDev = new BRDevice();
            if (!BRDev.ConnectSignal())
            {
                MessageBox.Show("Signal Connection Failed !", "No Signal Connected !");
            }
        }

        #region ISignalModule

        public override Device Device
        {
            get { return BRDev; }
        }

        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
