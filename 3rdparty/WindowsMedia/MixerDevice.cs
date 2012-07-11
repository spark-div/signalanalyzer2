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

namespace Ernzo.Windows.WaveAudio
{
    public class MixerControlDetails
    {
        internal tMIXERCONTROLDETAILS mxDetails;
        public MixerControlDetails()
        {
            mxDetails.cbStruct = Size;
        }
        public MixerControlDetails(tMIXERCONTROLDETAILS details)
        {
            mxDetails = details;
        }
        public int ControlID
        {
            set { mxDetails.dwControlID = value; }
            get { return mxDetails.dwControlID; }
        }
        public int NumChannels
        {
            set { mxDetails.cChannels = value; }
            get { return mxDetails.cChannels; }
        }
        public IntPtr HwndOwner
        {
            set { mxDetails.Flags.hwndOwner = value; }
            get { return mxDetails.Flags.hwndOwner; }
        }
        public int MultipleItems
        {
            set { mxDetails.Flags.cMultipleItems = value; }
            get { return mxDetails.Flags.cMultipleItems; }
        }
        public int CbDetails
        {
            set { mxDetails.cbDetails = value; }
            get { return mxDetails.cbDetails; }
        }
        public IntPtr DataArray
        {
            set { mxDetails.paDetails = value; }
            get { return mxDetails.paDetails; }
        }
        public int CbStruct
        {
            set { mxDetails.cbStruct = value; }
            get { return mxDetails.cbStruct; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tMIXERCONTROLDETAILS));
            }
        }
        public static explicit operator tMIXERCONTROLDETAILS(MixerControlDetails details)
        {
            return details.mxDetails;
        }
    }

    /// <summary>
    /// MixerControl
    /// </summary>
    public class MixerControl
    {
        internal tMIXERCONTROLW mxCtrl;
        public MixerControl()
        {
            mxCtrl.cbStruct = Size;
        }
        public MixerControl(tMIXERCONTROLW ctrl)
        {
            mxCtrl = ctrl;
        }
        public int ControlID
        {
            set { mxCtrl.dwControlID = value; }
            get { return mxCtrl.dwControlID; }
        }
        public int ControlType
        {
            set { mxCtrl.dwControlType = value; }
            get { return mxCtrl.dwControlType; }
        }
        public int ControlFlag
        {
            set { mxCtrl.fdwControl = value; }
            get { return mxCtrl.fdwControl; }
        }
        public int MultipleItems
        {
            set { mxCtrl.cMultipleItems = value; }
            get { return mxCtrl.cMultipleItems; }
        }
        public string ShortName
        {
            set { mxCtrl.szShortName = value; }
            get { return mxCtrl.szShortName; }
        }
        public string Name
        {
            set { mxCtrl.szName = value; }
            get { return mxCtrl.szName; }
        }
        public tMIXERCONTROL_BOUNDS Bounds
        {
            set { mxCtrl.Bounds = value; }
            get { return mxCtrl.Bounds; }
        }
        public tMIXERCONTROL_METRICS Metrics
        {
            set { mxCtrl.Metrics = value; }
            get { return mxCtrl.Metrics; }
        }
        public int CbStruct
        {
            set { mxCtrl.cbStruct = value; }
            get { return mxCtrl.cbStruct; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tMIXERCONTROLW));
            }
        }
        public static explicit operator tMIXERCONTROLW(MixerControl mxctrl)
        {
            return mxctrl.mxCtrl;
        }
    }

    /// <summary>
    /// MixerLineControls
    /// </summary>
    public class MixerLineControls
    {
        internal tMIXERLINECONTROLSW mxLineCtrls;
        public MixerLineControls()
        {
            mxLineCtrls.cbStruct = Size;
        }
        public MixerLineControls(tMIXERLINECONTROLSW lineCtrls)
        {
            mxLineCtrls = lineCtrls;
        }
        public int LineID
        {
            set { mxLineCtrls.dwLineID = value; }
            get { return mxLineCtrls.dwLineID; }
        }
        public int ControlID
        {
            // ControlID and ControlType point to same memory
            set { mxLineCtrls.cType.dwControlID = value; }
            get { return mxLineCtrls.cType.dwControlID; }
        }
        public int ControlType
        {
            // ControlID and ControlType point to same memory
            set { mxLineCtrls.cType.dwControlType = value; }
            get { return mxLineCtrls.cType.dwControlType; }
        }
        public int NumControls
        {
            set { mxLineCtrls.cControls = value; }
            get { return mxLineCtrls.cControls; }
        }
        public int CbControl
        {
            set { mxLineCtrls.cbmxctrl = value; }
            get { return mxLineCtrls.cbmxctrl; }
        }
        public IntPtr DataArray
        {
            set { mxLineCtrls.pamxctrl = value; }
            get { return mxLineCtrls.pamxctrl; }
        }
        public int CbStruct
        {
            set { mxLineCtrls.cbStruct = value; }
            get { return mxLineCtrls.cbStruct; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tMIXERLINECONTROLSW));
            }
        }
        public static explicit operator tMIXERLINECONTROLSW(MixerLineControls mxlineCtrls)
        {
            return mxlineCtrls.mxLineCtrls;
        }
    }

    /// <summary>
    /// MixerLineInfo
    /// </summary>
    public class MixerLineInfo
    {
        internal tMIXERLINEW mxLineInfo;
        public MixerLineInfo()
        {
            mxLineInfo.cbStruct = Size;
        }
        public MixerLineInfo(tMIXERLINEW lineInfo)
        {
            mxLineInfo = lineInfo;
        }
        public int Destination
        {
            set { mxLineInfo.dwDestination = value; }
            get { return mxLineInfo.dwDestination; }
        }
        public int Source
        {
            set { mxLineInfo.dwSource = value; }
            get { return mxLineInfo.dwSource; }
        }
        public int LineID
        {
            set { mxLineInfo.dwLineID = value; }
            get { return mxLineInfo.dwLineID; }
        }
        public int LineInfo
        {
            set { mxLineInfo.fdwLine = value; }
            get { return mxLineInfo.fdwLine; }
        }
        public int UserInfo
        {
            set { mxLineInfo.dwUser = value; }
            get { return mxLineInfo.dwUser; }
        }
        public int ComponentType
        {
            set { mxLineInfo.dwComponentType = value; }
            get { return mxLineInfo.dwComponentType; }
        }
        public int NumChannels
        {
            set { mxLineInfo.cChannels = value; }
            get { return mxLineInfo.cChannels; }
        }
        public int NumConnections
        {
            set { mxLineInfo.cConnections = value; }
            get { return mxLineInfo.cConnections; }
        }
        public int NumControls
        {
            set { mxLineInfo.cControls = value; }
            get { return mxLineInfo.cControls; }
        }
        public tMIXERLINEW_Target Target
        {
            set { mxLineInfo.Target = value; }
            get { return mxLineInfo.Target; }
        }
        public int CbStruct
        {
            set { mxLineInfo.cbStruct = value; }
            get { return mxLineInfo.cbStruct; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tMIXERLINEW));
            }
        }
        public static explicit operator tMIXERLINEW(MixerLineInfo lineInfo)
        {
            return lineInfo.mxLineInfo;
        }
    }
    
    /// <summary>
    /// MixerCaps
    /// </summary>
    public class MixerCaps
    {
        internal tMIXERCAPSW mxCaps;
        public MixerCaps()
        {
        }
        public MixerCaps(tMIXERCAPSW caps)
        {
            mxCaps = caps;
        }
        public int ManufacturerId
        {
            get { return mxCaps.wMid; }
        }
        public int ProductId
        {
            get { return mxCaps.wPid; }
        }
        public int Version
        {
            get { return mxCaps.vDriverVersion; }
        }
        public int SupportFlag
        {
            get { return mxCaps.fdwSupport; }
        }
        public int Destinations
        {
            get { return mxCaps.cDestinations; }
        }
        public string ProductName
        {
            get { return mxCaps.szPname; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tMIXERCAPSW));
            }
        }
        public static explicit operator tMIXERCAPSW(MixerCaps mxc)
        {
            return mxc.mxCaps;
        }
    }

    /// <summary>
    /// WaveMixerDevice
    /// </summary>
    public class WaveMixerDevice : IDisposable
    {
        public const int INVALID_MIXER_ID = -1;
        private IntPtr  _hMixer;    // Wave Audio Mixer
        public WaveMixerDevice()
        {
            // just for consistency
        }
        ~WaveMixerDevice()
        {
            Dispose(false);
        }
        public bool IsOpen()
        {
            return (_hMixer != IntPtr.Zero);
        }
        public int GetId()
        {
            int mixerId = INVALID_MIXER_ID;
            if (IsOpen())
            {
                int mmr = MixerDevice.mixerGetID(_hMixer, ref mixerId, MixerConstants.MIXER_OBJECTF_HMIXER);
            }
            return mixerId;
        }
        public int Open(int mixerId, int fdwOpen, IntPtr hWndCallback)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (!IsOpen())
            {
                mmr = MixerDevice.mixerOpen(ref _hMixer, mixerId, hWndCallback, IntPtr.Zero, fdwOpen);
            }
            return mmr;
        }
        public int Close()
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = MixerDevice.mixerClose(_hMixer);
                if (mmr == WaveConstants.MMSYSERR_NOERROR)
                {
                    _hMixer = IntPtr.Zero;
                }
            }
            return mmr;
        }
        public int GetControlDetails(ref MixerControlDetails mxcd, int fdwDetails)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                fdwDetails |= MixerConstants.MIXER_OBJECTF_HMIXER;
                mmr = MixerDevice.mixerGetControlDetailsW(_hMixer, ref mxcd.mxDetails, fdwDetails);
            }
            return mmr;
        }
        public int SetControlDetails(ref MixerControlDetails mxcd, int fdwDetails)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                fdwDetails |= MixerConstants.MIXER_OBJECTF_HMIXER;
                mmr = MixerDevice.mixerSetControlDetails(_hMixer, ref mxcd.mxDetails, fdwDetails);
            }
            return mmr;
        }
        public int GetLineControls(ref MixerLineControls mxlcs, int fdwControls)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                fdwControls |= MixerConstants.MIXER_OBJECTF_HMIXER;
                mmr = MixerDevice.mixerGetLineControlsW(_hMixer, ref mxlcs.mxLineCtrls, fdwControls);
            }
            return mmr;
        }
        public int GetLineInfo(ref MixerLineInfo mxli, int fdwInfo)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                fdwInfo |= MixerConstants.MIXER_OBJECTF_HMIXER;
                mmr = MixerDevice.mixerGetLineInfoW(_hMixer, ref mxli.mxLineInfo, fdwInfo);
            }
            return mmr;
        }
        public int GetDeviceCaps(ref MixerCaps mxcaps)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = MixerDevice.mixerGetDevCapsW(_hMixer, ref mxcaps.mxCaps, MixerCaps.Size);
            }
            return mmr;
        }
        public int MixerMessage(int uMsg, IntPtr dwParam1, IntPtr dwParam2)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                int mixerId = INVALID_MIXER_ID;
                mmr = MixerDevice.mixerGetID(_hMixer, ref mixerId, MixerConstants.MIXER_OBJECTF_HMIXER);
                if (mmr == WaveConstants.MMSYSERR_NOERROR)
                {
                    mmr = MixerDevice.mixerMessage((IntPtr)mixerId, uMsg, dwParam1, dwParam2);
                }
            }
            return mmr;
        }

        static int GetNumDevices()
        {
            return MixerDevice.mixerGetNumDevs();
        }
        static int GetMixerDeviceID(IntPtr hmxobj, int flags)
        {
            int mixerId = INVALID_MIXER_ID;
            int mmr = MixerDevice.mixerGetID(hmxobj, ref mixerId, flags);
            return mixerId;
        }

        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            // release unmanaged types
            try
            {
                Close();
            }
            catch (Exception e)
            {
                // don't throw
                System.Diagnostics.Trace.WriteLine(e.ToString());
            }
        }
    }
}
