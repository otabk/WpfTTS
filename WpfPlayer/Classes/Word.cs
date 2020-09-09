using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Threading;

namespace WpfPlayer.Classes
{
	class SWord
	{
		public string Syllable { get; set; }
		public string TWavPath { get; set; }
	}
	class TWord
	{
		public SWord[] Syllables { get; set; }
		public string Word { get; set; }
		public ConcatenatingSampleProvider Wav = null;
		 
		public void Init()
		{
			ISampleProvider[] sources = new ISampleProvider[Syllables.Length];
			for (int i = 0; i < Syllables.Length; i++)
			{
				var wavSource = new AudioFileReader(Syllables[i].TWavPath);
				sources[i] = wavSource.Take(new TimeSpan(0, 0, 0, 0, (int)(wavSource.TotalTime.Milliseconds * 0.8)));
			}
			Wav = new ConcatenatingSampleProvider(sources);
		}

		/*public void Play()
		{
			using(var wo = new WaveOutEvent())
			{
				wo.Init(Wav);
				wo.Play();
				while(wo.PlaybackState == PlaybackState.Playing) Thread.Sleep(10);
			}
		}*/
	}
}
