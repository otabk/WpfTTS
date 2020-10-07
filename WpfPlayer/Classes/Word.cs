using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Windows;

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
			try
			{
				ISampleProvider[] sources = new ISampleProvider[Syllables.Length];
				for (int i = 0; i < Syllables.Length; i++)
				{
					var wavSource = new AudioFileReader(Syllables[i].TWavPath);
					sources[i] = wavSource.Take(new TimeSpan(0, 0, 0, 0, (int)(wavSource.TotalTime.Milliseconds * 0.9))).Skip(new TimeSpan(0, 0, 0, 0, (int)(wavSource.TotalTime.Milliseconds * 0.15)));
				}
				Wav = new ConcatenatingSampleProvider(sources);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
	}
}
