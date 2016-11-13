namespace ThinkingHome.NooLite.PR1132
{
	public class PR1132ControlGroup
	{
		public PR1132ControlGroup()
		{
			Sensors = new bool[4];
			ChannelNumbers = new int?[8];
		}

		public string Name { get; set; }

		public bool Enabled { get; set; }

		public int?[] ChannelNumbers { get; private set; }

		public bool[] Sensors { get; private set; }
	}
}