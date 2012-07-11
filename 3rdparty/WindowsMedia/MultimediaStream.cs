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
using System.Runtime.InteropServices;

namespace Ernzo.Windows.DirectShowLib.MMStreaming
{
    /// <summary>
    /// Multimedia streaming
    /// Use this class to build a streaming graph
    /// </summary>
    public class MultimediaStream : IDisposable
    {
        private IAMMultiMediaStream _pMMS;
        private IGraphBuilder       _pGB;
        public MultimediaStream()
        {
        }
        ~MultimediaStream()
        {
            // this is not called if Dispose was called first
            Dispose(false);
        }

        public bool IsValid
        {
            get { return (_pMMS != null ); }
        }

        public int Initialize(STREAM_TYPE streamType, int dwFlags, IGraphBuilder pFilterGraph)
        {
            if (IsValid)
            {
                throw new InvalidOperationException("Instance is already active.");
            }
            AMMultiMediaStream amms = new AMMultiMediaStream();
            _pMMS = (IAMMultiMediaStream)amms;
            int hr = _pMMS.Initialize(streamType, dwFlags, pFilterGraph);
            if (hr == MSStatus.MS_S_OK)
            {
                hr = _pMMS.GetFilterGraph(out _pGB);
            }
            return hr;
        }

        public int Terminate()
        {
            int hr = MSStatus.MS_S_FALSE;
            if (IsValid)
            {
                _pGB = null;
                Marshal.FinalReleaseComObject(_pMMS);
                _pMMS = null;
            }
            hr = MSStatus.MS_S_OK;
            return hr;
        }

        public int GetFilterGraph(out IGraphBuilder ppFilterGraph)
        {
            ppFilterGraph = null;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                ppFilterGraph = _pGB;
                hr = MSStatus.MS_S_OK;
            }
            return hr;
        }

        public int GetDuration(out long pDuration)
        {
            pDuration = 0;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.GetDuration(out pDuration);
            }
            return hr;
        }

        public int GetTime(out long pCurrentTime)
        {
            pCurrentTime = 0;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.GetTime(out pCurrentTime);
            }
            return hr;
        }

        public int GetState(out STREAM_STATE pCurrentState)
        {
            pCurrentState = STREAM_STATE.STREAMSTATE_STOP;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.GetState(out pCurrentState );
            }
            return hr;
        }

        public int SetState(STREAM_STATE NewState)
        {
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.SetState(NewState);
            }
            return hr;
        }

        public int Seek(long seekTime)
        {
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.Seek(seekTime);
            }
            return hr;
        }

        public int OpenFile(string fileName, int dwFlags)
        {
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.OpenFile(fileName, dwFlags);
                if (hr == MSStatus.MS_S_OK && _pGB == null)
                {
                    hr = _pMMS.GetFilterGraph(out _pGB);
                }
            }
            return hr;
        }

        public int AddMediaStream(Object pStreamObject, Guid PurposeID, int dwFlags, out IMediaStream ppNewStream)
        {
            ppNewStream = null;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.AddMediaStream(pStreamObject, PurposeID, dwFlags, out ppNewStream);
            }
            return hr;
        }

        public int GetMediaStream(Guid idPurpose, out IMediaStream ppMediaStream)
        {
            ppMediaStream = null;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.GetMediaStream(idPurpose, out ppMediaStream);
            }
            return hr;
        }

        public int AddSourceFilter(string fileName, string filterName, out IBaseFilter ppFilter)
        {
            ppFilter = null;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid && (_pGB != null))
            {
                hr = _pGB.AddSourceFilter(fileName, filterName, out ppFilter);
            }
            return hr;
        }

        public int AddFilter(string filterName, IBaseFilter pFilter)
        {
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid && (_pGB != null))
            {
                hr = _pGB.AddFilter(pFilter, filterName);
            }
            return hr;
        }

        public int FindFilterByName(string filterName, out IBaseFilter ppFilter)
        {
            ppFilter = null;
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid && (_pGB != null))
            {
                IFilterGraph pFG = (IFilterGraph)_pGB;
                hr = pFG.FindFilterByName(filterName, out ppFilter);
            }
            return hr;
        }

        public int Render(int dwFlags)
        {
            int hr = MSStatus.MS_E_HANDLE;
            if (IsValid)
            {
                hr = _pMMS.Render(dwFlags);
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
                Terminate();
            }
            catch (Exception e)
            {
                // Dispose should not throw
                System.Diagnostics.Trace.WriteLine(string.Format("*** Exception occured: {0}***", e.ToString()));
            }
        }
    }
}
