using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NSCore;

namespace NeuSys
{
    public partial class MainForm : NeuSysConsole
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutNeuSysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }


        private void Signal_newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new Signal.SMainForm(this);
            SubscribeModule(t);
            t.Show();
        }

        private void blackRockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new sBlackRock.sBRMainForm(this);
            SubscribeModule(t);
            t.Show();
        }
        
        private void tDTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new sTDT.sTDTMainForm(this);
            SubscribeModule(t);
            t.Show();
        }


        private void Analysis_newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new Analysis.AMainForm(this);
            SubscribeModule(t);
            t.Show();
        }


        private void Control_newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void stiLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new cStiLib.cSLMainForm(this);
            SubscribeModule(t);
            t.Show();
        }

        
    }
}
