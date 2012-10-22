using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ernzo.Windows.WaveAudio;
using System.Runtime.InteropServices;

namespace SignalAnalyzer2
{
    public partial class ChooseDevice : Form
    {
        public ChooseDevice()
        {
            InitializeComponent();
            enumrateDevs();
        }
        void enumrateDevs()
        {
            tWAVEINCAPSA woc = new tWAVEINCAPSA();
            int   iNumDevs, i; 

            /* Get the number of Digital Audio Out devices in this computer */
            iNumDevs = WaveInput.waveInGetNumDevs(); 

            /* Go through all of those devices, displaying their names */ 
            for (i = 0; i < iNumDevs; i++) 
            {
                IntPtr iptr = new IntPtr(i);
                /* Get info about the next device */
                if (WaveInput.waveInGetDevCapsA((Int32)i, ref woc, System.Runtime.InteropServices.Marshal.SizeOf(woc)) == WaveConstants.MMSYSERR_NOERROR) 
                { 
                    /* Display its Device ID and name */ 
                    DeviceCB.Items.Add(woc.szPname);
                } 
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int ChosenDeviceID
        {
            get
            {
                return DeviceCB.SelectedIndex;
            }
            set
            {
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
