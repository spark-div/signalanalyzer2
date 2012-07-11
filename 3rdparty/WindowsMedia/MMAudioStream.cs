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
using System.Text;
using Ernzo.Windows.WaveAudio;

namespace Ernzo.Windows.DirectShowLib.MMStreaming
{
    /// <summary>
    /// MMAudioStream
    /// </summary>
    public class MMAudioStream : IDisposable
    {
        private tWAVEFORMATEX       _wfmt;          ///< Audio format
        private IAudioMediaStream   _pAudioStream;  ///< Audio Stream
        private IAudioData          _pAudioData;    ///< Audio Stream data
        private IAudioStreamSample  _pAudioSample;  ///< Audio Stream sample
        public MMAudioStream()
        {
        }
        ~MMAudioStream()
        {
            // this is not called if Dispose was called first
            Dispose(false);
        }

        public tWAVEFORMATEX Format
        {
            get { return _wfmt; }
            set { _wfmt = value; }
        }

        public bool IsValid
        {
            get { return (_pAudioStream != null); }
        }

        public int SetMediaStream(IMediaStream pMediaStream)
        {
            if (pMediaStream == null)
            {
                throw new ArgumentNullException("pMediaStream");
            }
            if (IsValid)
            {
                throw new InvalidOperationException("Instance is already active.");
            }
            int hr = MSStatus.MS_E_HANDLE;
            _pAudioStream = pMediaStream as IAudioMediaStream;
            if (_pAudioStream != null)
            {
                hr = _pAudioStream.GetFormat(out _wfmt);
                if (MSStatus.Succeed(hr))
                {
                    AMAudioData amAudio = new AMAudioData();
                    _pAudioData = (IAudioData)amAudio;
                    hr = _pAudioData.SetFormat(ref _wfmt);
                }
                else
                {
                    _pAudioStream = null;
                }
            }

            return hr;
        }
        public int ReleaseMediaStream()
        {
            int hr = MSStatus.MS_S_FALSE;
            if (IsValid)
            {
                hr = CompletionStatus((int)COMPLETION_STATUS_FLAGS.COMPSTAT_ABORT, System.Threading.Timeout.Infinite);
                if ( MSStatus.Succeed(hr) )
                {
                    _pAudioSample = null;
                    Marshal.ReleaseComObject(_pAudioStream);
                    _pAudioStream = null;
                    Marshal.FinalReleaseComObject(_pAudioData);
                    _pAudioData = null;
                }
            }
            return hr;
        }
        public int GetSampleData(IntPtr pbData, ref int dwSize, int dwFlags, int dwTimeout)
        {
            if (pbData == IntPtr.Zero || dwSize <= 0 )
            {
                throw new ArgumentNullException("pbData");
            }
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = MSStatus.MS_S_OK;
                if (_pAudioSample == null)
                {
                    hr = _pAudioStream.CreateSample(_pAudioData, 0, out _pAudioSample);
                }
                if (MSStatus.Succeed(hr))
                {
                    hr = _pAudioData.SetBuffer(dwSize, pbData, 0);
                    hr = _pAudioSample.Update((int)SSUPDATE_FLAGS.SSUPDATE_ASYNC, IntPtr.Zero, null, 0);
                    if (hr == MSStatus.MS_S_PENDING)
                    {
                        hr = _pAudioSample.CompletionStatus(dwFlags, dwTimeout);
                    }
                    if (hr == MSStatus.MS_S_ENDOFSTREAM)
                    {
                        hr = MSStatus.MS_S_FALSE;
                    }
                    if (MSStatus.Succeed(hr))
                    {
                        int dwLength;
                        IntPtr dataPtr;
                        _pAudioData.GetInfo(out dwLength, out dataPtr, out dwSize);
                    }
                }
            }
            return hr;
        }
        public int GetSampleTimes(out long pStartTime, out long pEndTime, out long pCurrentTime)
        {
            pStartTime = 0;
            pEndTime = 0;
            pCurrentTime = 0;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                if (_pAudioSample != null)
                {
                    hr = _pAudioSample.GetSampleTimes(out pStartTime, out pEndTime, out pCurrentTime);
                }
            }
            return hr;
        }

        public int CompletionStatus(int dwFlags, int dwTimeout)
        {
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                if (_pAudioSample != null)
                {
                    hr = _pAudioSample.CompletionStatus(dwFlags, dwTimeout);
                    if (hr == MSStatus.MS_S_ENDOFSTREAM)
                    {
                        hr = MSStatus.MS_S_FALSE;
                    }
                }
            }
            return hr;
        }

        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            try
            {
                ReleaseMediaStream();
            }
            catch (Exception e)
            {
                // Dispose should not throw
                System.Diagnostics.Trace.WriteLine(string.Format("*** Exception occured: {0}***", e.ToString()));
            }
        }
    }
}
