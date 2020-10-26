using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WpfPlayer.Classes
{
	class SWord //бўғин ва файл
	{
		[JsonProperty("slog", Order = 1)]
		public string Syllable { get; set; }

		[JsonProperty("path", Order = 2)]
		public string TWavPath { get; set; }
	}

	class TWord //сўз ва бўғинлар массиви ҳамда товуш
	{
		[JsonProperty("word", Order = 1)]
		public string Word { get; set; }

		[JsonProperty("slogs", Order = 2)]
		public SWord[] Syllables { get; set; }

		[JsonIgnore]
		public ConcatenatingSampleProvider Wav = null;

		public void Init()
		{
			try
			{
				ISampleProvider[] sources = new ISampleProvider[Syllables.Length];
				for (int i = 0; i < Syllables.Length; i++)
				{
					if (File.Exists(Syllables[i].TWavPath))
					{
						var wavSource = new AudioFileReader(Syllables[i].TWavPath);
						sources[i] = wavSource.Take(new TimeSpan(0, 0, 0, 0, (int)(wavSource.TotalTime.Milliseconds * 0.9))).Skip(new TimeSpan(0, 0, 0, 0, (int)(wavSource.TotalTime.Milliseconds * 0.15)));
					}
					else
					{
						if (!MainWindow.notFounList.Contains(Syllables[i].TWavPath))
							MainWindow.notFounList.Add(Syllables[i].TWavPath);
						sources[i] = null;
					}
				}
				if(sources != null)
					Wav = new ConcatenatingSampleProvider(sources);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
