using System.Text.RegularExpressions;

namespace WpfPlayer.Classes
{
	class FixNumbers
	{
		string _pattern = @"[0-9]+[.,][0-9]+";
		public FixNumbers()
		{
		}

		public string Fix (string text)
		{
			var matches = Regex.Matches(text, _pattern);
			for (int i = 0; i < matches.Count; i++)
			{

			}
			var reslttext = Regex.Replace(text, _pattern, "", RegexOptions.IgnoreCase);
			return text;
		}
	}
}
