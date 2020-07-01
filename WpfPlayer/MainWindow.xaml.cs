using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPlayer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string tempString;
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
				var p = new Paragraph(new Run("\t" + i));
				p.MouseEnter += Paragraph_MouseEnter;
				p.MouseLeave += Paragraph_MouseLeave;
				rtbx.Document.Blocks.Add(p);
			}
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
	}
}
