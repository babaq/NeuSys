#region File Description
//-----------------------------------------------------------------------------
// CEDDevice.cs
//
// NeuSys nSignal Cambridge Electronic Design Device
// Copyright (c) Zhang Li. 2009-06-02.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

namespace nSignal
{
    /// <summary>
    /// CED Device
    /// </summary>
    public class CEDDevice : Device
    {
        public CEDDevice()
        {
            LoadnsAPI(Vendor);
        }

        #region Device

        public override bool ConnectDevice()
        {
            return false;
        }

        public override bool DisconnectDevice()
        {
            return false;
        }

        public override DeviceState DeviceState { get; set; }

        public override Vendor Vendor
        {
            get { return Vendor.CED; }
        }

        #endregion

        #region Signal

        public override bool ConnectSignal()
        {
            return false;
        }

        public override bool DisconnectSignal()
        {
            return false;
        }

        #endregion

    }

}
