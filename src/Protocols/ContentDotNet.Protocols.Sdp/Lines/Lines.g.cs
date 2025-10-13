#nullable enable

namespace ContentDotNet.Protocols.Sdp.Lines
{
	using ContentDotNet.Protocols.Sdp.Abstractions;

	
	/// <summary>
	///   Represents an SDP line that represents Version with the character 'v'.
	/// </summary>
	public class SdpVersionLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpVersionLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpVersionLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'v'.
		/// </summary>
		public char Character => 'v';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Version with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateVersion(int value)
		{
			return TrySetSlot(0, value.ToString());
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetVersion(out int result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = 0;
				return false;
			}

			
			return int.TryParse(this._whitespaceSeparatedValue[0], out result);

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetVersion(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents Origin with the character 'o'.
	/// </summary>
	public class SdpOriginLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpOriginLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpOriginLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'o'.
		/// </summary>
		public char Character => 'o';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Username with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateUsername(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetUsername(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the SessionId with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateSessionId(int value)
		{
			return TrySetSlot(1, value.ToString());
		}

		/// <summary>
		///   Attempts to parse and return the item at index 1 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetSessionId(out int result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 2)
			{
				result = 0;
				return false;
			}

			
			return int.TryParse(this._whitespaceSeparatedValue[1], out result);

			
		}

		
		/// <summary>
		///   Attempts to mutate the SessionVersion with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateSessionVersion(int value)
		{
			return TrySetSlot(2, value.ToString());
		}

		/// <summary>
		///   Attempts to parse and return the item at index 2 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetSessionVersion(out int result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 3)
			{
				result = 0;
				return false;
			}

			
			return int.TryParse(this._whitespaceSeparatedValue[2], out result);

			
		}

		
		/// <summary>
		///   Attempts to mutate the NetworkType with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateNetworkType(string value)
		{
			return TrySetSlot(3, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 3 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetNetworkType(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 4)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[3];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the AddressType with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateAddressType(string value)
		{
			return TrySetSlot(4, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 4 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetAddressType(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 5)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[4];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the UnicastAddress with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateUnicastAddress(string value)
		{
			return TrySetSlot(5, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 5 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetUnicastAddress(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 6)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[5];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetUsername(out _) && this.TryGetSessionId(out _) && this.TryGetSessionVersion(out _) && this.TryGetNetworkType(out _) && this.TryGetAddressType(out _) && this.TryGetUnicastAddress(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents SessionName with the character 's'.
	/// </summary>
	public class SdpSessionNameLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpSessionNameLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpSessionNameLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 's'.
		/// </summary>
		public char Character => 's';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Name with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateName(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetName(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetName(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents SessionInformation with the character 'i'.
	/// </summary>
	public class SdpSessionInformationLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpSessionInformationLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpSessionInformationLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'i'.
		/// </summary>
		public char Character => 'i';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Information with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateInformation(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetInformation(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetInformation(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents Uri with the character 'u'.
	/// </summary>
	public class SdpUriLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpUriLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpUriLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'u'.
		/// </summary>
		public char Character => 'u';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Uri with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateUri(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetUri(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetUri(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents EmailAddress with the character 'e'.
	/// </summary>
	public class SdpEmailAddressLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpEmailAddressLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpEmailAddressLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'e'.
		/// </summary>
		public char Character => 'e';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Address with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateAddress(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetAddress(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetAddress(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents PhoneNumber with the character 'p'.
	/// </summary>
	public class SdpPhoneNumberLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpPhoneNumberLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpPhoneNumberLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'p'.
		/// </summary>
		public char Character => 'p';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Number with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateNumber(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetNumber(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetNumber(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents ConnectionInformation with the character 'c'.
	/// </summary>
	public class SdpConnectionInformationLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpConnectionInformationLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpConnectionInformationLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'c'.
		/// </summary>
		public char Character => 'c';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the NetworkType with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateNetworkType(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetNetworkType(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the AddressType with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateAddressType(string value)
		{
			return TrySetSlot(1, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 1 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetAddressType(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 2)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[1];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the ConnectionAddress with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateConnectionAddress(string value)
		{
			return TrySetSlot(2, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 2 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetConnectionAddress(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 3)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[2];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetNetworkType(out _) && this.TryGetAddressType(out _) && this.TryGetConnectionAddress(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents BandwidthInformation with the character 'b'.
	/// </summary>
	public class SdpBandwidthInformationLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpBandwidthInformationLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpBandwidthInformationLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'b'.
		/// </summary>
		public char Character => 'b';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the ColonSeparatedInformation with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateColonSeparatedInformation(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetColonSeparatedInformation(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetColonSeparatedInformation(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents TimeActive with the character 't'.
	/// </summary>
	public class SdpTimeActiveLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpTimeActiveLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpTimeActiveLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 't'.
		/// </summary>
		public char Character => 't';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the StartTime with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateStartTime(int value)
		{
			return TrySetSlot(0, value.ToString());
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetStartTime(out int result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = 0;
				return false;
			}

			
			return int.TryParse(this._whitespaceSeparatedValue[0], out result);

			
		}

		
		/// <summary>
		///   Attempts to mutate the StopTime with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateStopTime(int value)
		{
			return TrySetSlot(1, value.ToString());
		}

		/// <summary>
		///   Attempts to parse and return the item at index 1 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetStopTime(out int result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 2)
			{
				result = 0;
				return false;
			}

			
			return int.TryParse(this._whitespaceSeparatedValue[1], out result);

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetStartTime(out _) && this.TryGetStopTime(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents RepeatTimes with the character 'r'.
	/// </summary>
	public class SdpRepeatTimesLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpRepeatTimesLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpRepeatTimesLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'r'.
		/// </summary>
		public char Character => 'r';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Days with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateDays(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetDays(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the Hours with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateHours(string value)
		{
			return TrySetSlot(1, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 1 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetHours(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 2)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[1];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the Minutes with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateMinutes(string value)
		{
			return TrySetSlot(2, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 2 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetMinutes(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 3)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[2];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the Seconds with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateSeconds(string value)
		{
			return TrySetSlot(3, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 3 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetSeconds(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 4)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[3];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetDays(out _) && this.TryGetHours(out _) && this.TryGetMinutes(out _) && this.TryGetSeconds(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents TimeZoneAdjustment with the character 'z'.
	/// </summary>
	public class SdpTimeZoneAdjustmentLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpTimeZoneAdjustmentLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpTimeZoneAdjustmentLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'z'.
		/// </summary>
		public char Character => 'z';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the StartAdjustmentTime with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateStartAdjustmentTime(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetStartAdjustmentTime(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the StartOffset with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateStartOffset(string value)
		{
			return TrySetSlot(1, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 1 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetStartOffset(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 2)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[1];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the EndAdjustmentTime with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateEndAdjustmentTime(string value)
		{
			return TrySetSlot(2, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 2 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetEndAdjustmentTime(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 3)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[2];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the EndOffset with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateEndOffset(string value)
		{
			return TrySetSlot(3, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 3 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetEndOffset(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 4)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[3];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetStartAdjustmentTime(out _) && this.TryGetStartOffset(out _) && this.TryGetEndAdjustmentTime(out _) && this.TryGetEndOffset(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents EncryptionKeys with the character 'k'.
	/// </summary>
	public class SdpEncryptionKeysLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpEncryptionKeysLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpEncryptionKeysLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'k'.
		/// </summary>
		public char Character => 'k';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Keys with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateKeys(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetKeys(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetKeys(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents Attributes with the character 'a'.
	/// </summary>
	public class SdpAttributesLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpAttributesLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpAttributesLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'a'.
		/// </summary>
		public char Character => 'a';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Value with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateValue(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetValue(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetValue(out _);
		}
	}

	
	/// <summary>
	///   Represents an SDP line that represents MediaDescriptors with the character 'm'.
	/// </summary>
	public class SdpMediaDescriptorsLine : ISdpLineModel
	{
		private string? _rawText;

		private string? _value;
		private string[]? _whitespaceSeparatedValue;

		/// <summary>
		///   Initializes a new instance of the <see cref="SdpMediaDescriptorsLine" /> class with the specified
		///   raw SDP line string provided as the SDP line to parse.
		/// </summary>
		/// <param name="rawText">The SDP line, including the character and equals sign prefix.</param>
		public SdpMediaDescriptorsLine(string? rawText)
		{
			this._rawText = rawText;
			this.Initialize();
		}

		/// <summary>
		///   The character used to identify an SDP line as this one. Always returns
		///   the character 'm'.
		/// </summary>
		public char Character => 'm';

		/// <summary>
		///   Represents the actual raw SDP line string backing this class.
		/// </summary>
		public string? RawText
		{
			get
			{
				return this._rawText;
			}

			set
			{
				string? previousValue = this._rawText;
				this._rawText = value;
				try
				{
					this.Initialize();
				}
				catch
				{
					this._rawText = previousValue;
					throw;
				}
			}
		}

		/// <summary>
		///   Applies parsing routines.
		/// </summary>
		private void Initialize()
		{
			if (this._rawText != null)
			{
				int offsetOfEqualsChar = this._rawText.IndexOf('=');
				if (offsetOfEqualsChar == -1)
					throw new SdpException("The provided SDP line has no equals character");

				if (!this._rawText.StartsWith(this.Character.ToString()))
					throw new SdpException($"Provided SDP line is not compatible with this SDP line model: It must start with {this.Character}, but it's {this._rawText[0]}");

				this._value = this._rawText.Substring(offsetOfEqualsChar);
				this._whitespaceSeparatedValue = this._value.Split(' ');
			}
		}

		/// <summary>
		///   Changes the slot <paramref name="i" /> to be <paramref name="value" />.
		/// </summary>
		/// <param name="i">The index of the slot.</param>
		/// <param name="value">The value to alter the slot with.</param>
		/// <returns>Success status</returns>
		private bool TrySetSlot(int i, string value)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < (i + 1))
				return false;

			this._whitespaceSeparatedValue[i] = value;
			return true;
		}

		
		/// <summary>
		///   Attempts to mutate the Media with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateMedia(string value)
		{
			return TrySetSlot(0, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 0 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetMedia(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 1)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[0];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the Port with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutatePort(string value)
		{
			return TrySetSlot(1, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 1 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetPort(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 2)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[1];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the Proto with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateProto(string value)
		{
			return TrySetSlot(2, value);
		}

		/// <summary>
		///   Attempts to parse and return the item at index 2 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetProto(out string? result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 3)
			{
				result = null;
				return false;
			}

			
			result = this._whitespaceSeparatedValue[2];
			return true;

			
		}

		
		/// <summary>
		///   Attempts to mutate the Format with the value <paramref name="value" />.
		/// </summary>
		/// <param name="value">What to muate with</param>
		/// <returns>
		///   <see langword="true" /> if mutation was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryMutateFormat(int value)
		{
			return TrySetSlot(3, value.ToString());
		}

		/// <summary>
		///   Attempts to parse and return the item at index 3 of whitespace-separated
		///   parts in the line.
		/// </summary>
		/// <param name="result">Output is placed here.</param>
		/// <returns>
		///   <see langword="true" /> if parsing was successful, <see langword="false" /> otherwise.
		/// </returns>
		public bool TryGetFormat(out int result)
		{
			if (this._whitespaceSeparatedValue == null ||
				this._whitespaceSeparatedValue.Length < 4)
			{
				result = 0;
				return false;
			}

			
			return int.TryParse(this._whitespaceSeparatedValue[3], out result);

			
		}

		
		/// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
		public bool IsValid()
		{
		
			return this.TryGetMedia(out _) && this.TryGetPort(out _) && this.TryGetProto(out _) && this.TryGetFormat(out _);
		}
	}

	}

