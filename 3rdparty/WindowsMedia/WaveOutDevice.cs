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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace Ernzo.Windows.WaveAudio
{
    /// <summary>
    /// WaveOutStatus
    /// </summary>
    public sealed class WaveOutStatus
    {
        private WaveOutStatus()
        {
            // nothing to construct
        }
        public static string GetErrorText(int mmr)
        {
            StringBuilder message = new StringBuilder(256);
            WaveOutput.waveOutGetErrorTextW(mmr, message, 256);
            return message.ToString();
        }
        public static void ThrowExceptionForHR(int mmr)
        {
            if (mmr != WaveConstants.MMSYSERR_NOERROR)
            {
                throw new SystemException( GetErrorText(mmr) );
            }
        }
    }

    /// <summary>
    /// WaveOutCaps
    /// Wave Output Capabilities
    /// </summary>
    public sealed class WaveOutCaps
    {
        internal tWAVEOUTCAPSW woCaps;
        public WaveOutCaps()
        {
        }
        public WaveOutCaps(tWAVEOUTCAPSW caps)
        {
            woCaps = caps;
        }

        int ManufacturerId
        {
            get { return woCaps.wMid; }
        }
        int ProductId
        {
            get { return woCaps.wPid; }
        }
        int Version
        {
            get { return woCaps.vDriverVersion; }
        }
        int Formats
        {
            get { return woCaps.dwFormats; }
        }
        int NumChannels
        {
            get { return woCaps.wChannels; }
        }
        string ProductName
        {
            get { return woCaps.szPname; }
        }
        int Supported
        {
            get { return woCaps.dwSupport; }
        }
        public static int Size
        {
            get
            {
                return Marshal.SizeOf(typeof(tWAVEOUTCAPSW));
            }
        }
        public static explicit operator tWAVEOUTCAPSW(WaveOutCaps wocaps)
        {
            return wocaps.woCaps;
        }
    }

    /// <summary>
    /// WaveOutDevice
    /// Wave Output Device (e.g.:SPKR)
    /// </summary>
    public class WaveOutDevice : IWaveDevice, IDisposable
    {
        private IntPtr  _hWaveOut;      // Wave Audio Output
        private int _DeviceState;       // Wave state
        private int _currentKey;        // Current key (used in map)
        private Dictionary<int, WaveBuffer> _AudioBuffers; // Audio buffer map
        private IWaveNotifyHandler _WaveNotify; // Audio Event handler
        private DriverCallback _Callback;       // Callback
        public WaveOutDevice()
        {
            _hWaveOut = IntPtr.Zero;
            _DeviceState = (int)WaveStatus.waveClosed;
            _AudioBuffers = new Dictionary<int, WaveBuffer>();
            _WaveNotify = null;
            _Callback = null;
        }
        ~WaveOutDevice()
        {
            Dispose(false);
        }
        public bool IsOpen()
        {
            return (_hWaveOut != IntPtr.Zero);
        }
        public IntPtr GetId()
        {
            return _hWaveOut;
        }
        public WaveStatus GetDeviceStatus()
        {
            // this is safer than 'return _DeviceState;'
            WaveStatus wStatus = (WaveStatus)Interlocked.CompareExchange(ref _DeviceState,
                                        (int)WaveStatus.waveClosed,
                                        (int)WaveStatus.waveClosed);
            return wStatus;
        }
        public int Open(int deviceId, WaveFormat wfmt)
        {
            int mmr = WaveConstants.MMSYSERR_HANDLEBUSY;
            if (!IsOpen())
            {
                _Callback = new DriverCallback(waveOutProc);
                GCHandle handle = GCHandle.Alloc(_Callback);
                mmr = WaveOutput.waveOutOpen(ref _hWaveOut, deviceId, ref wfmt.wfmt.Format,
                                          Marshal.GetFunctionPointerForDelegate(_Callback),
                                          IntPtr.Zero,
                                          WaveConstants.CALLBACK_FUNCTION);
                handle.Free();
                if (mmr == WaveConstants.MMSYSERR_NOERROR)
                {
                    Interlocked.Exchange(ref _DeviceState, (int)WaveStatus.waveStopped);
                }
            }
            return mmr;
        }
        public int Close()
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = Stop();
                mmr = WaveOutput.waveOutClose(_hWaveOut);
                if (mmr == WaveConstants.MMSYSERR_NOERROR)
                {
                    Interlocked.Exchange(ref _DeviceState, (int)WaveStatus.waveClosed);
                    _hWaveOut = IntPtr.Zero;
                    _Callback = null;
                }
            }
            return mmr;
        }
        public int Start()
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = WaveOutput.waveOutRestart(_hWaveOut);
                if (mmr == WaveConstants.MMSYSERR_NOERROR)
                {
                    Interlocked.Exchange(ref _DeviceState, (int)WaveStatus.waveStarted);
                }
            }
            return mmr;
        }
        public int Stop()
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                Interlocked.Exchange(ref _DeviceState, (int)WaveStatus.waveStopped);
                mmr = WaveOutput.waveOutReset(_hWaveOut);
                if (mmr == WaveConstants.MMSYSERR_NOERROR)
                {
                    mmr = WaveOutput.waveOutPause(_hWaveOut);
                }
            }
            return mmr;
        }
        public int Reset()
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = WaveOutput.waveOutReset(_hWaveOut);
            }
            return mmr;
        }
        public int Pause()
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                Interlocked.Exchange(ref _DeviceState, (int)WaveStatus.wavePaused);
                mmr = WaveOutput.waveOutPause(_hWaveOut);
            }
            return mmr;
        }
        public int GetPosition(WaveTime wti)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = WaveOutput.waveOutGetPosition(_hWaveOut, ref wti.tMMT, WaveTime.Size);
            }
            return mmr;
        }
        public int AddBuffer(WaveBuffer wbuf)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = WaveOutput.waveOutWrite(_hWaveOut, ref wbuf.whdr, WaveBuffer.Size);
                if (mmr == WaveConstants.MMSYSERR_NOERROR)
                {
                    Interlocked.Increment(ref _currentKey);
                    wbuf.UserData = _currentKey;
                    _AudioBuffers[_currentKey] = wbuf;
                }
            }
            return mmr;
        }
        public int PrepareBuffer(WaveBuffer wbuf)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = WaveOutput.waveOutPrepareHeader(_hWaveOut, ref wbuf.whdr, WaveBuffer.Size);
            }
            return mmr;
        }
        public int UnprepareBuffer(WaveBuffer wbuf)
        {
            int mmr = WaveConstants.MMSYSERR_INVALHANDLE;
            if (IsOpen())
            {
                mmr = WaveOutput.waveOutUnprepareHeader(_hWaveOut, ref wbuf.whdr, WaveBuffer.Size);
            }
            return mmr;
        }
        static  public int GetDevicesCount()
        {
            return WaveOutput.waveOutGetNumDevs();
        }
        static  int GetDeviceCaps(int deviceID, WaveOutCaps woc)
        {
            return WaveOutput.waveOutGetDevCapsW(deviceID, ref woc.woCaps, WaveOutCaps.Size);
        }
        public bool SetNotifyHandler(IWaveNotifyHandler notifyHander)
        {
            bool result = false;
            if (notifyHander != null)
            {
                if (_WaveNotify == null)
                {
                    Interlocked.Exchange(ref _WaveNotify, notifyHander);
                }
                else
                {
                    // Only 1 member is supported
                    throw new InvalidOperationException();
                }
            }
            else
            {
                Interlocked.Exchange(ref _WaveNotify, null);
            }
            return result;
        }

        /// <summary>
        /// waveOutProc
        /// Wave Callback
        /// </summary>
        /// <param name="hwo">Wave Output Handle</param>
        /// <param name="uMsg">Wave Output message (MM_WOM_OPEN, MM_WOM_DATA, MM_WOM_CLOSE)</param>
        /// <param name="dwInstance">Application Instance data (NULL)</param>
        /// <param name="dwParam1">This pointer represent a tWAVEHDR when MM_WOM_DATA is received. Null otherwise</param>
        /// <param name="dwParam2">Null</param>
        void waveOutProc(IntPtr hwo, int uMsg, IntPtr dwInstance,
                                 IntPtr dwParam1, IntPtr dwParam2)
        {
            if (_WaveNotify != null)
            {
                // Pop audio buffer only for data
                WaveBuffer wbuf = null;
                if (dwParam1 != IntPtr.Zero)
                {
                    tWAVEHDR whdr = (tWAVEHDR)Marshal.PtrToStructure(dwParam1, typeof(tWAVEHDR));
                    if (!_AudioBuffers.TryGetValue((int)whdr.dwUser, out wbuf))
                    {
                        System.Diagnostics.Debug.Assert(false, string.Format("Failed to get pointer :{0}", (int)whdr.dwUser));
                        return;
                    }
                    _AudioBuffers.Remove((int)whdr.dwUser);
                }
                _WaveNotify.ProcessEvent(this, uMsg, wbuf);
            }
        }

        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                // release unmanaged types
                Close();
            }
            catch (Exception e)
            {
                // don't throw
                System.Diagnostics.Trace.WriteLine(e.ToString());
            }
            finally
            {
                SetNotifyHandler(null);
            }
            if (disposing)
            {
                // release managed
                if (_AudioBuffers != null)
                {
                    System.Diagnostics.Debug.Assert(_AudioBuffers.Count == 0, "Audio buffers may be leaking");
                    _AudioBuffers = null;
                }
            }
        }
    }
}
