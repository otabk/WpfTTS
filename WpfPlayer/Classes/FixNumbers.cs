using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WpfPlayer.Classes
{
	class FixNumbers
	{
		static Regex pattern = new Regex(@"(\d+[,\.]+\d+)|([0-9]+)");
		static Regex ippattern = new Regex(@"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)");
		private static string[] _birlik = new string[] { "бир", "икки", "уч", "тўрт", "беш", "олти", "етти", "саккиз", "тўққиз" };
		private static string[] _10lik = new string[] { "ўн", "йигирма", "ўттиз", "қирқ", "эллик", "олтмиш", "етмиш", "саксон", "тўқсон" };
		private static string[] _1000lik = new string[] { "юз", "минг", "миллион", "миллиард", "триллион" };
		private static string[] _kasr = new string[] { "ўндан", "юздан", "мингдан", "ўн мингдан", "юз мингдан", "миллиондан", "ўн миллиондан", "юз миллиондан", "миллиарддан" };
		private static string _butun = "бутун";
		public FixNumbers()
		{
		}

		public static string Fix (string text)
		{
			text = FindIp(text);
			var matches = pattern.Matches(text).Cast<Match>().Select(m => m.Value).OrderByDescending(x=>x.Length).ToArray();
			
			if (matches.Length > 0)
			{
				for (int i = 0; i < matches.Length; i++)
				{
					var tmparr = matches[i].Split(new[] { ".", "," }, StringSplitOptions.RemoveEmptyEntries);
					string textnumber;
					if (tmparr.Length == 1)
					{
						textnumber = Num2Text(tmparr[0]);
					}
					else
					{
						textnumber = $"{ Num2Text(tmparr[0])} {_butun} { _kasr[tmparr[1].Length - 1]} { Num2Text(tmparr[1])}";
					}
					if (matches[i] == "499")
					{
						var a = 1;
					}
					text = text.Replace(matches[i], textnumber);
				}
			}
			return text;
		}

		private static string FindIp(string text)
		{
			var matches = ippattern.Matches(text);
			if (matches.Count > 0)
			{
				for (int i = 0; i < matches.Count; i++)
				{
					var temparr = matches[i].Value.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
					text = text.Replace(matches[i].Value, $"{Num2Text(temparr[0])} нуқта {Num2Text(temparr[1])} нуқта {Num2Text(temparr[2])} нуқта {Num2Text(temparr[3])}"); //xxx.xxx.xxx.xxx
				}
			}
			return text;
		}

		private static string Num2Text(string number)
		{
			var sb = new List<string>();
			var charArray = number.ToCharArray();
			var numbers = Array.ConvertAll(charArray, i => (int)char.GetNumericValue(i));
			var nlemght = numbers.Length;
			for (int i = 0, j = nlemght; i < nlemght; i++, j--)
			{
				switch (j)
				{
					case 1:
						sb.Add(DigitToText(numbers[i]));
						break;
					case 2:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 3:
						if (numbers[i] == 0)
							break;
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 4:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[1]);
						break;
					case 5:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 6:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 7:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[2]);
						break;
					case 8:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 9:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 10:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[3]);
						break;
					case 11:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 12:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 13:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[4]);
						break;
					case 14:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 15:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					//todo kasr sonlarni qoshish
					default:
						break;
				}
			}
			return string.Join(" ", sb.ToArray());
		}

		private static string DigitToText(int c)
		{
			string r = "";
			switch (c)
			{
				case 1:
					r = _birlik[0];
					break;
				case 2:
					r = _birlik[1];
					break;
				case 3:
					r = _birlik[2];
					break;
				case 4:
					r = _birlik[3];
					break;
				case 5:
					r = _birlik[4];
					break;
				case 6:
					r = _birlik[5];
					break;
				case 7:
					r = _birlik[6];
					break;
				case 8:
					r = _birlik[7];
					break;
				case 9:
					r = _birlik[8];
					break;
				default:
					break;
			}
			return r;
		}

		private static string Digit10ToText(int c)
		{
			string r = "";
			switch (c)
			{
				case 1:
					r = _10lik[0];
					break;
				case 2:
					r = _10lik[1];
					break;
				case 3:
					r = _10lik[2];
					break;
				case 4:
					r = _10lik[3];
					break;
				case 5:
					r = _10lik[4];
					break;
				case 6:
					r = _10lik[5];
					break;
				case 7:
					r = _10lik[6];
					break;
				case 8:
					r = _10lik[7];
					break;
				case 9:
					r = _10lik[8];
					break;
				default:
					break;
			}
			return r;
		}
	}
}
