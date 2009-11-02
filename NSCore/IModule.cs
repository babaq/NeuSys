using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSCore
{
    public interface IModule
    {
        Type Type { get; }

        bool OnSignalRecord();
        bool OnSignalIdle();
        bool OnControlRun();
        bool OnControlStop();
    }
}
