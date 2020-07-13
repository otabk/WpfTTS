using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlayer.Classes
{
	class NumberToText
	{
		private Int64 _number = 0;
		private List<string> _result;
		private string[] _words, _word10, _w100n;
		public NumberToText(Int64 number)
		{
			_number = number;
			_words = new string[] { "бир", "икки", "уч", "тўрт", "беш", "олти", "етти", "саккиз", "тўққиз" };
			_word10 = new string[] { "ўн", "йигирма", "ўттиз", "қирқ", "эллик", "олтмиш", "етмиш", "саксон", "тўқсон" };
			_w100n = new string[] { "юз", "минг", "миллион", "миллиард", "триллион" };
		}

		void Divider(Int64 number)
		{
			var n = number.ToString().ToCharArray();
			var nlemght = n.Length;
			for (int i = 0, j = nlemght; i < nlemght; i++, j--)
			{
				switch (n[i])
				{
					case '1':
						_result.Add("бир");
						break;
					case '2':
						_result.Add("икки");
						break;
					case '3':
						_result.Add("уч");
						break;
					case '4':
						_result.Add("тўрт");
						break;
					case '5':
						_result.Add("беш");
						break;
					case '6':
						_result.Add("олти");
						break;
					case '7':
						_result.Add("етти");
						break;
					case '8':
						_result.Add("саккиз");
						break;
					case '9':
						_result.Add("тўққиз");
						break;
				}
			}
		}

		private string Num2Text(char n, int index)
		{
			var sb = new List<string>();
			var i = (int)n;

			switch (index)
			{
				case 1:
					sb.Add(DigitToText(n));
					break;
				case 2:
					sb.Add(Digit10ToText(n));
					sb.Add(DigitToText(n));
					break;
				case 3:
					sb.Add(DigitToText(n));
					sb.Add(_w100n[0]);
					break;
				case 4:
					sb.Add(DigitToText(n));
					sb.Add(_w100n[1]);
					break;
				case 5:
					sb.Add(Digit10ToText(n));
					sb.Add(DigitToText(n));
					sb.Add(_w100n[1]);
					break;
				case 6:
					sb.Add(DigitToText(n));
					sb.Add(_w100n[0]);
					sb.Add(Digit10ToText(n));
					sb.Add(_w100n[1]);
					break;
				case 7:
					sb.Add(_word10[6]);
					break;
				case 8:
					sb.Add(_word10[7]);
					break;
				case 9:
					sb.Add(_word10[8]);
					break;
				default:
					break;
			}
			return "";
		}

		private string DigitToText(char c)
		{
			string r = "";
			switch (c)
			{
				case '1':
					r = _words[0];
					break;
				case '2':
					r = _words[1];
					break;
				case '3':
					r = _words[2];
					break;
				case '4':
					r = _words[3];
					break;
				case '5':
					r = _words[4];
					break;
				case '6':
					r = _words[5];
					break;
				case '7':
					r = _words[6];
					break;
				case '8':
					r = _words[7];
					break;
				case '9':
					r = _words[8];
					break;
				default:
					break;
			}
			return r;
		}

		private string Digit10ToText(char c)
		{
			string r = "";
			switch (c)
			{
				case '1':
					r = _word10[0];
					break;
				case '2':
					r = _word10[1];
					break;
				case '3':
					r = _word10[2];
					break;
				case '4':
					r = _word10[3];
					break;
				case '5':
					r = _word10[4];
					break;
				case '6':
					r = _word10[5];
					break;
				case '7':
					r = _word10[6];
					break;
				case '8':
					r = _word10[7];
					break;
				case '9':
					r = _word10[8];
					break;
				default:
					break;
			}
			return r;
		}

		//private void DTS(int value1, int value2, int value3)
		//{
		//	switch (value1) // 1-чи сонни аниклаб уни текстга угириш
		//	{
		//		case 1:
		//			if (value1 == 1 & value2 == 0 & value3 == 0)
		//			{
		//				_result.Add("юз");
		//				break;
		//			}
		//			else
		//			{
		//				_result.Add("бир");
		//				break;
		//			}
		//		case 2:
		//			_result.Add("икки");
		//			break;
		//		case 3:
		//			_result.Add("уч");
		//			break;
		//		case 4:
		//			_result.Add("тўрт");
		//			break;
		//		case 5:
		//			_result.Add("беш");
		//			break;
		//		case 6:
		//			_result.Add("олти");
		//			break;
		//		case 7:
		//			_result.Add("етти");
		//			break;
		//		case 8:
		//			_result.Add("саккиз");
		//			break;
		//		case 9:
		//			_result.Add("тўққиз");
		//			break;
		//	}

		//	if (value1 != 0) // 1-чи аникланган сон юзликда булса у холда 1-сондан кейин "юз" матнини кушиш
		//	{
		//		if (value1 == 1 & value2 == 0 & value3 == 0) ;
		//		else
		//		{					
		//			_result.Add("юз");
		//		}
		//	}

		//	switch (value2)  // 2-чи сонни аниклаб уни текстга угириш
		//	{
		//		case 1:					
		//			_result.Add("ўн");
		//			break;
		//		case 2:					
		//			_result.Add("йигирма");
		//			break;
		//		case 3:					
		//			_result.Add("ўттиз");
		//			break;
		//		case 4:					
		//			_result.Add("қирқ");
		//			break;
		//		case 5:					
		//			_result.Add("эллик");
		//			break;
		//		case 6:					
		//			_result.Add("олтмиш");
		//			break;
		//		case 7:					
		//			_result.Add("етмиш");
		//			break;
		//		case 8:					
		//			_result.Add("саксон");
		//			break;
		//		case 9:					
		//			_result.Add("тўқсон");
		//			break;
		//	}

		//	switch (value3)   // 3-чи сонни аниклаб уни текстга угириш
		//	{
		//		case 1:					
		//			_result.Add("бир");
		//			break;
		//		case 2:					
		//			_result.Add("икки");
		//			break;
		//		case 3:					
		//			_result.Add("уч");
		//			break;
		//		case 4:					
		//			_result.Add("тўрт");
		//			break;
		//		case 5:					
		//			_result.Add("беш");
		//			break;
		//		case 6:					
		//			_result.Add("олти");
		//			break;
		//		case 7:					
		//			_result.Add("етти");
		//			break;
		//		case 8:					
		//			_result.Add("саккиз");
		//			break;
		//		case 9:					
		//			_result.Add("тўққиз");
		//			break;
		//	}


		//	if (flag == 1)
		//	{				
		//		_result.Add("миллиард");
		//	}

		//	if (flag == 2)
		//	{				
		//		_result.Add("миллион");
		//	}

		//	if (flag == 3)
		//	{				
		//		_result.Add("минг");
		//	}
		//}
	}
}
