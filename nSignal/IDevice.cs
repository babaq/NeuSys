#region File Description
//-----------------------------------------------------------------------------
// IDevice.cs
//
// NeuSys nSignal Signal Acquisition Hardware Interface
// Copyright (c) Zhang Li. 2009-02-24.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace nSignal
{
    /// <summary>
    /// Interface to Abstract Hardware
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Connect to Hardware Device
        /// </summary>
        /// <returns>true(succeeds), false(fails)</returns>
        bool ConnectDevice();
        /// <summary>
        /// Disconnect from Hardware Device
        /// </summary>
        /// <returns>true(succeeds), false(fails)</returns>
        bool DisconnectDevice();
        /// <summary>
        /// Gets/Sets the Hardware Device State
        /// </summary>
        DeviceState DeviceState { get; set; }
        /// <summary>
        /// Gets the Current Hardware Device Vendor
        /// </summary>
        Vendor Vendor { get; }
    }

    /// <summary>
    /// Hardware Device States
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// Idle
        /// </summary>
        Idle,
        /// <summary>
        /// Standby
        /// </summary>
        Standby,
        /// <summary>
        /// Preview
        /// </summary>
        Preview,
        /// <summary>
        /// Record
        /// </summary>
        Record
    }

    /// <summary>
    /// Hardware Device Vendors
    /// </summary>
    public enum Vendor
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,
        /// <summary>
        /// AlphaOmega
        /// </summary>
        AlphaOmega,
        /// <summary>
        /// BlackRock
        /// </summary>
        BlackRock,
        /// <summary>
        /// CED
        /// </summary>
        CED,
        /// <summary>
        /// Plexon
        /// </summary>
        Plexon,
        /// <summary>
        /// TDT
        /// </summary>
        TDT
    }

}
