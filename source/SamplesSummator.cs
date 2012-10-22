using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SignalAnalyzer2
{
    public class SamplesSummator
    {
        private int mcurrent_sample_set = 0;
        private int mnum_of_sets_to_accumulate = 8;
        //previous samples
        private double[,] mPreviousAmplSpectrum;
        private int mNumOfSamples;
        private int mSamplesPerSecond;
        private double[] currentSpectrumSum;
        private double[] resultSpectrum;
        //======================================
        public SamplesSummator(int numSamples, int asamplesPerSecond)
        {
            mNumOfSamples = numSamples;
            mSamplesPerSecond = asamplesPerSecond;
            mPreviousAmplSpectrum = new double[mnum_of_sets_to_accumulate, mNumOfSamples];
            currentSpectrumSum = new double[mNumOfSamples];
            resultSpectrum = new double[mNumOfSamples];
        }

        protected double[] SumSamples()
        {
            Array.Clear(resultSpectrum, 0, resultSpectrum.Length);
            for (int i = 0; i < mnum_of_sets_to_accumulate; ++i)
            {                
                for (int j = 0; j < mNumOfSamples; ++j)
                {
                    resultSpectrum[j] += mPreviousAmplSpectrum[i, j];
                }
            }

            resultSpectrum.CopyTo(currentSpectrumSum, 0);

            return resultSpectrum;
        }

        public double[] addAnotherSample(double[] AmplSpectrum, uint size)
        {
            for (int i = 0; i < mNumOfSamples; ++i)
            {
                mPreviousAmplSpectrum[mcurrent_sample_set, i] = AmplSpectrum[i];
            }
            
            mcurrent_sample_set++;
            if (mcurrent_sample_set >= mnum_of_sets_to_accumulate)
            {
                mcurrent_sample_set = 0;
            }
            return SumSamples();
        }

        protected int getIndexByFrequency(int aFreq)
        {
            return (aFreq * mNumOfSamples) / mSamplesPerSecond;
        }

        public double computeDigest()
        {
            int idx_995Hz = getIndexByFrequency(995);
            double digest = currentSpectrumSum[idx_995Hz]
                          + currentSpectrumSum[idx_995Hz + 1]
                          + currentSpectrumSum[idx_995Hz + 1]
                          + currentSpectrumSum[idx_995Hz + 1]
                          + currentSpectrumSum[idx_995Hz + 1];
            return digest;
        }
        public double computeDelta()
        {
            int idx_995Hz = getIndexByFrequency(995);
            double digest = currentSpectrumSum[idx_995Hz]
                          + currentSpectrumSum[idx_995Hz + 1]
                          + currentSpectrumSum[idx_995Hz + 1]
                          + currentSpectrumSum[idx_995Hz + 1]
                          + currentSpectrumSum[idx_995Hz + 1];
            return digest;
        }
    }
}