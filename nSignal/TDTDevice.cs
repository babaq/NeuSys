#region File Description
//-----------------------------------------------------------------------------
// TDTDevice.cs
//
// NeuSys nSignal Tucker-Davis Technologies Device
// Copyright (c) Zhang Li. 2009-02-24.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TTANKXLib;
using TDEVACCXLib;
#endregion

namespace nSignal
{
    /// <summary>
    /// TDT Device
    /// </summary>
    public class TDTDevice : Device
    {
        TTankXClass TTX;
        TDevAccXClass TDX;
        public TTankXClass TTankX
        {
            get { return TTX; }
        }
        public TDevAccXClass TDevAccX
        {
            get { return TDX; }
        }
        # region TDT TTank Event TypeCode Define

        public const int EVTYPE_UNKNOWN = 0x00000000;
        public const int EVTYPE_STRON = 0x00000101;
        public const int EVTYPE_STROFF = 0x00000102;
        public const int EVTYPE_SCALAR = 0x00000201;
        public const int EVTYPE_STREAM = 0x00008101;
        public const int EVTYPE_SNIP = 0x00008201;
        public const int EVTYPE_MARK = 0x00008801;
        public const int EVTYPE_HASDATA = 0x00008000;

        # endregion
        #region TDT TTank Read Event Options Define

        public const int GET_ALL = 0x0000;
        public const int GET_NEW = 0x0001;
        public const int GET_SAME = 0x0002;
        public const int GET_JUSTTIMES = 0x0100;
        public const int GET_DOUBLES = 0x0200;
        public const int GET_NODATA = 0x0400;
        public const int GET_FILTERED = 0x1000;
        public const int GET_ORDERED = 0x2000;

        #endregion


        public TDTDevice()
        {
            try
            {
                TTX = new TTankXClass();
                TDX = new TDevAccXClass();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            LoadnsAPI(Vendor);
        }


        #region Device Interface

        public override bool ConnectDevice()
        {
            return ConnectDevice("Local");
        }

        public bool ConnectDevice(string DeviceService)
        {
            return Convert.ToBoolean(TDX.ConnectServer(DeviceService));
        }

        public override bool DisconnectDevice()
        {
            try
            {
                TDX.CloseConnection();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public override DeviceState DeviceState
        {
            get { return (DeviceState)TDX.GetSysMode(); }
            set { TDX.SetSysMode((int)value); }
        }

        public override Vendor Vendor
        {
            get { return Vendor.TDT; }
        }

        #endregion

        #region Signal Interface

        public override bool ConnectSignal()
        {
            return ConnectSignal("Local");
        }

        public bool ConnectSignal(string SignalService)
        {
            return Convert.ToBoolean(TTX.ConnectServer(SignalService, "NeuSys"));
        }

        public override bool DisconnectSignal()
        {
            try
            {
                TTX.CloseTank();
                TTX.ReleaseServer();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        #endregion

        public enum ItemCode
        {
            AllItem,
            WaveformDataSizeInByte,
            EventType,
            EventCode,
            ChannelNumber,
            SortingNumber,
            TimeStamp,
            /// <summary>
            /// Valid when no waveform data is attached
            /// </summary>
            ScalarValue,
            DataFormatCode,
            /// <summary>
            ///  Requires attached wavefrom data
            /// </summary>
            WaveformSampleRateInHz,
            /// <summary>
            ///  Returns 0
            /// </summary>
            NotUsed,
            XDimensionFilterID,
            YDimensionFilterID,
            ZDimensionFilterID,
            FillItem
        }

        public void CloseTank()
        {
            TTX.CloseTank();
        }

        public int OpenTank(string TankName, string AccessMode)
        {
            return TTX.OpenTank(TankName, AccessMode);
        }

        public int SelectBlock(string BlockName)
        {
            if (TTX.SelectBlock(BlockName) == 1)
            {
                return TTX.CreateEpocIndexing();
            }
            else
            {
                return 0;
            }
        }

        public string HotBlock
        {
            get { return TTX.GetHotBlock(); }
        }

        public string nsFile(string Server, string Tank, string Block)
        {
            try
            {
                string file = Directory.GetCurrentDirectory() + "\\nsTDT.stb";
                string[] nsfile = { "SERVER=" + Server, "TANK=" + Tank, "BLOCK=" + Block };

                File.WriteAllLines(file, nsfile, Encoding.UTF8);
                return file;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public string EvTypeToString(int evTypeCode)
        {
            return TTX.EvTypeToString(evTypeCode);
        }

        public int[] GetEventCodes(int EvType)
        {
            return (int[])TTX.GetEventCodes(EvType);
        }

        public string CodeToString(int EvCode)
        {
            return TTX.CodeToString(EvCode);
        }

        public int StringToEvCode(string EvCode)
        {
            return TTX.StringToEvCode(EvCode);
        }

        public int ReadEvents(int MaxRet,int EventCode,int Channel, int SortCode,double T1, double T2,int Options)
        {
            return TTX.ReadEvents(Math.Min(MaxRet, 10000000), EventCode, Channel, SortCode, T1, T2, Options);
        }

        public object ParseEvInfoV(int RecIndex,int nRecs,ItemCode nItem)
        {
            return TTX.ParseEvInfoV(RecIndex, nRecs, (int)nItem);
        }

        public object ParseEvV(int RecIndex, int nRecs)
        {
            return TTX.ParseEvV(RecIndex, nRecs);
        }

    }
}
