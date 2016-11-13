using System.Collections;

namespace ThinkingHome.NooLite.PR1132
{
	public class PR1132Timer
	{
		public PR1132Timer()
		{
			Days = new bool[7];
		}

		public string Name { get; set; }
		public bool Enabled { get; set; }
		public bool RunOnce { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public int Channel { get; set; }
		public bool[] Days { get; set; }
		public PR1132TimerCommad Command { get; set; }
	}
}