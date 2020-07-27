﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfPlayer.Classes;

namespace WpfPlayer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string tempString;
		private Regex regex = new Regex(@"[^.!?]*[.!?]");
		SentenceDivider _sd;
		WordDivider wordDivider = new WordDivider();
		List<WordsDB> wordsDBs;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Run_MouseEnter(object sender, MouseEventArgs e)
		{
			var r = (Run)sender;
			r.Background = Brushes.Aquamarine;
		}

		private void Run_MouseLeave(object sender, MouseEventArgs e)
		{
			var r = (Run)sender;
			r.Background = Brushes.White;
		}

		private void Run_MouseLeftButtonDown(object sender, MouseEventArgs e)
		{
			var r = (Run)sender;
			var t = r.Text.Replace(".", "");
			wordDivider.Text = t;
			var words = wordDivider.GetWords();
			List<string> slogs = new List<string>();
			foreach (string i in words)
			{
				
				slogs.Add((wordsDBs.First(w => w.Word == i)).Slog);
			}
			MessageBox.Show(string.Join(" ", slogs), "Selected sentense");
		}

		private void Paragraph_MouseEnter(object sender, MouseEventArgs e)
		{
			var p = (Paragraph)sender;
			p.Background = Brushes.Aquamarine;
		}

		private void Paragraph_MouseLeave(object sender, MouseEventArgs e)
		{
			var p = (Paragraph)sender;
			p.Background = Brushes.White;
		}

		private void Paragraph_MouseDown(object sender, MouseEventArgs e)
		{
			var p = (Paragraph)sender;
			if (e.LeftButton == MouseButtonState.Pressed)
				p.Inlines.FirstInline.Background = Brushes.Red;
			else
				p.Inlines.FirstInline.Background = Brushes.White;
		}

		private void OpenFileMenu_Click(object sender, RoutedEventArgs e)
		{
			string text, file_path;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				file_path = openFileDialog.FileName;
				text = File.ReadAllText(file_path, Encoding.GetEncoding("utf-8")).ToLower();
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
					var p = new Paragraph(new Run("\t"));
					foreach (Match g in c)
					{
						var run = new Run(g.Value);
						run.MouseEnter += Run_MouseEnter;
						run.MouseLeave += Run_MouseLeave;
						run.MouseLeftButtonDown += Run_MouseLeftButtonDown;
						p.Inlines.Add(run);
					}
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
			var sentences = _sd.GetSentences();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			using (var fs = File.OpenText("WordSlog2019.json"))
			{
				tempString = fs.ReadToEnd();
			}
			wordsDBs = JsonConvert.DeserializeObject<List<WordsDB>>(tempString);
		}
	}
}
