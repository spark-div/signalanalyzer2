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
using System.Security;
using System.Text;
using System.Runtime.InteropServices;

namespace Ernzo.Windows.WaveAudio
{
    internal class WaveAudioLib
    {
        #if WINCE
        public const string LibName = "Coredll.dll";
        #else
        public const string LibName = "WinMM.dll";
        #endif
    }

    sealed public class WaveConstants
    {
        private WaveConstants()
        {
        }
        /* flags used with waveOutOpen(), waveInOpen(), midiInOpen(), and */
        /* midiOutOpen() to specify the type of the dwCallback parameter. */
        public const int CALLBACK_TYPEMASK  = 0x00070000;
        public const int CALLBACK_NULL      = 0x00000000;
        public const int CALLBACK_WINDOW    = 0x00010000;
        public const int CALLBACK_TASK      = 0x00020000;
        public const int CALLBACK_FUNCTION  = 0x00030000;
        public const int CALLBACK_THREAD    = WaveConstants.CALLBACK_TASK;
        public const int CALLBACK_EVENT     = 0x00050000;

        /* device ID for wave device mapper */
        public const int WAVE_MAPPER        = -1;

        /* wave callback messages */
        public const int MM_WOM_OPEN        = 0x3BB;
        public const int MM_WOM_CLOSE       = 0x3BC;
        public const int MM_WOM_DONE        = 0x3BD;
        public const int MM_WIM_OPEN        = 0x3BE;
        public const int MM_WIM_CLOSE       = 0x3BF;
        public const int MM_WIM_DATA        = 0x3C0;

        public const int WOM_OPEN           = WaveConstants.MM_WOM_OPEN;
        public const int WOM_CLOSE          = WaveConstants.MM_WOM_CLOSE;
        public const int WOM_DONE           = WaveConstants.MM_WOM_DONE;
        public const int WIM_OPEN           = WaveConstants.MM_WIM_OPEN;
        public const int WIM_CLOSE          = WaveConstants.MM_WIM_CLOSE;
        public const int WIM_DATA           = WaveConstants.MM_WIM_DATA;

        /* flags for dwFlags field of WAVEHDR */
        public const int WHDR_DONE          = 1;
        public const int WHDR_PREPARED      = 2;
        public const int WHDR_BEGINLOOP     = 4;
        public const int WHDR_ENDLOOP       = 8;
        public const int WHDR_INQUEUE       = 16;

        /* flags for dwSupport field of WAVEOUTCAPS */
        public const int WAVECAPS_PITCH     = 1;
        public const int WAVECAPS_PLAYBACKRATE = 2;
        public const int WAVECAPS_VOLUME    = 4;
        public const int WAVECAPS_LRVOLUME  = 8;
        public const int WAVECAPS_SYNC      = 16;
        public const int WAVECAPS_SAMPLEACCURATE = 32;

        /* types for wType field in MMTIME struct */
        public const int TIME_MS            = 0x0001;
        public const int TIME_SAMPLES       = 0x0002;
        public const int TIME_BYTES         = 0x0004;
        public const int TIME_SMPTE         = 0x0008;
        public const int TIME_MIDI          = 0x0010;
        public const int TIME_TICKS         = 0x0020;

        public const int WAVE_FORMAT_PCM  = 0x00000001;

        /* defines for dwFormat field of WAVEINCAPS and WAVEOUTCAPS */
        public const int WAVE_INVALIDFORMAT = 0x00000000;
        public const int WAVE_FORMAT_1M08 = 0x00000001;     /* 11.025 kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_1S08 = 0x00000002;     /* 11.025 kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_1M16 = 0x00000004;     /* 11.025 kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_1S16 = 0x00000008;     /* 11.025 kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_2M08 = 0x00000010;     /* 22.05  kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_2S08 = 0x00000020;     /* 22.05  kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_2M16 = 0x00000040;     /* 22.05  kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_2S16 = 0x00000080;     /* 22.05  kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_4M08 = 0x00000100;     /* 44.1   kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_4S08 = 0x00000200;     /* 44.1   kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_4M16 = 0x00000400;     /* 44.1   kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_4S16 = 0x00000800;     /* 44.1   kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_44M08 = 0x00000100;    /* 44.1   kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_44S08 = 0x00000200;    /* 44.1   kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_44M16 = 0x00000400;    /* 44.1   kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_44S16 = 0x00000800;    /* 44.1   kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_48M08 = 0x00001000;    /* 48     kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_48S08 = 0x00002000;    /* 48     kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_48M16 = 0x00004000;    /* 48     kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_48S16 = 0x00008000;    /* 48     kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_96M08 = 0x00010000;    /* 96     kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_96S08 = 0x00020000;    /* 96     kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_96M16 = 0x00040000;    /* 96     kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_96S16 = 0x00080000;    /* 96     kHz, Stereo, 16-bit */

        /* dwChannelMask: channel location for WAVEFORMATEX */
        public const int SPEAKER_FRONT_LEFT     = 0x1;
        public const int SPEAKER_FRONT_RIGHT    = 0x2;
        public const int SPEAKER_FRONT_CENTER   = 0x4;
        public const int SPEAKER_LOW_FREQUENCY  = 0x8;
        public const int SPEAKER_BACK_LEFT      = 0x10;
        public const int SPEAKER_BACK_RIGHT     = 0x20;
        public const int SPEAKER_FRONT_LEFT_OF_CENTER = 0x40;
        public const int SPEAKER_FRONT_RIGHT_OF_CENTER = 0x80;
        public const int SPEAKER_BACK_CENTER    = 0x100;
        public const int SPEAKER_SIDE_LEFT      = 0x200;
        public const int SPEAKER_SIDE_RIGHT     = 0x400;
        public const int SPEAKER_TOP_CENTER     = 0x800;
        public const int SPEAKER_TOP_FRONT_LEFT = 0x1000;
        public const int SPEAKER_TOP_FRONT_CENTER = 0x2000;
        public const int SPEAKER_TOP_FRONT_RIGHT = 0x4000;
        public const int SPEAKER_TOP_BACK_LEFT  = 0x8000;
        public const int SPEAKER_TOP_BACK_CENTER = 0x10000;
        public const int SPEAKER_TOP_BACK_RIGHT = 0x20000;
        public const int SPEAKER_RESERVED       = unchecked((int)0x80000000);

        /* general error return values */
        public const int MMSYSERR_BASE          = 0;
        public const int MMSYSERR_NOERROR       = 0;
        public const int MMSYSERR_ERROR         = (WaveConstants.MMSYSERR_BASE + 1);
        public const int MMSYSERR_BADDEVICEID   = (WaveConstants.MMSYSERR_BASE + 2);
        public const int MMSYSERR_NOTENABLED    = (WaveConstants.MMSYSERR_BASE + 3);
        public const int MMSYSERR_ALLOCATED     = (WaveConstants.MMSYSERR_BASE + 4);
        public const int MMSYSERR_INVALHANDLE   = (WaveConstants.MMSYSERR_BASE + 5);
        public const int MMSYSERR_NODRIVER      = (WaveConstants.MMSYSERR_BASE + 6);
        public const int MMSYSERR_NOMEM         = (WaveConstants.MMSYSERR_BASE + 7);
        public const int MMSYSERR_NOTSUPPORTED  = (WaveConstants.MMSYSERR_BASE + 8);
        public const int MMSYSERR_BADERRNUM     = (WaveConstants.MMSYSERR_BASE + 9);
        public const int MMSYSERR_INVALFLAG     = (WaveConstants.MMSYSERR_BASE + 10);
        public const int MMSYSERR_INVALPARAM    = (WaveConstants.MMSYSERR_BASE + 11);
        public const int MMSYSERR_HANDLEBUSY    = (WaveConstants.MMSYSERR_BASE + 12);
        public const int MMSYSERR_INVALIDALIAS  = (WaveConstants.MMSYSERR_BASE + 13);
        public const int MMSYSERR_BADDB         = (WaveConstants.MMSYSERR_BASE + 14);
        public const int MMSYSERR_KEYNOTFOUND   = (WaveConstants.MMSYSERR_BASE + 15);
        public const int MMSYSERR_READERROR     = (WaveConstants.MMSYSERR_BASE + 16);
        public const int MMSYSERR_WRITEERROR    = (WaveConstants.MMSYSERR_BASE + 17);
        public const int MMSYSERR_DELETEERROR   = (WaveConstants.MMSYSERR_BASE + 18);
        public const int MMSYSERR_VALNOTFOUND   = (WaveConstants.MMSYSERR_BASE + 19);
        public const int MMSYSERR_NODRIVERCB    = (WaveConstants.MMSYSERR_BASE + 20);
        public const int MMSYSERR_MOREDATA      = (WaveConstants.MMSYSERR_BASE + 21);
        public const int MMSYSERR_LASTERROR     = (WaveConstants.MMSYSERR_BASE + 21);
    }

    sealed public class MixerConstants
    {
        /* mixer handle type */
        public const int MIXER_OBJECTF_HANDLE  = unchecked((int)0x80000000);
        public const int MIXER_OBJECTF_MIXER   = 0x00000000;
        public const int MIXER_OBJECTF_HMIXER  = MixerConstants.MIXER_OBJECTF_HANDLE | MixerConstants.MIXER_OBJECTF_MIXER;
        public const int MIXER_OBJECTF_WAVEOUT = 0x10000000;
        public const int MIXER_OBJECTF_HWAVEOUT = MixerConstants.MIXER_OBJECTF_HANDLE | MixerConstants.MIXER_OBJECTF_WAVEOUT;
        public const int MIXER_OBJECTF_WAVEIN  = 0x20000000;
        public const int MIXER_OBJECTF_HWAVEIN = MixerConstants.MIXER_OBJECTF_HANDLE | MixerConstants.MIXER_OBJECTF_WAVEIN;
        public const int MIXER_OBJECTF_MIDIOUT = 0x30000000;
        public const int MIXER_OBJECTF_HMIDIOUT = MixerConstants.MIXER_OBJECTF_HANDLE | MixerConstants.MIXER_OBJECTF_MIDIOUT;
        public const int MIXER_OBJECTF_MIDIIN  = 0x40000000;
        public const int MIXER_OBJECTF_HMIDIIN = MixerConstants.MIXER_OBJECTF_HANDLE | MixerConstants.MIXER_OBJECTF_MIDIIN;
        public const int MIXER_OBJECTF_AUX = 0x50000000;

        /*  MIXERLINE.fdwLine */
        public const int MIXERLINE_LINEF_ACTIVE = 0x00000001;
        public const int MIXERLINE_LINEF_DISCONNECTED = 0x00008000;
        public const int MIXERLINE_LINEF_SOURCE = unchecked((int)0x80000000);

        /*  MIXERLINE.dwComponentType */
        public const int MIXERLINE_COMPONENTTYPE_DST_FIRST      = 0x00000000;
        public const int MIXERLINE_COMPONENTTYPE_DST_UNDEFINED  = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 0);
        public const int MIXERLINE_COMPONENTTYPE_DST_DIGITAL    = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 1);
        public const int MIXERLINE_COMPONENTTYPE_DST_LINE       = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 2);
        public const int MIXERLINE_COMPONENTTYPE_DST_MONITOR    = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 3);
        public const int MIXERLINE_COMPONENTTYPE_DST_SPEAKERS   = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 4);
        public const int MIXERLINE_COMPONENTTYPE_DST_HEADPHONES = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 5);
        public const int MIXERLINE_COMPONENTTYPE_DST_TELEPHONE  = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 6);
        public const int MIXERLINE_COMPONENTTYPE_DST_WAVEIN     = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 7);
        public const int MIXERLINE_COMPONENTTYPE_DST_VOICEIN    = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 8);
        public const int MIXERLINE_COMPONENTTYPE_DST_LAST       = (MixerConstants.MIXERLINE_COMPONENTTYPE_DST_FIRST + 8);
        public const int MIXERLINE_COMPONENTTYPE_SRC_FIRST      = 0x00001000;
        public const int MIXERLINE_COMPONENTTYPE_SRC_UNDEFINED  = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 0);
        public const int MIXERLINE_COMPONENTTYPE_SRC_DIGITAL    = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 1);
        public const int MIXERLINE_COMPONENTTYPE_SRC_LINE       = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 2);
        public const int MIXERLINE_COMPONENTTYPE_SRC_MICROPHONE = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 3);
        public const int MIXERLINE_COMPONENTTYPE_SRC_SYNTHESIZER = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 4);
        public const int MIXERLINE_COMPONENTTYPE_SRC_COMPACTDISC = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 5);
        public const int MIXERLINE_COMPONENTTYPE_SRC_TELEPHONE  = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 6);
        public const int MIXERLINE_COMPONENTTYPE_SRC_PCSPEAKER  = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 7);
        public const int MIXERLINE_COMPONENTTYPE_SRC_WAVEOUT    = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 8);
        public const int MIXERLINE_COMPONENTTYPE_SRC_AUXILIARY  = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 9);
        public const int MIXERLINE_COMPONENTTYPE_SRC_ANALOG     = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 10);
        public const int MIXERLINE_COMPONENTTYPE_SRC_LAST       = (MixerConstants.MIXERLINE_COMPONENTTYPE_SRC_FIRST + 10);

        /*  MIXERLINE.Target.dwType */
        public const int MIXERLINE_TARGETTYPE_UNDEFINED     = 0;
        public const int MIXERLINE_TARGETTYPE_WAVEOUT       = 1;
        public const int MIXERLINE_TARGETTYPE_WAVEIN        = 2;
        public const int MIXERLINE_TARGETTYPE_MIDIOUT       = 3;
        public const int MIXERLINE_TARGETTYPE_MIDIIN        = 4;
        public const int MIXERLINE_TARGETTYPE_AUX           = 5;

        public const int MIXER_GETLINEINFOF_DESTINATION     = 0;
        public const int MIXER_GETLINEINFOF_SOURCE          = 1;
        public const int MIXER_GETLINEINFOF_LINEID          = 2;
        public const int MIXER_GETLINEINFOF_COMPONENTTYPE   = 3;
        public const int MIXER_GETLINEINFOF_TARGETTYPE      = 4;
        public const int MIXER_GETLINEINFOF_QUERYMASK       = 15;

        /*  MIXERCONTROL.fdwControl */
        public const int MIXERCONTROL_CONTROLF_UNIFORM      = 1;
        public const int MIXERCONTROL_CONTROLF_MULTIPLE     = 2;
        public const int MIXERCONTROL_CONTROLF_DISABLED     = unchecked((int)0x80000000);

        /*  MIXERCONTROL_CONTROLTYPE_xxx building block defines */
        public const int MIXERCONTROL_CT_CLASS_MASK         = unchecked((int)0xF0000000);
        public const int MIXERCONTROL_CT_CLASS_CUSTOM       = unchecked((int)0x00000000);
        public const int MIXERCONTROL_CT_CLASS_METER        = unchecked((int)0x10000000);
        public const int MIXERCONTROL_CT_CLASS_SWITCH       = unchecked((int)0x20000000);
        public const int MIXERCONTROL_CT_CLASS_NUMBER       = unchecked((int)0x30000000);
        public const int MIXERCONTROL_CT_CLASS_SLIDER       = unchecked((int)0x40000000);
        public const int MIXERCONTROL_CT_CLASS_FADER        = unchecked((int)0x50000000);
        public const int MIXERCONTROL_CT_CLASS_TIME         = unchecked((int)0x60000000);
        public const int MIXERCONTROL_CT_CLASS_LIST         = unchecked((int)0x70000000);

        public const int MIXERCONTROL_CT_SUBCLASS_MASK      = unchecked((int)0x0F000000);
        public const int MIXERCONTROL_CT_SC_SWITCH_BOOLEAN  = 0x00000000;
        public const int MIXERCONTROL_CT_SC_SWITCH_BUTTON   = unchecked((int)0x01000000);
        public const int MIXERCONTROL_CT_SC_METER_POLLED    = 0x00000000;
        public const int MIXERCONTROL_CT_SC_TIME_MICROSECS  = 0x00000000;
        public const int MIXERCONTROL_CT_SC_TIME_MILLISECS  = unchecked((int)0x01000000);
        public const int MIXERCONTROL_CT_SC_LIST_SINGLE     = 0x00000000;
        public const int MIXERCONTROL_CT_SC_LIST_MULTIPLE   = unchecked((int)0x01000000);

        public const int MIXERCONTROL_CT_UNITS_MASK         = unchecked((int)0x00FF0000);
        public const int MIXERCONTROL_CT_UNITS_CUSTOM       = 0x00000000;
        public const int MIXERCONTROL_CT_UNITS_BOOLEAN      = unchecked((int)0x00010000);
        public const int MIXERCONTROL_CT_UNITS_SIGNED       = unchecked((int)0x00020000);
        public const int MIXERCONTROL_CT_UNITS_UNSIGNED     = unchecked((int)0x00030000);
        public const int MIXERCONTROL_CT_UNITS_DECIBELS     = unchecked((int)0x00040000);
        public const int MIXERCONTROL_CT_UNITS_PERCENT      = unchecked((int)0x00050000);

        /*  Commonly used control types for specifying MIXERCONTROL.dwControlType */
        public const int MIXERCONTROL_CONTROLTYPE_CUSTOM        = (MixerConstants.MIXERCONTROL_CT_CLASS_CUSTOM | MixerConstants.MIXERCONTROL_CT_UNITS_CUSTOM);
        public const int MIXERCONTROL_CONTROLTYPE_BOOLEANMETER  = (MixerConstants.MIXERCONTROL_CT_CLASS_METER
                    | (MixerConstants.MIXERCONTROL_CT_SC_METER_POLLED | MixerConstants.MIXERCONTROL_CT_UNITS_BOOLEAN));
        public const int MIXERCONTROL_CONTROLTYPE_SIGNEDMETER   = (MixerConstants.MIXERCONTROL_CT_CLASS_METER
                    | (MixerConstants.MIXERCONTROL_CT_SC_METER_POLLED | MixerConstants.MIXERCONTROL_CT_UNITS_SIGNED));
        public const int MIXERCONTROL_CONTROLTYPE_PEAKMETER     = (MixerConstants.MIXERCONTROL_CONTROLTYPE_SIGNEDMETER + 1);
        public const int MIXERCONTROL_CONTROLTYPE_UNSIGNEDMETER = (MixerConstants.MIXERCONTROL_CT_CLASS_METER
                    | (MixerConstants.MIXERCONTROL_CT_SC_METER_POLLED | MixerConstants.MIXERCONTROL_CT_UNITS_UNSIGNED));
        public const int MIXERCONTROL_CONTROLTYPE_BOOLEAN       = (MixerConstants.MIXERCONTROL_CT_CLASS_SWITCH
                    | (MixerConstants.MIXERCONTROL_CT_SC_SWITCH_BOOLEAN | MixerConstants.MIXERCONTROL_CT_UNITS_BOOLEAN));
        public const int MIXERCONTROL_CONTROLTYPE_ONOFF         = (MixerConstants.MIXERCONTROL_CONTROLTYPE_BOOLEAN + 1);
        public const int MIXERCONTROL_CONTROLTYPE_MUTE          = (MixerConstants.MIXERCONTROL_CONTROLTYPE_BOOLEAN + 2);
        public const int MIXERCONTROL_CONTROLTYPE_MONO          = (MixerConstants.MIXERCONTROL_CONTROLTYPE_BOOLEAN + 3);
        public const int MIXERCONTROL_CONTROLTYPE_LOUDNESS      = (MixerConstants.MIXERCONTROL_CONTROLTYPE_BOOLEAN + 4);
        public const int MIXERCONTROL_CONTROLTYPE_STEREOENH     = (MixerConstants.MIXERCONTROL_CONTROLTYPE_BOOLEAN + 5);
        public const int MIXERCONTROL_CONTROLTYPE_BASS_BOOST    = (MixerConstants.MIXERCONTROL_CONTROLTYPE_BOOLEAN + 8823);
        public const int MIXERCONTROL_CONTROLTYPE_BUTTON        = (MixerConstants.MIXERCONTROL_CT_CLASS_SWITCH
                    | (MixerConstants.MIXERCONTROL_CT_SC_SWITCH_BUTTON | MixerConstants.MIXERCONTROL_CT_UNITS_BOOLEAN));
        public const int MIXERCONTROL_CONTROLTYPE_DECIBELS      = (MixerConstants.MIXERCONTROL_CT_CLASS_NUMBER | MixerConstants.MIXERCONTROL_CT_UNITS_DECIBELS);
        public const int MIXERCONTROL_CONTROLTYPE_SIGNED        = (MixerConstants.MIXERCONTROL_CT_CLASS_NUMBER | MixerConstants.MIXERCONTROL_CT_UNITS_SIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_UNSIGNED      = (MixerConstants.MIXERCONTROL_CT_CLASS_NUMBER | MixerConstants.MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_PERCENT       = (MixerConstants.MIXERCONTROL_CT_CLASS_NUMBER | MixerConstants.MIXERCONTROL_CT_UNITS_PERCENT);
        public const int MIXERCONTROL_CONTROLTYPE_SLIDER        = (MixerConstants.MIXERCONTROL_CT_CLASS_SLIDER | MixerConstants.MIXERCONTROL_CT_UNITS_SIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_PAN           = (MixerConstants.MIXERCONTROL_CONTROLTYPE_SLIDER + 1);
        public const int MIXERCONTROL_CONTROLTYPE_QSOUNDPAN     = (MixerConstants.MIXERCONTROL_CONTROLTYPE_SLIDER + 2);
        public const int MIXERCONTROL_CONTROLTYPE_FADER         = (MixerConstants.MIXERCONTROL_CT_CLASS_FADER | MixerConstants.MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_VOLUME        = (MixerConstants.MIXERCONTROL_CONTROLTYPE_FADER + 1);
        public const int MIXERCONTROL_CONTROLTYPE_BASS          = (MixerConstants.MIXERCONTROL_CONTROLTYPE_FADER + 2);
        public const int MIXERCONTROL_CONTROLTYPE_TREBLE        = (MixerConstants.MIXERCONTROL_CONTROLTYPE_FADER + 3);
        public const int MIXERCONTROL_CONTROLTYPE_EQUALIZER     = (MixerConstants.MIXERCONTROL_CONTROLTYPE_FADER + 4);
        public const int MIXERCONTROL_CONTROLTYPE_SINGLESELECT  = (MixerConstants.MIXERCONTROL_CT_CLASS_LIST
                    | (MixerConstants.MIXERCONTROL_CT_SC_LIST_SINGLE | MixerConstants.MIXERCONTROL_CT_UNITS_BOOLEAN));
        public const int MIXERCONTROL_CONTROLTYPE_MUX           = (MixerConstants.MIXERCONTROL_CONTROLTYPE_SINGLESELECT + 1);
        public const int MIXERCONTROL_CONTROLTYPE_MULTIPLESELECT = (MixerConstants.MIXERCONTROL_CT_CLASS_LIST
                    | (MixerConstants.MIXERCONTROL_CT_SC_LIST_MULTIPLE | MixerConstants.MIXERCONTROL_CT_UNITS_BOOLEAN));
        public const int MIXERCONTROL_CONTROLTYPE_MIXER         = (MixerConstants.MIXERCONTROL_CONTROLTYPE_MULTIPLESELECT + 1);
        public const int MIXERCONTROL_CONTROLTYPE_MICROTIME     = (MixerConstants.MIXERCONTROL_CT_CLASS_TIME
                    | (MixerConstants.MIXERCONTROL_CT_SC_TIME_MICROSECS | MixerConstants.MIXERCONTROL_CT_UNITS_UNSIGNED));
        public const int MIXERCONTROL_CONTROLTYPE_MILLITIME     = (MixerConstants.MIXERCONTROL_CT_CLASS_TIME
                    | (MixerConstants.MIXERCONTROL_CT_SC_TIME_MILLISECS | MixerConstants.MIXERCONTROL_CT_UNITS_UNSIGNED));

        public const int MIXER_GETLINECONTROLSF_ALL             = 0x00000000;
        public const int MIXER_GETLINECONTROLSF_ONEBYID         = 0x00000001;
        public const int MIXER_GETLINECONTROLSF_ONEBYTYPE       = 0x00000002;
        public const int MIXER_GETLINECONTROLSF_QUERYMASK       = 0x0000000F;

        public const int MIXER_GETCONTROLDETAILSF_VALUE         = 0x00000000;
        public const int MIXER_GETCONTROLDETAILSF_LISTTEXT      = 0x00000001;
        public const int MIXER_GETCONTROLDETAILSF_QUERYMASK     = 0x0000000F;

        public const int MIXER_SETCONTROLDETAILSF_VALUE         = 0x00000000;
        public const int MIXER_SETCONTROLDETAILSF_CUSTOM        = 0x00000001;
        public const int MIXER_SETCONTROLDETAILSF_QUERYMASK     = 0x0000000F;

        public const int MM_MIM_MOREDATA                        = 0x3CC;
        public const int MM_MIXM_LINE_CHANGE                    = 0x3D0;
        public const int MM_MIXM_CONTROL_CHANGE                 = 0x3D1;
    }

    /**
     * Driver Callback
     **/
    [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
    [SuppressUnmanagedCodeSecurityAttribute]
    public delegate void DriverCallback(IntPtr hdrvr, int uMsg, IntPtr dwUser, IntPtr dw1, IntPtr dw2);

    /**
     * WAVEFORMATEX structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, Pack=2)]
    public struct tWAVEFORMATEX
    {
        public short wFormatTag;        /* format type */
        public short nChannels;         /* number of channels (i.e. mono, stereo...) */
        public int   nSamplesPerSec;    /* sample rate */
        public int   nAvgBytesPerSec;   /* for buffer estimation */
        public short nBlockAlign;       /* block size of data */
        public short wBitsPerSample;    /* number of bits per sample of mono data */
        public short cbSize;            /* the count in bytes of the size of */
                                        /* extra information (after cbSize) */
    }

    /**
     * WAVEFORMATEXTENSIBLE structure
     * (same as WAVEFORMATEXTENSIBLE)
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tWAVEFORMATEXTENSIBLE
    {
        public tWAVEFORMATEX Format;        /* format */
        public unionSamples Samples;        /* Samples */
        public int dwChannelMask;           /* which channels are present in stream */
        public Guid SubFormat;              /* Subformat */
    }

    /**
     * unionSamples
     * (union)
     **/
    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct unionSamples
    {
        [FieldOffsetAttribute(0)]
        public short wValidBitsPerSample;  /* bits of precision  */

        [FieldOffsetAttribute(0)]
        public short wSamplesPerBlock;     /* valid if wBitsPerSample==0 */

        [FieldOffsetAttribute(0)]
        public short wReserved;            /* If neither applies, set to zero. */
    }

    /**
     * WAVEHDR structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tWAVEHDR
    {
        public IntPtr lpData;           /* pointer to locked data buffer */
        public int    dwBufferLength;   /* length of data buffer */
        public int    dwBytesRecorded;  /* used for input only */
        public int    dwUser;           /* for client's use */
        public int    dwFlags;          /* assorted flags (see defines) */
        public int    dwLoops;          /* loop control counter */
        public IntPtr lpNext;           /* reserved for driver */
        public IntPtr reserved;         /* reserved for driver */
    }

    /**
     * WAVEINCAPSA structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tWAVEINCAPSA
    {
        public short wMid;              /* manufacturer ID */
        public short wPid;              /* product ID */
        public int   vDriverVersion;    /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name (NULL terminated string) */

        public int   dwFormats;         /* formats supported */
        public short wChannels;         /* number of channels supported */
        public short wReserved1;        /* structure packing */
    }

    /**
     * WAVEINCAPSW structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tWAVEINCAPSW
    {
        public short wMid;              /* manufacturer ID */
        public short wPid;              /* product ID */
        public int   vDriverVersion;    /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name (NULL terminated string) */

        public int   dwFormats;         /* formats supported */
        public short wChannels;         /* number of channels supported */
        public short wReserved1;        /* structure packing */
    }

    /**
     * WAVEINCAPS2A structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tWAVEINCAPS2A
    {
        public short wMid;              /* manufacturer ID */
        public short wPid;              /* product ID */
        public int   vDriverVersion;    /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name (NULL terminated string) */

        public int   dwFormats;         /* formats supported */
        public short wChannels;         /* number of channels supported */
        public short wReserved1;        /* structure packing */
        public Guid  ManufacturerGuid;  /* for extensible MID mapping */
        public Guid  ProductGuid;       /* for extensible PID mapping */
        public Guid  NameGuid;          /* for name lookup in registry */
    }

    /**
     * WAVEINCAPS2W structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tWAVEINCAPS2W
    {
        public short wMid;              /* manufacturer ID */
        public short wPid;              /* product ID */
        public int   vDriverVersion;    /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name (NULL terminated string) */

        public int   dwFormats;         /* formats supported */
        public short wChannels;         /* number of sources supported */
        public short wReserved1;        /* structure packing */

        public Guid ManufacturerGuid;   /* for extensible MID mapping */
        public Guid ProductGuid;        /* for extensible PID mapping */
        public Guid NameGuid;           /* for name lookup in registry */
    }

    /**
     * WAVEOUTCAPSA structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tWAVEOUTCAPSA
    {
        public short wMid;              /* manufacturer ID */
        public short wPid;              /* product ID */
        public int   vDriverVersion;    /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name (NULL terminated string) */

        public int   dwFormats;         /* formats supported */
        public short wChannels;         /* number of sources supported */
        public short wReserved1;        /* packing */
        public int   dwSupport;         /* functionality supported by driver */
    }

    /**
     * WAVEOUTCAPSW structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tWAVEOUTCAPSW
    {
        public short wMid;              /* manufacturer ID */
        public short wPid;              /* product ID */
        public int   vDriverVersion;    /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name (NULL terminated string) */

        public int   dwFormats;         /* formats supported */
        public short wChannels;         /* number of sources supported */
        public short wReserved1;        /* packing */
        public int   dwSupport;         /* functionality supported by driver */
    }

    /**
     * MMTIME structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMMTIME
    {
        public int wType;           /* indicates the contents of the union */
        public unionU u;
    }

    /**
     * MMTIME internal union
     **/
    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct unionU
    {
        [FieldOffsetAttribute(0)]
        public int ms;              /* milliseconds */

        [FieldOffsetAttribute(0)]
        public int sample;          /* samples */

        [FieldOffsetAttribute(0)]
        public int cb;              /* byte count */

        [FieldOffsetAttribute(0)]
        public int ticks;           /* ticks in MIDI stream */

        [FieldOffsetAttribute(0)]
        public structSmpte smpte;

        [FieldOffsetAttribute(0)]
        public structMidi midi;
    }

    /**
     * smte internal structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct structSmpte
    {
        public byte hour;       /* hours */
        public byte min;        /* minutes */
        public byte sec;        /* seconds */
        public byte frame;      /* frames */
        public byte fps;        /* frames per second */
        public byte dummy;
        public byte pad0;
        public byte pad1;
    }

    /**
     * midi internal structure
     */
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct structMidi
    {
        public int songptrpos;  /* song pointer position */
    }

    /// <summary>
    /// Wave Audio Input
    /// Multimedia Recording
    /// </summary>
    sealed public class WaveInput
    {
        private WaveInput()
        {
            // nothing to construct
        }

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInGetID")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInGetID(IntPtr hwi, ref int puDeviceID);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInOpen")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInOpen(ref IntPtr phwi, int uDeviceID, ref tWAVEFORMATEX pwfx, IntPtr dwCallback, IntPtr dwInstance, int fdwOpen);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInClose")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInClose(IntPtr hwi);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInStart")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInStart(IntPtr hwi);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInStop")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInStop(IntPtr hwi);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInReset")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInReset(IntPtr hwi);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInPrepareHeader")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInPrepareHeader(IntPtr hwi, ref tWAVEHDR pwh, int cbwh);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInUnprepareHeader")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInUnprepareHeader(IntPtr hwi, ref tWAVEHDR pwh, int cbwh);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInAddBuffer")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInAddBuffer(IntPtr hwi, ref tWAVEHDR pwh, int cbwh);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInMessage")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInMessage(IntPtr hwi, int uMsg, IntPtr dw1, IntPtr dw2);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInGetDevCapsA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInGetDevCapsA([MarshalAsAttribute(UnmanagedType.I4)] int uDeviceID, ref tWAVEINCAPSA pwic, int cbwic);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInGetDevCapsW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInGetDevCapsW([MarshalAsAttribute(UnmanagedType.I4)] int uDeviceID, ref tWAVEINCAPSW pwic, int cbwic);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInGetPosition")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInGetPosition(IntPtr hwi, ref tMMTIME pmmt, int cbmmt);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInGetNumDevs")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInGetNumDevs();

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInGetErrorTextA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInGetErrorTextA(int mmrError, [MarshalAsAttribute(UnmanagedType.LPStr)] StringBuilder pszText, int cchText);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveInGetErrorTextW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveInGetErrorTextW(int mmrError, [MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder pszText, int cchText);
    }

    /// <summary>
    /// Wave Audio Output
    /// Multimedia Playback
    /// </summary>
    sealed public class WaveOutput
    {
        private WaveOutput()
        {
            // nothing to construct
        }

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetID")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetID(IntPtr hwo, ref int puDeviceID);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutOpen")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutOpen(ref IntPtr phwo, int uDeviceID, ref tWAVEFORMATEX pwfx, IntPtr dwCallback, IntPtr dwInstance, int fdwOpen);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutClose")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutClose(IntPtr hwo);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutPause")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutPause(IntPtr hwo);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutRestart")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutRestart(IntPtr hwo);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutReset")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutReset(IntPtr hwo);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutPrepareHeader")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutPrepareHeader(IntPtr hwo, ref tWAVEHDR pwh, int cbwh);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutUnprepareHeader")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutUnprepareHeader(IntPtr hwo, ref tWAVEHDR pwh, int cbwh);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutWrite")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutWrite(IntPtr hwo, ref tWAVEHDR pwh, int cbwh);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutBreakLoop")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutBreakLoop(IntPtr hwo);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetPitch")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetPitch(IntPtr hwo, ref int pdwPitch);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutSetPitch")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutSetPitch(IntPtr hwo, int dwPitch);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetVolume")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetVolume(IntPtr hwo, ref int pdwVolume);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutSetVolume")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutSetVolume(IntPtr hwo, int dwVolume);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutMessage")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutMessage(IntPtr hwo, int uMsg, IntPtr dw1, IntPtr dw2);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetDevCapsA")]
        [SuppressUnmanagedCodeSecurityAttribute]        
        //public static extern int waveInGetDevCapsA(int uDeviceID, ref tWAVEOUTCAPSA pwic, int cbwic);
        public static extern int waveOutGetDevCapsA([MarshalAsAttribute(UnmanagedType.I4)] int uDeviceID, ref tWAVEOUTCAPSA pwoc, int cbwoc);        

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetDevCapsW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetDevCapsW([MarshalAsAttribute(UnmanagedType.SysUInt)] int uDeviceID, ref tWAVEOUTCAPSW pwoc, int cbwoc);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetPosition")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetPosition(IntPtr hwo, ref tMMTIME pmmt, int cbmmt);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetPlaybackRate")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetPlaybackRate(IntPtr hwo, ref int pdwRate);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutSetPlaybackRate")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutSetPlaybackRate(IntPtr hwo, int dwRate);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetNumDevs")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetNumDevs();

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetErrorTextA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetErrorTextA(int mmrError, [MarshalAsAttribute(UnmanagedType.LPStr)] StringBuilder pszText, int cchText);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "waveOutGetErrorTextW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int waveOutGetErrorTextW(int mmrError, [MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder pszText, int cchText);
    }

    /**
     * MIXERCAPSA structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tMIXERCAPSA
    {
        public short wMid;              /* manufacturer id */
        public short wPid;              /* product id */
        public int vDriverVersion;      /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name */

        public int fdwSupport;          /* misc. support bits */
        public int cDestinations;       /* count of destinations */
    }

    /**
     * MIXERCAPSW structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tMIXERCAPSW
    {
        public short wMid;              /* manufacturer id */
        public short wPid;              /* product id */
        public int vDriverVersion;      /* version of the driver */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /* product name */

        public int fdwSupport;          /* misc. support bits */
        public int cDestinations;       /* count of destinations */
    }

    /**
     * MIXERLINEA structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tMIXERLINEA
    {
        public int cbStruct;            /* size of MIXERLINE structure */
        public int dwDestination;       /* zero based destination index */
        public int dwSource;            /* zero based source index (if source) */
        public int dwLineID;            /* unique line id for mixer device */
        public int fdwLine;             /* state/information about line */
        public int dwUser;              /* driver specific information */
        public int dwComponentType;     /* component type line connects to */
        public int cChannels;           /* number of channels line supports */
        public int cConnections;        /* number of connections [possible] */
        public int cControls;           /* number of controls at this line */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szShortName;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szName;

        public tMIXERLINEA_Target Target;
    }

    /**
     * tMIXERLINEA_Target
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tMIXERLINEA_Target
    {
        public int dwType;              /* MIXERLINE_TARGETTYPE_xxxx */
        public int dwDeviceID;          /* target device ID of device type */
        public short wMid;              /* of target device */
        public short wPid;              /*      " */
        public int vDriverVersion;      /*      " */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /*      " */
    }

    /**
     * MIXERLINEW structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tMIXERLINEW
    {
        public int cbStruct;            /* size of MIXERLINE structure */
        public int dwDestination;       /* zero based destination index */
        public int dwSource;            /* zero based source index (if source) */
        public int dwLineID;            /* unique line id for mixer device */
        public int fdwLine;             /* state/information about line */
        public int dwUser;              /* driver specific information */
        public int dwComponentType;     /* component type line connects to */
        public int cChannels;           /* number of channels line supports */
        public int cConnections;        /* number of connections [possible] */
        public int cControls;           /* number of controls at this line */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szShortName;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szName;

        public tMIXERLINEW_Target Target;
    }

    /**
     * tMIXERLINEW_Target
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tMIXERLINEW_Target
    {
        public int dwType;              /* MIXERLINE_TARGETTYPE_xxxx */
        public int dwDeviceID;          /* target device ID of device type */
        public short wMid;              /* of target device */
        public short wPid;              /*      " */
        public int vDriverVersion;      /*      " */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /*      " */
    }

    /**
     * MIXERLINECONTROLS structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERLINECONTROLSA
    {
        public int cbStruct;            /* size in bytes of MIXERLINECONTROLS */
        public int dwLineID;            /* line id (from MIXERLINE.dwLineID) */
        public tMIXERLINECONTROLSA_DUMMY Union1;
        public int cControls;           /* count of controls pmxctrl points to */
        public int cbmxctrl;            /* size in bytes of _one_ MIXERCONTROL */
        public IntPtr pamxctrl;         /* pointer to first MIXERCONTROL array */
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct tMIXERLINECONTROLSA_DUMMY
    {
        [FieldOffsetAttribute(0)]
        public int dwControlID;         /* MIXER_GETLINECONTROLSF_ONEBYID */

        [FieldOffsetAttribute(0)]
        public int dwControlType;       /* MIXER_GETLINECONTROLSF_ONEBYTYPE */
    }

    /**
     * MIXERLINECONTROLSW structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERLINECONTROLSW
    {
        public int cbStruct;        /* size in bytes of MIXERLINECONTROLS */
        public int dwLineID;        /* line id (from MIXERLINE.dwLineID) */
        public tMIXERLINECONTROLSW_TYPE cType;
        public int cControls;       /* count of controls pmxctrl points to */
        public int cbmxctrl;        /* size in bytes of _one_ MIXERCONTROL */
        public IntPtr pamxctrl;     /* pointer to first MIXERCONTROL array */
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct tMIXERLINECONTROLSW_TYPE
    {
        [FieldOffsetAttribute(0)]
        public int dwControlID;     /* MIXER_GETLINECONTROLSF_ONEBYID */

        [FieldOffsetAttribute(0)]
        public int dwControlType;   /* MIXER_GETLINECONTROLSF_ONEBYTYPE */
    }

    /**
     * MIXERCONTROLA structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tMIXERCONTROLA
    {
        public int cbStruct;            /* size in bytes of MIXERCONTROL */
        public int dwControlID;         /* unique control id for mixer device */
        public int dwControlType;       /* MIXERCONTROL_CONTROLTYPE_xxx */
        public int fdwControl;          /* MIXERCONTROL_CONTROLF_xxx */
        public int cMultipleItems;      /* if MIXERCONTROL_CONTROLF_MULTIPLE set */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szShortName;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szName;

        public tMIXERCONTROL_BOUNDS Bounds;
        public tMIXERCONTROL_METRICS Metrics;
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct tMIXERCONTROL_BOUNDS
    {
        [FieldOffsetAttribute(0)]
        public tMIXERCONTROL_BOUNDS1 Signed;

        [FieldOffsetAttribute(0)]
        public tMIXERCONTROL_BOUNDS2 Unsigned;

        [FieldOffsetAttribute(0)]
        public tMIXERCONTROL_DUMMYSTRUCTNAME3 Struct3; // struct size: sizeof(int)*6
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct tMIXERCONTROL_METRICS
    {
        [FieldOffsetAttribute(0)]
        public int cSteps;          /* # of steps between min & max */

        [FieldOffsetAttribute(0)]
        public int cbCustomData;    /* size in bytes of custom data */

        [FieldOffsetAttribute(0)]
        public tMIXERCONTROL_DUMMYSTRUCTNAME3 Struct3; // struct size: sizeof(int)*6
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERCONTROL_BOUNDS1
    {
        public int lMinimum;        /* signed minimum for this control */
        public int lMaximum;        /* signed maximum for this control */
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERCONTROL_BOUNDS2
    {
        public int dwMinimum;       /* unsigned minimum for this control */
        public int dwMaximum;       /* unsigned maximum for this control */
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERCONTROL_DUMMYSTRUCTNAME3
    {
        public int dwDummy1;
        public int dwDummy2;
        public int dwDummy3;
        public int dwDummy4;
        public int dwDummy5;
        public int dwDummy6;
    }

    /**
     * MIXERCONTROLW structure
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct tMIXERCONTROLW
    {
        public int cbStruct;            /* size in bytes of MIXERCONTROL */
        public int dwControlID;         /* unique control id for mixer device */
        public int dwControlType;       /* MIXERCONTROL_CONTROLTYPE_xxx */
        public int fdwControl;          /* MIXERCONTROL_CONTROLF_xxx */
        public int cMultipleItems;      /* if MIXERCONTROL_CONTROLF_MULTIPLE set */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szShortName;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szName;

        public tMIXERCONTROL_BOUNDS Bounds;
        public tMIXERCONTROL_METRICS Metrics;
    }

    /**
     * MIXERCONTROLDETAILS
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERCONTROLDETAILS
    {
        public int cbStruct;        /* size in bytes of MIXERCONTROLDETAILS */
        public int dwControlID;     /* control id to get/set details on */
        public int cChannels;       /* number of channels in paDetails array */
        public tMIXERCONTROLDETAILS_FLAGS Flags;
        public int cbDetails;       /* size of _one_ details_XX struct */
        public IntPtr paDetails;    /* pointer to array of details_XX structs */
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct tMIXERCONTROLDETAILS_FLAGS
    {
        [FieldOffsetAttribute(0)]
        public IntPtr hwndOwner;    /* for MIXER_SETCONTROLDETAILSF_CUSTOM */

        [FieldOffsetAttribute(0)]
        public int cMultipleItems;  /* if _MULTIPLE, the number of items per channel */
    }

    /**
     * MIXERCONTROLDETAILS_LISTTEXT
     **/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tMIXERCONTROLDETAILS_LISTTEXT
    {
        public int dwParam1;
        public int dwParam2;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szName;
    }

    /**
     * MIXERCONTROLDETAILS_BOOLEAN
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERCONTROLDETAILS_BOOLEAN
    {
        public int fValue;
    }

    /**
     * MIXERCONTROLDETAILS_SIGNED
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERCONTROLDETAILS_SIGNED
    {
        public int lValue;
    }

    /**
     * MIXERCONTROLDETAILS_UNSIGNED
     **/
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tMIXERCONTROLDETAILS_UNSIGNED
    {
        public int dwValue;
    }

    /// <summary>
    /// Mixer device
    /// </summary>
    sealed class MixerDevice
    {
        private MixerDevice()
        {
            // nothing to construct
        }

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerOpen")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerOpen(ref IntPtr phmx, int uMxId, IntPtr dwCallback, IntPtr dwInstance, int fdwOpen);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerClose")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerClose(IntPtr hmx);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetID")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetID(IntPtr hmxobj, ref int puMxId, int fdwId);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerMessage")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerMessage(IntPtr hmx, int uMsg, IntPtr dwParam1, IntPtr dwParam2);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetNumDevs")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetNumDevs();

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetDevCapsA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetDevCapsA(IntPtr hmxobj, ref tMIXERCAPSA pmxcaps, int cbmxcaps);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetDevCapsW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetDevCapsW(IntPtr hmxobj, ref tMIXERCAPSW pmxcaps, int cbmxcaps);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetLineInfoA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetLineInfoA(IntPtr hmxobj, ref tMIXERLINEA pmxl, int fdwInfo);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetLineInfoW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetLineInfoW(IntPtr hmxobj, ref tMIXERLINEW pmxl, int fdwInfo);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetLineControlsA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetLineControlsA(IntPtr hmxobj, ref tMIXERLINECONTROLSA pmxlc, int fdwControls);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetLineControlsW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetLineControlsW(IntPtr hmxobj, ref tMIXERLINECONTROLSW pmxlc, int fdwControls);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerSetControlDetails")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerSetControlDetails(IntPtr hmxobj, ref tMIXERCONTROLDETAILS pmxcd, int fdwDetails);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetControlDetailsA")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetControlDetailsA(IntPtr hmxobj, ref tMIXERCONTROLDETAILS pmxcd, int fdwDetails);

        [DllImportAttribute(WaveAudioLib.LibName, EntryPoint = "mixerGetControlDetailsW")]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int mixerGetControlDetailsW(IntPtr hmxobj, ref tMIXERCONTROLDETAILS pmxcd, int fdwDetails);
    }
}
