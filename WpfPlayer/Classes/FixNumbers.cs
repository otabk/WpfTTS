﻿using System.Text.RegularExpressions;

namespace WpfPlayer.Classes
{
	class FixNumbers
	{
		Regex fractional = new Regex(@"[0-9]+[,.][0-9]+");
		public FixNumbers()
		{
		}

		public string Fix (string text)
		{
			var fmatch = fractional.Matches(text);
			if (fmatch.Count > 0)
			{
				fractional.Replace("", );
			}
			return text;
		}
	}
}
