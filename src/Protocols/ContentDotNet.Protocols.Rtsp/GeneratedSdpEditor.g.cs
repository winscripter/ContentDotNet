namespace ContentDotNet.Protocols.Rtsp
{
	public partial class RtspMessageSdpEditor
	{
		/// <summary>
		///   This method checks if there's a line starting with <c>v=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsVLine() => ContainsLine('v');

		/// <summary>
		///   If a line starting with v= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>v=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor VLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with v= exists, it's deleted and its contents
		///   are replaced with v=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after v= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeVLine(x) = VLine("v=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeVLine(string value)
		{
			return Line('v', "v=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>o=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsOLine() => ContainsLine('o');

		/// <summary>
		///   If a line starting with o= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>o=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor OLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with o= exists, it's deleted and its contents
		///   are replaced with o=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after o= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeOLine(x) = OLine("o=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeOLine(string value)
		{
			return Line('o', "o=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>s=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsSLine() => ContainsLine('s');

		/// <summary>
		///   If a line starting with s= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>s=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor SLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with s= exists, it's deleted and its contents
		///   are replaced with s=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after s= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeSLine(x) = SLine("s=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeSLine(string value)
		{
			return Line('s', "s=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>i=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsILine() => ContainsLine('i');

		/// <summary>
		///   If a line starting with i= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>i=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor ILine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with i= exists, it's deleted and its contents
		///   are replaced with i=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after i= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeILine(x) = ILine("i=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeILine(string value)
		{
			return Line('i', "i=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>u=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsULine() => ContainsLine('u');

		/// <summary>
		///   If a line starting with u= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>u=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor ULine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with u= exists, it's deleted and its contents
		///   are replaced with u=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after u= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeULine(x) = ULine("u=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeULine(string value)
		{
			return Line('u', "u=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>e=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsELine() => ContainsLine('e');

		/// <summary>
		///   If a line starting with e= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>e=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor ELine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with e= exists, it's deleted and its contents
		///   are replaced with e=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after e= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeELine(x) = ELine("e=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeELine(string value)
		{
			return Line('e', "e=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>p=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsPLine() => ContainsLine('p');

		/// <summary>
		///   If a line starting with p= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>p=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor PLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with p= exists, it's deleted and its contents
		///   are replaced with p=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after p= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangePLine(x) = PLine("p=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangePLine(string value)
		{
			return Line('p', "p=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>c=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsCLine() => ContainsLine('c');

		/// <summary>
		///   If a line starting with c= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>c=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor CLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with c= exists, it's deleted and its contents
		///   are replaced with c=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after c= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeCLine(x) = CLine("c=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeCLine(string value)
		{
			return Line('c', "c=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>b=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsBLine() => ContainsLine('b');

		/// <summary>
		///   If a line starting with b= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>b=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor BLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with b= exists, it's deleted and its contents
		///   are replaced with b=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after b= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeBLine(x) = BLine("b=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeBLine(string value)
		{
			return Line('b', "b=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>t=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsTLine() => ContainsLine('t');

		/// <summary>
		///   If a line starting with t= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>t=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor TLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with t= exists, it's deleted and its contents
		///   are replaced with t=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after t= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeTLine(x) = TLine("t=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeTLine(string value)
		{
			return Line('t', "t=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>r=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsRLine() => ContainsLine('r');

		/// <summary>
		///   If a line starting with r= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>r=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor RLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with r= exists, it's deleted and its contents
		///   are replaced with r=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after r= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeRLine(x) = RLine("r=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeRLine(string value)
		{
			return Line('r', "r=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>z=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsZLine() => ContainsLine('z');

		/// <summary>
		///   If a line starting with z= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>z=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor ZLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with z= exists, it's deleted and its contents
		///   are replaced with z=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after z= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeZLine(x) = ZLine("z=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeZLine(string value)
		{
			return Line('z', "z=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>k=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsKLine() => ContainsLine('k');

		/// <summary>
		///   If a line starting with k= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>k=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor KLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with k= exists, it's deleted and its contents
		///   are replaced with k=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after k= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeKLine(x) = KLine("k=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeKLine(string value)
		{
			return Line('k', "k=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>a=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsALine() => ContainsLine('a');

		/// <summary>
		///   If a line starting with a= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>a=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor ALine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with a= exists, it's deleted and its contents
		///   are replaced with a=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after a= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeALine(x) = ALine("a=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeALine(string value)
		{
			return Line('a', "a=" + value);
		}
		/// <summary>
		///   This method checks if there's a line starting with <c>m=</c>.
		/// </summary>
		/// <returns><c>true</c> if such line exists; otherwise, <c>false</c>.</returns>
		public bool ContainsMLine() => ContainsLine('m');

		/// <summary>
		///   If a line starting with m= exists, it's deleted and its contents
		///   are replaced with <paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="wholeLineString">The entire line, including the leading <c>m=</c>.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		public RtspMessageSdpEditor MLine(string wholeLineString)
		{
			Line('v', wholeLineString);
			return this;
		}

		/// <summary>
		///   If a line starting with m= exists, it's deleted and its contents
		///   are replaced with m=<paramref name="wholeLineString"/>.
		/// </summary>
		/// <param name="value">Whatever comes after m= in the SDP line.</param>
		/// <returns>
		///   This instance, after modifying.
		/// </returns>
		/// <remarks>
		///   This method is identical as follows:
		///   <code>
		///     ChangeMLine(x) = MLine("m=" + x)
		///   </code>
		/// </remarks>
		public RtspMessageSdpEditor ChangeMLine(string value)
		{
			return Line('m', "m=" + value);
		}
	}
}
