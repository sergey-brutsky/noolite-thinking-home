namespace ThinkingHome.NooLite.ReceivedData
{
	public class RX1164ReceivedCommandData : ReceivedCommandData
	{
		public RX1164ReceivedCommandData(byte[] buf)
			: base(buf)
		{
		}

		public bool ToggleFlag
		{
			get
			{
				return (buf[1] & 0x80) > 0;	// 7й бит 1-го байта
			}
		}
	}
}
