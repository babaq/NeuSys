using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nSignal;

namespace NSCore
{
    public class SignalModule : Form, IModule, ISignalModule
    {
        public static NeuSysConsole NeuSysConsole;
        public string nsFile;
        public int nsFileHandle;


        #region IModule

        public Type Type
        {
            get { return typeof(SignalModule); }
        }

        public virtual bool OnSignalRecord()
        {
            return false;
        }
        public virtual bool OnSignalIdle()
        {
            return false;
        }
        public virtual bool OnControlRun()
        {
            return false;
        }
        public virtual bool OnControlStop()
        {
            return false;
        }

        #endregion

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            UnLoadnsFile();
            Device.Dispose();
            if (NeuSysConsole != null)
            {
                NeuSysConsole.UnsubscribeModule(this);
                NeuSysConsole = null;
            }
            base.Dispose(disposing);
        }

        #endregion

        #region ISignalModule Members

        public virtual Device Device
        {
            get { return new Device(); }
        }

        public virtual bool UnLoadnsAPI()
        {
            bool hr = Device.UnloadnsAPI();
            if (!hr)
            {
                MessageBox.Show("Can't UnLoad Previously Loaded NeuroShare Library.", "UnLoadnsAPI Error !");
            }
            return hr;
        }

        public virtual bool UnLoadnsFile()
        {
            if (nsFileHandle != 0)
            {
                if (Device.ns_CloseFile(nsFileHandle) != ns_RETURN.ns_OK)
                {
                    MessageBox.Show("Can't Close Previously Loaded NeuroShare File.", "ns_CloseFile Error !");
                    return false;
                }
            }
            return true;
        }

        #endregion

    }
}
