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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.CompilerServices;
using Ernzo.Windows.DirectShowLib;
using Ernzo.Windows.WaveAudio; // get 'tWAVEFORMATEX'

namespace Ernzo.Windows.DirectShowLib.MMStreaming
{
    public enum STREAM_TYPE
    {
        STREAMTYPE_READ     = 0,
        STREAMTYPE_WRITE    = 1,
        STREAMTYPE_TRANSFORM= 2
    }

    public enum STREAM_STATE
    {
        STREAMSTATE_STOP    = 0,
        STREAMSTATE_RUN     = 1
    }

    public enum COMPLETION_STATUS_FLAGS
    {
        COMPSTAT_NONE       = 0,
        COMPSTAT_NOUPDATEOK = 1,
        COMPSTAT_WAIT       = 2,
        COMPSTAT_ABORT      = 4
    }

    //  Flags definitions for IAMMultiMediaStream::Initialize
    public enum AMMSF_INIT
    {
        AMMSF_NONE          = 0,
        AMMSF_NOGRAPHTHREAD = 0x00000001
    }

    //  Flags definitions for AddMediaStream and IAMMediaStream::Initialize
    public enum AMMSF_CREATE
    {
        AMMSF_NONE               = 0,
        //  Don't add a stream - create a default renderer instead
        //  for the supplied purpose id
        AMMSF_ADDDEFAULTRENDERER = 0x00000001,
        AMMSF_CREATEPEER         = 0x00000002,

        //  If no samples are created when we run or the last sample
        //  is deleted then terminate this stream
        AMMSF_STOPIFNOSAMPLES    = 0x00000004,

        //  If Update is not called keep going
        AMMSF_NOSTALL            = 0x00000008
    }

    //  Flag definitions for OpenFile and OpenMoniker
    public enum AMMSF_OPEN
    {
        AMMSF_RENDERTOEXISTING   = 0x00000000,
        AMMSF_RENDERALLSTREAMS   = 0x00000001,
        AMMSF_NORENDER           = 0x00000002,
        AMMSF_RENDERTYPEMASK     = 0x00000003,
        AMMSF_NOCLOCK            = 0x00000004,
        AMMSF_RUN                = 0x00000008
    }

    public enum OUTPUT_STATE
    {
        Disabled = 0,
        ReadData = 1,
        RenderData = 2
    }

    //  Flags for IMultiMediaStream::GetInformation
    public enum MMSSF_FLAGS
    {
        MMSSF_NONE          = 0,
        MMSSF_HASCLOCK      = 1,
        MMSSF_SUPPORTSEEK   = 2,
        MMSSF_ASYNCHRONOUS  = 4
    }

    //  Flags for StreamSample::Update
    public enum SSUPDATE_FLAGS
    {
        SSUPDATE_NONE       = 0,
        SSUPDATE_ASYNC      = 1,
        SSUPDATE_CONTINUOUS = 2
    }

    public sealed class MSPurposeId
    {
        private MSPurposeId()
        {
        }
        public static readonly Guid PrimaryAudio = new Guid(0xa35ff56b, 0x9fda, 0x11d0, 0x8f, 0xdf, 0, 0xc0, 0x4f, 0xd9, 0x18, 0x9d);
        public static readonly Guid PrimaryVideo = new Guid(0xa35ff56a, 0x9fda, 0x11d0, 0x8f, 0xdf, 0, 0xc0, 0x4f, 0xd9, 0x18, 0x9d);
    }

    public sealed class MSStatus
    {
        private MSStatus()
        {
        }
        public const int MS_S_OK                    = 0;
        public const int MS_S_FALSE                 = 1;
        public const int MS_S_PENDING               = unchecked((int)0x00040001);
        public const int MS_S_NOUPDATE              = unchecked((int)0x00040002);
        public const int MS_S_ENDOFSTREAM           = unchecked((int)0x00040003);
        public const int MS_E_SAMPLEALLOC           = unchecked((int)0x80040401);
        public const int MS_E_PURPOSEID             = unchecked((int)0x80040402);
        public const int MS_E_NOSTREAM              = unchecked((int)0x80040403);
        public const int MS_E_NOSEEKING             = unchecked((int)0x80040404);
        public const int MS_E_INCOMPATIBLE          = unchecked((int)0x80040405);
        public const int MS_E_BUSY                  = unchecked((int)0x80040406);
        public const int MS_E_NOTINIT               = unchecked((int)0x80040407);
        public const int MS_E_SOURCEALREADYDEFINED  = unchecked((int)0x80040408);
        public const int MS_E_INVALIDSTREAMTYPE     = unchecked((int)0x80040409);
        public const int MS_E_NOTRUNNING            = unchecked((int)0x8004040a);
        public const int MS_E_HANDLE                = unchecked((int)0x80070006);

        /// <summary>
        /// GetErrorText
        /// Get text description for status code
        /// http://msdn.microsoft.com/en-us/library/ms783646(VS.85).aspx
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <returns>error description (null for unknown)</returns>
        public static string GetErrorText(int errorCode)
        {
            string errText = null;
            switch (errorCode)
            {
                case MSStatus.MS_S_OK:
                case MSStatus.MS_S_FALSE:
                    errText = "Operation succeeds.";
                    break;
                case MSStatus.MS_S_PENDING:
                    errText = "Sample update is not yet complete.";
                    break;
                case MSStatus.MS_S_NOUPDATE:
                    errText = "Sample was not updated after forced completion.";
                    break;
                case MSStatus.MS_S_ENDOFSTREAM:
                    errText = "End of stream. Sample not updated.";
                    break;
                case MSStatus.MS_E_SAMPLEALLOC:
                    errText = "An IMediaStream object could not be removed from an IMultiMediaStream object because it still contains at least one allocated sample.";
                    break;
                case MSStatus.MS_E_PURPOSEID:
                    errText = "The specified purpose ID can't be used for the call.";
                    break;
                case MSStatus.MS_E_NOSTREAM:
                    errText = "No stream can be found with the specified attributes.";
                    break;
                case MSStatus.MS_E_NOSEEKING:
                    errText = "Seeking not supported for this IMultiMediaStream object.";
                    break;
                case MSStatus.MS_E_INCOMPATIBLE:
                    errText = "The stream formats are not compatible.";
                    break;
                case MSStatus.MS_E_BUSY:
                    errText = "The sample is busy.";
                    break;
                case MSStatus.MS_E_NOTINIT:
                    errText = "The object can't accept the call because its initialize function or equivalent has not been called.";
                    break;
                case MSStatus.MS_E_SOURCEALREADYDEFINED:
                    errText = "Source already defined.";
                    break;
                case MSStatus.MS_E_INVALIDSTREAMTYPE:
                    errText = "The stream type is not valid for this operation.";
                    break;
                case MSStatus.MS_E_NOTRUNNING:
                    errText = "The IMultiMediaStream object is not in running state.";
                    break;
            }
            return errText;
        }

        /// <summary>
        /// Simulate COM (SUCCEEDED) macro
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static bool Succeed(int errorCode)
        {
            // MS_S_PENDING and MS_S_ENDOFSTREAM are positive error code
            return (errorCode >= 0);
        }

        /// <summary>
        /// ThrowExceptionForHR
        /// Throw an exception for HR value (doesn't throw for S_OK, S_FALSE)
        /// </summary>
        /// <param name="errorCode">Error code</param>
        public static void ThrowExceptionForHR(int errorCode)
        {
            if ((errorCode < 0) ||
                (errorCode >= MSStatus.MS_S_PENDING && errorCode <= MSStatus.MS_S_ENDOFSTREAM)
                )
            {
                string errText = GetErrorText(errorCode);

                if (!string.IsNullOrEmpty(errText))
                {
                    throw new COMException(errText, errorCode);
                }
                else
                {
                    Marshal.ThrowExceptionForHR(errorCode);
                }
            }
        }
    }


    [ComImport, Guid("B502D1BD-9A57-11D0-8FDE-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaStream
    {
        [PreserveSig]
        int GetMultiMediaStream([MarshalAs(UnmanagedType.Interface)] out IMultiMediaStream ppMultiMediaStream);

        [PreserveSig]
        int GetInformation(out Guid pPurposeId, out STREAM_TYPE pType);

        [PreserveSig]
        int SetSameFormat([In, MarshalAs(UnmanagedType.Interface)] IMediaStream pStreamThatHasDesiredFormat, [In] int dwFlags);

        [PreserveSig]
        int AllocateSample([In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppSample);

        [PreserveSig]
        int CreateSharedSample([In, MarshalAs(UnmanagedType.Interface)] IStreamSample pExistingSample, [In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppNewSample);

        [PreserveSig]
        int SendEndOfStream(int dwFlags);
    }

    [ComImport, Guid("F7537560-A3BE-11D0-8212-00C04FC32C45"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioMediaStream : IMediaStream
    {
        [PreserveSig]
        new int GetMultiMediaStream([MarshalAs(UnmanagedType.Interface)] out IMultiMediaStream ppMultiMediaStream);

        [PreserveSig]
        new int GetInformation(out Guid pPurposeId, out STREAM_TYPE pType);

        [PreserveSig]
        new int SetSameFormat([In, MarshalAs(UnmanagedType.Interface)] IMediaStream pStreamThatHasDesiredFormat, [In] int dwFlags);

        [PreserveSig]
        new int AllocateSample([In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppSample);

        [PreserveSig]
        new int CreateSharedSample([In, MarshalAs(UnmanagedType.Interface)] IStreamSample pExistingSample, [In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppNewSample);

        [PreserveSig]
        new int SendEndOfStream([In] int dwFlags);

        [PreserveSig]
        int GetFormat(out tWAVEFORMATEX pWaveFormatCurrent);

        [PreserveSig]
        int SetFormat([In] ref tWAVEFORMATEX lpWaveFormat);

        [PreserveSig]
        int CreateSample([In, MarshalAs(UnmanagedType.Interface)] IAudioData pAudioData, [In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IAudioStreamSample ppSample);
    }

    [ComImport, Guid("327FC560-AF60-11D0-8212-00C04FC32C45"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMemoryData
    {
        [PreserveSig]
        int SetBuffer([In] int cbSize, [In] IntPtr pbData, [In] int dwFlags);

        [PreserveSig]
        int GetInfo(out int pdwLength, out IntPtr ppbData, out int pcbActualData);

        [PreserveSig]
        int SetActual([In] int cbDataValid);
    }

    [ComImport, Guid("54C719C0-AF60-11D0-8212-00C04FC32C45"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioData : IMemoryData
    {
        [PreserveSig]
        new int SetBuffer([In] int cbSize, [In] IntPtr pbData, [In] int dwFlags);

        [PreserveSig]
        new int GetInfo(out int pdwLength, out IntPtr ppbData, out int pcbActualData);

        [PreserveSig]
        new int SetActual([In] int cbDataValid);

        [PreserveSig]
        int GetFormat(out tWAVEFORMATEX pWaveFormatCurrent);

        [PreserveSig]
        int SetFormat([In] ref tWAVEFORMATEX lpWaveFormat);
    }

    [ComImport, Guid("B502D1BE-9A57-11D0-8FDE-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamSample
    {
        [PreserveSig]
        int GetMediaStream([MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);

        [PreserveSig]
        int GetSampleTimes(out long pStartTime, out long pEndTime, out long pCurrentTime);

        [PreserveSig]
        int SetSampleTimes([In] ref long pStartTime, [In] ref long pEndTime);

        [PreserveSig]
        int Update([In] int dwFlags, [In] IntPtr hEvent, [In, MarshalAs(UnmanagedType.Interface)] IStreamSample pfnAPC, [In] int dwAPCData);

        [PreserveSig]
        int CompletionStatus([In] int dwFlags, [In] int dwMilliseconds);
    }

    [ComImport, Guid("345FEE00-ABA5-11D0-8212-00C04FC32C45"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioStreamSample : IStreamSample
    {
        [PreserveSig]
        new int GetMediaStream([MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);

        [PreserveSig]
        new int GetSampleTimes(out long pStartTime, out long pEndTime, out long pCurrentTime);

        [PreserveSig]
        new int SetSampleTimes([In] ref long pStartTime, [In] ref long pEndTime);

        [PreserveSig]
        new int Update([In] int dwFlags, [In] IntPtr hEvent, [In, MarshalAs(UnmanagedType.Interface)] IStreamSample pfnAPC, [In] int dwAPCData);

        [PreserveSig]
        new int CompletionStatus([In] int dwFlags, [In] int dwMilliseconds);

        [PreserveSig]
        int GetAudioData([MarshalAs(UnmanagedType.Interface)] out IAudioData ppAudio);
    }

    [ComImport, Guid("AB6B4AFB-F6E4-11D0-900D-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMMediaTypeSample : IStreamSample
    {
        [PreserveSig]
        new int GetMediaStream([MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);
        
        [PreserveSig]
        new int GetSampleTimes(out long pStartTime, out long pEndTime, out long pCurrentTime);
        
        [PreserveSig]
        new int SetSampleTimes([In] ref long pStartTime, [In] ref long pEndTime);
        
        [PreserveSig]
        int Update([In] int dwFlags, [In] IntPtr hEvent, [In] IntPtr pfnAPC, [In] IntPtr dwAPCData);
        
        [PreserveSig]
        new int CompletionStatus([In] int dwFlags, [In] int dwMilliseconds);
        
        [PreserveSig]
        int SetPointer([In] IntPtr pBuffer, [In] int lSize);
        
        [PreserveSig]
        int GetPointer(out IntPtr ppBuffer);
        
        [PreserveSig]
        int GetSize();
        
        [PreserveSig]
        int GetTime(out long pTimeStart, out long pTimeEnd);
        
        [PreserveSig]
        int SetTime([In] long pTimeStart, [In] long pTimeEnd);
        
        [PreserveSig]
        int IsSyncPoint();
        
        [PreserveSig]
        int SetSyncPoint([In, MarshalAs(UnmanagedType.Bool)] bool IsSyncPoint);
        
        [PreserveSig]
        int IsPreroll();
        
        [PreserveSig]
        int SetPreroll([In, MarshalAs(UnmanagedType.Bool)] bool IsPreroll);
        
        [PreserveSig]
        int GetActualDataLength();
        
        [PreserveSig]
        int SetActualDataLength([In] int Size);
        
        [PreserveSig]
        int GetMediaType(out AMMediaType ppMediaType);
        
        [PreserveSig]
        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pMediaType);
        
        [PreserveSig]
        int IsDiscontinuity();
        
        [PreserveSig]
        int SetDiscontinuity([In, MarshalAs(UnmanagedType.Bool)] bool Discontinuity);
        
        [PreserveSig]
        int GetMediaTime(out long pTimeStart, out long pTimeEnd);
        
        [PreserveSig]
        int SetMediaTime([In] long pTimeStart, [In] long pTimeEnd);
    }
 
    [ComImport, Guid("B502D1BC-9A57-11D0-8FDE-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMultiMediaStream
    {
        [PreserveSig]
        int GetInformation(out int pdwFlags, out STREAM_TYPE pStreamType);

        [PreserveSig]
        int GetMediaStream([In, MarshalAs(UnmanagedType.LPStruct)] Guid idPurpose, [MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);

        [PreserveSig]
        int EnumMediaStreams([In] int Index, [MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);

        [PreserveSig]
        int GetState(out STREAM_STATE pCurrentState);

        [PreserveSig]
        int SetState([In] STREAM_STATE NewState);

        [PreserveSig]
        int GetTime(out long pCurrentTime);

        [PreserveSig]
        int GetDuration(out long pDuration);

        [PreserveSig]
        int Seek([In] long SeekTime);

        [PreserveSig]
        int GetEndOfStreamEventHandle(out IntPtr phEOS);
    }

    [ComImport, Guid("BEBE595C-9A6F-11D0-8FDE-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMMultiMediaStream : IMultiMediaStream
    {
        [PreserveSig]
        new int GetInformation(out int pdwFlags, out STREAM_TYPE pStreamType);
        
        [PreserveSig]
        new int GetMediaStream([In, MarshalAs(UnmanagedType.LPStruct)] Guid idPurpose, [MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);
        
        [PreserveSig]
        new int EnumMediaStreams([In] int Index, [MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);
        
        [PreserveSig]
        new int GetState(out STREAM_STATE pCurrentState);
        
        [PreserveSig]
        new int SetState([In] STREAM_STATE NewState);
        
        [PreserveSig]
        new int GetTime(out long pCurrentTime);
        
        [PreserveSig]
        new int GetDuration(out long pDuration);
        
        [PreserveSig]
        new int Seek([In] long SeekTime);
        
        [PreserveSig]
        new int GetEndOfStreamEventHandle(out IntPtr phEOS);
        
        [PreserveSig]
        int Initialize([In] STREAM_TYPE StreamType, [In] int dwFlags, [In, MarshalAs(UnmanagedType.Interface)] IGraphBuilder pFilterGraph);
        
        [PreserveSig]
        int GetFilterGraph([MarshalAs(UnmanagedType.Interface)] out IGraphBuilder ppGraphBuilder);
        
        [PreserveSig]
        int GetFilter([MarshalAs(UnmanagedType.Interface)] out IMediaStreamFilter ppFilter);
        
        [PreserveSig]
        int AddMediaStream([In, MarshalAs(UnmanagedType.IUnknown)] object pStreamObject, [In, MarshalAs(UnmanagedType.LPStruct)] Guid PurposeId, [In] int dwFlags, out IMediaStream ppNewStream);
        
        [PreserveSig]
        int OpenFile([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In] int dwFlags);
        
        [PreserveSig]
        int OpenMoniker([In, MarshalAs(UnmanagedType.Interface)] IBindCtx pCtx, [In, MarshalAs(UnmanagedType.Interface)] IMoniker pMoniker, [In] int dwFlags);
        
        [PreserveSig]
        int Render([In] int dwFlags);
    }

    [ComImport, Guid("BEBE595D-9A6F-11D0-8FDE-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMMediaStream : IMediaStream
    {
        [PreserveSig]
        new int GetMultiMediaStream([MarshalAs(UnmanagedType.Interface)] out IMultiMediaStream ppMultiMediaStream);

        [PreserveSig]
        new int GetInformation(out Guid pPurposeId, out STREAM_TYPE pType);

        [PreserveSig]
        new int SetSameFormat([In, MarshalAs(UnmanagedType.Interface)] IMediaStream pStreamThatHasDesiredFormat, [In] int dwFlags);

        [PreserveSig]
        new int AllocateSample([In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppSample);

        [PreserveSig]
        new int CreateSharedSample([In, MarshalAs(UnmanagedType.Interface)] IStreamSample pExistingSample, [In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppNewSample);

        [PreserveSig]
        new int SendEndOfStream([In] int dwFlags);

        [PreserveSig]
        int Initialize([In, MarshalAs(UnmanagedType.IUnknown)] object pSourceObject, [In] int dwFlags, [In, MarshalAs(UnmanagedType.LPStruct)] Guid PurposeId, [In] STREAM_TYPE StreamType);

        [PreserveSig]
        int SetState([In] FilterState State);

        [PreserveSig]
        int JoinAMMultiMediaStream([In, MarshalAs(UnmanagedType.Interface)] IAMMultiMediaStream pAMMultiMediaStream);

        [PreserveSig]
        int JoinFilter([In, MarshalAs(UnmanagedType.Interface)] IMediaStreamFilter pMediaStreamFilter);

        [PreserveSig]
        int JoinFilterGraph([In, MarshalAs(UnmanagedType.Interface)] IFilterGraph pFilterGraph);
    }

    [ComImport, Guid("AB6B4AFA-F6E4-11D0-900D-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMMediaTypeStream : IMediaStream
    {
        [PreserveSig]
        new int GetMultiMediaStream([MarshalAs(UnmanagedType.Interface)] out IMultiMediaStream ppMultiMediaStream);
        
        [PreserveSig]
        new int GetInformation(out Guid pPurposeId, out STREAM_TYPE pType);
        
        [PreserveSig]
        new int SetSameFormat([In, MarshalAs(UnmanagedType.Interface)] IMediaStream pStreamThatHasDesiredFormat, [In] int dwFlags);
        
        [PreserveSig]
        new int AllocateSample([In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppSample);
        
        [PreserveSig]
        new int CreateSharedSample([In, MarshalAs(UnmanagedType.Interface)] IStreamSample pExistingSample, [In] int dwFlags, [MarshalAs(UnmanagedType.Interface)] out IStreamSample ppNewSample);
        
        [PreserveSig]
        new int SendEndOfStream([In] int dwFlags);
        
        [PreserveSig]
        int GetFormat([Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pMediaType, [In] int dwFlags);
        
        [PreserveSig]
        int SetFormat([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pMediaType, [In] int dwFlags);
        
        [PreserveSig]
        int CreateSample([In] int lSampleSize, [In] IntPtr pbBuffer, [In] int dwFlags, [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [MarshalAs(UnmanagedType.Interface)] out IAMMediaTypeSample ppAMMediaTypeSample);
        
        [PreserveSig]
        int GetStreamAllocatorRequirements(out AllocatorProperties pProps);
        
        [PreserveSig]
        int SetStreamAllocatorRequirements([In, MarshalAs(UnmanagedType.LPStruct)] AllocatorProperties pProps);
    }

    [ComImport, Guid("BEBE595E-9A6F-11D0-8FDE-00C04FD9189D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaStreamFilter : IBaseFilter, IMediaFilter, IPersist
    {
        [PreserveSig]
        new int GetClassID(out Guid pClassID);

        [PreserveSig]
        new int Stop();

        [PreserveSig]
        new int Pause();

        [PreserveSig]
        new int Run([In] long tStart);

        [PreserveSig]
        new int GetState([In] int dwMilliSecsTimeout, out FilterState State);

        [PreserveSig]
        new int SetSyncSource([In, MarshalAs(UnmanagedType.Interface)] IReferenceClock pClock);

        [PreserveSig]
        new int GetSyncSource([MarshalAs(UnmanagedType.Interface)] out IReferenceClock pClock);

        [PreserveSig]
        new int EnumPins([MarshalAs(UnmanagedType.Interface)] out IEnumPins ppEnum);

        [PreserveSig]
        new int FindPin([In, MarshalAs(UnmanagedType.LPWStr)] string Id, [MarshalAs(UnmanagedType.Interface)] out IPin ppPin);

        [PreserveSig]
        new int QueryFilterInfo(out FilterInfo pInfo);

        [PreserveSig]
        new int JoinFilterGraph([In, MarshalAs(UnmanagedType.Interface)] IFilterGraph pGraph, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        [PreserveSig]
        new int QueryVendorInfo([MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo);

        [PreserveSig]
        int AddMediaStream([In, MarshalAs(UnmanagedType.Interface)] IAMMediaStream pAMMediaStream);

        [PreserveSig]
        int GetMediaStream([In, MarshalAs(UnmanagedType.LPStruct)] Guid idPurpose, [MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);

        [PreserveSig]
        int EnumMediaStreams([In] int Index, [MarshalAs(UnmanagedType.Interface)] out IMediaStream ppMediaStream);

        [PreserveSig]
        int SupportSeeking([In, MarshalAs(UnmanagedType.Bool)] bool bRenderer);

        [PreserveSig]
        int ReferenceTimeToStreamTime([In, Out] ref long pTime);

        [PreserveSig]
        int GetCurrentStreamTime(out long pCurrentStreamTime);

        [PreserveSig]
        int WaitUntil([In] long WaitStreamTime);

        [PreserveSig]
        int Flush([In, MarshalAs(UnmanagedType.Bool)] bool bCancelEOS);

        [PreserveSig]
        int EndOfStream();
    }


    /// <summary>
    /// CLSID_AMMultiMediaStream
    /// </summary>
    [ComImport, Guid("49C47CE5-9BA4-11D0-8212-00C04FC32C45")]
    public class AMMultiMediaStream
    {
    }

    /// <summary>
    /// CLSID_AMMediaTypeStream
    /// </summary>
    [ComImport, Guid("CF0F2F7C-F7BF-11d0-900D-00C04FD9189D")]
    public class AMMediaTypeStream
    {
    }

    /// <summary>
    /// CLSID_AMDirectDrawStream
    /// </summary>
    [ComImport, Guid("49C47CE4-9BA4-11D0-8212-00C04FC32C45")]
    public class AMDirectDrawStream
    {
    }

    /// <summary>
    /// CLSID_AMAudioStream
    /// </summary>
    [ComImport, Guid("8496E040-AF4C-11D0-8212-00C04FC32C45")]
    public class AMAudioStream
    {
    }

    /// <summary>
    /// CLSID_AMAudioData
    /// </summary>
    [ComImport, Guid("F2468580-AF8A-11D0-8212-00C04FC32C45")]
    public class AMAudioData
    {
    }

}
