using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfPlayer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string tempString;
		Regex regex = new Regex(@"[^.!?]*[.!?]");
		public MainWindow()
		{
			InitializeComponent();
			using(var fs = File.OpenText("temp.txt"))
			{
				tempString = fs.ReadToEnd();
			}
			var m = tempString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var i in m)
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
		}

		private void Run_MouseEnter(object sender, MouseEventArgs e)
		{
			var r = (Run)e.Source;
			r.Background = Brushes.Aquamarine;
		}

		private void Run_MouseLeave(object sender, MouseEventArgs e)
		{
			var r = (Run)e.Source;
			r.Background = Brushes.White;
		}

		private void Run_MouseLeftButtonDown(object sender, MouseEventArgs e)
		{
			var r = (Run)e.Source;
			MessageBox.Show(r.Text, "Selected sentense");
		}

		private void Paragraph_MouseEnter(object sender, MouseEventArgs e)
		{
			var p = (Paragraph)e.Source;
			p.Background = Brushes.Aquamarine;
		}

		private void Paragraph_MouseLeave(object sender, MouseEventArgs e)
		{
			var p = (Paragraph)e.Source;
			p.Background = Brushes.White;
		}

		private void Paragraph_MouseDown(object sender, MouseEventArgs e)
		{
			var p = (Paragraph)e.Source;
			if (e.LeftButton == MouseButtonState.Pressed)
				p.Inlines.FirstInline.Background = Brushes.Red;
			else
				p.Inlines.FirstInline.Background = Brushes.White;
		}
	}
}
