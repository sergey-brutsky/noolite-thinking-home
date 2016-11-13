namespace ThinkingHome.NooLite.ReceivedData
{
	public class MicroclimateReceivedCommandData : RX2164ReceivedCommandData
	{
		public MicroclimateReceivedCommandData(byte[] buf)
			: base(buf)
		{
		}

		public decimal Temperature
		{
			get
			{
				int value = ((Data[1] & 0x0F) << 8) + Data[0];

				if (value >= 0x800)
				{
					value = value - 0x1000;
				}

				return ((decimal)value) / 10;
			}
		}

		public int Humidity
		{
			get { return Data[2]; }
		}
	}
}
