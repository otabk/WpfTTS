using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlayer.Classes
{
	static class Translator
	{
		//private string _word;
		/*public Translator(string word)
		{
			_word = word;
		}*/

        public static string Translit(string str)
        {
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "j", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "x", "ts", "ch", "sh", "\'", "e", "yu", "ya", "h", "g\'", "q", "o\'" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё" , "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х",  "ц",  "ч",  "ш", "ъ",  "э",  "ю",  "я", "ҳ", "ғ", "қ", "ў" };
            for (int i = 0; i < 34; i++)
            {
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }
    }
}
