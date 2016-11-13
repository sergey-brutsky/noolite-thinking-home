using System.Linq;

namespace ThinkingHome.NooLite.ReceivedData
{
	public abstract class ReceivedCommandData
	{
		public readonly byte[] buf;

		protected ReceivedCommandData(byte[] buf)
		{
			this.buf = (byte[])buf.Clone();
		}

		public bool Binding
		{
			get
			{
				return (buf[1] & 0x40) > 0;	// 6й бит 1-го байта
			}
		}

		public byte Cmd
		{
			get { return buf[3]; }
		}

		public byte Channel
		{
			get { return buf[2]; }
		}

		internal CommandFormat DataFormat
		{
			get { return (CommandFormat)buf[4]; }
		}

		public byte[] Data
		{
			get
			{
				switch (DataFormat)
				{
					case CommandFormat.OneByteData:
						return new[] { buf[5] };
					case CommandFormat.FourByteData:
						return new[] { buf[5], buf[6], buf[7], buf[8] };
					case CommandFormat.Undefined:
					case CommandFormat.LED:
						return new byte[0];
					default:
						return new byte[0];
				}
			}
		}

		public override bool Equals(object obj)
		{
			var other = obj as ReceivedCommandData;

			if (other == null || buf == null || other.buf == null || buf.Length != other.buf.Length)
			{
				return false;
			}

			return !buf.Where((t, i) => t != other.buf[i]).Any();
		}

		public override int GetHashCode()
		{
			return (buf != null ? buf.Sum(x => x) : 0);
		}

		public override string ToString()
		{
			return string.Join("", buf.Select(b => b.ToString("x2")));
		}
	}
}
