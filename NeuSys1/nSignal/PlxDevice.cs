#region File Description
//-----------------------------------------------------------------------------
// PlxDevice.cs
//
// NeuSys nSignal Plexon Neurotechnology Research Systems Device
// Copyright (c) Zhang Li. 2009-06-05.
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
    /// Plexon Device
    /// </summary>
    public class PlxDevice : Device
    {
        public PlxDevice()
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
            get { return Vendor.Plexon; }
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
