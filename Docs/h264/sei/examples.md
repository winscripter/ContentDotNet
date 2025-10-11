# Implementing custom SEI object
This document provides an example for implementing custom SEI payloads in C# using ContentDotNet.

The below class is the SEI model.
```cs
internal class MySeiModel
{
	public uint A { get; }
	public uint B { get; }

	public MySeiModel()
	{
	}
}
```

The below class is the SEI IO.
```cs
internal class MySeiIO : IH264SeiIO<MySeiModel>
{
	public MySeiModel Read(H264RbspState rbspState, BitStreamReader bitStreamReader)
	{
		var msm = new MySeiModel()
		{
			A = bitStreamReader.ReadUE(),
			B = bitStreamReader.ReadUE()
		};
		return msm;
	}

	public Task<MySeiModel> ReadAsync(H264RbspState rbspState, BitStreamReader bitStreamReader)
	{
		// No async support in this example to keep it simple 🥲
		throw new NotImplementedException();
	}

	public void Write(MySeiModel element, BitStreamWriter bitStreamWriter, H264RbspState rbspState)
	{
		bitStreamWriter.Write(element.A);
		bitStreamWriter.Write(element.B);
	}

	public Task WriteAsync(MySeiModel element, BitStreamWriter bitStreamWriter, H264RbspState rbspState)
	{
		// No async support in this example to keep it simple 🥲
		throw new NotImplementedException();
	}
}
```
The below class is used to provide the SEI model.
```cs
internal class MySeiObject : IH264SeiObject<MySeiModel>
{
	private static readonly IH264SeiIO<MySeiModel> s_ioInstance = new MySeiIO();

	public uint Id => 42;
	public string FunctionName => string.Empty; // Does not come from ITU-T
	public string Name => "Custom SEI Object";
	public uint PayloadSize { get; set; } // Will be set automatically
	public IH264SeiIO<MySeiModel> IO => s_ioInstance;

	public MySeiObject()
	{
	}
}
```
