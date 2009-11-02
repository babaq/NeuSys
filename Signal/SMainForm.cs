using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NSCore;
using nSignal;

namespace Signal
{
    public partial class SMainForm : SignalModule
    {
        Device Dev;
        

        public SMainForm() : this(null)
        {
        }

        public SMainForm(NeuSysConsole console)
        {
            InitializeComponent();

            NeuSysConsole = console;
            Dev = new Device();

            if (!Dev.ConnectSignal())
            {
                MessageBox.Show("Signal Connection Failed !", "No Signal Connected !");
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region ISignalModule

        public override Device Device
        {
            get { return Dev; }
        }

        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.Title = "Open Data File";
            openfileDialog.Filter = "NeuroShare Native File (*.nsn)|*.nsn|" +
                                "BlackRock Neural Event File (*.nev)|*.nev|" +
                                "TDT TTank Access (*.stb)|*.stb|" +
                                "All Files (*.*)|*.*";

            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openfileDialog.FileName;
                string filetype = file.Remove(0, file.LastIndexOf(".") + 1);

                if (!UnLoadnsFile()) return;
                if (!UnLoadnsAPI()) return;
                switch (filetype)
                {
                    case "nsn":
                        Dev.LoadnsAPI(Vendor.Unknown);
                        break;
                    case"nev":
                        Dev.LoadnsAPI(Vendor.BlackRock);
                        break;
                    case "stb":
                        Dev.LoadnsAPI(Vendor.TDT);
                        break;
                }

                nsFile = file;
            }
        }

        void nsfileinfo()
        {
            var fileinfo = new ns_FILEINFO();
            var hr = Dev.ns_GetFileInfo(nsFileHandle, ref fileinfo, Marshal.SizeOf(fileinfo));

            var entityinfo = new ns_ENTITYINFO();
            hr = Dev.ns_GetEntityInfo(nsFileHandle, 0, ref entityinfo, Marshal.SizeOf(entityinfo));

            var eventinfo = new ns_EVENTINFO();
            hr = Dev.ns_GetEventInfo(nsFileHandle, 106, ref eventinfo, Marshal.SizeOf(eventinfo));


            double timestamp  = 0.0;
            byte[] data = new byte[eventinfo.dwMaxDataLength];
            int datasize=0;
            hr = Dev.ns_GetEventData(nsFileHandle,106,13,ref timestamp,data,eventinfo.dwMaxDataLength,ref datasize);
            Int16 eventdata = BitConverter.ToInt16(data, 0);

            var analoginfo = new ns_ANALOGINFO();
            hr = Dev.ns_GetAnalogInfo(nsFileHandle, 101, ref analoginfo, Marshal.SizeOf(analoginfo));

            double[] pdata = new double[10];
            hr = Dev.ns_GetAnalogData(nsFileHandle, 101, 0, 10, ref datasize, pdata);


            var segmentinfo = new ns_SEGMENTINFO();
            hr = Dev.ns_GetSegmentInfo(nsFileHandle, 0, ref segmentinfo, Marshal.SizeOf(segmentinfo));

            var segmentsourceinfo = new ns_SEGSOURCEINFO();
            hr = Dev.ns_GetSegmentSourceInfo(nsFileHandle, 0, 0, ref segmentsourceinfo, Marshal.SizeOf(segmentsourceinfo));

            double[,] pd = new double[1,48]; 
            int unit=0;
            hr = Dev.ns_GetSegmentData(nsFileHandle, 0, 0, ref timestamp, pd, sizeof(double)*pd.Length, ref datasize, ref unit);

            var neuralinfo = new ns_NEURALINFO();
            hr = Dev.ns_GetNeuralInfo(nsFileHandle, 0, ref neuralinfo, Marshal.SizeOf(neuralinfo));
            hr = Dev.ns_GetNeuralData(nsFileHandle, 0, 0, 10, pdata);

            byte[] msg = new byte[256];
            hr = Dev.ns_GetLastErrorMsg(msg, 256);
            string MSG =Encoding.Default.GetString(msg);

            hr = Dev.ns_CloseFile(nsFileHandle);
        }

    }
}
