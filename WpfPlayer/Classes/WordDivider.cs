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
		private char[] _trimChars = { ' ', '.', '!', ':', ';', '-' };

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
			if (string.IsNullOrEmpty(Text))
			{
				return null;
			}
			List<string> words = Text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
			for (int i = 0; i < words.Count; i++)
			{
				words[i] = words[i].Trim(_trimChars);
			}
			return words;
		}
	}
}
