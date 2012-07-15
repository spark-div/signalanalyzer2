using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
//using Ernzo.WinForms.Controls;
using Ernzo.Windows.WaveAudio;
using Ernzo.DSP;
using ZedGraph;

namespace SignalAnalyzer2
{

    public partial class SygnalAnalyzerForm : Form, IWaveNotifyHandler
    {
        private const int WM_USER = 0x0400;
        private const int WM_AUDIO_DONE = WM_USER + 0x100;
        private const int MAX_BUFFERS = 4;
        //private const int MAX_BUFSIZE = 2048;
        private const double FFT_SPEED = 0.06;
        private const int NUM_FREQUENCY = 19;
        private int[] METER_FREQUENCY = new int[NUM_FREQUENCY] { 30, 60, 80, 90, 100, 150, 200, 330, 480, 660, 880, 1000, 1500, 2000, 3000, 5000, 8000, 12000, 16000 };
        private int[] _meterData = new int[NUM_FREQUENCY];
        private double[] RealIn;
        private double[] RealOut;
        private double[] ImagOut;
        private double[] AmplOut;
        private byte[] waveData;    // copy of wave data - unless 'unsafe' code is an option
        private WaveBuffer[] _waveBuffer;
        private WaveInDevice _waveInput;
        private IntPtr _CopyWindowHandle;
        private WaveFormat _wfmt;
        private uint _bufferSize;
        private uint _numSamples;
        private int _numOutBuffers;

        public SygnalAnalyzerForm()
        {
            InitializeComponent();
            _wfmt = new WaveFormat();
            _wfmt.SetPCMFormat(11025, 1, 16);
            _bufferSize = FFT.NextPowerOfTwo((UInt32)(_wfmt.BytesPerSecond * FFT_SPEED));
            _numSamples = 0;
            _numOutBuffers = 0;
           // ------------------------------------------------------
            InputZGraphCtrl.GraphPane.Title.Text = "Input, mV";
            InputZGraphCtrl.GraphPane.XAxis.MinorGrid.IsVisible = true;
            InputZGraphCtrl.GraphPane.YAxis.MinorGrid.IsVisible = true;
            SpectrumZGraphCtrl.GraphPane.Title.Text = "Spectrum, dB";
            SpectrumZGraphCtrl.GraphPane.XAxis.MinorGrid.IsVisible = true;
            SpectrumZGraphCtrl.GraphPane.YAxis.MinorGrid.IsVisible = true;
//            SpectrumZGraphCtrl.GraphPane.YAxis.Type = AxisType.Log;
        }

        private void Terminate()
        {
            ReleaseWaveInput();
            ReleaseFFTData();
            _CopyWindowHandle = IntPtr.Zero;
/*
            this.btnPlay.Enabled = true;
            this.btnStop.Enabled = false;
            this.btnMute.Enabled = false;
            this.ctlVolume.Enabled = false;
 */ 
        }
        private void SygnalAnalyzerForm_Load(object sender, EventArgs e)
        {
/*
            this.peakMeterCtrl1.SetRange(40, 70, 100);
            this.peakMeterCtrl1.Start(33);
            this.btnPlay.Enabled = true;
            this.btnStop.Enabled = false;
            this.btnMute.Enabled = false;
            this.ctlVolume.Enabled = false;
            this.btnBrowse.Enabled = false; // not used
 */
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Terminate();
            base.OnHandleDestroyed(e);
        }

        private void StartCaptureBtn_Click(object sender, EventArgs e)
        {
            if (_waveInput == null)
            {
                CreateWaveInput();
            }
            if (!_waveInput.IsOpen())
            {
                _CopyWindowHandle = this.Handle;
                try
                {
                    int mmr = _waveInput.Open(WaveConstants.WAVE_MAPPER, _wfmt);
                    WaveInStatus.ThrowExceptionForHR(mmr);

                    CreateBuffers();
                    AllocFFTData();
                    _numSamples = 0;
                    _numOutBuffers = 0;
                    for (int index = 0; index < MAX_BUFFERS; ++index)
                    {
                        WaveBuffer curBuffer = _waveBuffer[index];
                        mmr = _waveInput.PrepareBuffer(curBuffer);
                        mmr = _waveInput.AddBuffer(curBuffer);
                        WaveInStatus.ThrowExceptionForHR(mmr);
                        Interlocked.Increment(ref _numOutBuffers);
                    }

                    mmr = _waveInput.Start();
                    WaveInStatus.ThrowExceptionForHR(mmr);
                }
                catch (Exception ex) // Typically: COMException, SystemException
                {
                    DumpDebugMessage(ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Terminate();
        }

        private void btnMute_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ctlVolume_ValueChanged(object sender, EventArgs e)
        {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                // Process audio done message
                case WM_AUDIO_DONE:
                {
                    GCHandle gch = (GCHandle)(m.LParam);
                    WaveBuffer wbuf = gch.Target as WaveBuffer;
                    if (wbuf != null && _waveInput != null)
                    {
                        try
                        {
                            int mmr = _waveInput.UnprepareBuffer(wbuf);
                            WaveInStatus.ThrowExceptionForHR(mmr);
                            Interlocked.Decrement(ref _numOutBuffers);
                            WaveStatus status = _waveInput.GetDeviceStatus();
                            if (status == WaveStatus.waveStarted)
                            {
                                ProcessAudioData(wbuf);
                            }
                        }
                        catch (Exception ex) // Typically: COMException, SystemException
                        {
                            DumpDebugMessage(ex.Message);
                        }
                    }
                    else
                    {
                        DumpDebugMessage("WM_AUDIO_DONE - AUDIO STOPPED");
                    }
                    if (gch.IsAllocated)
                    {
                        gch.Free();
                    }
                    return;
                }
                default:
                break;
            }
            base.WndProc(ref m);
        }

        private delegate void WndProcCallback(ref Message m);
        public void ProcessEvent(IWaveDevice waveDevice, int uMsg, WaveBuffer wbuf)
        {
            if (waveDevice == _waveInput)
            {
                switch (uMsg)
                {
                    case WaveConstants.MM_WIM_OPEN:
                        DumpDebugMessage("Wave Opened");
                        break;
                    case WaveConstants.MM_WIM_DATA:
                        {
                            GCHandle gch = GCHandle.Alloc(wbuf);
                            // Create message
                            if (this.IsHandleCreated)
                            {
                                Message m = Message.Create(_CopyWindowHandle, WM_AUDIO_DONE, IntPtr.Zero, (IntPtr)gch);
                                WndProcCallback wndCallback = new WndProcCallback(WndProc);
                                // Ensure all calls will be thread-safe
                                this.BeginInvoke(wndCallback, m);
                            }
                        }
                        break;
                    case WaveConstants.MM_WIM_CLOSE:
                        DumpDebugMessage("Wave Closed");
                        break;
                }
            }
        }

        private void ProcessAudioData(WaveBuffer wbuf)
        {
            try
            {
                if (_waveInput.GetDeviceStatus() == WaveStatus.waveStarted)
                {
                    ComputeFFT(wbuf);

                    int mmr = _waveInput.PrepareBuffer(wbuf);
                    WaveInStatus.ThrowExceptionForHR(mmr);

                    mmr = _waveInput.AddBuffer(wbuf);
                    WaveInStatus.ThrowExceptionForHR(mmr);
                    Interlocked.Increment(ref _numOutBuffers);
                }
            }
            catch (Exception ex) // COMException, SystemException (waveinput)
            {
                DumpDebugMessage(ex.Message);
            }
        }

        private PointPairList m_pointsList;

        private void drawCapturedData(double[] InputSignal, uint arraySize)
        {
           m_pointsList = new PointPairList();
           for (int i = 0; i < arraySize; i++)
           {
              {
                 double _x = Convert.ToDouble(i);
                 double _y = Convert.ToDouble(InputSignal[i]);
                 m_pointsList.Add(_x, _y);
              }
           }

           GraphPane graphPane = InputZGraphCtrl.GraphPane;
           graphPane.CurveList.Clear();
           // Generate a blue curve with Star symbols
           LineItem myCurve = graphPane.AddCurve("Input, mV", m_pointsList, Color.Blue, SymbolType.None);
           InputZGraphCtrl.AxisChange();
           InputZGraphCtrl.Invalidate();
        }

        private void drawSpectrum(double[] AmplSpectrum, uint size )
        {
           m_pointsList = new PointPairList();
           for (int i = 0; i < _numSamples; i++)
           {
              {
                 double _x = Convert.ToDouble(i);
                 double _y = Convert.ToDouble(AmplSpectrum[i]);
                 m_pointsList.Add(_x, _y);
              }
           }

           GraphPane graphPane = SpectrumZGraphCtrl.GraphPane;
           graphPane.CurveList.Clear();
           // Generate a blue curve with Star symbols
           LineItem myCurve = graphPane.AddCurve("Spectrum", m_pointsList, Color.Blue, SymbolType.None);
           SpectrumZGraphCtrl.AxisChange();
           SpectrumZGraphCtrl.Invalidate();
        }

        void ComputeFFT(WaveBuffer wbuf)
        {
            if (GetAudioData(wbuf.AudioData, wbuf.BytesRecorded, _wfmt))
            {
                drawCapturedData(RealIn, _numSamples);
                // adjust FFT Sample to next power 2 but fill data with silence
                uint pow2Samples = FFT.NextPowerOfTwo(_numSamples);
                if (pow2Samples != _numSamples)
                {
                    double dsilence = 0.0;
                    if (_wfmt.BitsPerSample == 8)
                    {
                        dsilence = 128.0;
                    }
                    for (uint ii = _numSamples; ii < pow2Samples; ii++)
                    {
                        RealIn[ii] = dsilence;
                    }
                    _numSamples = pow2Samples;
                }

                // You may want to add 'USE_FFTLIB' under the project settings (Build->General tab)
                // to get better performance
#if USE_FFTLIB
                // Do the FFT
                FFTLib.ComputeD(_numSamples, RealIn, null, RealOut, ImagOut, false);
                // We can skip N/2 to N samples (mirror frequencies) - Digital samples are real integer
                FFTLib.NormD(_numSamples / 2, RealOut, ImagOut, AmplOut);
#else
                FFT.Compute(_numSamples, RealIn, null, RealOut, ImagOut, false);
                FFT.Norm(_numSamples / 2, RealOut, ImagOut, AmplOut);
#endif

#if USING_PEAKMETER
                double maxAmpl = (_wfmt.BitsPerSample == 8) ? (127.0 * 127.0) : (32767.0 * 32767.0);
                               
                // update meter
                int centerFreq = (int)(_wfmt.SamplesPerSecond / 2);
                for (int i = 0; i < NUM_FREQUENCY; ++i)
                {
                    if (METER_FREQUENCY[i] > centerFreq)
                        _meterData[i] = 0;
                    else
                    {
                        int indice = (int)(METER_FREQUENCY[i] * _numSamples / _wfmt.SamplesPerSecond);
                        int metervalue = (int)(100+ 20.0 * Math.Log10(AmplOut[indice] / maxAmpl));
                        _meterData[i] = metervalue;
                    }
                }
                
                peakMeterCtrl1.SetData(_meterData, 0, NUM_FREQUENCY);
#else
                drawSpectrum(AmplOut, NUM_FREQUENCY);
#endif //USING_PEAKMETER
                _numSamples = 0; // ready to do it again
            }
            else
            {
                DumpDebugMessage("GET Audio data failed.");
            }
        }

        bool GetAudioData(IntPtr ptr, int cbSize, WaveFormat wfmt)
        {
            bool samplesReady = false;
            if (cbSize == 0)
                return false; // no data
            switch (wfmt.BitsPerSample)
            {
                case 8:
                    {
                        // NOTE: waveData member is necessary to prevent using 'unsafe' code block
                        Marshal.Copy(ptr, waveData, 0, (int)cbSize);
                        if (wfmt.Channels == 1) // mono
                        {
                            for (uint i = 0; i < cbSize; ++i)
                            {
                                RealIn[i] = (double)((waveData[i] - 128) << 6);// Out = (In-128)*64
                            }
                            _numSamples = (uint)cbSize;
                        }
                        else if (wfmt.Channels == 2) // stereo
                        {
                            // Stereo has Right+Left channels
                            int Samples = cbSize >> 1;
                            for (uint i = 0, j = 0; i < Samples; ++i, j += 2)
                            {
                                RealIn[i] = (double)((waveData[j] - 128) << 6); // Out = (In-128)*64
                                // LeftIn[i] = (double)((waveData[j+1]-128)<<6); // Out = (In-128)*64
                            }
                            _numSamples = (uint)Samples;
                        }
                        samplesReady = (_numSamples != 0);
                    }
                    break;
                case 16:
                    {
                        // NOTE: waveData member is necessary to prevent using 'unsafe' code block
                        Marshal.Copy(ptr, waveData, 0, (int)cbSize);
                        if (wfmt.Channels == 1) // mono
                        {
                            int Samples = cbSize >> 1;
                            for (uint i = 0, j = 0; i < Samples; ++i, j += 2)
                            {
                                short val = (short)unchecked(((waveData[j + 1] << 8) + waveData[j]));
                                RealIn[i] = (double)val;
                            }
                            _numSamples = (uint)Samples;
                        }
                        else if (wfmt.Channels == 2) // stereo
                        {
                            // Stereo has Right+Left channels
                            int Samples = cbSize >> 2;
                            for (uint i = 0, j = 0; i < Samples; ++i, j += 4)
                            {
                                short val = unchecked((short)((waveData[j + 1] << 8) + waveData[j])); // right
                                RealIn[i] = (double)val;
                                // val = unchecked((short)((waveData[j+3] << 8) + waveData[j+2])); // left
                            }
                            _numSamples = (uint)Samples;
                        }
                        samplesReady = (_numSamples != 0);
                    }
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "Format not supported"); // not supported
                    break;
            }
            return samplesReady;
        }
        bool PutAudioData(IntPtr pbData, int cbSize, int recvBytes, WaveFormat wfmt)
        {
            bool samplesReady = true;
            switch (wfmt.BitsPerSample)
            {
                case 8:
                    {
                        // fill with silence - no smoothing
                        if (cbSize > recvBytes)
                        {
                            MemorySet(pbData, 0x80, (cbSize - recvBytes));
                        }
                    }
                    break;
                case 16:
                default:
                    {
                        // fill with silence - no smoothing
                        if (cbSize > recvBytes)
                        {
                            MemorySet(pbData, 0x00, (cbSize - recvBytes));
                        }
                    }
                    break;
            }
            return samplesReady;
        }
        void MemorySet(IntPtr ptrData, byte val, int cb)
        {
            byte[] silence = { val };
            for (int i = 0; i < cb; ++i)
            {
                IntPtr ptrDest = (IntPtr)((int)ptrData + i);
                Marshal.Copy(silence, 0, ptrDest, 1);
            }
        }

        void CreateWaveInput()
        {
            if (_waveInput == null)
            {
                _waveInput = new WaveInDevice();
                _waveInput.SetNotifyHandler(this);
            }
        }
        void ReleaseWaveInput()
        {
            if (_waveInput != null)
            {
                _waveInput.Close();
                ReleaseBuffers();
                _waveInput.SetNotifyHandler(null);
                _waveInput = null;
            }
        }
        void CreateBuffers()
        {
            if (_waveBuffer == null)
            {
                _waveBuffer = new WaveBuffer[MAX_BUFFERS];
                for (int ii = 0; ii < MAX_BUFFERS; ++ii)
                {
                    _waveBuffer[ii] = new WaveBuffer();
                    _waveBuffer[ii].Allocate((int)_bufferSize);
                }
            }
        }
        void ReleaseBuffers()
        {
            if (_waveBuffer != null)
            {
                for (int ii = 0; ii < MAX_BUFFERS; ++ii)
                {
                    _waveBuffer[ii].Dispose();
                }
                _waveBuffer = null;
            }
        }

        void AllocFFTData()
        {
            int numSamples = (int)_bufferSize / _wfmt.BlockAlign;
            RealIn = new double[numSamples];
            RealOut = new double[numSamples];
            ImagOut = new double[numSamples];
            AmplOut = new double[numSamples];
            waveData = new byte[_bufferSize];
        }
        void ReleaseFFTData()
        {
            RealIn = null;
            RealOut = null;
            ImagOut = null;
            AmplOut = null;
            waveData = null;
        }

        [ConditionalAttribute("DEBUG")]
        void DumpWaveErrorMessage(string message, int error)
        {
            StringBuilder sError = new StringBuilder(256);
            WaveInput.waveInGetErrorTextW(error, sError, 256);
            Trace.WriteLine(string.Format("{0} {1}", message, sError.ToString()));
        }

        [ConditionalAttribute("DEBUG")]
        void DumpDebugMessage(string message)
        {
            Trace.WriteLine(message);
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
           Terminate();
        }
    }
}