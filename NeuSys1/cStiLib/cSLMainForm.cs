using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ServiceModel;
using NSCore;
using cStiLib.ExService;

namespace cStiLib
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class cSLMainForm : ControlModule,ExConsole.IExServiceCallback, ExService.IExServiceCallback
    {
        ExConsole.ExServiceClient proxyExC;
        ExService.ExServiceClient proxyExS;
        bool isproxyExC_OK;
        bool isproxyExS_OK;
        string ExS_Type = "";
        bool isExdataChanged;
        List<object> exData = new List<object>();


        public cSLMainForm() : this(null)
        {
        }

        public cSLMainForm(NeuSysConsole console)
        {
            InitializeComponent();

            try
            {
                NeuSysConsole = console;
                proxyExS = new ExService.ExServiceClient(new InstanceContext(this));
                proxyExC = new ExConsole.ExServiceClient(new InstanceContext(this));

                exdataGrid.CellValueChanged += new DataGridViewCellEventHandler(exdataGrid_CellValueChanged);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        void exdataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isExdataChanged = true;
        }

        private string TryGetExConsole()
        {
            if (!isproxyExC_OK)
            {
                try
                {
                    if (proxyExC.State == CommunicationState.Faulted)
                    {
                        proxyExC.Abort();
                        proxyExC = new ExConsole.ExServiceClient(new InstanceContext(this));
                    }
                    proxyExC.Subscribe(true);
                    experimentlist.Items.AddRange(proxyExC.GetEx());
                    isproxyExC_OK = true;
                    return null;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return null;
        }

        private string TryGetExService()
        {
            if (!isproxyExS_OK)
            {
                try
                {
                    if (proxyExS.State == CommunicationState.Faulted)
                    {
                        proxyExS.Abort();
                        proxyExS = new ExService.ExServiceClient(new InstanceContext(this));
                    }
                    proxyExS.Subscribe(true);
                    isproxyExS_OK = true;
                    return null;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return null;
        }

        private void SearchService()
        {
            string hr_exc = "";
            string hr_exs = "";
            if (!isproxyExC_OK)
            {
                hr_exc = TryGetExConsole();
            }
            if (isproxyExC_OK)
            {
                excstate.Text = "ExConsole Online";
                excstate.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                excstate.Text = "ExConsole Offline";
                excstate.ForeColor = System.Drawing.Color.Red;
            }

            if (!isproxyExS_OK)
            {
                hr_exs = TryGetExService();
            }
            if (isproxyExS_OK)
            {
                exsstate.Text = "ExService Online";
                exsstate.ForeColor = System.Drawing.Color.Green;
                InitExServiceUI();
            }
            else
            {
                exsstate.Text = "ExService Offline";
                exsstate.ForeColor = System.Drawing.Color.Red;
            }

            info.Text = hr_exc + hr_exs;
        }

        void InitExServiceUI()
        {
            ExS_Type = proxyExS.GetExType();
            if (!string.IsNullOrEmpty(ExS_Type))
            {
                switch (ExS_Type)
                {
                    case "CenterSurround_2":
                        GratingPara cgrating = proxyExS.GetGrating(0);
                        exData.Add(cgrating);

                        exdataGrid.ColumnCount = 2;
                        exdataGrid.RowCount = 2;
                        exdataGrid[0, 0].Value = "cgrating.contrast";
                        exdataGrid[1, 0].Value = cgrating.contrast;
                        break;
                }
            }
        }

        void SetExServiceUI()
        {
            if (!string.IsNullOrEmpty(ExS_Type) && isExdataChanged)
            {
                switch (ExS_Type)
                {
                    case "CenterSurround_2":
                        proxyExS.SetGrating(0, (GratingPara)exData[0]);
                        break;
                }
            }
        }

        void ClearExServiceUI()
        {
            exdataGrid.Columns.Clear();
            exdataGrid.Rows.Clear();

            isExdataChanged = false;
            exData.Clear();
            ExS_Type = null;
        }

        #region IModule

        public override bool OnSignalRecord()
        {
            try
            {
                if (OnSignalIdle())
                {
                    runbutton_Click(runbutton, EventArgs.Empty);
                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public override bool OnSignalIdle()
        {
            try
            {
                stopbutton_Click(stopbutton, EventArgs.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private string OnRunStop(bool runstop)
        {
            var tdt = NeuSysConsole.CurrentModule("TDT Signal Client");
            try
            {
                string hr = "";
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.info.Text = "Server has changed its Run/Stop state to " +
                                         runstop.ToString();
                        hr = this.Text + " has Done.";
                    });
                }
                else
                {
                    this.info.Text = "Server has changed its Run/Stop state to " + runstop.ToString();
                    hr = this.Text + " has Done.";
                }

                if (tdt != null)
                {
                    if (runstop)
                    {
                        tdt.OnControlRun();
                    }
                    else
                    {
                        tdt.OnControlStop();
                    }
                }

                return hr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();

            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string exPath = Path.GetFullPath(assemblyLocation);

            openfileDialog.InitialDirectory = exPath;
            openfileDialog.Title = "Load Script";
            openfileDialog.Filter = "All Files (*.*)|*.*|" +
                                "F# Script (*.fsx)|*.fsx|" +
                                "IronPython Script (*.py)|*.py";

            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openfileDialog.FileName);

                string ex = openfileDialog.FileName.Substring(openfileDialog.FileName.LastIndexOf("\\") + 1);
                string hresult = proxyExS.InvokeScript(ex, reader.ReadToEnd());
                if (hresult != null)
                {
                    MessageBox.Show(hresult);
                }
                reader.Close();
            }
        }

        private void invokebutton_Click(object sender, EventArgs e)
        {
            if (isproxyExC_OK)
            {
                try
                {
                    string hresult = proxyExC.Invoke(experimentlist.SelectedItem.ToString());
                    if (string.IsNullOrEmpty(hresult))
                    {
                        this.info.Text = experimentlist.SelectedItem.ToString() + " has invoked in server.";
                    }
                    else
                    {
                        this.info.Text = hresult;
                    }
                }
                catch (Exception ex)
                {
                    ExC_OnServiceDispose();
                    this.info.Text = ex.Message;
                }
            }
            else
            {
                info.Text = "No ExConsole Online Now.";
            }
        }

        private void terminatebutton_Click(object sender, EventArgs e)
        {
            if (isproxyExC_OK)
            {
                try
                {
                    string hr = proxyExC.Terminate(experimentlist.SelectedItem.ToString());
                    if (string.IsNullOrEmpty(hr))
                    {
                        this.info.Text = experimentlist.SelectedItem.ToString() + " has terminated in server.";
                    }
                    else
                    {
                        this.info.Text = hr;
                    }
                }
                catch (Exception ex)
                {
                    ExC_OnServiceDispose();
                    this.info.Text = ex.Message;
                }
            }
            else
            {
                info.Text = "No ExConsole Online Now.";
            }
        }

        private void runbutton_Click(object sender, EventArgs e)
        {
            if (isproxyExS_OK)
            {
                try
                {
                    if (!proxyExS.get_RunStop())
                    {
                        SetExServiceUI();
                        proxyExS.set_RunStop(true);
                        refreshbutton_Click(sender, e);
                    }
                }
                catch (CommunicationException ex)
                {
                    ExS_OnServiceDispose();
                    info.Text = ex.Message;
                }
            }
            else
            {
               info.Text = "No ExService Online Now.";
            }
        }

        private void stopbutton_Click(object sender, EventArgs e)
        {
            if (isproxyExS_OK)
            {
                try
                {
                    if (proxyExS.get_RunStop())
                    {
                        proxyExS.set_RunStop(false);
                        refreshbutton_Click(sender, e);
                    }
                }
                catch (CommunicationException ex)
                {
                    ExS_OnServiceDispose();
                    info.Text = ex.Message;
                }
            }
            else
            {
                info.Text = "No ExService Online Now.";
            }
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            if (isproxyExS_OK)
            {
                try
                {
                    string hr = proxyExS.get_RunStop().ToString();
                    info.Text = "Experiment Run/Stop State is " + hr;
                }
                catch (CommunicationException ex)
                {
                    ExC_OnServiceDispose();
                    info.Text = ex.Message;
                }
            }
            else
            {
                info.Text = "No ExService Online Now.";
            }
        }

        #region ExConsole IExServiceCallback Members

        string ExConsole.IExServiceCallback.OnRunStop(bool runstop)
        {
            return OnRunStop(runstop);
        }

        void ExConsole.IExServiceCallback.OnServiceDispose()
        {
            ExC_OnServiceDispose();
        }

        #endregion

        #region ExService IExServiceCallback Members

        string ExService.IExServiceCallback.OnRunStop(bool runstop)
        {
            return OnRunStop(runstop);
        }

        void ExService.IExServiceCallback.OnServiceDispose()
        {
            ExS_OnServiceDispose();
        }

        #endregion

        void ExC_OnServiceDispose()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        isproxyExC_OK = false;
                        excstate.Text = "ExConsole Offline";
                        excstate.ForeColor = System.Drawing.Color.Red;
                    });
                }
                else
                {
                    isproxyExC_OK = false;
                    excstate.Text = "ExConsole Offline";
                    excstate.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void ExS_OnServiceDispose()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        isproxyExS_OK = false;
                        exsstate.Text = "ExService Offline";
                        exsstate.ForeColor = System.Drawing.Color.Red;
                        ClearExServiceUI();
                    });
                }
                else
                {
                    isproxyExS_OK = false;
                    exsstate.Text = "ExService Offline";
                    exsstate.ForeColor = System.Drawing.Color.Red;
                    ClearExServiceUI();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void searchOnlineServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchService();
        }


    }
}
