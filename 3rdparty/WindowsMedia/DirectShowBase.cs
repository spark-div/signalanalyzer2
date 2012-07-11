///////////////////////////////////////////////////////////////////////////////
//  Copyright (c) 2008 Ernest Laurentin (http://www.ernzo.com)
//
// This software is provided 'as-is', without any express or implied
// warranty. In no event will the authors be held liable for any damages
// arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not
// claim that you wrote the original software. If you use this software
// in a product, an acknowledgment in the product documentation would be
// appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such, and must not be
// misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace Ernzo.Windows.DirectShowLib
{
    // We can remove Quartz class altogether by declaring all the error codes
    // TODO: http://msdn.microsoft.com/en-us/library/ms783645(VS.85).aspx
    //       ms-help://MS.LHSMSSDK.1033/MS.LHSWinSDK.1033/directshow/htm/errorandsuccesscodes.htm

    public sealed class Quartz
    {
        private Quartz()
        {
            // nothing to construct
        }

        [DllImportAttribute("Quartz.dll", EntryPoint = "AMGetErrorTextA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int AMGetErrorTextA(int mmrError, [MarshalAsAttribute(UnmanagedType.LPStr)] StringBuilder pszText, int cchText);

        [DllImportAttribute("Quartz.dll", EntryPoint = "AMGetErrorTextW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int AMGetErrorTextW(int mmrError, [MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder pszText, int cchText);

    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AMMediaType
    {
        public Guid majortype;
        public Guid subtype;
        public int bFixedSizeSamples;
        public int bTemporalCompression;
        public int lSampleSize;
        public Guid formattype;
        [MarshalAs(UnmanagedType.IUnknown)]
        public object pUnk;
        public int cbFormat;
        public IntPtr pbFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AllocatorProperties
    {
        public int cBuffers;
        public int cbBuffer;
        public int cbAlign;
        public int cbPrefix;
    }

    public enum FilterState
    {
        State_Stopped = 0,
        State_Paused,
        State_Running
    }

    public enum PinDirection
    {
        PINDIR_INPUT,
        PINDIR_OUTPUT
    }

    [Flags]
    public enum AM_SEEKING_SeekingCapabilities
    {
        AM_SEEKING_CanSeekAbsolute  = 1,
        AM_SEEKING_CanSeekForwards  = 2,
        AM_SEEKING_CanSeekBackwards = 4,
        AM_SEEKING_CanGetCurrentPos = 8,
        AM_SEEKING_CanGetStopPos    = 0x10,
        AM_SEEKING_CanGetDuration   = 0x20,
        AM_SEEKING_CanPlayBackwards = 0x40,
        AM_SEEKING_CanDoSegments    = 0x80,
        AM_SEEKING_Source           = 0x100
    }

    [Flags]
    public enum AM_SEEKING_SeekingFlags
    {
        AM_SEEKING_NoPositioning        = 0,
        AM_SEEKING_AbsolutePositioning  = 1,
        AM_SEEKING_RelativePositioning  = 2,
        AM_SEEKING_IncrementalPositioning = 3,
        AM_SEEKING_PositioningBitsMask  = 3,
        AM_SEEKING_SeekToKeyFrame       = 0x04,
        AM_SEEKING_ReturnTime           = 0x08,
        AM_SEEKING_Segment              = 0x10,
        AM_SEEKING_NoFlush              = 0x20
    }

    //  block flags
    enum AM_PIN_FLOW_CONTROL_BLOCK_FLAGS
    {
        AM_PIN_FLOW_CONTROL_UNBLOCK = 0x00000000, //  0 means unblock
        AM_PIN_FLOW_CONTROL_BLOCK   = 0x00000001
    }

    //  IGraphConfig::Reconnect flags
    public enum AM_GRAPH_CONFIG_RECONNECT_FLAGS
    {
        AM_GRAPH_CONFIG_RECONNECT_NONE = 0x00000000,
        AM_GRAPH_CONFIG_RECONNECT_DIRECTCONNECT = 0x00000001,
        AM_GRAPH_CONFIG_RECONNECT_CACHE_REMOVED_FILTERS = 0x00000002,
        AM_GRAPH_CONFIG_RECONNECT_USE_ONLY_CACHED_FILTERS = 0x00000004
    }

    // IGraphConfig::SetFilterFlags flags
    public enum AM_FILTER_FLAGS
    {
        AM_FILTER_FLAGS_NONE      = 0x00000000,
        AM_FILTER_FLAGS_REMOVABLE = 0x00000001
    }

    // IGraphConfig::RemoveFilterEx flags
    public enum REM_FILTER_FLAGS
    {
        REMFILTERF_NONE = 0x00000000,
        REMFILTERF_LEAVECONNECTED = 0x00000001
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct FilterInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public short[] achName;
        [MarshalAs(UnmanagedType.Interface)]
        public IFilterGraph pGraph;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PinInfo
    {
        [MarshalAs(UnmanagedType.Interface)]
        public IBaseFilter pFilter;
        public PinDirection dir;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public short[] achName;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tagBITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tagBITMAPINFO
    {
        public tagBITMAPINFOHEADER bmiHeader;
        public IntPtr bmiColors;    // tagRGBQUAD[1] - Need to alloc array
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tagRGBQUAD
    {
        public byte rgbBlue;
        public byte rgbGreen;
        public byte rgbRed;
        public byte rgbReserved;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tagRECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tagVIDEOINFOHEADER2
    {
        public tagRECT rcSource;
        public tagRECT rcTarget;
        public int dwBitRate;
        public int dwBitErrorRate;
        public long AvgTimePerFrame;
        public int dwInterlaceFlags;
        public int dwCopyProtectFlags;
        public int dwPictAspectRatioX;
        public int dwPictAspectRatioY;
        public int dwControlFlags;
        public int dwReserved2;
        public tagBITMAPINFOHEADER bmiHeader;
    }

    [ComImport, Guid("56A8689F-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph
    {
        [PreserveSig]
        int AddFilter([In, MarshalAs(UnmanagedType.Interface)] IBaseFilter pFilter, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        [PreserveSig]
        int RemoveFilter([In, MarshalAs(UnmanagedType.Interface)] IBaseFilter pFilter);

        [PreserveSig]
        int EnumFilters([MarshalAs(UnmanagedType.Interface)] out IEnumFilters ppEnum);

        [PreserveSig]
        int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName, [MarshalAs(UnmanagedType.Interface)] out IBaseFilter ppFilter);

        [PreserveSig]
        int ConnectDirect([In, MarshalAs(UnmanagedType.Interface)] IPin ppinOut, [In, MarshalAs(UnmanagedType.Interface)] IPin ppinIn, [In] ref AMMediaType pmt);

        [PreserveSig]
        int Reconnect([In, MarshalAs(UnmanagedType.Interface)] IPin pPin);

        [PreserveSig]
        int Disconnect([In, MarshalAs(UnmanagedType.Interface)] IPin pPin);

        [PreserveSig]
        int SetDefaultSyncSource();
    }

    [ComImport, Guid("56A868A9-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphBuilder : IFilterGraph
    {
        [PreserveSig]
        new int AddFilter([In, MarshalAs(UnmanagedType.Interface)] IBaseFilter pFilter, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);
        
        [PreserveSig]
        new int RemoveFilter([In, MarshalAs(UnmanagedType.Interface)] IBaseFilter pFilter);
        
        [PreserveSig]
        new int EnumFilters([MarshalAs(UnmanagedType.Interface)] out IEnumFilters ppEnum);
        
        [PreserveSig]
        new int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName, [MarshalAs(UnmanagedType.Interface)] out IBaseFilter ppFilter);
        
        [PreserveSig]
        new int ConnectDirect([In, MarshalAs(UnmanagedType.Interface)] IPin ppinOut, [In, MarshalAs(UnmanagedType.Interface)] IPin ppinIn, [In] ref AMMediaType pmt);
        
        [PreserveSig]
        new int Reconnect([In, MarshalAs(UnmanagedType.Interface)] IPin pPin);
        
        [PreserveSig]
        new int Disconnect([In, MarshalAs(UnmanagedType.Interface)] IPin pPin);
        
        [PreserveSig]
        new int SetDefaultSyncSource();
        
        [PreserveSig]
        int Connect([In, MarshalAs(UnmanagedType.Interface)] IPin ppinOut, [In, MarshalAs(UnmanagedType.Interface)] IPin ppinIn);
        
        [PreserveSig]
        int Render([In, MarshalAs(UnmanagedType.Interface)] IPin ppinOut);
        
        [PreserveSig]
        int RenderFile([In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFile, [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrPlayList);
        
        [PreserveSig]
        int AddSourceFilter([In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFileName, [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName, [MarshalAs(UnmanagedType.Interface)] out IBaseFilter ppFilter);
        
        [PreserveSig]
        int SetLogFile([In] IntPtr hFile);
        
        [PreserveSig]
        int Abort();
        
        [PreserveSig]
        int ShouldOperationContinue();
    }

    [ComImport, Guid("03A1EB8E-32BF-4245-8502-114D08A9CB88"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphConfig
    {
        [PreserveSig]
        int Reconnect([In] IPin pOutputPin, [In] IPin pInputPin, [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmtFirstConnection, [In] IBaseFilter pUsingFilter, [In] IntPtr hAbortEvent, [In] int dwFlags);
        
        [PreserveSig]
        int Reconfigure([In] IGraphConfigCallback pCallback, [In] IntPtr pvContext, [In] int dwFlags, [In] IntPtr hAbortEvent);
        
        [PreserveSig]
        int AddFilterToCache([In] IBaseFilter pFilter);
        
        [PreserveSig]
        int EnumCacheFilter(out IEnumFilters pEnum);
        
        [PreserveSig]
        int RemoveFilterFromCache([In] IBaseFilter pFilter);
        
        [PreserveSig]
        int GetStartTime(out long prtStart);
        
        [PreserveSig]
        int PushThroughData([In] IPin pOutputPin, [In] IPinConnection pConnection, [In] IntPtr hEventAbort);
        
        [PreserveSig]
        int SetFilterFlags([In] IBaseFilter pFilter, [In] int dwFlags);
        
        [PreserveSig]
        int GetFilterFlags([In] IBaseFilter pFilter, out int pdwFlags);
        
        [PreserveSig]
        int RemoveFilterEx([In] IBaseFilter pFilter, int Flags);
    }

    [ComImport, Guid("ADE0FD60-D19D-11D2-ABF6-00A0C905F375"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphConfigCallback
    {
        [PreserveSig]
        int Reconfigure(IntPtr pvContext, int dwFlags);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("56A868AB-0AD4-11CE-B03A-0020AF0BA770")]
    public interface IGraphVersion
    {
        [PreserveSig]
        int QueryVersion(out int pVersion);
    }

    [ComImport, Guid("0000010C-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersist
    {
        [PreserveSig]
        int GetClassID(out Guid pClassID);
    }

    [ComImport, Guid("56A86899-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaFilter : IPersist
    {
        [PreserveSig]
        new int GetClassID(out Guid pClassID);
        
        [PreserveSig]
        int Stop();
        
        [PreserveSig]
        int Pause();
        
        [PreserveSig]
        int Run(long tStart);
        
        [PreserveSig]
        int GetState([In] int dwMilliSecsTimeout, out FilterState State);
        
        [PreserveSig]
        int SetSyncSource([In, MarshalAs(UnmanagedType.Interface)] IReferenceClock pClock);
        
        [PreserveSig]
        int GetSyncSource([MarshalAs(UnmanagedType.Interface)] out IReferenceClock pClock);
    }

    [ComImport, Guid("56A86895-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseFilter : IMediaFilter
    {
        [PreserveSig]
        new int GetClassID(out Guid pClassID);
        
        [PreserveSig]
        new int Stop();
        
        [PreserveSig]
        new int Pause();
        
        [PreserveSig]
        new int Run(long tStart);
        
        [PreserveSig]
        new int GetState([In] int dwMilliSecsTimeout, out FilterState State);
        
        [PreserveSig]
        new int SetSyncSource([In, MarshalAs(UnmanagedType.Interface)] IReferenceClock pClock);
        
        [PreserveSig]
        new int GetSyncSource([MarshalAs(UnmanagedType.Interface)] out IReferenceClock pClock);
        
        [PreserveSig]
        int EnumPins([MarshalAs(UnmanagedType.Interface)] out IEnumPins ppEnum);
        
        [PreserveSig]
        int FindPin([In, MarshalAs(UnmanagedType.LPWStr)] string Id, [MarshalAs(UnmanagedType.Interface)] out IPin ppPin);
        
        [PreserveSig]
        int QueryFilterInfo(out FilterInfo pInfo);
        
        [PreserveSig]
        int JoinFilterGraph([In, MarshalAs(UnmanagedType.Interface)] IFilterGraph pGraph, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);
        
        [PreserveSig]
        int QueryVendorInfo([MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo);
    }

    [ComImport, Guid("56A8689A-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample
    {
        [PreserveSig]
        int GetPointer(out IntPtr ppBuffer);
        
        [PreserveSig]
        int GetSize();
        
        [PreserveSig]
        int GetTime(out long pTimeStart, out long pTimeEnd);
        
        [PreserveSig]
        int SetTime(ref long pTimeStart, ref long pTimeEnd);
        
        [PreserveSig]
        int IsSyncPoint();
        
        [PreserveSig]
        int SetSyncPoint([In, MarshalAs(UnmanagedType.Bool)] bool bIsSyncPoint);
        
        [PreserveSig]
        int IsPreroll();
        
        [PreserveSig]
        int SetPreroll([In, MarshalAs(UnmanagedType.Bool)] bool bIsPreroll);
        
        [PreserveSig]
        int GetActualDataLength();
        
        [PreserveSig]
        int SetActualDataLength([In] int len);
        
        [PreserveSig]
        int GetMediaType(out AMMediaType ppMediaType);
        
        [PreserveSig]
        int SetMediaType(AMMediaType pMediaType);
        
        [PreserveSig]
        int IsDiscontinuity();
        
        [PreserveSig]
        int SetDiscontinuity([In, MarshalAs(UnmanagedType.Bool)] bool bDiscontinuity);
        
        [PreserveSig]
        int GetMediaTime(out long pTimeStart, out long pTimeEnd);
        
        [PreserveSig]
        int SetMediaTime(ref long pTimeStart, ref long pTimeEnd);
    }

    [ComImport, Guid("36B73884-C2C8-11CF-8B46-00805F6CEF60"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample2 : IMediaSample
    {
        [PreserveSig]
        new int GetPointer(out IntPtr ppBuffer);
        
        [PreserveSig]
        new int GetSize();
        
        [PreserveSig]
        new int GetTime(out long pTimeStart, out long pTimeEnd);
        
        [PreserveSig]
        new int SetTime(ref long pTimeStart, ref long pTimeEnd);
        
        [PreserveSig]
        new int IsSyncPoint();
        
        [PreserveSig]
        new int SetSyncPoint([In, MarshalAs(UnmanagedType.Bool)] bool bIsSyncPoint);
        
        [PreserveSig]
        new int IsPreroll();
        
        [PreserveSig]
        new int SetPreroll([In, MarshalAs(UnmanagedType.Bool)] bool bIsPreroll);
        
        [PreserveSig]
        new int GetActualDataLength();
        
        [PreserveSig]
        new int SetActualDataLength([In] int len);
        
        [PreserveSig]
        new int GetMediaType(out AMMediaType ppMediaType);
        
        [PreserveSig]
        new int SetMediaType(AMMediaType pMediaType);
        
        [PreserveSig]
        new int IsDiscontinuity();
        
        [PreserveSig]
        new int SetDiscontinuity([In, MarshalAs(UnmanagedType.Bool)] bool bDiscontinuity);
        
        [PreserveSig]
        new int GetMediaTime(out long pTimeStart, out long pTimeEnd);
        
        [PreserveSig]
        new int SetMediaTime(ref long pTimeStart, ref long pTimeEnd);
        
        [PreserveSig]
        int GetProperties([In] int cbProperties, [In] IntPtr pbProperties);
        
        [PreserveSig]
        int SetProperties([In] int cbProperties, [In] IntPtr pbProperties);
    }

    [ComImport, Guid("68961E68-832B-41EA-BC91-63593F3E70E3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample2Config
    {
        [PreserveSig]
        int GetSurface([MarshalAs(UnmanagedType.IUnknown)] out object ppDirect3DSurface9);
    }

    [ComImport, Guid("36b73880-c2c8-11cf-8b46-00805f6cef60"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSeeking
    {
        [PreserveSig]
        int GetCapabilities(out AM_SEEKING_SeekingCapabilities pCapabilities);
        
        [PreserveSig]
        int CheckCapabilities([In, Out] ref AM_SEEKING_SeekingCapabilities pCapabilities);
        
        [PreserveSig]
        int IsFormatSupported([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);
        
        [PreserveSig]
        int QueryPreferredFormat(out Guid pFormat);
        
        [PreserveSig]
        int GetTimeFormat(out Guid pFormat);
        
        [PreserveSig]
        int IsUsingTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);
        
        [PreserveSig]
        int SetTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);
        
        [PreserveSig]
        int GetDuration(out long pDuration);
        
        [PreserveSig]
        int GetStopPosition(out long pStop);
        
        [PreserveSig]
        int GetCurrentPosition(out long pCurrent);
        
        [PreserveSig]
        int ConvertTimeFormat(out long pTarget, [In, MarshalAs(UnmanagedType.LPStruct)] Guid pTargetFormat, [In] long Source, [In, MarshalAs(UnmanagedType.LPStruct)] Guid pSourceFormat);
        
        [PreserveSig]
        int SetPositions([In, Out] ref long pCurrent, [In] AM_SEEKING_SeekingFlags dwCurrentFlags, [In, Out] ref long pStop, [In] AM_SEEKING_SeekingFlags dwStopFlags);
        
        [PreserveSig]
        int GetPositions(out long pCurrent, out long pStop);
        
        [PreserveSig]
        int GetAvailable(out long pEarliest, out long pLatest);
        
        [PreserveSig]
        int SetRate([In] double dRate);
        
        [PreserveSig]
        int GetRate(out double pdRate);
        
        [PreserveSig]
        int GetPreroll(out long pllPreroll);
    }

    [ComImport, Guid("56A8689C-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemAllocator
    {
        [PreserveSig]
        int SetProperties([In] ref AllocatorProperties pRequest, out AllocatorProperties pActual);
        
        [PreserveSig]
        int GetProperties(out AllocatorProperties pProps);
        
        [PreserveSig]
        int Commit();
        
        [PreserveSig]
        int Decommit();
        
        [PreserveSig]
        int GetBuffer([MarshalAs(UnmanagedType.Interface)] out IMediaSample ppBuffer, [In] ref long pStartTime, [In] ref long pEndTime, [In] int dwFlags);
        
        [PreserveSig]
        int ReleaseBuffer([In, MarshalAs(UnmanagedType.Interface)] IMediaSample pBuffer);
    }

    [ComImport, Guid("379A0CF0-C1DE-11D2-ABF5-00A0C905F375"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemAllocatorCallbackTemp : IMemAllocator
    {
        [PreserveSig]
        new int SetProperties([In] ref AllocatorProperties pRequest, out AllocatorProperties pActual);
        
        [PreserveSig]
        new int GetProperties(out AllocatorProperties pProps);
        
        [PreserveSig]
        new int Commit();
        
        [PreserveSig]
        new int Decommit();
        
        [PreserveSig]
        new int GetBuffer([MarshalAs(UnmanagedType.Interface)] out IMediaSample ppBuffer, [In] ref long pStartTime, [In] ref long pEndTime, [In] int dwFlags);
        
        [PreserveSig]
        new int ReleaseBuffer([In, MarshalAs(UnmanagedType.Interface)] IMediaSample pBuffer);
        
        [PreserveSig]
        int SetNotify([In, MarshalAs(UnmanagedType.Interface)] IMemAllocatorNotifyCallbackTemp pNotify);
        
        [PreserveSig]
        int GetFreeCount(out int plBuffersFree);
    }

    [ComImport, Guid("92980B30-C1DE-11D2-ABF5-00A0C905F375"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemAllocatorNotifyCallbackTemp
    {
        [PreserveSig]
        int NotifyRelease();
    }

    [ComImport, Guid("56A8689D-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemInputPin
    {
        [PreserveSig]
        int GetAllocator([MarshalAs(UnmanagedType.Interface)] out IMemAllocator ppAllocator);
        
        [PreserveSig]
        int NotifyAllocator([In, MarshalAs(UnmanagedType.Interface)] IMemAllocator pAllocator, [In] int bReadOnly);
        
        [PreserveSig]
        int GetAllocatorRequirements(out AllocatorProperties pProps);
        
        [PreserveSig]
        int Receive([In, MarshalAs(UnmanagedType.Interface)] IMediaSample pSample);
        
        [PreserveSig]
        int ReceiveMultiple([In, MarshalAs(UnmanagedType.Interface)] ref IMediaSample pSamples, [In] int nSamples, out int nSamplesProcessed);
        
        [PreserveSig]
        int ReceiveCanBlock();
    }

    [ComImport, Guid("56A86891-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPin
    {
        [PreserveSig]
        int Connect([In, MarshalAs(UnmanagedType.Interface)] IPin pReceivePin, [In] ref AMMediaType pmt);
        
        [PreserveSig]
        int ReceiveConnection([In, MarshalAs(UnmanagedType.Interface)] IPin pConnector, [In] ref AMMediaType pmt);
        
        [PreserveSig]
        int Disconnect();
        
        [PreserveSig]
        int ConnectedTo([MarshalAs(UnmanagedType.Interface)] out IPin pPin);
        
        [PreserveSig]
        int ConnectionMediaType(out AMMediaType pmt);
        
        [PreserveSig]
        int QueryPinInfo(out PinInfo pInfo);
        
        [PreserveSig]
        int QueryDirection(out PinDirection pPinDir);
        
        [PreserveSig]
        int QueryId([MarshalAs(UnmanagedType.LPWStr)] out string Id);
        
        [PreserveSig]
        int QueryAccept([In] ref AMMediaType pmt);
        
        [PreserveSig]
        int EnumMediaTypes([MarshalAs(UnmanagedType.Interface)] out IEnumMediaTypes ppEnum);
        
        [PreserveSig]
        int QueryInternalConnections([MarshalAs(UnmanagedType.Interface)] out IPin apPin, [In, Out] ref int nPin);
        
        [PreserveSig]
        int EndOfStream();
        
        [PreserveSig]
        int BeginFlush();
        
        [PreserveSig]
        int EndFlush();
        
        [PreserveSig]
        int NewSegment([In] long tStart, [In] long tStop, [In] double dRate);
    }

    [ComImport, Guid("4a9a62d3-27d4-403d-91e9-89f540e55534"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPinConnection
    {
        [PreserveSig]
        int DynamicQueryAccept(AMMediaType pmt);
        
        [PreserveSig]
        int NotifyEndOfStream([In] IntPtr hNotifyEvent);
        
        [PreserveSig]
        int IsEndPin();
        
        [PreserveSig]
        int DynamicDisconnect();
    }

    [ComImport, Guid("C56E9858-DBF3-4F6B-8119-384AF2060DEB"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPinFlowControl
    {
        [PreserveSig]
        int Block([In] int dwBlockFlags, [In] IntPtr hEvent);
    }

    [ComImport, Guid("56A86897-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClock
    {
        [PreserveSig]
        int GetTime(out long pTime);

        [PreserveSig]
        int AdviseTime([In] long baseTime, [In] long streamTime, [In] IntPtr hEvent, out int pdwAdviseCookie);

        [PreserveSig]
        int AdvisePeriodic([In] long startTime, [In] long periodTime, [In] IntPtr hSemaphore, out int pdwAdviseCookie);

        [PreserveSig]
        int Unadvise([In] int dwAdviseCookie);
    }

    [ComImport, Guid("36B73885-C2C8-11CF-8B46-00805F6CEF60"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClock2 : IReferenceClock
    {
        [PreserveSig]
        new int GetTime(out long pTime);
        
        [PreserveSig]
        new int AdviseTime([In] long baseTime, [In] long streamTime, [In] IntPtr hEvent, out int pdwAdviseCookie);
        
        [PreserveSig]
        new int AdvisePeriodic([In] long startTime, [In] long periodTime, [In] IntPtr hSemaphore, out int pdwAdviseCookie);
        
        [PreserveSig]
        new int Unadvise([In] int dwAdviseCookie);
    }

    [ComImport, Guid("EBEC459C-2ECA-4D42-A8AF-30DF557614B8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClockTimerControl
    {
        [PreserveSig]
        int SetDefaultTimerResolution(long timerResolution);
        
        [PreserveSig]
        int GetDefaultTimerResolution(ref long pTimerResolution);
    }

    [ComImport, Guid("56A86893-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumFilters
    {
        [PreserveSig]
        int Next([In] int cFilters, [MarshalAs(UnmanagedType.Interface)] out IBaseFilter ppFilter, out int pcFetched);

        [PreserveSig]
        int Skip([In] int cFilters);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([MarshalAs(UnmanagedType.Interface)] out IEnumFilters ppEnum);
    }

    [ComImport, Guid("56A86892-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumPins
    {
        [PreserveSig]
        int Next([In] int cPins, [MarshalAs(UnmanagedType.Interface)] out IPin ppPins, out int pcFetched);
        
        [PreserveSig]
        int Skip([In] int cPins);
        
        [PreserveSig]
        int Reset();
        
        [PreserveSig]
        int Clone([MarshalAs(UnmanagedType.Interface)] out IEnumPins ppEnum);
    }

    [ComImport, Guid("89C31040-846B-11CE-97D3-00AA0055595A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumMediaTypes
    {
        [PreserveSig]
        int Next([In] int cMediaTypes, [MarshalAs(UnmanagedType.LPStruct)] out AMMediaType ppMediaTypes, out int pcFetched);
        
        [PreserveSig]
        int Skip([In] int cMediaTypes);
        
        [PreserveSig]
        int Reset();
        
        [PreserveSig]
        int Clone([MarshalAs(UnmanagedType.Interface)] out IEnumMediaTypes ppEnum);
    }

    [ComImport, Guid("A3D8CEC0-7E5A-11CF-BBC5-00805F6CEF20"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMovieSetup
    {
        [PreserveSig]
        int Register();
        
        [PreserveSig]
        int Unregister();
    }

    [ComImport, Guid("56A868B3-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBasicAudio
    {
        [PreserveSig]
        int put_Volume([In] int lVolume);
        
        [PreserveSig]
        int get_Volume(out int plVolume);
        
        [PreserveSig]
        int put_Balance([In] int lBalance);
        
        [PreserveSig]
        int get_Balance(out int plBalance);
    }

    [ComImport, Guid("56a868b5-0ad4-11ce-b03a-0020af0ba770"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBasicVideo
    {
        [PreserveSig]
        int get_AvgTimePerFrame(out double pAvgTimePerFrame);
        
        [PreserveSig]
        int get_BitRate(out int pBitRate);
        
        [PreserveSig]
        int get_BitErrorRate(out int pBitRate);
        
        [PreserveSig]
        int get_VideoWidth(out int pVideoWidth);
        
        [PreserveSig]
        int get_VideoHeight(out int pVideoHeight);
        
        [PreserveSig]
        int put_SourceLeft([In] int SourceLeft);
        
        [PreserveSig]
        int get_SourceLeft(out int pSourceLeft);
        
        [PreserveSig]
        int put_SourceWidth([In] int SourceWidth);
        
        [PreserveSig]
        int get_SourceWidth(out int pSourceWidth);
        
        [PreserveSig]
        int put_SourceTop([In] int SourceTop);
        
        [PreserveSig]
        int get_SourceTop(out int pSourceTop);
        
        [PreserveSig]
        int put_SourceHeight([In] int SourceHeight);
        
        [PreserveSig]
        int get_SourceHeight(out int pSourceHeight);
        
        [PreserveSig]
        int put_DestinationLeft([In] int DestinationLeft);
        
        [PreserveSig]
        int get_DestinationLeft(out int pDestinationLeft);
        
        [PreserveSig]
        int put_DestinationWidth([In] int DestinationWidth);
        
        [PreserveSig]
        int get_DestinationWidth(out int pDestinationWidth);
        
        [PreserveSig]
        int put_DestinationTop([In] int DestinationTop);
        
        [PreserveSig]
        int get_DestinationTop(out int pDestinationTop);
        
        [PreserveSig]
        int put_DestinationHeight([In] int DestinationHeight);
        
        [PreserveSig]
        int get_DestinationHeight(out int pDestinationHeight);
        
        [PreserveSig]
        int SetSourcePosition([In] int left, [In] int top, [In] int width, [In] int height);
        
        [PreserveSig]
        int GetSourcePosition(out int left, out int top, out int width, out int height);
        
        [PreserveSig]
        int SetDefaultSourcePosition();
        
        [PreserveSig]
        int SetDestinationPosition([In] int left, [In] int top, [In] int width, [In] int height);
        
        [PreserveSig]
        int GetDestinationPosition(out int left, out int top, out int width, out int height);
        
        [PreserveSig]
        int SetDefaultDestinationPosition();
        
        [PreserveSig]
        int GetVideoSize(out int pWidth, out int pHeight);
        
        [PreserveSig]
        int GetVideoPaletteEntries([In] int StartIndex, [In] int Entries, out int pRetrieved, out IntPtr pPalette);
        
        [PreserveSig]
        int GetCurrentImage([In, Out] ref int pBufferSize, [Out] IntPtr pDIBImage);
        
        [PreserveSig]
        int IsUsingDefaultSource();
        
        [PreserveSig]
        int IsUsingDefaultDestination();
    }

    [ComImport, Guid("56A868B6-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEvent
    {
        [PreserveSig]
        int GetEventHandle(out IntPtr hEvent);
        
        [PreserveSig]
        int GetEvent(out int lEventCode, out IntPtr lParam1, out IntPtr lParam2, [In] int msTimeout);
        
        [PreserveSig]
        int WaitForCompletion([In] int msTimeout, out int pEvCode);
        
        [PreserveSig]
        int CancelDefaultHandling([In] int lEvCode);
        
        [PreserveSig]
        int RestoreDefaultHandling([In] int lEvCode);
        
        [PreserveSig]
        int FreeEventParams([In] int lEvCode, [In] IntPtr lParam1, [In] IntPtr lParam2);
    }

    [ComImport, Guid("56A868C0-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEventEx : IMediaEvent
    {
        [PreserveSig]
        new int GetEventHandle(out IntPtr hEvent);
        
        [PreserveSig]
        new int GetEvent(out int lEventCode, out IntPtr lParam1, out IntPtr lParam2, [In] int msTimeout);
        
        [PreserveSig]
        new int WaitForCompletion([In] int msTimeout, out int pEvCode);
        
        [PreserveSig]
        new int CancelDefaultHandling([In] int lEvCode);
        
        [PreserveSig]
        new int RestoreDefaultHandling([In] int lEvCode);
        
        [PreserveSig]
        new int FreeEventParams([In] int lEvCode, [In] IntPtr lParam1, [In] IntPtr lParam2);
        
        [PreserveSig]
        int SetNotifyWindow([In] IntPtr hwnd, [In] int lMsg, [In] IntPtr lInstanceData);
        
        [PreserveSig]
        int SetNotifyFlags([In] int lNoNotifyFlags);
        
        [PreserveSig]
        int GetNotifyFlags(out int lplNoNotifyFlags);
    }

    [ComImport, Guid("56A868A2-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaEventSink
    {
        [PreserveSig]
        int Notify([In] int evCode, [In] IntPtr EventParam1, [In] IntPtr EventParam2);
    }

    [ComImport, Guid("6B652FFF-11FE-4FCE-92AD-0266B5D7C78F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabber
    {
        [PreserveSig]
        int SetOneShot([In, MarshalAs(UnmanagedType.Bool)] bool OneShot);
        
        [PreserveSig]
        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);
        
        [PreserveSig]
        int GetConnectedMediaType([Out, MarshalAs(UnmanagedType.LPStruct)]out AMMediaType pmt);
        
        [PreserveSig]
        int SetBufferSamples([In, MarshalAs(UnmanagedType.Bool)] bool BufferThem);
        
        [PreserveSig]
        int GetCurrentBuffer(ref int pBufferSize, IntPtr pBuffer);
        
        [PreserveSig]
        int GetCurrentSample(out IMediaSample ppSample);
        
        [PreserveSig]
        int SetCallback(ISampleGrabberCB pCallback, int WhichMethodToCallback);
    }

    [ComImport, Guid("0579154A-2B53-4994-B0D0-E773148EFF85"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabberCB
    {
        [PreserveSig]
        int SampleCB(double SampleTime, IMediaSample pSample);
        
        [PreserveSig]
        int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen);
    }

}
