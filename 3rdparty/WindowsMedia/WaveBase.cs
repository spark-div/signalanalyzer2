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
    /// <summary>
    /// WaveBuffer
    /// </summary>
    public sealed class WaveBuffer : IDisposable
    {
        internal tWAVEHDR whdr;
        private GCHandle _whdrHandle;
        public WaveBuffer()
        {
            AllocWhdr();
        }
        ~WaveBuffer()
        {
            Dispose(false);
        }
        public IntPtr AudioData
        {
            set { whdr.lpData = value; }
            get { return whdr.lpData; }
        }
        public int BufferLength
        {
            set { whdr.dwBufferLength = value; }
            get { return whdr.dwBufferLength; }
        }
        public int BytesRecorded
        {
            set { whdr.dwBytesRecorded = value; }
            get { return whdr.dwBytesRecorded; }
        }
        public int Flags
        {
            set { whdr.dwFlags = value; }
            get { return whdr.dwFlags; }
        }
        public int Loops
        {
            set { whdr.dwLoops = value; }
            get { return whdr.dwLoops; }
        }
        public int UserData
        {
            set { whdr.dwUser = value; }
            get { return whdr.dwUser; }
        }
        public bool IsDone
        {
            get { return ( Flags & WaveConstants.WHDR_DONE) != 0; }
        }
        public bool IsPrepared
        {
            get { return ( Flags & WaveConstants.WHDR_PREPARED) != 0; }
        }
        public IntPtr Next
        {
            get { return whdr.lpNext; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tWAVEHDR));
            }
        }
        public bool Allocate(int size)
        {
            FreeMemory();
            AudioData = Marshal.AllocHGlobal(size);
            // exception is thrown if alloc fails
            BufferLength = size;
            return (AudioData != IntPtr.Zero);
        }
        public void FreeMemory()
        {
            if (AudioData != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(AudioData);
                AudioData = IntPtr.Zero;
                BufferLength = 0;
            }
        }
        private void AllocWhdr()
        {
            _whdrHandle = GCHandle.Alloc(this.whdr, GCHandleType.Pinned);
        }
        private void FreeWhdr()
        {
            if ((IntPtr)_whdrHandle != IntPtr.Zero)
            {
                if (_whdrHandle.IsAllocated)
                {
                    _whdrHandle.Free();
                }
            }
        }
        
        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            try
            {
                // release unmanaged types
                FreeMemory();
            }
            catch (Exception e)
            {
                // Dispose should not throw
                System.Diagnostics.Trace.WriteLine(string.Format("*** Allocation failure {0}***", e.ToString()));
            }
            if (disposing)
            {
                FreeWhdr();
            }
        }
    }


    /// <summary>
    /// WaveFormat
    /// </summary>
    public sealed class WaveFormat
    {
        internal tWAVEFORMATEXTENSIBLE wfmt;
        public WaveFormat()
        {
        }
        public WaveFormat(tWAVEFORMATEX fmt)
        {
            wfmt.Format = fmt;
            ValidBitsPerSample = 0;
            ChannelMask = 0;
            SubFormat = Guid.Empty;
        }
        public WaveFormat(tWAVEFORMATEXTENSIBLE fmt)
        {
            wfmt = fmt;
        }
        public void SetPCMFormat(int samplesPerSec, short channels, short bitsPerSec)
        {
            wfmt.Format.wFormatTag = WaveConstants.WAVE_FORMAT_PCM;
            wfmt.Format.nSamplesPerSec = samplesPerSec;
            wfmt.Format.nChannels = channels;
            wfmt.Format.wBitsPerSample = bitsPerSec;
            wfmt.Format.nBlockAlign = (short)unchecked(wfmt.Format.nChannels * (wfmt.Format.wBitsPerSample / 8));
            wfmt.Format.nAvgBytesPerSec = (wfmt.Format.nSamplesPerSec * wfmt.Format.nBlockAlign);
            wfmt.Format.cbSize = 0;
            ValidBitsPerSample = 0;
            ChannelMask = 0;
            SubFormat = Guid.Empty;
        }
        public short FormatTag
        {
            set { wfmt.Format.wFormatTag = value; }
            get { return wfmt.Format.wFormatTag; }
        }
        public int SamplesPerSecond
        {
            set
            {
                wfmt.Format.nSamplesPerSec = value;
                wfmt.Format.nBlockAlign = (short)unchecked(wfmt.Format.nChannels * (wfmt.Format.wBitsPerSample / 8));
                wfmt.Format.nAvgBytesPerSec = (wfmt.Format.nSamplesPerSec * wfmt.Format.nBlockAlign);
            }
            get { return wfmt.Format.nSamplesPerSec; }
        }
        public short Channels
        {
            set
            {
                wfmt.Format.nChannels = value;
                wfmt.Format.nBlockAlign = (short)unchecked(wfmt.Format.nChannels * (wfmt.Format.wBitsPerSample / 8));
                wfmt.Format.nAvgBytesPerSec = (wfmt.Format.nSamplesPerSec * wfmt.Format.nBlockAlign);
            }
            get { return wfmt.Format.nChannels; }
        }
        public short BitsPerSample
        {
            set
            {
                wfmt.Format.wBitsPerSample = value;
                wfmt.Format.nBlockAlign = (short)unchecked(wfmt.Format.nChannels * (wfmt.Format.wBitsPerSample / 8));
                wfmt.Format.nAvgBytesPerSec = (wfmt.Format.nSamplesPerSec * wfmt.Format.nBlockAlign);
            }
            get { return wfmt.Format.wBitsPerSample; }
        }
        public short BlockAlign
        {
            set { wfmt.Format.nBlockAlign = value; }
            get { return wfmt.Format.nBlockAlign; }
        }
        public int BytesPerSecond
        {
            set { wfmt.Format.nAvgBytesPerSec = value; }
            get { return wfmt.Format.nAvgBytesPerSec; }
        }
        public short ValidBitsPerSample
        {
            set { wfmt.Samples.wValidBitsPerSample = value; }
            get { return wfmt.Samples.wValidBitsPerSample; }
        }
        public short SamplesPerBlock
        {
            // just for clarity - same field offset as ValidBitsPerSample
            set { wfmt.Samples.wSamplesPerBlock = value; }
            get { return wfmt.Samples.wSamplesPerBlock; }
        }
        public int ChannelMask
        {
            set { wfmt.dwChannelMask = value; }
            get { return wfmt.dwChannelMask; }
        }
        public Guid SubFormat
        {
            set { wfmt.SubFormat = value; }
            get { return wfmt.SubFormat; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tWAVEFORMATEXTENSIBLE));
            }
        }
        public static explicit operator tWAVEFORMATEX(WaveFormat fmt)
        {
            return fmt.wfmt.Format;
        }
        public static explicit operator tWAVEFORMATEXTENSIBLE(WaveFormat fmt)
        {
            return fmt.wfmt;
        }
    }


    /// <summary>
    /// WaveTime
    /// </summary>
    public sealed class WaveTime
    {
        internal tMMTIME tMMT;
        public WaveTime()
        {
        }
        public WaveTime(tMMTIME tmmt)
        {
            tMMT = tmmt;
        }
        public int Type
        {
            set { tMMT.wType = value; }
            get { return tMMT.wType; }
        }
        int TimeInfo
        {
            set { tMMT.u.ms = value; }
            get { return tMMT.u.ms; }
        }
        public static int Size
        {
            get {
                return Marshal.SizeOf(typeof(tMMTIME));
            }
        }
        public static explicit operator tMMTIME(WaveTime tmmt)
        {
            return tmmt.tMMT;
        }
    }


    /// <summary>
    /// WaveStatus
    /// </summary>
    public enum WaveStatus
    {
        waveClosed = 0, ///< Closed
        waveStopped,    ///< Stopped
        waveStarted,    ///< Started
        wavePaused      ///< Paused
    }


    /// <summary>
    /// IWaveNotifyHandler
    /// </summary>
    public interface IWaveNotifyHandler
    {
        void ProcessEvent(IWaveDevice waveDevice, int uMsg, WaveBuffer pwbuf);
    }

    /// <summary>
    /// IWaveDevice
    /// Wave device interface
    /// </summary>
    public interface IWaveDevice
    {
        bool IsOpen();
        IntPtr GetId();
        WaveStatus GetDeviceStatus();
        int Open(int deviceId, WaveFormat wfmt);
        int Close();
        int Start();
        int Stop();
        int Reset();
        int Pause();
        int GetPosition(WaveTime wti);
        int AddBuffer(WaveBuffer wbuf);
        int PrepareBuffer(WaveBuffer wbuf);
        int UnprepareBuffer(WaveBuffer wbuf);
        bool SetNotifyHandler(IWaveNotifyHandler notifyHander);
    }
}
