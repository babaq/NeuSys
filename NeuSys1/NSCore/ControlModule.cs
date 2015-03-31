using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCore
{
    public class ControlModule : Form, IModule, IControlModule
    {
        public static NeuSysConsole NeuSysConsole;


        #region IModule

        public virtual Type Type
        {
            get { return typeof(ControlModule); }
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
            if (NeuSysConsole != null)
            {
                NeuSysConsole.UnsubscribeModule(this);
                NeuSysConsole = null;
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}
