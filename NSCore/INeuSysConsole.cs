using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSCore
{
    public interface INeuSysConsole
    {
        bool SubscribeModule(IModule module);
        bool UnsubscribeModule(IModule module);

        IModule CurrentModule(string modulename);
        IModule[] CurrentModule(Type moduletype);
    }
}
