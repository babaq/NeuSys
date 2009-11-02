#region File Description
//-----------------------------------------------------------------------------
// ISignal.cs
//
// NeuSys nSignal Signal Structure/Method Interface
// Copyright (c) Zhang Li. 2009-02-24.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
#endregion

namespace nSignal
{
    /// <summary>
    /// Signal Interface According and Extending 'neuroshare' Standard
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// Connect to Signal Source
        /// </summary>
        /// <returns>true(succeeds), false(fails)</returns>
        bool ConnectSignal();
        /// <summary>
        /// Disconnect from Signal Source
        /// </summary>
        /// <returns>true(succeeds), false(fails)</returns>
        bool DisconnectSignal();
    }


    #region NeuroShare Define

    /// <summary>
    /// All of the Neuroshare API functions return a 32-bit integer declared as type ns_RETURN.
    /// </summary>
    public enum ns_RETURN
    {
        /// <summary>
        /// Invalid entity index specified
        /// </summary>
        ns_BADINDEX = -7,
        /// <summary>
        /// Invalid source identifier specified
        /// </summary>
        ns_BADSOURCE,
        /// <summary>
        /// Invalid or inappropriate entity identifier specified
        /// </summary>
        ns_BADENTITY,
        /// <summary>
        /// Invalid file handle passed to function
        /// </summary>
        ns_BADFILE,
        /// <summary>
        /// File access or read error
        /// </summary>
        ns_FILEERROR,
        /// <summary>
        /// Library unable to open file type
        /// </summary>
        ns_TYPEERROR,
        /// <summary>
        /// Generic linked library error
        /// </summary>
        ns_LIBERROR,
        /// <summary>
        /// Function successful
        /// </summary>
        ns_OK
    }

    /// <summary>
    /// Describe the type of event data
    /// </summary>
    public enum ns_EVENTTYPE
    {
        /// <summary>
        /// Text string
        /// </summary>
        ns_EVENT_TEXT,
        /// <summary>
        /// Comma separated values
        /// </summary>
        ns_EVENT_CSV,
        /// <summary>
        /// 8-bit binary values
        /// </summary>
        ns_EVENT_BYTE,
        /// <summary>
        /// 16-bit binary values
        /// </summary>
        ns_EVENT_WORD,
        /// <summary>
        /// 32-bit binary values
        /// </summary>
        ns_EVENT_DWORD
    }

    /// <summary>
    /// Specify the type of entity data
    /// </summary>
    public enum ns_ENTITYTYPE
    {
        /// <summary>
        /// Unknown Type
        /// </summary>
        ns_ENTITY_UNKNOWN,
        /// <summary>
        /// Discrete events that consist of small time-stamped text or binary data packets.
        /// These are used to represent data such as trial markers, experimental events,
        /// digital input values, and embedded user comments.
        /// </summary>
        ns_ENTITY_EVENT,
        /// <summary>
        /// Continuous, sampled data that represent digitized analog signals such
        /// as position, force, and other experiment signals, as well as electrode signals such as EKG,
        /// EEG and extracellular microelectrode recordings. Analog Entities may also contain gaps
        /// in time from data files that do not record data between experimental trials.
        /// </summary>
        ns_ENTITY_ANALOG,
        /// <summary>
        /// Short, time-stamped segments of digitized analog signals in which
        /// the segments are separated by variable amounts of time. Segment Entities can contain
        /// data from more than one source. They are intended to represent discontinuous analog
        /// signals such as extracellular spike waveforms from electrodes or groups of electrodes.
        /// </summary>
        ns_ENTITY_SEGMENT,
        /// <summary>
        /// Timestamps of event and segment entitities that are known to
        /// represent neural action potential firing times. For example, if a segment entity contains
        /// sorted neural spike waveforms, each sorted unit is also exported as a neural entity. If an
        /// event entity is known by the library to only contain neuron firing times, it should be
        /// exported as a neural event entity instead of an event entity. This entity provides a simple,
        /// efficient representation of neural firing times for high-level neuroscience analysis
        /// programs. It avoids the problems of requiring applications to look for spike timing
        /// within different entity types and the problems associated with decoding one or more unit
        /// firing times from within single segment entries.
        /// </summary>
        ns_ENTITY_NEURALEVENT
    }

    /// <summary>
    /// Additional library flags.
    /// </summary>
    public enum ns_LIBFLAG
    {
        /// <summary>
        /// includes debug info linkage
        /// </summary>
        ns_LIBRARY_DEBUG = 0x01,
        /// <summary>
        /// file was patched or modified
        /// </summary>
        ns_LIBRARY_MODIFIED = 0x02,
        /// <summary>
        /// pre-release or beta version
        /// </summary>
        ns_LIBRARY_PRERELEASE = 0x04,
        /// <summary>
        /// different from release version
        /// </summary>
        ns_LIBRARY_SPECIALBUILD = 0x08,
        /// <summary>
        /// library is multithread safe
        /// </summary>
        ns_LIBRARY_MULTITHREADED = 0x10
    }

    /// <summary>
    /// Specify whether the index to be retrieved belongs to the data item
    /// occurring before or after the specified time dTime.
    /// </summary>
    public enum ns_INDEXFLAG
    {
        /// <summary>
        /// return the data entry occuring before
        /// and inclusive of the time dTime.
        /// </summary>
        ns_BEFORE = -1,
        /// <summary>
        /// return the data entry occuring at or closest
        /// to the time dTime
        /// </summary>
        ns_CLOSEST,
        /// <summary>
        /// return the data entry occuring after
        /// and inclusive of the time dTime.
        /// </summary>
        ns_AFTER
    }

    #endregion

    #region NeuroShare Struct

    /// <summary>
    /// The size is 64 Bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_FILEDESC
    {
        /// <summary>
        /// Text description of the file type or file family
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        string szDescription;
        /// <summary>
        /// Extension used on PC, Linux, and Unix Platforms.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        string szExtension;
        /// <summary>
        /// Application and Type Codes used on Mac Platforms.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        string szMacCodes;
        /// <summary>
        /// null-terminated code used at the file beginning.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        string szMagicCode;
    }

    /// <summary>
    /// The size is 1192 Bytes.
    /// 
    /// The dwFileDescCount and FileDesc fields provide a method for the library to describe
    /// the file types that it is capable of opening. The ns_LIBRARYINFO structure provides
    /// room for up to 16 file types. The number of valid ns_FILEDESC structures are reported
    /// in dwFileDescCount. Unused ns_FILEDESC structures should be set to all zeros or not returned.
    /// 
    /// Neural Event Files File formats that consist of pools of files in a directory that belong to a
    /// single data set should be opened with an index file or one of the pool member files.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_LIBRARYINFO
    {
        /// <summary>
        /// Major version number of this library.
        /// </summary>
        public int dwLibVersionMaj;
        /// <summary>
        /// Minor version number of this library.
        /// </summary>
        public int dwLibVersionMin;
        /// <summary>
        /// Major version number of API specification that library complies with
        /// </summary>
        public int dwAPIVersionMaj;
        /// <summary>
        /// Minor version number of API specification that library complies with
        /// </summary>
        public int dwAPIVersionMin;
        /// <summary>
        /// Text description of the library.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        string szDescription;
        /// <summary>
        /// Name of library creator.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        string szCreator;
        /// <summary>
        /// Year of last modification date
        /// </summary>
        public int dwTime_Year;
        /// <summary>
        /// Month (1-12; January = 1) of last modification date
        /// </summary>
        public int dwTime_Month;
        /// <summary>
        /// Day of the month (1-31) of last modification date
        /// </summary>
        public int dwTime_Day;
        /// <summary>
        /// Additional LIBFLAG.
        /// </summary>
        public int dwFlags;
        /// <summary>
        /// Maximum number of files library can simultaneously open.
        /// </summary>
        public int dwMaxFiles;
        /// <summary>
        /// Number of valid description entries in the following array.
        /// </summary>
        public int dwFileDescCount;
        /// <summary>
        /// Text descriptor of files that the DLL can interpret.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public ns_FILEDESC[] FileDesc;
    }

    /// <summary>
    /// The size is 400 Bytes.
    /// 
    /// The time and date variables in the ns_FILEINO structure refer to
    /// the beginning (time zero in the source file) of the time span to which the data is referenced.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_FILEINFO
    {
        /// <summary>
        /// Human readable manufacturer's file type descriptor.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szFileType;
        /// <summary>
        /// Number of entities in the data file. This number is used
        /// to enumerate all the entities in the data file from 0 to
        /// (dwEntityCount –1) and to identify each entity in function calls (dwEntityID).
        /// </summary>
        public int dwEntityCount;
        /// <summary>
        /// Minimum timestamp resolution in seconds.
        /// </summary>
        public double dTimeStampResolution;
        /// <summary>
        /// Time span covered by the data file in seconds.
        /// </summary>
        public double dTimeSpan;
        /// <summary>
        /// Information about the application that created the file.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szAppName;
        /// <summary>
        /// Year.
        /// </summary>
        public int dwTime_Year;
        /// <summary>
        /// Month (1-12; January = 1).
        /// </summary>
        public int dwTime_Month;
        /// <summary>
        /// Used to be - Day of the week (Sunday = 0).
        /// </summary>
        public int dwReserved;
        /// <summary>
        /// Day of the month (1-31).
        /// </summary>
        public int dwTime_Day;
        /// <summary>
        /// Hour since midnight (0-23).
        /// </summary>
        public int dwTime_Hour;
        /// <summary>
        /// Minute after the hour (0-59).
        /// </summary>
        public int dwTime_Min;
        /// <summary>
        /// Seconds after the minute (0-59).
        /// </summary>
        public int dwTime_Sec;
        /// <summary>
        /// Milliseconds after the second (0-1000).
        /// </summary>
        public int dwTime_MilliSec;
        /// <summary>
        /// Comments embedded in the source file.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szFileComment;
    }

    /// <summary>
    /// The size is 40 Bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_ENTITYINFO
    {
        /// <summary>
        /// Specifies the label or name of the entity.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szEntityLabel;
        /// <summary>
        /// Flag specifying the ENTITYTYPE recorded on this channel.
        /// </summary>
        public int dwEntityType;
        /// <summary>
        /// Number of data items for the specified entity in the file.
        /// </summary>
        public int dwItemCount;
    }

    /// <summary>
    /// The size is 140 Bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_EVENTINFO
    {
        /// <summary>
        /// A EVENTTYPE code describing the type of event data associated with each indexed entry.
        /// </summary>
        public int dwEventType;
        /// <summary>
        /// Minimum number of bytes that can be returned for an Event.
        /// </summary>
        public int dwMinDataLength;
        /// <summary>
        /// Maximum number of bytes that can be returned for an Event.
        /// </summary>
        public int dwMaxDataLength;
        /// <summary>
        /// Provides descriptions of the data fields for CSV Event Entities.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSVDesc;
    }

    /// <summary>
    /// The size is 264 Bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_ANALOGINFO
    {
        /// <summary>
        /// The sampling rate in Hz used to digitize the analog values.
        /// </summary>
        public double dSampleRate;
        /// <summary>
        /// Minimum possible value of the input signal.
        /// </summary>
        public double dMinVal;
        /// <summary>
        /// Maximum possible value of the input signal.
        /// </summary>
        public double dMaxVal;
        /// <summary>
        /// Specifies the recording units of measurement.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szUnits;
        /// <summary>
        /// Minimum input step size that can be resolved.
        /// (E.g. for a +/- 1 Volt 16-bit ADC this value is .0000305).
        /// </summary>
        public double dResolution;
        /// <summary>
        /// X coordinate of source in meters.
        /// </summary>
        public double dLocationX;
        /// <summary>
        /// Y coordinate of source in meters.
        /// </summary>
        public double dLocationY;
        /// <summary>
        /// Z coordinate of source in meters.
        /// </summary>
        public double dLocationZ;
        /// <summary>
        /// Additional manufacturer-specific position information
        /// (e.g. electrode number in a tetrode).
        /// </summary>
        public double dLocationUser;
        /// <summary>
        /// High frequency cutoff in Hz of the source signal filtering.
        /// </summary>
        public double dHighFreqCorner;
        /// <summary>
        /// Order of the filter used for high frequency cutoff.
        /// </summary>
        public int dwHighFreqOrder;
        /// <summary>
        /// Type of filter used for high frequency cutoff (text format).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szHighFilterType;
        /// <summary>
        /// Low frequency cutoff in Hz of the source signal filtering.
        /// </summary>
        public double dLowFreqCorner;
        /// <summary>
        /// Order of the filter used for low frequency cutoff.
        /// </summary>
        public int dwLowFreqOrder;
        /// <summary>
        /// Type of filter used for low frequency cutoff (text format).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szLowFilterType;
        /// <summary>
        /// Additional text information about the signal source.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szProbeInfo;
    }

    /// <summary>
    /// The size is 52 Bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_SEGMENTINFO
    {
        /// <summary>
        /// Number of sources contributing to the Segment Entity data.
        /// For example, with tetrodes, this number would be 4.
        /// </summary>
        public int dwSourceCount;
        /// <summary>
        /// Minimum number of samples in each Segment data item.
        /// </summary>
        public int dwMinSampleCount;
        /// <summary>
        /// Maximum number of samples in each Segment data item.
        /// </summary>
        public int dwMaxSampleCount;
        /// <summary>
        /// The sampling rate in Hz used to digitize source signals.
        /// </summary>
        public double dSampleRate;
        /// <summary>
        /// Specifies the recording units of measurement.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szUnits;
    }

    /// <summary>
    /// The size is 248 Bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_SEGSOURCEINFO
    {
        /// <summary>
        /// Minimum possible value of the input signal.
        /// </summary>
        public double dMinVal;
        /// <summary>
        /// Maximum possible value of the input signal.
        /// </summary>
        public double dMaxVal;
        /// <summary>
        /// Minimum input step size that can be resolved.
        /// (E.g. for a +/- 1 Volt 16-bit ADC this value is .0000305).
        /// </summary>
        public double dResolution;
        /// <summary>
        /// Time difference (in sec) between the nominal timestamp
        /// and the actual sampling time of the source probe. This
        /// value will be zero when all source probes are sampled simultaneously.
        /// </summary>
        public double dSubSampleShift;
        /// <summary>
        /// X coordinate of source in meters.
        /// </summary>
        public double dLocationX;
        /// <summary>
        /// Y coordinate of source in meters.
        /// </summary>
        public double dLocationY;
        /// <summary>
        /// Z coordinate of source in meters.
        /// </summary>
        public double dLocationZ;
        /// <summary>
        /// Additional manufacturer-specific position information
        /// (e.g. electrode number in a tetrode).
        /// </summary>
        public double dLocationUser;
        /// <summary>
        /// High frequency cutoff in Hz of the source signal filtering.
        /// </summary>
        public double dHighFreqCorner;
        /// <summary>
        /// Order of the filter used for high frequency cutoff.
        /// </summary>
        public int dwHighFreqOrder;
        /// <summary>
        /// Type of filter used for high frequency cutoff (text format).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szHighFilterType;
        /// <summary>
        /// Low frequency cutoff in Hz of the source signal filtering.
        /// </summary>
        public double dLowFreqCorner;
        /// <summary>
        /// Order of the filter used for low frequency cutoff.
        /// </summary>
        public int dwLowFreqOrder;
        /// <summary>
        /// Type of filter used for low frequency cutoff (text format).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szLowFilterType;
        /// <summary>
        /// Additional text information about the signal source.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szProbeInfo;
    }

    /// <summary>
    /// The size is 136 Bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ns_NEURALINFO
    {
        /// <summary>
        /// Optional ID number of a source entity. If the Neural Event is
        /// derived from other entity sources, such as Segment Entities,
        /// this value links the Neural Event back to the source.
        /// </summary>
        public int dwSourceEntityID;
        /// <summary>
        /// Optional sorted unit ID number used in the source Entity.
        /// </summary>
        public int dwSourceUnitID;
        /// <summary>
        /// Text information about the source probe or the label of a source Segment Entity.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szProbeInfo;
    }

    #endregion

}
