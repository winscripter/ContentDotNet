namespace ContentDotNet.Extensions.Video.Mp4.Boxes.IO
{
    using ContentDotNet.Binary;
    using ContentDotNet.Extensions.Video.Mp4.Boxes.Data;

    /// <summary>
    ///   IO class for Mp4TrackBoxData
    /// </summary>
    public class Mp4TrackBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TrackBoxIO
        /// </summary>
        public static readonly Mp4TrackBoxIO Instance = new Mp4TrackBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TrackBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4TrackBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4TrackBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4TrackBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4TrackBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4TrackFragmentBoxData
    /// </summary>
    public class Mp4TrackFragmentBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TrackFragmentBoxIO
        /// </summary>
        public static readonly Mp4TrackFragmentBoxIO Instance = new Mp4TrackFragmentBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TrackFragmentBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4TrackFragmentBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4TrackFragmentBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4TrackFragmentBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4TrackFragmentBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4SampleTableBoxData
    /// </summary>
    public class Mp4SampleTableBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4SampleTableBoxIO
        /// </summary>
        public static readonly Mp4SampleTableBoxIO Instance = new Mp4SampleTableBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4SampleTableBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4SampleTableBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4SampleTableBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4SampleTableBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4SampleTableBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4SchemeInformationBoxData
    /// </summary>
    public class Mp4SchemeInformationBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4SchemeInformationBoxIO
        /// </summary>
        public static readonly Mp4SchemeInformationBoxIO Instance = new Mp4SchemeInformationBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4SchemeInformationBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4SchemeInformationBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4SchemeInformationBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4SchemeInformationBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4SchemeInformationBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4MovieExtendsBoxData
    /// </summary>
    public class Mp4MovieExtendsBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4MovieExtendsBoxIO
        /// </summary>
        public static readonly Mp4MovieExtendsBoxIO Instance = new Mp4MovieExtendsBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4MovieExtendsBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4MovieExtendsBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4MovieExtendsBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MovieExtendsBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MovieExtendsBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4MovieBoxData
    /// </summary>
    public class Mp4MovieBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4MovieBoxIO
        /// </summary>
        public static readonly Mp4MovieBoxIO Instance = new Mp4MovieBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4MovieBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4MovieBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4MovieBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MovieBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MovieBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4MovieFragmentBoxData
    /// </summary>
    public class Mp4MovieFragmentBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4MovieFragmentBoxIO
        /// </summary>
        public static readonly Mp4MovieFragmentBoxIO Instance = new Mp4MovieFragmentBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4MovieFragmentBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4MovieFragmentBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4MovieFragmentBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MovieFragmentBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MovieFragmentBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4MediaInformationBoxData
    /// </summary>
    public class Mp4MediaInformationBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4MediaInformationBoxIO
        /// </summary>
        public static readonly Mp4MediaInformationBoxIO Instance = new Mp4MediaInformationBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4MediaInformationBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4MediaInformationBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4MediaInformationBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MediaInformationBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MediaInformationBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4MediaBoxData
    /// </summary>
    public class Mp4MediaBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4MediaBoxIO
        /// </summary>
        public static readonly Mp4MediaBoxIO Instance = new Mp4MediaBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4MediaBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4MediaBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4MediaBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MediaBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4MediaBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

    /// <summary>
    ///   IO class for Mp4DataInformationBoxData
    /// </summary>
    public class Mp4DataInformationBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4DataInformationBoxIO
        /// </summary>
        public static readonly Mp4DataInformationBoxIO Instance = new Mp4DataInformationBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4DataInformationBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(Mp4Box.Read(stream, box.FourCCToIO));
            box.Data = new Mp4DataInformationBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream)
        {
            var children = new List<Mp4Box>();
            long initPos = stream.BaseStream.Position;
            while (initPos < (initPos + (box.Size - 8)))
                children.Add(await Mp4Box.ReadAsync(stream, box.FourCCToIO));
            box.Data = new Mp4DataInformationBoxData(children);
        }
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4DataInformationBoxData)box.Data).Children)
                b.Write(stream);
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            foreach (Mp4Box b in ((Mp4DataInformationBoxData)box.Data).Children)
                await b.WriteAsync(stream);
        }
    }

}

