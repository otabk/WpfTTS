using Microsoft.Win32;
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
using System.Linq;

namespace WpfPlayer
{
	//что-то нетак
	public partial class MainWindow : Window
	{
		WaveOutEvent Player = new WaveOutEvent();
		CancellationTokenSource cts = new CancellationTokenSource();

		public static List<string> notFounList = new List<string>();

		int _index = 0, _wordindex = 0;
		List<TWord[]> Gaplar = new List<TWord[]>();
		//Regex regex = new Regex(@"[^.!?]*[][.\s]");
		Regex regex = new Regex(@"(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?)\s");
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
			NullBackground();
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
			int[] sentencesInParagraph = new int[paragraphs.Length];
			List<string> gaplar = new List<string>();
			for (int i = 0; i < paragraphs.Length; i++)
			{
				var tempgaplar = regex.Split(paragraphs[i]);
				sentencesInParagraph[i] = tempgaplar.Length;
				foreach (string s in tempgaplar)
				{
					gaplar.Add(s);
				}
			}
			//var gaplar = regex.Matches(text);
			Gaplar = new List<TWord[]>();
			var missingSlogs = new List<string>();
			int slogIndex = 0, lastSlogIndex;
			//for (int i = 0; i < gaplar.Count; i++)
			for (int i = 0; i < gaplar.Count; i++)
			{
				//var suzlar = WordDivider.GetWords(gaplar[i].Value);
				var suzlar = WordDivider.GetWords(gaplar[i]);
				if (suzlar != null)
				{
					TWord[] words = new TWord[suzlar.Length];
					for (int j = 0; j < suzlar.Length; j++)
					{
						var sloglar = analyzer.Analyze(suzlar[j]);
						SWord[] sWords = null;
						if (sloglar != null)
						{
							lastSlogIndex = sloglar.Length - 1;
							sWords = new SWord[sloglar.Length];
							for (int l = 0; l < sloglar.Length; l++)
							{
								try
								{
									if (l == 0)
									{
										if (sloglar[l] == suzlar[j])
											slogIndex = 0;
										else
											slogIndex = 1;
									}
									else
									{
										if (l == lastSlogIndex)
											slogIndex = 3;
										else
											slogIndex = 2;
									}
									var audioPath = GetAudioPath(sloglar[l], slogIndex);
									if (audioPath != null)
										sWords[l] = new SWord() { Syllable = sloglar[l], TWavPath = audioPath }; // [0] ni o'rniga bo'g'inni so'zni qayerida kelishini yozish kerak
									else
									{
										if (!notFounList.Contains(audioPath))
											notFounList.Add(audioPath);
									}
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
					Gaplar.Add(words);
				}
			}
			using (var sw = File.AppendText("NotFoundedAudioFiles.txt"))
			{
				for (int i = 0; i < notFounList.Count; i++)
				{
					sw.WriteLine(notFounList[i]);
				}
			}
			//using (var sw = File.CreateText("TextData.json"))
			//{
			//	sw.Write(JsonConvert.SerializeObject(Gaplar, Formatting.Indented));
			//}
			if (gaplar.Count > 0)
			{
				for (int i = 0, j = 0; i < sentencesInParagraph.Length; i++)
				{
					//string s = paragraphs[i];
					//if (gaplar.Count > 0)
					var p = new Paragraph();
					for (int l = 0; l < sentencesInParagraph[i]; l++)
					{
						string gap = gaplar[j];
						//var str = gap.Value.Trim();
						var str = gap.Trim();
						var run = new Run(str);
						var emptyrun = new Run(" ");
						run.FontStretch = FontStretches.UltraExpanded;
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
			if (!Directory.Exists("\\Data"))
				Directory.CreateDirectory("\\Data");
			//using (var sr = File.OpenText("AudioPath.json"))
			//{
			//	string paths = sr.ReadToEnd();
			//	audioPaths = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(paths);
			//}
			//using (var sw = File.CreateText("AudioPath.json"))
			//{
			//	audioPaths = audioPaths.OrderBy(o => o.Key).ToDictionary(o => o.Key, o => o.Value);
			//	sw.Write(JsonConvert.SerializeObject(audioPaths));
			//}
			using (var sr = File.OpenText("!temp.txt"))
			{
				PrepareText(sr.ReadToEnd());
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
					if (_index < Gaplar.Count - 1)
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
			Dispatcher.Invoke(() =>
			{
				cts.Cancel();
				cts.Dispose();
				cts = new CancellationTokenSource();
				NullBackground();
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

		public string GetAudioPath(string slog, int sindex)
		{
			var translatedSlog = Translator.Translit(slog);
			var path = $"Data\\{translatedSlog}_{sindex}.wav";
			if (!File.Exists(path))
			{
				MessageBox.Show($"{path}.wav not founded!\n\rPlease, copy {translatedSlog}_{sindex}.wav to Data directory.", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
				return "";
			}
			return path;
		}

		public void NullBackground()
		{
			for (int i = 0; i < runsList.Count; i++)
			{
				runsList[i].Background = null;
			}
		}
	}
}
