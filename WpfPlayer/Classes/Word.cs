namespace WpfPlayer.Classes
{
	class SWord
	{
		public string Syllable { get; set; }
		public string TWavPath { get; set; }
	}
	class TWord
	{
		public SWord[] Syllables { get; set; }
		public string Word { get; set; }
		public string Wav { get; set; }
	}
}
