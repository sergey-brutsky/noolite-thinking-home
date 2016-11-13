namespace ThinkingHome.NooLite.ReceivedData
{
	public class RX2164ReceivedCommandData : ReceivedCommandData
	{
		public RX2164ReceivedCommandData(byte[] buf)
			: base(buf)
		{
		}

		public int ToggleValue
		{
			get { return buf[1] & 0x3f; }
		}
	}
}
