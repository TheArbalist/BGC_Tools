﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using NUnit.Framework;
using BGC.IO;
using BGC.Audio;

namespace BGC.Tests
{
    public class WAVEncodingTests
    {
        [Test]
        public void TestSingleChannelWave()
        {
            float[] singleChannelSamples = CreateSineWave(1);
            string singleChannelFile = DataManagement.PathForDataFile("Test", "singleChannel.wav");
            string secondSingleChannel = DataManagement.PathForDataFile("Test", "singleChannel2.wav");

            Assert.IsTrue(WaveEncoding.SaveFile(
                filepath: singleChannelFile,
                channels: 1,
                samples: singleChannelSamples,
                overwrite: true));

            singleChannelSamples = null;

            Assert.IsTrue(File.Exists(singleChannelFile));

            Assert.IsTrue(WaveEncoding.LoadFile(
                filepath: singleChannelFile,
                channels: out int channels,
                samples: out singleChannelSamples));

            Assert.IsTrue(channels == 1);
            Assert.IsTrue(singleChannelSamples != null);

            Assert.IsTrue(WaveEncoding.SaveFile(
                filepath: secondSingleChannel,
                channels: 1,
                samples: singleChannelSamples,
                overwrite: true));
        }

        [Test]
        public void TestDualChannelWave()
        {
            float[] dualChannelSamples = CreateSineWave(2);
            string dualChannelFile = DataManagement.PathForDataFile("Test", "dualChannel.wav");
            string secondDualChannel = DataManagement.PathForDataFile("Test", "dualChannel2.wav");

            Assert.IsTrue(WaveEncoding.SaveFile(
                filepath: dualChannelFile,
                channels: 2,
                samples: dualChannelSamples,
                overwrite: true));

            dualChannelSamples = null;

            Assert.IsTrue(File.Exists(dualChannelFile));

            Assert.IsTrue(WaveEncoding.LoadFile(
                filepath: dualChannelFile,
                channels: out int channels,
                samples: out dualChannelSamples));

            Assert.IsTrue(channels == 2);
            Assert.IsTrue(dualChannelSamples != null);

            Assert.IsTrue(WaveEncoding.SaveFile(
                filepath: secondDualChannel,
                channels: 2,
                samples: dualChannelSamples,
                overwrite: true));
        }

        private float[] CreateSineWave(int channels)
        {
            const float samplingRate = 44100f;
            const float duration = 1f;
            const float freq = 440f;

            int samplesPerChannel = (int)(samplingRate * duration);
            int sampleCount = samplesPerChannel * channels;

            float[] samples = new float[sampleCount];

            float arg = 2 * Mathf.PI * freq;

            for (int i = 0; i < samplesPerChannel; i++)
            {
                float time = i / samplingRate;
                for (int j = 0; j < channels; j++)
                {
                    samples[channels * i + j] = 0.25f * Mathf.Sin(arg * time);
                }
            }

            return samples;
        }


    }
}