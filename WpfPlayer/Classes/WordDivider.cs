using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfPlayer.Classes
{
	class WordDivider
	{
		Regex regex = new Regex(@"[A-Za-z]+");

		public string Text { get; set; }

		public WordDivider(string s)
		{
			Text = s;
		}

		public WordDivider()
		{

		}

		public List<string> GetWords()
		{
			//TODO: raqamlarni va qisqartirilgan so'zlar bilan ishlash, masalan BMT
			if (string.IsNullOrEmpty(Text))
			{
				return null;
			}
			var words = new List<string>();
			var matches = regex.Matches(Text);
			foreach (Match m in matches)
			{
				words.Add(m.Value);
			}
			return words;
		}

		public List<string> GetWords(string word)
		{
			if (string.IsNullOrEmpty(word))
			{
				return null;
			}
			var words = new List<string>();
			var matches = regex.Matches(word);
			foreach (Match m in matches)
			{
				words.Add(m.Value);
			}
			return words;
		}
	}
	}
}
