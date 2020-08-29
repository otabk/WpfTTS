using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfPlayer.Classes;

namespace WpfPlayer
{
	public partial class MainWindow : Window
	{
		public SoundPlayer soundPlayer = new SoundPlayer();
		public int _index = 0, _wavindex = 0;
		public List<string> wavList;;
		private Regex regex = new Regex(@"[^.!?]*[.!?]");
		SentenceDivider _sd;
		WordDivider wordDivider = new WordDivider();
		List<Run> runs = new List<Run>();
		Analyzer analyzer = new Analyzer();
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Run_MouseEnter(object sender, MouseEventArgs e)
		{
			var r = (Run)sender;
			r.Background = Brushes.LightSkyBlue;
		}

		private void Run_MouseLeave(object sender, MouseEventArgs e)
		{
			var r = (Run)sender;
			r.Background = null;
		}

		private void Run_MouseLeftButtonDown(object sender, MouseEventArgs e)
		{
			var r = (Run)sender;
			_index = runs.IndexOf(r);
			wordDivider.Text = r.Text;
			var words = wordDivider.GetWords();
			List<string> tempslogs = new List<string>();
			List<string> slogs; ;
			wavList = new List<string>();
			foreach (string i in words)
			{
				slogs = (List<string>)analyzer.Analyze(i); //0 1 0 1 1 0
				tempslogs.Add(string.Join("-", slogs));
				slogs.Add("<ws>"); //probel belgisi
			}
			string result = string.Join(" ", tempslogs);
			MessageBox.Show(result, $"Selected sentense [id = {_index}].");
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
			
		}

		void PrepareText(string text)
		{
			//using(var fs = File.OpenText("temp.txt"))
			//{
			//	tempString = fs.ReadToEnd();
			//}
			var paragraphs = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			_sd = new SentenceDivider(text);
			foreach (var i in paragraphs)
			{
				var c = regex.Matches(i);
				if (c.Count > 0)
				{
					var p = new Paragraph();	// { Padding = new Thickness(10, 0, 0, 0) };
					foreach (Match g in c)
					{
						var run = new Run(g.Value.Trim());
						var emptyrun = new Run(" ");
						run.MouseEnter += Run_MouseEnter;
						run.MouseLeave += Run_MouseLeave;
						run.MouseLeftButtonDown += Run_MouseLeftButtonDown;
						p.Inlines.Add(run);
						p.Inlines.Add(emptyrun);
						runs.Add(run);
					}
					p.Inlines.Remove(p.Inlines.LastInline);
					rtbx.Document.Blocks.Add(p);
				}
				//var g = i.Split(new[] { ".","!","?","," }, StringSplitOptions.RemoveEmptyEntries);

				//var p = new Paragraph(new Run("\t" + i));
				//foreach (var i in g)
				//{
				//	var run = new Run(s);
				//	run.MouseEnter += Run_MouseEnter;
				//	run.MouseLeave += Run_MouseLeave;
				//	p.Inlines.Add(run);
				//}
				//p.MouseEnter += Paragraph_MouseEnter;
				//p.MouseLeave += Paragraph_MouseLeave;
				//rtbx.Document.Blocks.Add(p);
			}
			//var sentences = _sd.GetSentences();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//using (var fs = File.OpenText("WordSlog2019.json"))
			//{
			//	tempString = fs.ReadToEnd();
			//}
			//wordsDBs = JsonConvert.DeserializeObject<List<WordsDB>>(tempString);
		}

		private void PrevBtn_Click(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			switch (btn.Name)
			{
				case "PrevBtn":
					if (_wavindex > 0)
					{
						_wavindex -= 1;
						soundPlayer.SoundLocation = "";
					}
					break;
				case "StopBtn":
					break;
				case "PlayBtn":
					break;
				case "NextBtn":
					break;
				default:
					break;
			}
		}
	}
}
