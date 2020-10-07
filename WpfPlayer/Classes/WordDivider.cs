using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfPlayer.Classes
{
	static class WordDivider
	{
		static Regex regex = new Regex(@"[a-яA-Я0-9ёўқҳғЁЎҚҲҒ]+");

		//[a-яA-Я0-9ўқҳғЎҚҲҒ]+ ushbu regex barcha so'zlarni oladi lekin matn ichidagi belgilarni olmayapti masalan '»' belgisini   

		public static string Text { get; set; }

		public static string[] GetWords()
		{
			//TODO: raqamlarni va qisqartirilgan so'zlar bilan ishlash, masalan BMT
			if (string.IsNullOrEmpty(Text))
			{
				return null;
			}
			Text = Text.Replace("-", " ").Replace("«", "").Replace("»", "");
			Text = FixNumbers.Fix(Text);
			var words = new List<string>();
			var matches = regex.Matches(Text);
			foreach (Match m in matches)
			{
				words.Add(m.Value);
			}
			return words.ToArray();
		}

		public static string[] GetWords(string word)
		{
			if (string.IsNullOrEmpty(word))
			{
				return null;
			}
			word = word.Replace("-", " ").Replace("«", "").Replace("»", "");
			word = FixNumbers.Fix(word);
			var words = new List<string>();
			var matches = regex.Matches(word);
			foreach (Match m in matches)
			{
				words.Add(m.Value);
			}
			return words.ToArray();
		}
	}
}
