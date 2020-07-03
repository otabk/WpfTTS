using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlayer.Classes
{
	class NumberToText
	{
		private Int64 _number = 0;
		private List<string> _result;
		private string[] _word10, _word100, _wordk, _wordm, _wordb, _wordt, _word100, _word100;
		public NumberToText(Int64 number)
		{
			_number = number;
			_word10 = new string[] { "ўн", "йигирма", "ўттиз", "қирқ", "эллик", "олтмиш", "етмиш", "саксон", "тўқсон", };
		}

		void Divider(Int64 number)
		{
			var n = number.ToString().ToCharArray();
			var nlemght = n.Length;
			for (int i = nlemght, j = 0; i > 0; i--, j++)
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
