using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace WpfPlayer.Classes
{
	class Analyzer
	{
		public string Text;
		int totalword = 0;
		string sword, acword, fullword;
		int[] posStr = new int[30];

		public Analyzer(string text)
		{
			Text = text;
		}

		private void Analyze(string text)
		{
			char[] char_array = text.ToCharArray();
			int Symbol = text.Length;                   // Катордаги белгилар сони
			int wordlen = 0;
			int unliall = 0;                            // Унли харфлар сони
			int pos = 0;                                // Бугинни жойини аниклаш
			int unli = 0;                               // Унли харфларни санаш
			List<string> strarr = new List<string>();   // Сузлар массиви (сигими 100 та суз)
			int nword = 0;                              // Сузлар хисоблагичи
			int probel = 1;                             // Пробел мавжудлиги

			//var word_array = text.Split(new string[] { " ", ".", ",", "-", ".", "." }, StringSplitOptions.RemoveEmptyEntries);
			// Каторни ва сузни тахлил килиш параметрлари (охири)-----------------------
			for (int i = 0; i < Symbol; i++)
			{
				if (char_array[i] != 65279) // UTF-8 ни англатувчи BOM белгини олиб ташлаш
				{
					if (char_array[i] != 45 & char_array[i] != 32) //Чизикча "-" ва пробелга " " тенг булмаса
					{
						acword += char_array[i]; // Харфлардан сузни йигиш
						probel = 0;
					}
					else
					{
						if (probel != 1)  // агар олдинги белги пробел ёки чизикча булмаса
						{
							strarr[nword] = acword; // сузни массивга ёзиш 
							acword = "";           // узгарувчини бушатиш
							nword++;
						}
						probel = 1;
					}
				}
			}
			if (probel != 1)
			{
				strarr[nword] = acword;
				acword = "";
			}
			else nword--;
			// Сузларни тахлил килиш учун массивдан олиш (боши)--------------------
			for (int i = 0; i < nword + 1; i++) //nword+1
			{
				pos = 0;
				unliall = 0;
				unli = 0;
				sword = strarr[i];
				wordlen = sword.Length;

				// Суздаги унли харфлар сонини аниклаш - кодировка Unicode - UTF-8
				for (int j = 0; j < wordlen; j++)
				{
					if (sword[j] == 1072 || //а
								sword[j] == 1040 || //А
								sword[j] == 1086 || //о
								sword[j] == 1054 || //О
								sword[j] == 1080 || //и
								sword[j] == 1048 || //И
								sword[j] == 1101 || //э
								sword[j] == 1069 || //Э
								sword[j] == 1091 || //у
								sword[j] == 1059 || //У
								sword[j] == 1118 || //ў
								sword[j] == 1038 || //Ў
								sword[j] == 1077 || //е
								sword[j] == 1045 || //Е
								sword[j] == 1105 || //ё
								sword[j] == 1025 || //Ё
								sword[j] == 1102 || //ю
								sword[j] == 1070 || //Ю
								sword[j] == 1103 || //я
								sword[j] == 1071)  //Я
					{
						posStr[j] = 0;
						unliall++;
					}
					{
						if (sword[i] == 1066 || (int)sword[i] == 1098) //Ъ ва ъ белгиларини аниклаш
						{
							posStr[i] = 2;
						}
						else
						{
							if (sword[i] == 1068 || (int)sword[i] == 1100) //Ь ва ь белгиларини аниклаш
								posStr[i] = 3;
							else
								posStr[i] = 1;
						}
					}
					// агар суз бир бугиндан иборат булса
					if (unliall == 1)
					{
						//fileout << sword << endl;  //faylga yozish???
					}
					else 
					{
						for (int k = 0; k < unliall - 1; k++)
						{

							// "Аэро" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 & unliall - unli > 2) & ((int)sword[pos] == 1040 || (int)sword[pos] == 1072))
							{
								if (((int)sword[pos] == 1040 & (int)sword[pos + 1] == 1069 & (int)sword[pos + 2] == 1056 & (int)sword[pos + 3] == 1054) ||
										((int)sword[pos] == 1040 & (int)sword[pos + 1] == 1101 & (int)sword[pos + 2] == 1088 & (int)sword[pos + 3] == 1086) ||
										((int)sword[pos] == 1072 & (int)sword[pos + 1] == 1101 & (int)sword[pos + 2] == 1088 & (int)sword[pos + 3] == 1086))
								{
									if (wordlen == 4)
										fullword = fullword + sword[pos] + "-" + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
									else
										fullword = fullword + sword[pos] + "-" + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
									pos = pos + 4;
									unli = unli + 3;
									k = k + 3;
									if (wordlen - pos == 0)
									{
										//filega yozish;
										//fileout << fullword << endl;
										goto endword;
									}
									if (unliall - unli == 1) goto lastsyl;
									//i++;
								}
							}

							/*
									// "Авиа" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
										if ((wordlen-pos > 3 & unliall-unli > 2) & ((int)sword[pos] == 1040 || (int)sword[pos] == 1072))
										{
										if (((int)sword[pos+0] == 1040 & (int)sword[pos+1] == 1069 & (int)sword[pos+2] == 1056 & (int)sword[pos+3] == 1054 ) ||
											((int)sword[pos+0] == 1040 & (int)sword[pos+1] == 1101 & (int)sword[pos+2] == 1088 & (int)sword[pos+3] == 1086 ) ||
											((int)sword[pos+0] == 1072 & (int)sword[pos+1] == 1101 & (int)sword[pos+2] == 1088 & (int)sword[pos+3] == 1086 ))
											{
												if (wordlen == 4)
													fullword = fullword + sword[pos+0] + "-" + sword[pos+1] + "-" + sword[pos+2] + sword[pos+3];
												else
													fullword = fullword + sword[pos+0] + "-" + sword[pos+1] + "-" + sword[pos+2] + sword[pos+3] + "-";
												pos = pos+4;
												unli = unli + 3;
												i = i + 3;
												if (wordlen-pos == 0)
												{
													fileout << fullword << endl;
													goto endword;
												}
												if (unliall-unli == 1) goto lastsyl;
												//i++;
											}
										}
							*/

							// "Фото" ва "Фотон" сузларини ажратиб олиш-----------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 1060 || (int)sword[pos] == 1092))
							{
								if (((int)sword[pos + 0] == 1060 & (int)sword[pos + 1] == 1054 & (int)sword[pos + 2] == 1058 & (int)sword[pos + 3] == 1054) ||
										((int)sword[pos + 0] == 1060 & (int)sword[pos + 1] == 1086 & (int)sword[pos + 2] == 1090 & (int)sword[pos + 3] == 1086) ||
										((int)sword[pos + 0] == 1092 & (int)sword[pos + 1] == 1086 & (int)sword[pos + 2] == 1090 & (int)sword[pos + 3] == 1086))
								{
									if (wordlen == 4)
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
										pos = pos + 4;
									}
									else
									{
										if (wordlen == 5)
										{
											fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4];
											pos = pos + 5;
										}
										else
										{
											if (((int)sword[pos + 4] == 1053 || (int)sword[pos + 4] == 1085) & posStr[pos + 5] == 1)
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-";
												pos = pos + 5;
											}
											else
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
												pos = pos + 4;
											}
										}
									}

									unli = unli + 2;
									k = k + 2;

									if (wordlen - pos == 0)
									{
										//fileout << fullword << endl;
										goto endword;
									}
									if (unliall - unli == 1)
									{
										goto lastsyl;
									}
								}
							}
							// "Теле" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 1058 || (int)sword[pos] == 1090))
							{
								if (((int)sword[pos + 0] == 1058 || (int)sword[pos + 0] == 1090) & // Т ёки т
										((int)sword[pos + 1] == 1045 || (int)sword[pos + 1] == 1077) & // Е ёки е
										((int)sword[pos + 2] == 1051 || (int)sword[pos + 2] == 1083) & // Л ёки л
										((int)sword[pos + 3] == 1045 || (int)sword[pos + 3] == 1077)) // Е ёки е
								{
									if (wordlen == 4)
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
									else
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
									pos = pos + 4;
									unli = unli + 2;
									k = k + 2; // бугинлар сони
									if (wordlen - pos == 0)
									{
										//fileout << fullword << endl;
										goto endword;
									}
									if (unliall - unli == 1) goto lastsyl;
									//i++;
								}
							}


							// "Авто" ва "Автор" сузларини ажратиб олиш-----------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 1040 || (int)sword[pos] == 1072))
							{
								if (((int)sword[pos + 0] == 1040 || (int)sword[pos + 0] == 1072) & // А ёки а
										((int)sword[pos + 1] == 1042 || (int)sword[pos + 1] == 1074) & // В ёки в
										((int)sword[pos + 2] == 1058 || (int)sword[pos + 2] == 1090) & // Т ёки т
										((int)sword[pos + 3] == 1054 || (int)sword[pos + 3] == 1086)) // О ёки о
								{
									if (wordlen == 4)
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
										pos = pos + 4;
									}
									else
									{
										if (wordlen == 5)
										{
											fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4];
											pos = pos + 5;
										}
										else
										{
											if ((((int)sword[pos + 4] == 1056 || (int)sword[pos + 4] == 1088) ||                    // Р ёки р (авто-р)
													((int)sword[pos + 4] == 1051 || (int)sword[pos + 4] == 1083)) & posStr[pos + 5] == 1) // Л ёки л (авто-л)
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-";
												pos = pos + 5;
											}
											else
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
												pos = pos + 4;
											}
										}
									}

									unli = unli + 2;
									k = k + 2;

									if (wordlen - pos == 0)
									{
										//fileout << fullword << endl;
										goto endword;
									}
									if (unliall - unli == 1) goto lastsyl;
								}
							}

							// охири "ё" билан тугаган сузлар билан ишлаш
							if ((unliall - unli == 2) & ((int)sword[wordlen - 1] == 1025 || (int)sword[wordlen - 1] == 1105)) // 2 та унли ва охири "ё" булса
							{
								for (int l = pos; l < wordlen - 1; l++) fullword = fullword + sword[l];
								fullword = fullword + "-" + sword[wordlen - 1];
								//fileout << fullword << endl;
								goto wordfin;
							}

							// Агар суз унли харфдан бошланса------------------------------------------------------------------------------------
							if (posStr[pos] == 0)
							{
								if (wordlen - pos > 4) // агар суз узунлиги 4 харфдан катта булса
								{
									// 0111+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //гссс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //011 ни олиш (авжланмок, антракт)
										pos = pos + 3;
										unli++;
										goto endunli;
									}
								}

								if (wordlen - pos > 3) // агар суз узунлиги 3 харфдан катта булса
								{
									// 011+1 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1) //гсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //011 ни олиш
										pos = pos + 3;
										unli++;
										goto endunli;
									}
									// 011+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 0) //гсс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //01 ни олиш
										pos = pos + 2;
										unli++;
										goto endunli;
									}
									// 02 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 2) //г+ъ
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //02 ни олиш
										pos = pos + 2;
										unli++;
										goto endunli;
									}
									// 012 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 2) //гс+ъ
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //012 ни олиш
										pos = pos + 3;
										unli++;
										goto endunli;
									}
								}
								// агар суз узунлиги 3 харф ва ундан кичик булса
								{
									// 013 холатни аниклаш (альфа, ультра) юмшатиш белгили сузлар
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 3) // гсь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //013 ни олиш
										pos = pos + 3;
										unli++;
										goto endunli;
									}

									// 01+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0) //сг+с
									{
										if ((sword[pos + 1] == 1049 || sword[pos + 1] == 1081) & (sword[pos + 2] == 1025 || sword[pos + 2] == 1105)) // агар унлидан кейин "йё" булса
										{
											fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //01 ни олиш (унли+й ни олиш) - ай-ёр, ай-ём
											pos = pos + 2;
											unli++;
											goto endunli;
										}
										else
										{
											fullword = fullword + sword[pos + 0] + "-"; //0 ни олиш (акс холда факат унлини олиш)
											pos = pos + 1;
											unli++;
											goto endunli;
										}
									}
									// 0+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 0) //г+г
									{
										fullword = fullword + sword[pos + 0] + "-"; //0 ни олиш
										pos = pos + 1;
										unli++;
										goto endunli;
									}
								}
							endunli:;
							}

							// Агар суз ундош харфдан бошланса
							else
							{
								if (wordlen - pos > 5) // агар суз узунлиги 5 харфдан катта булса
								{
									// 11011+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1) //ссгсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //11011 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}
									// 11011+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 0) //ссгсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1101 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 101311+1 холатни аниклаш (фильтрлаш, фильтрнинг), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1 & posStr[pos + 6] == 1) //сгсьсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + sword[pos + 5] + "-"; //101311 ни олиш
										pos = pos + 6;
										unli++;
										goto endundosh;
									}

									// 10131+1 холатни аниклаш (мультфильм, фильмнинг), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1) //сгсьс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //10131 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}

									// 10111+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 0) //сгссс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш (кон-тракт)
										pos = pos + 3;
										unli++;
										goto endundosh;
									}
								}

								if (wordlen - pos > 4) // агар суз узунлиги 4 харфдан катта булса
								{
									// 1101+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //ссгс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1101 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1101+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //ссгс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //110 ни олиш
										pos = pos + 3;
										unli++;
										goto endundosh;
									}
									// 1101+3 холатни аниклаш (статья)
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 3) //ссгс+ь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //11013 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}

									// 1011+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //сгсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1011 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1011+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //сгсс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш
										pos = pos + 3;
										unli++;
										goto endundosh;
									}

									// 1011+3 холатни аниклаш (компьютер), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 3) //сгсс+ь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //1011+3 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}
									// 1013 холатни аниклаш (мульти, бальзам), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3) //сгсь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1013 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}

									// Айриш белгиси катнашган сузларда бугин кучириш
									// 1201 холатни аниклаш (съём-ка)
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 2 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1) //съгс
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1201 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1012 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 2) //сгсъ
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1012 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1021+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //сгъс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1021 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1021+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //сгъс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //102 ни олиш
										pos = pos + 3;
										unli++;
										goto endundosh;
									}
								}

								// 110 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0) //ссг кейинги товушни текшириш шарт эмас
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //110 ни олиш
									pos = pos + 3;
									unli++;
									goto endundosh;
								}

								// 101+1 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1) //сгс+с
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш
									pos = pos + 3;
									unli++;
									goto endundosh;
								}

								// 101+0 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 0) //сгс+с
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //10 ни олиш
									pos = pos + 2;
									unli++;
									goto endundosh;
								}

								// 102+0 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 0) //сгъ
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //102 ни олиш
									pos = pos + 3;
									unli++;
									goto endundosh;
								}

								// 10 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0) //сг кейинги товушни текшириш шарт эмас
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //10 ни олиш
									pos = pos + 2;
									unli++;
									goto endundosh;
								}

							endundosh:;
							}

							// Охирги бугинни аниклаш ва олиш
							if ((unliall - unli) == 1)
							{
								for (int n = pos; n < wordlen; n++) fullword += sword[n];
								//fileout << fullword << endl;
								goto wordfin;
							}
						}  // Бугинларни санаш ва шакллантириш цикли (охири) -------------------------------------------------
					wordfin:;
					}
				endword:
					sword = "";
					fullword = "";
				} // Укилган катордаги сузларни танлаш for цикли охири----------------------

			} // Укилган катор бушлигини текшириш if(str!="") шарти охири---------------
			//str.clear();
		} // Католарни укиш while(getline) фукцияси охири---------------------------
	}
}