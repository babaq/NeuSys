using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using nSignal;
using System.Runtime.InteropServices;

namespace NSCore
{
    public class AnalysisModule : Form, IModule, IAnalysisModule
    {
        public static NeuSysConsole NeuSysConsole;
        public static ScriptRuntime ASRT;
        public static string SourcePath;

        public ScriptEngine Engine;
        public ScriptScope Scope;
        public ScriptSource Source;
        public CompiledCode CCode;


        public AnalysisModule()
        {
            var t = Directory.GetCurrentDirectory();
            SourcePath = t + "\\Analysis";
            if (!Directory.Exists(SourcePath))
            {
                Directory.CreateDirectory(SourcePath);
            }

            ASRT = new ScriptRuntime(ScriptRuntimeSetup.ReadConfiguration(t+"\\NSCore.dll.config"));
            ASRT.LoadAssembly(Assembly.GetAssembly(typeof(IDevice)));
            ASRT.LoadAssembly(Assembly.GetAssembly(typeof(Marshal)));

            Scope = ASRT.CreateScope();
        }


        #region IModule

        public Type Type
        {
            get { return typeof(AnalysisModule); }
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

        #region IAnalysisModule

        public virtual object Eval()
        {
            if (Source != null)
            {
                return Source.Execute(Scope);
            }
            else
            {
                MessageBox.Show("No Script Source Loaded !");
                return null;
            }
        }

        public virtual object Run()
        {
            if (Source != null)
            {
                CCode = Source.Compile();
                return CCode.Execute(Scope);
            }
            else
            {
                MessageBox.Show("No Script Source Loaded !");
                return null;
            }
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
