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

    // Get latest Platform SDK to find complete list of GUID (look for $SDK_FOLDER\include\uuids.h)

    /// <summary>
    /// CLSID_CaptureGraphBuilder
    /// </summary>
    [ComImport, Guid("BF87B6E0-8C27-11d0-B3F0-00AA003761C5")]
    public class CaptureGraphBuilder
    {
    }

    /// <summary>
    /// CLSID_CaptureGraphBuilder2
    /// </summary>
    [ComImport, Guid("BF87B6E1-8C27-11d0-B3F0-00AA003761C5")]
    public class CaptureGraphBuilder2
    {
    }

    /// <summary>
    /// CLSID_DvdGraphBuilder
    /// </summary>
    [ComImport, Guid("FCC152B7-F372-11d0-8E00-00C04FD7C08B")]
    public class DvdGraphBuilder
    {
    }

    /// <summary>
    /// CLSID_AsyncReader
    /// </summary>
    [ComImport, Guid("E436EBB5-524F-11CE-9F53-0020AF0BA770")]
    public class AsyncReader
    {
    }

    /// <summary>
    /// CLSID_URLReader
    /// </summary>
    [ComImport, Guid("E436EBB6-524F-11CE-9F53-0020AF0BA770")]
    public class URLReader
    {
    }

    /// <summary>
    /// CLSID_FileStreamRenderer
    /// </summary>
    [ComImport, Guid("D51BD5A5-7548-11CF-A520-0080C77EF58A")]
    public class FileStreamRenderer
    {
    }

    /// <summary>
    /// CLSID_SystemClock
    /// </summary>
    [ComImport, Guid("E436EBB1-524F-11CE-9F53-0020AF0BA770")]
    public class SystemClock
    {
    }

    /// <summary>
    /// CLSID_FilterMapper
    /// </summary>
    [ComImport, Guid("E436EBB2-524F-11CE-9F53-0020AF0BA770")]
    public class FilterMapper
    {
    }

    /// <summary>
    /// CLSID_FilterGraph
    /// </summary>
    [ComImport, Guid("E436EBB3-524F-11CE-9F53-0020AF0BA770")]
    public class FilterGraph
    {
    }

    /// <summary>
    /// CLSID_FilterGraphNoThread
    /// </summary>
    [ComImport, Guid("E436EBB8-524F-11CE-9F53-0020AF0BA770")]
    public class FilterGraphNoThread
    {
    }

    /// <summary>
    /// CLSID_FileSource (MPEG file reader)
    /// </summary>
    [ComImport, Guid("701722E0-8AE3-11CE-A85C-00AA002FEAB5")]
    public class FileSource
    {
    }

    /// <summary>
    /// CLSID_MPEGISplitter
    /// </summary>
    [ComImport, Guid("336475D0-942A-11CE-A870-00AA002FEAB5")]
    public class MPEGISplitter
    {
    }

    /// <summary>
    /// CLSID_DMOWrapperFilter
    /// </summary>
    [ComImport, Guid("94297043-BD82-4DFD-B0DE-8177739C6D20")]
    public class DMOWrapperFilter
    {
    }

    /// <summary>
    /// CLSID_SampleGrabber
    /// </summary>
    [ComImport, Guid("C1F400A0-3F08-11D3-9F0B-006008039E37")]
    public class SampleGrabber
    {
    }

    /// <summary>
    /// CLSID_WaveParser
    /// </summary>
    [ComImport, Guid("D51BD5A1-7548-11CF-A520-0080C77EF58A")]
    public class WaveParser
    {
    }

    /// <summary>
    /// CLSID_WMAsfReader (WMSDK-based ASF reader)
    /// </summary>
    [ComImport, Guid("187463A0-5BB7-11D3-ACBE-0080C75E246E")]
    public class WMAsfReader
    {
    }

    /// <summary>
    /// CLSID_WMAsfWriter
    /// </summary>
    [ComImport, Guid("7C23220E-55BB-11D3-8B16-00C04FB6BD3D")]
    public class WMAsfWriter
    {
    }

    /// <summary>
    /// CLSID_AviSplitter
    /// </summary>
    [ComImport, Guid("1B544C20-FD0B-11CE-8C63-00AA0044B51E")]
    public class AviSplitter
    {
    }

    /// <summary>
    /// CLSID_AviReader
    /// </summary>
    [ComImport, Guid("1B544C21-FD0B-11CE-8C63-00AA0044B51E")]
    public class AviReader
    {
    }

    /// <summary>
    /// CLSID_VfwCapture (Vfw 2.0 Capture Driver)
    /// </summary>
    [ComImport, Guid("1B544C22-FD0B-11CE-8C63-00AA0044B51E")]
    public class VfwCapture
    {
    }

    /// <summary>
    /// CLSID_QuickTimeParser
    /// </summary>
    [ComImport, Guid("D51BD5A0-7548-11CF-A520-0080C77EF58A")]
    public class QuickTimeParser
    {
    }

    /// <summary>
    /// CLSID_QTDec
    /// </summary>
    [ComImport, Guid("FDFE9681-74A3-11D0-AFA7-00AA00B67A42")]
    public class QTDec
    {
    }

    /// <summary>
    /// CLSID_AudioRender (Waveout audio renderer)
    /// </summary>
    [ComImport, Guid("E30629D1-27E5-11CE-875D-00608CB78066")]
    public class WaveAudioRender
    {
    }

    /// <summary>
    /// CLSID_DSoundRender (DSound audio renderer)
    /// </summary>
    [ComImport, Guid("79376820-07D0-11CF-A24D-0020AFD79767")]
    public class DSoundRender
    {
    }

    /// <summary>
    /// CLSID_AudioRecord (Wavein audio recorder)
    /// </summary>
    [ComImport, Guid("E30629D2-27E5-11CE-875D-00608CB78066")]
    public class AudioRecord
    {
    }

    /// <summary>
    /// CLSID_SmartTee
    /// </summary>
    [ComImport, Guid("CC58E280-8AA1-11d1-B3F1-00AA003761C5")]
    public class SmartTee
    {
    }

    /// <summary>
    /// CLSID_NullRenderer
    /// </summary>
    [ComImport, Guid("C1F400A4-3F08-11D3-9F0B-006008039E37")]
    public class NullRenderer
    {
    }

    /// <summary>
    /// CLSID_ColorSpaceConverter
    /// </summary>
    [ComImport, Guid("1643E180-90F5-11CE-97D5-00AA0055595A")]
    public class ColorSpaceConverter
    {
    }

    /// <summary>
    /// CLSID_ACMWrapper
    /// </summary>
    [ComImport, Guid("6A08CF80-0E18-11CF-A24D-0020AFD79767")]
    public class ACMWrapper
    {
    }

    /// <summary>
    /// CLSID_CDeviceMoniker
    /// </summary>
    [ComImport, Guid("4315D437-5B8C-11d0-BD3B-00A0C911CE86")]
    public class CDeviceMoniker
    {
    }

    /// <summary>
    /// CLSID_SystemDeviceEnum
    /// </summary>
    [ComImport, Guid("62BE5D10-60EB-11d0-BD3B-00A0C911CE86")]
    public class CreateDevEnum
    {
    }

    /// <summary>
    /// CLSID_MjpegDec
    /// </summary>
    [ComImport, Guid("301056D0-6DFF-11d2-9EEB-006008039E37")]
    public class MjpegDec
    {
    }

    /// <summary>
    /// CLSID_MJPGEnc
    /// </summary>
    [ComImport, Guid("B80AB0A0-7416-11D2-9EEB-006008039E37")]
    public class MJPGEnc
    {
    }

    /// <summary>
    /// CLSID_DirectDraw
    /// </summary>
    [ComImport, Guid("D7B70EE0-4340-11CF-B063-0020AFC2CD35")]
    public class DirectDraw
    {
    }

    /// <summary>
    /// CLSID_DirectDrawClipper
    /// </summary>
    [ComImport, Guid("593817A0-7DB3-11CF-A2DE-00AA00B93356")]
    public class DirectDrawClipper
    {
    }

    // -------------------------------------------------------------------------
    // VMR GUIDS
    // -------------------------------------------------------------------------

    /// <summary>
    /// CLSID_VideoMixingRenderer
    /// </summary>
    [ComImport, Guid("B87BEB7B-8D29-423f-AE4D-6582C10175AC")]
    public class VideoMixingRenderer
    {
    }

    /// <summary>
    /// CLSID_VideoMixingRenderer9
    /// </summary>
    [ComImport, Guid("51B4ABF3-748F-4E3B-A276-C828330E926A")]
    public class VideoMixingRenderer9
    {
    }

    /// <summary>
    /// CLSID_VideoRendererDefault
    /// </summary>
    [ComImport, Guid("6BC1CFFA-8FC1-4261-AC22-CFB4CC38DB50")]
    public class VideoRendererDefault
    {
    }

    /// <summary>
    /// CLSID_VideoRenderer
    /// </summary>
    [ComImport, Guid("70E102B0-5556-11CE-97C0-00AA0055595A")]
    public class VideoRenderer
    {
    }

    // -------------------------------------------------------------------------
    // EVR GUIDS
    // -------------------------------------------------------------------------

    /// <summary>
    /// CLSID_EnhancedVideoRenderer
    /// </summary>
    [ComImport, Guid("FA10746C-9B63-4B6C-BC49-FC300EA5F256")]
    public class EnhancedVideoRenderer
    {
    }

    /// <summary>
    /// CLSID_MFVideoMixer9
    /// </summary>
    [ComImport, Guid("E474E05A-AB65-4F6A-827C-218B1BAAF31F")]
    public class MFVideoMixer9
    {
    }

    /// <summary>
    /// CLSID_MFVideoPresenter9
    /// </summary>
    [ComImport, Guid("98455561-5136-4D28-AB08-4CEE40EA2781")]
    public class MFVideoPresenter9
    {
    }

    /// <summary>
    /// CLSID_EVRTearlessWindowPresenter9
    /// </summary>
    [ComImport, Guid("A0A7A57B-59B2-4919-A694-ADD0A526C373")]
    public class EVRTearlessWindowPresenter9
    {
    }

    // -------------------------------------------------------------------------
    // Specialized filters
    // -------------------------------------------------------------------------

    /// <summary>
    /// CLSID_MPEGAudioDecoder
    /// </summary>
    [ComImport, Guid("4A2286E0-7BEF-11CE-9BD9-0000E202599C")]
    public class MPEGAudioDecoder
    {
    }

    /// <summary>
    /// CLSID_DivXDecFilter
    /// </summary>
    [ComImport, Guid("78766964-0000-0010-8000-00AA00389B71")]
    public class DivXDecFilter
    {
    }

    sealed public class MediaType
    {
        private MediaType()
        {
            // nothing to construct
        }

        public static readonly Guid MEDIATYPE_NULL = Guid.Empty;
        public static readonly Guid MEDIASUBTYPE_NULL = Guid.Empty;

        // -- Use this subtype if you don't have a use for a subtype for your type
        public static readonly Guid MEDIASUBTYPE_None = new Guid(0xe436eb8e, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);
        
        // -- 73646976-0000-0010-8000-00AA00389B71  'vids' == MEDIATYPE_Video
        public static readonly Guid MEDIATYPE_Video = new Guid(0x73646976, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        
        // -- 73647561-0000-0010-8000-00AA00389B71  'auds' == MEDIATYPE_Audio
        public static readonly Guid MEDIATYPE_Audio = new Guid(0x73647561, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        
        // -- 73747874-0000-0010-8000-00AA00389B71  'txts' == MEDIATYPE_Text
        public static readonly Guid MEDIATYPE_Text = new Guid(0x73747874, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
        
        // -- 7364696D-0000-0010-8000-00AA00389B71  'mids' == MEDIATYPE_Midi
        public static readonly Guid MEDIATYPE_Midi = new Guid(0x7364696D, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- e436eb83-524f-11ce-9f53-0020af0ba770            MEDIATYPE_Stream
        public static readonly Guid MEDIATYPE_Stream = new Guid(0xe436eb83, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // -- sub types ---
        // -- 4C504C43-0000-0010-8000-00AA00389B71  'CLPL' == MEDIASUBTYPE_CLPL
        public static readonly Guid MEDIASUBTYPE_CLPL = new Guid(0x4C504C43, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 56595559-0000-0010-8000-00AA00389B71  'YUYV' == MEDIASUBTYPE_YUYV
        public static readonly Guid MEDIASUBTYPE_YUYV = new Guid(0x56595559, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 56555949-0000-0010-8000-00AA00389B71  'IYUV' == MEDIASUBTYPE_IYUV
        public static readonly Guid MEDIASUBTYPE_IYUV = new Guid(0x56555949, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 39555659-0000-0010-8000-00AA00389B71  'YVU9' == MEDIASUBTYPE_YVU9
        public static readonly Guid MEDIASUBTYPE_YVU9 = new Guid(0x39555659, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 31313459-0000-0010-8000-00AA00389B71  'Y411' == MEDIASUBTYPE_Y411
        public static readonly Guid MEDIASUBTYPE_Y411 = new Guid(0x31313459, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 32595559-0000-0010-8000-00AA00389B71  'YUY2' == MEDIASUBTYPE_YUY2
        public static readonly Guid MEDIASUBTYPE_YUY2 = new Guid(0x32595559, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 55595659-0000-0010-8000-00AA00389B71  'YVYU' == MEDIASUBTYPE_YVYU
        public static readonly Guid MEDIASUBTYPE_YVYU = new Guid(0x55595659, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 59565955-0000-0010-8000-00AA00389B71  'UYVY' ==  MEDIASUBTYPE_UYVY
        public static readonly Guid MEDIASUBTYPE_UYVY = new Guid(0x59565955, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 31313259-0000-0010-8000-00AA00389B71  'Y211' ==  MEDIASUBTYPE_Y211
        public static readonly Guid MEDIASUBTYPE_Y211 = new Guid(0x31313259, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- 524a4c43-0000-0010-8000-00AA00389B71  'CLJR' ==  MEDIASUBTYPE_CLJR
        public static readonly Guid MEDIASUBTYPE_CLJR = new Guid(0x524a4c43, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // H.264 compressed video stream
        // 34363248-0000-0010-8000-00AA00389B71  'H264' == MEDIASUBTYPE_H264
        public static readonly Guid MEDIASUBTYPE_H264 = new Guid(0x34363248, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // -- e436eb7a-524f-11ce-9f53-0020af0ba770            MEDIASUBTYPE_RGB8
        public static readonly Guid MEDIASUBTYPE_RGB8 = new Guid(0xe436eb7a, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // -- e436eb7b-524f-11ce-9f53-0020af0ba770            MEDIASUBTYPE_RGB565
        public static readonly Guid MEDIASUBTYPE_RGB565 = new Guid(0xe436eb7b, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // -- e436eb7c-524f-11ce-9f53-0020af0ba770            MEDIASUBTYPE_RGB555
        public static readonly Guid MEDIASUBTYPE_RGB555 = new Guid(0xe436eb7c, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // -- e436eb7d-524f-11ce-9f53-0020af0ba770            MEDIASUBTYPE_RGB24
        public static readonly Guid MEDIASUBTYPE_RGB24 = new Guid(0xe436eb7d, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // -- e436eb7e-524f-11ce-9f53-0020af0ba770            MEDIASUBTYPE_RGB32
        public static readonly Guid MEDIASUBTYPE_RGB32 = new Guid(0xe436eb7e, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // 297C55AF-E209-4cb3-B757-C76D6B9C88A8            MEDIASUBTYPE_ARGB1555
        public static readonly Guid MEDIASUBTYPE_ARGB1555 = new Guid(0x297c55af, 0xe209, 0x4cb3, 0xb7, 0x57, 0xc7, 0x6d, 0x6b, 0x9c, 0x88, 0xa8);

        // 6E6415E6-5C24-425f-93CD-80102B3D1CCA            MEDIASUBTYPE_ARGB4444
        public static readonly Guid MEDIASUBTYPE_ARGB4444 = new Guid(0x6e6415e6, 0x5c24, 0x425f, 0x93, 0xcd, 0x80, 0x10, 0x2b, 0x3d, 0x1c, 0xca);

        // 773c9ac0-3274-11d0-B724-00aa006c1A01            MEDIASUBTYPE_ARGB32
        public static readonly Guid MEDIASUBTYPE_ARGB32 = new Guid(0x773c9ac0, 0x3274, 0x11d0, 0xb7, 0x24, 0x0, 0xaa, 0x0, 0x6c, 0x1a, 0x1);

        // 2f8bb76d-b644-4550-acf3-d30caa65d5c5            MEDIASUBTYPE_A2R10G10B10
        public static readonly Guid MEDIASUBTYPE_A2R10G10B10 = new Guid(0x2f8bb76d, 0xb644, 0x4550, 0xac, 0xf3, 0xd3, 0x0c, 0xaa, 0x65, 0xd5, 0xc5);

        // 576f7893-bdf6-48c4-875f-ae7b81834567            MEDIASUBTYPE_A2B10G10R10
        public static readonly Guid MEDIASUBTYPE_A2B10G10R10 = new Guid(0x576f7893, 0xbdf6, 0x48c4, 0x87, 0x5f, 0xae, 0x7b, 0x81, 0x83, 0x45, 0x67);

        // 6765706a-0000-0010-8000-00AA00389B71        MEDIASUBTYPE_Jpeg
        public static readonly Guid MEDIASUBTYPE_Jpeg = new Guid(0x6765706a, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);

        // 00000001-0000-0010-8000-00AA00389B71            MEDIASUBTYPE_PCM
        public static readonly Guid MEDIASUBTYPE_PCM = new Guid(0x00000001, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

        // e436eb8b-524f-11ce-9f53-0020af0ba770            MEDIASUBTYPE_WAVE
        public static readonly Guid MEDIASUBTYPE_WAVE = new Guid(0xe436eb8b, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // e436eb8c-524f-11ce-9f53-0020af0ba770            MEDIASUBTYPE_AU
        public static readonly Guid MEDIASUBTYPE_AU = new Guid(0xe436eb8c, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);

        // TODO: Add types
        //public static readonly Guid MEDIASUBTYPE_xxx = new Guid(
    }

    sealed public class FormatType
    {
        private FormatType()
        {
            // nothing to construct
        }

        public static readonly Guid FORMAT_NULL = Guid.Empty;
    
        // 0F6417D6-C318-11D0-A43F-00A0C9223196        FORMAT_None
        public static readonly Guid FORMAT_None = new Guid(0x0F6417D6, 0xc318, 0x11d0, 0xa4, 0x3f, 0x00, 0xa0, 0xc9, 0x22, 0x31, 0x96);

        // 05589f80-c356-11ce-bf01-00aa0055595a        FORMAT_VideoInfo
        public static readonly Guid FORMAT_VideoInfo = new Guid(0x05589f80, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a);

        // F72A76A0-EB0A-11d0-ACE4-0000C0CC16BA        FORMAT_VideoInfo2
        public static readonly Guid FORMAT_VideoInfo2 = new Guid(0xf72a76A0, 0xeb0a, 0x11d0, 0xac, 0xe4, 0x00, 0x00, 0xc0, 0xcc, 0x16, 0xba);

        // 05589f81-c356-11ce-bf01-00aa0055595a        FORMAT_WaveFormatEx
        public static readonly Guid FORMAT_WaveFormatEx = new Guid(0x05589f81, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a);

        // 05589f82-c356-11ce-bf01-00aa0055595a        FORMAT_MPEGVideo
        public static readonly Guid FORMAT_MPEGVideo = new Guid(0x05589f82, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a);
    }

    sealed public class TimeFormat
    {
        private TimeFormat()
        {
            // nothing to construct
        }

        public static readonly Guid TIME_FORMAT_NONE = Guid.Empty;

        // 7b785570-8c82-11cf-bc0c-00aa00ac74f6         TIME_FORMAT_FRAME
        public static readonly Guid TIME_FORMAT_FRAME = new Guid(0x7b785570, 0x8c82, 0x11cf, 0xbc, 0xc, 0x0, 0xaa, 0x0, 0xac, 0x74, 0xf6);

        // 7b785571-8c82-11cf-bc0c-00aa00ac74f6         TIME_FORMAT_BYTE
        public static readonly Guid TIME_FORMAT_BYTE = new Guid(0x7b785571, 0x8c82, 0x11cf, 0xbc, 0xc, 0x0, 0xaa, 0x0, 0xac, 0x74, 0xf6);

        // 7b785572-8c82-11cf-bc0c-00aa00ac74f6         TIME_FORMAT_SAMPLE
        public static readonly Guid TIME_FORMAT_SAMPLE = new Guid(0x7b785572, 0x8c82, 0x11cf, 0xbc, 0xc, 0x0, 0xaa, 0x0, 0xac, 0x74, 0xf6);

        // 7b785573-8c82-11cf-bc0c-00aa00ac74f6         TIME_FORMAT_FIELD
        public static readonly Guid TIME_FORMAT_FIELD = new Guid(0x7b785573, 0x8c82, 0x11cf, 0xbc, 0xc, 0x0, 0xaa, 0x0, 0xac, 0x74, 0xf6);

        // 7b785574-8c82-11cf-bc0c-00aa00ac74f6         TIME_FORMAT_MEDIA_TIME
        public static readonly Guid TIME_FORMAT_MEDIA_TIME = new Guid(0x7b785574, 0x8c82, 0x11cf, 0xbc, 0xc, 0x0, 0xaa, 0x0, 0xac, 0x74, 0xf6);
    }
}
