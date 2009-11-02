using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nSignal;
using NSCore;

namespace sTDT
{
    public partial class sTDTMainForm : SignalModule
    {
        TDTDevice TDTDev;
        

        public sTDTMainForm():this(null)
        {
        }

        public sTDTMainForm(NeuSysConsole console)
        {
            InitializeComponent();

            NeuSysConsole = console;
            TDTDev = new TDTDevice();

            if (!TDTDev.ConnectSignal())
            {
                MessageBox.Show("TTank Server 'Local' Connection Failed !", "No TTank Server Connected !");
            }
        }


        #region TTankInterface_Callback

        private void axServerSelect1_ServerChanged(object sender, AxTTankInterfaces.__ServerSelect_ServerChangedEvent e)
        {
            TDTDev.DisconnectSignal();

            if (!TDTDev.ConnectSignal(e.newServer))
            {
                MessageBox.Show("TTank Server " + e.newServer + " Connection Failed !", "No TTank Server Connected !");
            }
            else
            {
                axTankSelect1.UseServer = e.newServer;
                axTankSelect1.Refresh();
            }

        }

        private void axTankSelect1_TankChanged(object sender, AxTTankInterfaces.__TankSelect_TankChangedEvent e)
        {
            if (TDTDev.OpenTank(e.actTank, "R") == 0)
            {
                MessageBox.Show("Tank " + e.actTank + " Open Failed !", "No Tank Opened !");
            }
            else
            {
                axBlockSelect1.UseServer = e.actServer;
                axBlockSelect1.UseTank = e.actTank;
                axBlockSelect1.Refresh();
            }

        }

        private void axBlockSelect1_BlockChanged(object sender, AxTTankInterfaces.__BlockSelect_BlockChangedEvent e)
        {
            if (TDTDev.SelectBlock(e.actBlock) == 0)
            {
                MessageBox.Show("Block " + e.actBlock + " Open Failed !", "No Block Opened !");
            }
            else
            {
                axEventSelect1.UseServer = e.actServer;
                axEventSelect1.UseTank = e.actTank;
                axEventSelect1.UseBlock = e.actBlock;
                axEventSelect1.Refresh();
            }

        }

        private void axEventSelect1_ActEventChanged(object sender, AxTTankInterfaces.__EventSelect_ActEventChangedEvent e)
        {

        }

        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void record_Click(object sender, EventArgs e)
        {
            var stilib = NeuSysConsole.CurrentModule("StiLib Control Client");

            if (record.Text == "Record")
            {
                if (TDTDev.ConnectDevice(axServerSelect1.ActiveServer))
                {
                    if ((int)TDTDev.DeviceState < (int)DeviceState.Preview)
                    {
                        TDTDev.DeviceState = DeviceState.Record;
                        record.Text = "Idle";
                        record.ForeColor = Color.Black;

                        if (stilib != null)
                        {
                            Thread tempthread = new Thread(delegate() 
                                {
                                    try
                                    {
                                        var sl = NeuSysConsole.CurrentModule("StiLib Control Client"); 
                                        Thread.Sleep(4000); 
                                        sl.OnSignalRecord(); 
                                        Thread.CurrentThread.Abort();
                                    }
                                    catch(Exception)
                                    {
                                    }
                                });
                            tempthread.Name = "Delayed StiLib Experiment Run";
                            tempthread.Start();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("TDT OpenWorkbench Not Ready !", "No Connection !");
                }
            }
            else
            {
                if (TDTDev.DeviceState == DeviceState.Record)
                {
                    TDTDev.DeviceState = DeviceState.Idle;
                    TDTDev.DisconnectDevice();
                    record.Text = "Record";
                    record.ForeColor = Color.Red;

                    if (stilib != null)
                    {
                        stilib.OnSignalIdle();
                    }
                }
            }
        }

        private void preview_Click(object sender, EventArgs e)
        {
            if (preview.Text == "Preview")
            {
                if (TDTDev.ConnectDevice(axServerSelect1.ActiveServer))
                {
                    if ((int)TDTDev.DeviceState < (int)DeviceState.Preview)
                    {
                        TDTDev.DeviceState = DeviceState.Preview;
                        preview.Text = "Idle";
                        preview.ForeColor = Color.Black;
                    }
                }
                else
                {
                    MessageBox.Show("TDT OpenWorkbench Not Ready !", "No Connection !");
                }
            }
            else
            {
                if (TDTDev.DeviceState == DeviceState.Preview)
                {
                    TDTDev.DeviceState = DeviceState.Idle;
                    TDTDev.DisconnectDevice();
                    preview.Text = "Preview";
                    preview.ForeColor = Color.Blue;
                }
            }
        }

        private void off_Click(object sender, EventArgs e)
        {
            string nsfile = TDTDev.nsFile(axServerSelect1.ActiveServer, axTankSelect1.ActiveTank.Remove(0, axTankSelect1.ActiveTank.LastIndexOf("\\") + 1), axBlockSelect1.ActiveBlock);
            if (nsfile !=null)
            {
                nsFile = nsfile;
                var a = NeuSysConsole.CurrentModule("Analysis");
                if (a == null)
                {
                    var t = new Analysis.AMainForm(NeuSysConsole);
                    NeuSysConsole.SubscribeModule(t);
                    t.Show();
                }
            }
        }

        private void on_Click(object sender, EventArgs e)
        {
            if (TDTDev.ConnectDevice(axServerSelect1.ActiveServer))
            {
                if ((int)TDTDev.DeviceState >= 2)
                {
                    if (axTankSelect1.ActiveTank != "")
                    {
                        if (TDTDev.HotBlock != "")
                        {
                            if (axBlockSelect1.ActiveBlock == TDTDev.HotBlock)
                            {
                                string nsfile = TDTDev.nsFile(axServerSelect1.ActiveServer, axTankSelect1.ActiveTank.Remove(0, axTankSelect1.ActiveTank.LastIndexOf("\\") + 1), TDTDev.HotBlock);
                                if (nsfile != null)
                                {
                                    nsFile = nsfile;
                                    var a = NeuSysConsole.CurrentModule("Analysis");
                                    if (a == null)
                                    {
                                        var t = new Analysis.AMainForm(NeuSysConsole);
                                        NeuSysConsole.SubscribeModule(t);
                                        t.Show();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected Block Not the Active one, Reselect !", "Block Not Active !");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected Tank Not the Active one, Reselect !", "Tank Not Active !");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select the Online Tank to begin !", "No Tank Selected !");
                    }
                }
                else
                {
                    MessageBox.Show("TDT OpenWorkbench Not Running !", "Not Active !");
                }
            }
            else
            {
                MessageBox.Show("TDT OpenWorkbench Not Ready !", "No Connection !");
            }
        }

        private void recordIdleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            record_Click(sender, e);
        }

        private void previewIdleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            preview_Click(sender,e);
        }

        private void onlineAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            on_Click(sender, e);
        }

        private void offlineAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            off_Click(sender, e);
        }

        #region IModule

        public override bool OnControlRun()
        {
            try
            {
                record_Click(record, EventArgs.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool OnControlStop()
        {
            return OnControlRun();
        }

        #endregion

        #region ISignalModule

        public override Device Device
        {
            get { return TDTDev; }
        }

        #endregion

    }
}
