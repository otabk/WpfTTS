﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfPlayer.Classes;
using NAudio.Wave;
using System.Threading.Tasks;

namespace WpfPlayer
{
	//что-то нетак
	public partial class MainWindow : Window
	{
		WaveOutEvent Player = new WaveOutEvent();
		CancellationTokenSource cts = new CancellationTokenSource();


		int _index = 0, _wordindex = 0;
		Dictionary<int, TWord[]> Gaplar = new Dictionary<int, TWord[]>();
		Regex regex = new Regex(@"[^.!?]*[.!?]");
		List<Run> runsList = new List<Run>();
		Analyzer analyzer = new Analyzer();
		Dictionary<string, List<string>> audioPaths;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Run_MouseEnter(object sender, MouseEventArgs e)
		{
			//var r = (Run)sender;
			//r.Background = Brushes.LightSkyBlue;
		}

		private void Run_MouseLeave(object sender, MouseEventArgs e)
		{
			//var r = (Run)sender;
			//r.Background = null;
		}

		private void Run_MouseLeftButtonDownAsync(object sender, MouseEventArgs e)
		{
			var r = (Run)sender;
			_index = runsList.IndexOf(r);
			r.Background = Brushes.LightSkyBlue;
		}

		private void OpenFileMenu_Click(object sender, RoutedEventArgs e)
		{
			string text, file_path;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				file_path = openFileDialog.FileName;
				text = File.ReadAllText(file_path, Encoding.GetEncoding("utf-8"));
				Title = file_path;
				PrepareText(text);
			}
		}

		private void CloseWindowMenu_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		void PrepareText(string text) //matn kiritiladi
		{
			//using(var fs = File.OpenText("temp.txt"))
			//{
			//	tempString = fs.ReadToEnd();
			//}
			rtbx.Document.Blocks.Clear();
			runsList = new List<Run>();
			var paragraphs = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); //matnni abzatslarga bo'ladi
																											   //_sd = new SentenceDivider(text);
			var gaplar = regex.Matches(text); //abzatsni gaplarga bo'ladi
			Gaplar = new Dictionary<int, TWord[]>();
			var missingSlogs = new List<string>();
			for (int i = 0; i < gaplar.Count; i++)
			{
				var suzlar = WordDivider.GetWords(gaplar[i].Value);
				if (suzlar != null)
				{
					TWord[] words = new TWord[suzlar.Length];
					for (int j = 0; j < suzlar.Length; j++)
					{
						var sloglar = analyzer.Analyze(suzlar[j]);
						SWord[] sWords = new SWord[sloglar.Length];
						if (sloglar != null)
						{
							for (int l = 0; l < sloglar.Length; l++)
							{
								var translatedSlog = Translator.Translit(sloglar[l]);
								try
								{
									sWords[l] = new SWord() { Syllable = sloglar[l], TWavPath = audioPaths[translatedSlog][0] }; // [0] ni o'rniga bo'g'inni so'zni qayerida kelishini yozish kerak
								}
								catch
								{
									missingSlogs.Add(sloglar[l]);
								}
							}
						}
						words[j] = new TWord() { Word = suzlar[j], Syllables = sWords };
						words[j].Init();
					}
					Gaplar.Add(i, words);
				}
			}
			for (int i = 0, j = 0; i < paragraphs.Length; i++)
			{
				string s = paragraphs[i];
				if (gaplar.Count > 0)
				{
					var p = new Paragraph();
					foreach (Match gap in gaplar)
					{
						var str = gap.Value.Trim();
						var run = new Run(str);
						var emptyrun = new Run(" ");
						run.MouseEnter += Run_MouseEnter;
						run.MouseLeave += Run_MouseLeave;
						run.MouseLeftButtonDown += Run_MouseLeftButtonDownAsync;
						p.Inlines.Add(run);
						p.Inlines.Add(emptyrun);

						runsList.Add(run);
						j++;
					}
					p.Inlines.Remove(p.Inlines.LastInline);
					rtbx.Document.Blocks.Add(p);
				}
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			using (var sr = File.OpenText("AudioPath.txt"))
			{
				string paths = sr.ReadToEnd();
				audioPaths = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(paths);
			}
		}

		private void Btn_Click(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			switch (btn.Name)
			{
				case "PrevBtn":
					if (_index > 0)
					{
						Stop();
						_index -= 1;
						Play(cts.Token);
					}
					break;
				case "StopBtn":
					Stop();
					break;
				case "PlayBtn":
					Stop();
					Play(cts.Token);
					break;
				case "NextBtn":
					if (_index < Gaplar.Count)
					{
						_index += 1;
						Stop();
						Play(cts.Token);
					}
					break;
				default:
					break;
			}
		}

		public async void Play(CancellationToken token)
		{
			await Task.Run(() =>
			{
				if (token.IsCancellationRequested) { Stop(); return; }
				for (int i = _index; i < Gaplar.Count; i++, _index++)
				{
					if (token.IsCancellationRequested) { Stop(); return; }
					Dispatcher.Invoke(() =>
					{
						if (_index != 0)
							runsList[_index - 1].Background = null;
						runsList[_index].Background = Brushes.LightSkyBlue;
					});
					_wordindex = 0;
					for (int j = _wordindex; j < Gaplar[_index].Length; j++)
					{
						Player = new WaveOutEvent();
						if (token.IsCancellationRequested) { Stop(); return; }
						Player.Init(Gaplar[_index][j].Wav);
						Player.Play();
						while (Player.PlaybackState == PlaybackState.Playing)
						{
							Thread.Sleep(100);
							if (token.IsCancellationRequested) return;
						}
					}
				}
			});
		}

		public void Stop()
		{
			//await Task.Run(() =>
			//{
			Dispatcher.Invoke(() =>
			{
				cts.Cancel();
				cts.Dispose();
				cts = new CancellationTokenSource();
				if(runsList.Count != 0)
					runsList[_index].Background = null;
				for (int i = 0; i < Gaplar.Count; i++)
				{
					for (int j = 0; j < Gaplar[i].Length; j++)
					{
						Gaplar[i][j].Init();
					}					
				}
			});
			_wordindex = 0;
			Player.Stop();
			Player.Dispose();
			//});
		}

		public void DisposeWave()
		{
			if (Player != null)
			{
				if (Player.PlaybackState == PlaybackState.Playing)
					Player.Stop();
				Player.Dispose();
				Player = null;
			}
		}
	}
}
