##############################################
# File: nsBasic.py
#
# NeuSys Basic NeuroShare Analysis Script Using sim100.nev.
#
# Copyright (c) 2009-06-09 Zhang Li
##############################################

import clr
from System import BitConverter, Double, Array, Byte
from System.Text import Encoding
from nSignal import *
from System.Runtime.InteropServices import Marshal


# NeuroShare Get Library Info
li = ns_LIBRARYINFO()
ref_li = clr.Reference[ns_LIBRARYINFO](li)
hr_1i = device.ns_GetLibraryInfo(ref_li, Marshal.SizeOf(li))
libinfo = ref_li.Value

# NeuroShare Open Data File
hf = clr.Reference[int](0)
hr_hf = device.ns_OpenFile(nsfile,hf)
nsfilehandle = hf.Value

# NeuroShare Get File Info
fi = ns_FILEINFO()
ref_fi = clr.Reference[ns_FILEINFO](fi)
hr_fi = device.ns_GetFileInfo(nsfilehandle, ref_fi, Marshal.SizeOf(fi))
fileinfo = ref_fi.Value

# NeuroShare Get Entity Info
ei = ns_ENTITYINFO()
ref_ei = clr.Reference[ns_ENTITYINFO](ei)
hr_ei = device.ns_GetEntityInfo(nsfilehandle, 0, ref_ei, Marshal.SizeOf(ei))
entityinfo = ref_ei.Value

# NeuroShare Get Event Info
evi = ns_EVENTINFO()
ref_evi = clr.Reference[ns_EVENTINFO](evi)
hr_evi = device.ns_GetEventInfo(nsfilehandle, 106, ref_evi, Marshal.SizeOf(evi))
eventinfo = ref_evi.Value

# NeuroShare Get Event Data
ref_t = clr.Reference[Double](0.0)
ref_edata = Array[Byte](range(eventinfo.dwMaxDataLength))
ref_dsize = clr.Reference[int](0)
hr_ed = device.ns_GetEventData(nsfilehandle, 106, 13, ref_t, ref_edata, eventinfo.dwMaxDataLength, ref_dsize)
timestamp = ref_t.Value
datasize = ref_dsize.Value
eventdata = BitConverter.ToInt16(ref_edata, 0)

# NeuroShare Get Analog Info
ai = ns_ANALOGINFO()
ref_ai = clr.Reference[ns_ANALOGINFO](ai)
hr_ai = device.ns_GetAnalogInfo(nsfilehandle, 101, ref_ai, Marshal.SizeOf(ai))
analoginfo = ref_ai.Value

# NeuroShare Get Analog Data
ref_adata = Array[Double](range(10))
hr_ad = device.ns_GetAnalogData(nsfilehandle, 101, 0, 10, ref_dsize, ref_adata)

# NeuroShare Get Segment Info
si = ns_SEGMENTINFO()
ref_si = clr.Reference[ns_SEGMENTINFO](si)
hr_si = device.ns_GetSegmentInfo(nsfilehandle, 0, ref_si, Marshal.SizeOf(si))
segmentinfo = ref_si.Value

# NeuroShare Get SegmentSource Info
ssi = ns_SEGSOURCEINFO()
ref_ssi = clr.Reference[ns_SEGSOURCEINFO](ssi)
hr_ssi = device.ns_GetSegmentSourceInfo(nsfilehandle, 0, 0, ref_ssi, Marshal.SizeOf(ssi))
segmentsourceinfo = ref_ssi.Value

# NeuroShare Get Segment Data
ref_sdata = Array.CreateInstance(Double,1,48)
ref_unit = clr.Reference[int](0)
hr_sd = device.ns_GetSegmentData(nsfilehandle,0,0,ref_t,ref_sdata,Marshal.SizeOf(Double)*ref_sdata.Length,ref_dsize, ref_unit)

# NeuroShare Get Neural Info
ni = ns_NEURALINFO()
ref_ni = clr.Reference[ns_NEURALINFO](ni)
hr_ni = device.ns_GetNeuralInfo(nsfilehandle,0,ref_ni, Marshal.SizeOf(ni))
neuralinfo = ref_ni.Value

# NeuroShare Get Neural Data
hr_nd = device.ns_GetNeuralData(nsfilehandle,0,0,10,ref_adata)

# NeuroShare Get Last Error
ref_m = Array[Byte](range(256))
hr_le = device.ns_GetLastErrorMsg(ref_m, 256)
lasterrormsg = Encoding.Default.GetString(ref_m)

# NeuroShare Close Data File
hr_close = device.ns_CloseFile(nsfilehandle)
