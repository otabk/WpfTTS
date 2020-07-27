using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WpfPlayer.Classes
{
	class SentenceDivider
	{
		Regex regex = new Regex(@"[^.!?]*[.!?]");
		private string _text;

		public SentenceDivider(string text)
		{
			_text = text;
		}

		public List<string> GetSentences()
		{
			List<string> list = null;
			if (!string.IsNullOrEmpty(_text))
			{
				list = new List<string>();
				var matchs = regex.Matches(_text);
				if (matchs.Count > 0)
				{
					foreach (Match item in matchs)
					{
						var s = item.Value.Trim();
						list.Add(s);
					}
				}
			}
			return list;
		}
	}
}
