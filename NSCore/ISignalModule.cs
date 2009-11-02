using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nSignal;

namespace NSCore
{
    public interface ISignalModule
    {
        Device Device { get; }
        bool UnLoadnsAPI();
        bool UnLoadnsFile();
    }

}
