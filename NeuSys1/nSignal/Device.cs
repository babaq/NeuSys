#region File Description
//-----------------------------------------------------------------------------
// Device.cs
//
// NeuSys nSignal Base Virtual Hardware
// Copyright (c) Zhang Li. 2009-02-24.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
#endregion

namespace nSignal
{
    /// <summary>
    /// Base Device
    /// </summary>
    public class Device : IDevice, ISignal, IDisposable
    {
        int hModule;

        /// <summary>
        /// Constructor
        /// </summary>
        public Device()
        {
        }
        ~Device()
        {
            Dispose(false);
        }


        #region NeuroShare API Delegate Define

        /// <summary>
        /// Obtains information about the API library.
        /// </summary>
        /// <param name="pLibraryInfo">Pointer to structure to receive library version information.</param>
        /// <param name="dwLibraryInfoSize">Allocated size in bytes for ns_LIBRARYINFO structure.</param>
        /// <returns>This function returns ns_OK if the data is successfully retrieved. Otherwise ns_LIBERROR is returned.</returns>
        public delegate ns_RETURN ns_pGetLibraryInfo(ref ns_LIBRARYINFO pLibraryInfo, int dwLibraryInfoSize);

        /// <summary>
        /// Opens the file specified by pszFilename and returns a file handle, 
        /// hFile that is used to access the opened file.
        /// 
        /// All files are opened for read-only, as no writing capabilities have been implemented. If the
        /// command succeeds in opening the file, the application should call ns_CloseFile for each open
        /// file before terminating.
        /// The file handle hFile is a file enumeration created by the library and is recognizable only
        /// within the library. If the file is invalid or there is no file associated with it, a NULL file
        /// handle is returned.
        /// </summary>
        /// <param name="pszFilename">Pointer to a null-terminated string that specifies the name of the file to open.</param>
        /// <param name="hFile">Handle to the opened file. This value is returned by the function and is used for subsequent file operations within the library.</param>
        /// <returns>This function returns ns_OK if the file is successfully opened. Otherwise one of the following error codes is generated:
        /// ns_TYPEERROR - Library unable to open file type
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pOpenFile([MarshalAs(UnmanagedType.LPStr)]string pszFilename, ref int hFile);
        /// <summary>
        /// Provides general information about the data file referenced by hFile. This information is
        /// returned in the structure pointed to by pFileInfo. The number of bytes allocated for the file
        /// information structure is given by dwFileInfoSize.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="pFileInfo">Pointer to the ns_FILEINFO structure that receives the file information.</param>
        /// <param name="dwFileInfoSize">Allocated size in bytes for the ns_FILEINFO structure.</param>
        /// <returns>This function returns ns_OK if the file information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_FILEERROR - File access or read error
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetFileInfo(int hFile, ref ns_FILEINFO pFileInfo, int dwFileInfoSize);
        /// <summary>
        /// Closes a previously opened file specified by the file handle hFile.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <returns>This function returns ns_OK when the file is successfully closed. Otherwise the following error code is generated:
        /// ns_BADFILE - Invalid file handle passed to function.</returns>
        public delegate ns_RETURN ns_pCloseFile(int hFile);

        /// <summary>
        /// Retrieves general information about the entity, dwEntityID, from the file referenced by the
        /// file handle hFile. The information is passed in the structure pointed to by pEntityInfo. The
        /// number of bytes allocated for ns_ENTITYINFO is specified by dwEntityInfoSize.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file. The total number of
        /// entities in the data file is provided by the member dwEntityCount in the ns_FILEINFO structure.</param>
        /// <param name="pEntityInfo">Pointer to a ns_ENTITYINFO structure to receive entity information.</param>
        /// <param name="dwEntityInfoSize">Allocated size in bytes for the ns_ENTITYINFO structure..</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetEntityInfo(int hFile, int dwEntityID, ref ns_ENTITYINFO pEntityInfo, int dwEntityInfoSize);

        /// <summary>
        /// Retrieves information from the file referenced by hFile about the Event Entity, dwEntityID,
        /// in the structure pointed to by pEventsInfo. The structure has an allocated size of dwEventInfoSize bytes.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="pEventInfo">Pointer to a ns_EVENTINFO structure to receive the Event Entity information.</param>
        /// <param name="dwEventInfoSize">Allocated size in bytes for the ns_EVENTINFO structure.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetEventInfo(int hFile, int dwEntityID, ref ns_EVENTINFO pEventInfo, int dwEventInfoSize);
        /// <summary>
        /// Returns the data values from the file referenced by hFile and the Event Entity dwEntityID.
        /// The Event data entry specified by dwIndex is written to pData and the timestamp of the entry
        /// is returned to pdTimeStamp. dwDataBufferSize specifies the size in bytes allocated to the
        /// buffer at pData. The pointer pdwDataRetSize gives the actual number of bytes of data retrieved to the buffer.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="dwIndex">The index number of the requested Event data item.</param>
        /// <param name="pdTimeStamp">Pointer to a variable that receives the timestamp of the Event data item.</param>
        /// <param name="pData">Pointer to a buffer that receives the data for the Event entry. The format
        /// of Event data is specified by the member dwEventType in ns_EVENTINFO.</param>
        /// <param name="dwDataBufferSize">The number of bytes allocated to the receiving data buffer.</param>
        /// <param name="pdwDataRetSize">Pointer to a variable that receives the actual number of bytes of data retrieved in the data buffer.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_BADINDEX - Invalid entity index specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetEventData(int hFile, int dwEntityID, int dwIndex, ref double pdTimeStamp, byte[] pData, int dwDataBufferSize, ref int pdwDataRetSize);

        /// <summary>
        /// Returns information about the Analog Entity associated with dwEntityID and the file hFile.
        /// The information is stored in a ns_ANALOGINFO structure, pointed to by
        /// pAnalogSourceInfo. The size in bytes allocated for ns_ANALOGINFO is specified by dwAnalogInfoSize.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="pAnalogInfo">Pointer to a ns_ANALOGINFO structure.</param>
        /// <param name="dwAnalogInfoSize">Allocated size in bytes for ns_ANALOGINFO structure.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetAnalogInfo(int hFile, int dwEntityID, ref ns_ANALOGINFO pAnalogInfo, int dwAnalogInfoSize);
        /// <summary>
        /// Returns the data values associated with the Analog Entity indexed dwEntityID in the file
        /// referenced by hFile. The index of the first data value is dwStartIndex and the requested
        /// number of data samples is given by dwIndexCount. The requested data values are returned in
        /// the buffer pointed to by pData.
        /// Although the samples in an analog entity are indexed, they are not guaranteed to be
        /// continuous in time and may contain gaps between some of the indexes. When the requested
        /// data is returned, pdwContCount contains the number of Analog items, starting from
        /// dwStartIndex, which do not contain a time gap.
        /// If the index range specified by dwStartIndex to dwStartIndex+dwIndexCount contains
        /// invalid indexes, the function will return ns_BADINDEX.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the Analog Entity in the data file.</param>
        /// <param name="dwStartIndex">Starting index number of the analog data item.</param>
        /// <param name="dwIndexCount">Number of analog values to retrieve.</param>
        /// <param name="pdwContCount">Number of continuous data values retrieved. This field is ignored if the pointer is set to NULL.</param>
        /// <param name="pData">Pointer to an array of double precision values to receive the analog data.
        /// The user application must allocate sufficient space to hold dwIndexCount
        /// double values or dwIndexCount*sizeof(double) bytes. If this pointer is NULL, no data is returned</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_BADINDEX - Invalid entity index or range specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetAnalogData(int hFile, int dwEntityID, int dwStartIndex, int dwIndexCount, ref int pdwContCount, double[] pData);

        /// <summary>
        /// Retrieves information on the Segment Entity, dwEntityID, in the file referenced by the handle
        /// hFile. The information is written to the ns_SEGMENTINFO structure at pdwSegmentInfo.
        /// The size of memory in bytes allocated for the ns_SEGMENTINFO structure is specified by dwSegmentInfoSize.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="pdwSegmentInfo">Pointer to the structure ns_SEGMENTINFO that receives general
        /// segment information for the requested Segment Entity.</param>
        /// <param name="dwSegmentInfoSize">Allocated size in bytes for the structure ns_SEGMENTINFO.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetSegmentInfo(int hFile, int dwEntityID, ref ns_SEGMENTINFO pdwSegmentInfo, int dwSegmentInfoSize);
        /// <summary>
        /// Retrieves information about the source entity, dwSourceID, for the Segment Entity identified
        /// by dwEntityID, from the file referenced by the handle hFile. The information is written to
        /// the ns_SEGSOURCEINFO structure pointed to by pSourceInfo. The size in bytes allocated
        /// for ns_SEGSOURCEINFO is specified by dwSourceInfoSize.
        /// 
        /// The value of dwSourceID is an integer index value ranging from 0 to dwSourceCount -1
        /// (which is returned by the function ns_GetSegmentInfo).
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the Segment Entity.</param>
        /// <param name="dwSourceID">Identification number of the Segment Entity source.</param>
        /// <param name="pSourceInfo">Pointer to a ns_SEGSOURCEINFO structure that receives information about the source.</param>
        /// <param name="dwSourceInfoSize">Allocated size in bytes for ns_SEGSOURCEINFO structure.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_BADSOURCE - Invalid source identifier specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetSegmentSourceInfo(int hFile, int dwEntityID, int dwSourceID, ref ns_SEGSOURCEINFO pSourceInfo, int dwSourceInfoSize);
        /// <summary>
        /// Returns the Segment data values in entry nIndex of the entity dwEntityID from the file
        /// referenced by hFile. The data values are returned in the buffer pointed to by pData. The
        /// size in bytes allocated to the data buffer is specified by dwDataBufferSize. The timestamp of
        /// the entry is returned at pdTimeStamp. The actual number of samples written to the data
        /// buffer is returned at pdwSampleCount.
        /// 
        /// The data buffer should be accessed as a 2-dimensional array for samples and sources.
        /// In C, the array is declared as double data[sourcecount][maxsamplecount];
        /// and the values are referenced by data[source][sample]
        /// With pointers, the reference is *(pData+(maxsamplecount*sourcecount)+ sample)
        /// 
        /// The pdwUnitID field is a bit-field supporting multiple classification codes. A zero unit ID is
        /// unclassified, bit 0 is set if the segment is noise or an artifact, bit 1 indicates unit 1 is present,
        /// bit 2 indicates that unit 2 is present, etc.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="dwIndex">The index number of the requested Segment data item.</param>
        /// <param name="pdTimeStamp">Pointer to the time stamp of the requested Segment data item.</param>
        /// <param name="pData">Pointer to the buffer that is to receive the requested data.</param>
        /// <param name="dwDataBufferSize">Size in bytes allocated to the data buffer pointed to by pData.</param>
        /// <param name="pdwSampleCount">Pointer to the number of samples returned in the data buffer.</param>
        /// <param name="pdwUnitID">Pointer to the unit classification code for the Segment Entity.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_BADINDEX - Invalid entity index specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetSegmentData(int hFile, int dwEntityID, int dwIndex, ref double pdTimeStamp, double[,] pData, int dwDataBufferSize, ref int pdwSampleCount, ref int pdwUnitID);

        /// <summary>
        /// Retrieves information on Neural Event entity dwEntityID from the file referenced by hFile.
        /// The information is returned in the structure ns_NEURALINFO at the address pnNeuralInfo
        /// The memory allocated in bytes for the structure ns_NEURALINFO is given by dwNeuralInfoSize.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="pNeuralInfo">Pointer to the ns_NEURALINFO structure to receive the Neural Event information.</param>
        /// <param name="dwNeuralInfoSize">Allocated size in bytes for ns_NEURALINFO structure.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetNeuralInfo(int hFile, int dwEntityID, ref ns_NEURALINFO pNeuralInfo, int dwNeuralInfoSize);
        /// <summary>
        /// Returns an array of timestamps for the neural events of the entity specified by dwEntityID
        /// and referenced by the file handle hFile. The index of the first timestamp is nStartIndex and
        /// the requested number of timestamps is given by dwIndexCount. The timestamps are returned
        /// in the buffer pointed to by pData.
        /// </summary>
        /// <param name="hFile">Handle to an open file.</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="dwStartIndex">First index number of the requested Neural Events timestamp.</param>
        /// <param name="dwIndexCount">Number of timestamps to retrieve.</param>
        /// <param name="pData">Pointer to an array of double precision timestamps. The user
        /// application must allocate sufficient space (dwIndexCount*sizeof(double) bytes) to hold the requested data.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_BADINDEX - Invalid entity index specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetNeuralData(int hFile, int dwEntityID, int dwStartIndex, int dwIndexCount, double[] pData);

        /// <summary>
        /// Searches in the file referenced by hFile for the data item identified by the index dwEntityID.
        /// The flag specifies whether to locate the data item that starts before or after the time dTime.
        /// The index of the requested data item is returned at pdwIndex.
        /// </summary>
        /// <param name="hFile">Handle to an open file</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="dTime">Time of the data to search for</param>
        /// <param name="nFlag">ns_INDEXFLAG specifying whether the index to be retrieved belongs to the data item occurring before or after the specified time dTime.</param>
        /// <param name="pdwIndex">Pointer to variable to receive the entry index.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_FILEERROR - File access or read error
        /// ns_BADINDEX - Unable to find an valid index given the search parameters
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetIndexByTime(int hFile, int dwEntityID, double dTime, int nFlag, ref int pdwIndex);
        /// <summary>
        /// Retrieves the timestamp for the entity identified by dwEntityID and numbered dwIndex, from
        /// the data file referenced by hFile. The timestamp is returned at pdTime.
        /// </summary>
        /// <param name="hFile">Handle to an open file</param>
        /// <param name="dwEntityID">Identification number of the entity in the data file.</param>
        /// <param name="dwIndex">Index of the requested data.</param>
        /// <param name="pdTime">Pointer to the variable to receive the timestamp.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_BADFILE - Invalid file handle passed to function
        /// ns_BADENTITY - Invalid or inappropriate entity identifier specified
        /// ns_BADINDEX - Invalid entity index specified
        /// ns_FILEERROR - File access or read error
        /// ns_LIBERROR - Library Error</returns>
        public delegate ns_RETURN ns_pGetTimeByIndex(int hFile, int dwEntityID, int dwIndex, ref double pdTime);

        /// <summary>
        /// Returns the last error message in formatted text form to the buffer pointed to by
        /// pszMsgBuffer. This function should be called immediately following a function whose
        /// return value indicates that such a call will return useful data. Otherwise, the error set by
        /// the failed function may be wiped out by more recent function calls. dwMsgBufferSize
        /// specifies the size in bytes allocated to receive the text message.
        /// The maximum size of the error message text is 256 characters.
        /// </summary>
        /// <param name="pszMsgBuffer">Pointer to buffer to receive the text error message.</param>
        /// <param name="dwMsgBufferSize">Allocated size in bytes for the error message buffer.</param>
        /// <returns>This function returns ns_OK if the information is successfully retrieved. Otherwise one of the following error codes is generated:
        /// ns_LIBERROR - Library error</returns>
        public delegate ns_RETURN ns_pGetLastErrorMsg(byte[] pszMsgBuffer, int dwMsgBufferSize);

        #endregion

        #region NeuroShare API Delegate

        /// <summary>
        /// Obtains information about the API library.
        /// </summary>
        public ns_pGetLibraryInfo ns_GetLibraryInfo;

        /// <summary>
        /// Opens the file specified by pszFilename and returns a file handle, 
        /// hFile that is used to access the opened file.
        /// </summary>
        public ns_pOpenFile ns_OpenFile;
        /// <summary>
        /// Provides general information about the data file referenced by hFile. This information is
        /// returned in the structure pointed to by pFileInfo. The number of bytes allocated for the file
        /// information structure is given by dwFileInfoSize.
        /// </summary>
        public ns_pGetFileInfo ns_GetFileInfo;
        /// <summary>
        /// Closes a previously opened file specified by the file handle hFile.
        /// </summary>
        public ns_pCloseFile ns_CloseFile;

        /// <summary>
        /// Retrieves general information about the entity, dwEntityID, from the file referenced by the
        /// file handle hFile. The information is passed in the structure pointed to by pEntityInfo. The
        /// number of bytes allocated for ns_ENTITYINFO is specified by dwEntityInfoSize.
        /// </summary>
        public ns_pGetEntityInfo ns_GetEntityInfo;

        /// <summary>
        /// Retrieves information from the file referenced by hFile about the Event Entity, dwEntityID,
        /// in the structure pointed to by pEventsInfo. The structure has an allocated size of dwEventInfoSize bytes.
        /// </summary>
        public ns_pGetEventInfo ns_GetEventInfo;
        /// <summary>
        /// Returns the data values from the file referenced by hFile and the Event Entity dwEntityID.
        /// The Event data entry specified by dwIndex is written to pData and the timestamp of the entry
        /// is returned to pdTimeStamp. dwDataBufferSize specifies the size in bytes allocated to the
        /// buffer at pData. The pointer pdwDataRetSize gives the actual number of bytes of data retrieved to the buffer.
        /// </summary>
        public ns_pGetEventData ns_GetEventData;

        /// <summary>
        /// Returns information about the Analog Entity associated with dwEntityID and the file hFile.
        /// The information is stored in a ns_ANALOGINFO structure, pointed to by
        /// pAnalogSourceInfo. The size in bytes allocated for ns_ANALOGINFO is specified by dwAnalogInfoSize.
        /// </summary>
        public ns_pGetAnalogInfo ns_GetAnalogInfo;
        /// <summary>
        /// Returns the data values associated with the Analog Entity indexed dwEntityID in the file
        /// referenced by hFile. The index of the first data value is dwStartIndex and the requested
        /// number of data samples is given by dwIndexCount. The requested data values are returned in
        /// the buffer pointed to by pData.
        /// Although the samples in an analog entity are indexed, they are not guaranteed to be
        /// continuous in time and may contain gaps between some of the indexes. When the requested
        /// data is returned, pdwContCount contains the number of Analog items, starting from
        /// dwStartIndex, which do not contain a time gap.
        /// If the index range specified by dwStartIndex to dwStartIndex+dwIndexCount contains
        /// invalid indexes, the function will return ns_BADINDEX.
        /// </summary>
        public ns_pGetAnalogData ns_GetAnalogData;

        /// <summary>
        /// Retrieves information on the Segment Entity, dwEntityID, in the file referenced by the handle
        /// hFile. The information is written to the ns_SEGMENTINFO structure at pdwSegmentInfo.
        /// The size of memory in bytes allocated for the ns_SEGMENTINFO structure is specified by dwSegmentInfoSize.
        /// </summary>
        public ns_pGetSegmentInfo ns_GetSegmentInfo;
        /// <summary>
        /// Retrieves information about the source entity, dwSourceID, for the Segment Entity identified
        /// by dwEntityID, from the file referenced by the handle hFile. The information is written to
        /// the ns_SEGSOURCEINFO structure pointed to by pSourceInfo. The size in bytes allocated
        /// for ns_SEGSOURCEINFO is specified by dwSourceInfoSize.
        /// </summary>
        public ns_pGetSegmentSourceInfo ns_GetSegmentSourceInfo;
        /// <summary>
        /// Returns the Segment data values in entry nIndex of the entity dwEntityID from the file
        /// referenced by hFile. The data values are returned in the buffer pointed to by pData. The
        /// size in bytes allocated to the data buffer is specified by dwDataBufferSize. The timestamp of
        /// the entry is returned at pdTimeStamp. The actual number of samples written to the data
        /// buffer is returned at pdwSampleCount.
        /// </summary>
        public ns_pGetSegmentData ns_GetSegmentData;

        /// <summary>
        /// Retrieves information on Neural Event entity dwEntityID from the file referenced by hFile.
        /// The information is returned in the structure ns_NEURALINFO at the address pnNeuralInfo
        /// The memory allocated in bytes for the structure ns_NEURALINFO is given by dwNeuralInfoSize.
        /// </summary>
        public ns_pGetNeuralInfo ns_GetNeuralInfo;
        /// <summary>
        /// Returns an array of timestamps for the neural events of the entity specified by dwEntityID
        /// and referenced by the file handle hFile. The index of the first timestamp is nStartIndex and
        /// the requested number of timestamps is given by dwIndexCount. The timestamps are returned
        /// in the buffer pointed to by pData.
        /// </summary>
        public ns_pGetNeuralData ns_GetNeuralData;

        /// <summary>
        /// Searches in the file referenced by hFile for the data item identified by the index dwEntityID.
        /// The flag specifies whether to locate the data item that starts before or after the time dTime.
        /// The index of the requested data item is returned at pdwIndex.
        /// </summary>
        public ns_pGetIndexByTime ns_GetIndexByTime;
        /// <summary>
        /// Retrieves the timestamp for the entity identified by dwEntityID and numbered dwIndex, from
        /// the data file referenced by hFile. The timestamp is returned at pdTime.
        /// </summary>
        public ns_pGetTimeByIndex ns_GetTimeByIndex;

        /// <summary>
        /// Returns the last error message in formatted text form to the buffer pointed to by
        /// pszMsgBuffer. This function should be called immediately following a function whose
        /// return value indicates that such a call will return useful data. Otherwise, the error set by
        /// the failed function may be wiped out by more recent function calls. dwMsgBufferSize
        /// specifies the size in bytes allocated to receive the text message.
        /// The maximum size of the error message text is 256 characters.
        /// </summary>
        public ns_pGetLastErrorMsg ns_GetLastErrorMsg;

        #endregion


        #region DLL Dynamic Loading API

        /// <summary>
        /// Dynamicly Load DLL Library
        /// </summary>
        /// <param name="lpLibFileName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPTStr)]string lpLibFileName);

        /// <summary>
        /// Gets the Function Pointer of a Loaded DLL Library
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="lpProcName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)]string lpProcName);

        /// <summary>
        /// Free the Loaded DLL Library
        /// </summary>
        /// <param name="hModule"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool FreeLibrary(int hModule);

        #endregion

        #region Load/Unload NeuroShare API

        /// <summary>
        /// Load Vendor Specific NeuroShare Library
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool LoadnsAPI(Vendor v)
        {
            try
            {
                switch (v)
                {
                    case Vendor.AlphaOmega:
                        hModule = LoadLibrary("nsAOLibrary.dll");
                        break;
                    case Vendor.BlackRock:
                        hModule = LoadLibrary("nsNEVLibrary.dll");
                        break;
                    case Vendor.CED:
                        hModule = LoadLibrary("NSCEDSON.dll");
                        break;
                    case Vendor.Plexon:
                        hModule = LoadLibrary("nsPlxLibrary.dll");
                        break;
                    case Vendor.TDT:
                        hModule = LoadLibrary("nsTDTLib.dll");
                        break;
                    case Vendor.Unknown:
                        hModule = LoadLibrary("nsNSNLibrary.dll");
                        break;
                }

                IntPtr intPtr = GetProcAddress(hModule, "ns_GetLibraryInfo");
                ns_GetLibraryInfo = (ns_pGetLibraryInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetLibraryInfo));

                intPtr = GetProcAddress(hModule, "ns_OpenFile");
                ns_OpenFile = (ns_pOpenFile)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pOpenFile));
                intPtr = GetProcAddress(hModule, "ns_GetFileInfo");
                ns_GetFileInfo = (ns_pGetFileInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetFileInfo));
                intPtr = GetProcAddress(hModule, "ns_CloseFile");
                ns_CloseFile = (ns_pCloseFile)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pCloseFile));

                intPtr = GetProcAddress(hModule, "ns_GetEntityInfo");
                ns_GetEntityInfo = (ns_pGetEntityInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetEntityInfo));

                intPtr = GetProcAddress(hModule, "ns_GetEventInfo");
                ns_GetEventInfo = (ns_pGetEventInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetEventInfo));
                intPtr = GetProcAddress(hModule, "ns_GetEventData");
                ns_GetEventData = (ns_pGetEventData)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetEventData));

                intPtr = GetProcAddress(hModule, "ns_GetAnalogInfo");
                ns_GetAnalogInfo = (ns_pGetAnalogInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetAnalogInfo));
                intPtr = GetProcAddress(hModule, "ns_GetAnalogData");
                ns_GetAnalogData = (ns_pGetAnalogData)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetAnalogData));

                intPtr = GetProcAddress(hModule, "ns_GetSegmentInfo");
                ns_GetSegmentInfo = (ns_pGetSegmentInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetSegmentInfo));
                intPtr = GetProcAddress(hModule, "ns_GetSegmentSourceInfo");
                ns_GetSegmentSourceInfo = (ns_pGetSegmentSourceInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetSegmentSourceInfo));
                intPtr = GetProcAddress(hModule, "ns_GetSegmentData");
                ns_GetSegmentData = (ns_pGetSegmentData)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetSegmentData));

                intPtr = GetProcAddress(hModule, "ns_GetNeuralInfo");
                ns_GetNeuralInfo = (ns_pGetNeuralInfo)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetNeuralInfo));
                intPtr = GetProcAddress(hModule, "ns_GetNeuralData");
                ns_GetNeuralData = (ns_pGetNeuralData)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetNeuralData));

                intPtr = GetProcAddress(hModule, "ns_GetIndexByTime");
                ns_GetIndexByTime = (ns_pGetIndexByTime)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetIndexByTime));
                intPtr = GetProcAddress(hModule, "ns_GetTimeByIndex");
                ns_GetTimeByIndex = (ns_pGetTimeByIndex)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetTimeByIndex));

                intPtr = GetProcAddress(hModule, "ns_GetLastErrorMsg");
                ns_GetLastErrorMsg = (ns_pGetLastErrorMsg)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(ns_pGetLastErrorMsg));

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// UnLoad Current NeuroShare Library
        /// </summary>
        /// <returns></returns>
        public bool UnloadnsAPI()
        {
            if (hModule != 0)
            {
                return FreeLibrary(hModule);
            }
            return true;
        }

        #endregion


        #region Device

        /// <summary>
        /// Connect to Hardware Device
        /// </summary>
        /// <returns></returns>
        public virtual bool ConnectDevice()
        {
            return false;
        }

        /// <summary>
        /// Disconnect from Hardware Device
        /// </summary>
        public virtual bool DisconnectDevice()
        {
            return false;
        }

        /// <summary>
        /// Gets/Sets Hardware Device State
        /// </summary>
        public virtual DeviceState DeviceState { get; set; }

        /// <summary>
        /// Gets Current Hardware Device Vendor
        /// </summary>
        public virtual Vendor Vendor
        {
            get { return Vendor.Unknown; }
        }

        #endregion

        #region Signal

        /// <summary>
        /// Connect to Signal Source
        /// </summary>
        /// <returns></returns>
        public virtual bool ConnectSignal()
        {
            return false;
        }

        /// <summary>
        /// Disconnect from Signal Source
        /// </summary>
        public virtual bool DisconnectSignal()
        {
            return false;
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Release both managed and unmanaged resources
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Release both(true) and unmanaged(false) resources
        /// </summary>
        /// <param name="dispose"></param>
        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
            }
            if (!UnloadnsAPI())
            {
                MessageBox.Show("Unload NeuroShare Library Failed !", "FreeLibrary Failed !");
            }
            DisconnectSignal();
            DisconnectDevice();
        }

        #endregion

    }

}
