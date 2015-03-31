using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCore
{
    public class NeuSysConsole : Form, INeuSysConsole
    {
        Dictionary<string, ControlModule> cmdic = new Dictionary<string,ControlModule>();
        Dictionary<string, AnalysisModule> amdic = new Dictionary<string, AnalysisModule>();
        Dictionary<string, SignalModule> smdic = new Dictionary<string, SignalModule>();


        public virtual bool SubscribeModule(IModule m)
        {
            if (m.Type == typeof(ControlModule))
            {
                var t = m as ControlModule;
                if(!cmdic.ContainsValue(t))
                {
                    cmdic.Add(t.Text, t);
                }
                return true;
            }
            if (m.Type == typeof(AnalysisModule))
            {
                var t = m as AnalysisModule;
                if (!amdic.ContainsValue(t))
                {
                    amdic.Add(t.Text, t);
                }
                return true;
            }
            if (m.Type == typeof(SignalModule))
            {
                var t = m as SignalModule;
                if (!smdic.ContainsValue(t))
                {
                    smdic.Add(t.Text, t);
                }
                return true;
            }
            return false;
        }

        public virtual bool UnsubscribeModule(IModule m)
        {
            if (m.Type == typeof(ControlModule))
            {
                var t = m as ControlModule;
                if (cmdic.ContainsValue(t))
                {
                    cmdic.Remove(t.Text);
                }
                return true;
            }
            if (m.Type == typeof(AnalysisModule))
            {
                var t = m as AnalysisModule;
                if (amdic.ContainsValue(t))
                {
                    amdic.Remove(t.Text);
                }
                return true;
            }
            if (m.Type == typeof(SignalModule))
            {
                var t = m as SignalModule;
                if (smdic.ContainsValue(t))
                {
                    smdic.Remove(t.Text);
                }
                return true;
            }
            return false;
        }

        public virtual IModule CurrentModule(string modulename)
        {
            if (cmdic.ContainsKey(modulename))
            {
                return cmdic[modulename] as IModule;
            }
            if (amdic.ContainsKey(modulename))
            {
                return amdic[modulename] as IModule;
            }
            if (smdic.ContainsKey(modulename))
            {
                return smdic[modulename] as IModule;
            }
            return null;
        }

        public virtual IModule[] CurrentModule(Type moduletype)
        {
            if (moduletype == typeof(ControlModule) && cmdic.Count>0)
            {
                return cmdic.Values.ToArray<ControlModule>() as IModule[];
            }
            if (moduletype == typeof(AnalysisModule) && amdic.Count > 0)
            {
                return amdic.Values.ToArray<AnalysisModule>() as IModule[];
            }
            if (moduletype == typeof(SignalModule) && smdic.Count > 0)
            {
                return smdic.Values.ToArray<SignalModule>() as IModule[];
            }
            return null;
        }

    }
}
