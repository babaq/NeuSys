using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NSCore;
using Microsoft.Scripting.Hosting;
using nSignal;

namespace Analysis
{
    public partial class AMainForm : AnalysisModule
    {
        public AMainForm() : this(null)
        {
        }

        public AMainForm(NeuSysConsole console)
        {
            InitializeComponent();

            NeuSysConsole = console;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.InitialDirectory = SourcePath;
            openfileDialog.Title = "Load Analysis Script";
            openfileDialog.Filter = "IronPython Script (*.py)|*.py|" +
                                "IronRuby Script (*.rb)|*.rb|" +
                                "F# Script (*.fsx)|*.fsx|" +
                                "All Files (*.*)|*.*";

            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openfileDialog.FileName;
                string filetype = file.Remove(0, file.LastIndexOf(".") + 1);

                Engine = ASRT.GetEngineByFileExtension(filetype);
                Source = Engine.CreateScriptSourceFromFile(file);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignalModule signal = (SignalModule)NeuSysConsole.CurrentModule("Signal");
            if (signal != null)
            {
                Scope.SetVariable("device", signal.Device);
                Scope.SetVariable("nsfile", signal.nsFile);
                Scope.SetVariable("nsfilehandle", signal.nsFileHandle);
                Eval();
            }
            else
            {
                MessageBox.Show("No Signal Source !");
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignalModule[] signalmodule = (SignalModule[])NeuSysConsole.CurrentModule(typeof(SignalModule));
            if (signalmodule != null)
            {
                var dev = signalmodule[0].Device as TDTDevice;
                var es = dev.GetEventCodes(TDTDevice.EVTYPE_SNIP);
                var snipn = dev.ReadEvents(Int32.MaxValue, es[0], 1, 0, 0.0, 0.0, TDTDevice.GET_NEW);
                var snipt = dev.ParseEvInfoV(0, snipn, TDTDevice.ItemCode.TimeStamp);
                var d = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                //var sss = (int[])snipt;
                if (snipn == 0)
                {
                    snipt = d;
                }
                var x = new int[snipn];
                for(int xx=0;xx<x.Length;xx++)
                {
                    x[xx] = 4;
                }
                chart1.Series["Series1"].Points.DataBindXY((double[])snipt, x);
                chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                //foreach (int t in d)
                //{
                //    chart1.Series["Series1"].Points.AddY(t);
                //}
                //chart1.DataBind();
                //chart1.Invalidate();

                Scope.SetVariable("device", signalmodule[0].Device as TDTDevice);
                Run();
            }
            else
            {
                MessageBox.Show("No Signal Source !");
            }
        }

    }
}
