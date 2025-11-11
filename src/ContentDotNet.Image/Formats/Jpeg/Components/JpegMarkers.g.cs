namespace ContentDotNet.Image.Formats.Jpeg.Components
{
    /// <summary>
    ///   The JPEG markers.
    /// </summary>
    public enum JpegMarkers : ushort
    {
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC0.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Baseline DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof0 = 0xFFC0,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC1.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Extended sequential DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof1 = 0xFFC1,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC2.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Progressive DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof2 = 0xFFC2,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC3.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Lossless (sequential)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof3 = 0xFFC3,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC5.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Differential sequential DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof5 = 0xFFC5,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC6.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Differential progressive DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof6 = 0xFFC6,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC7.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Differential lossless (sequential)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof7 = 0xFFC7,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC8.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg = 0xFFC8,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC9.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Extended sequential DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof9 = 0xFFC9,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFCA.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Progressive DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof10 = 0xFFCA,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFCB.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Lossless (sequential)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof11 = 0xFFCB,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFCD.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Differential sequential DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof13 = 0xFFCD,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFCE.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Differential progressive DCT</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof14 = 0xFFCE,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFCF.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Differential lossless</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sof15 = 0xFFCF,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFC4.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Define Huffman Table(s)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Dht = 0xFFC4,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFCC.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Define arithmetic coding conditioning(s)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Dac = 0xFFCC,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD0.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '0'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst0 = 0xFFD0,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD1.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '1'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst1 = 0xFFD1,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD2.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '2'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst2 = 0xFFD2,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD3.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '3'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst3 = 0xFFD3,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD4.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '4'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst4 = 0xFFD4,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD5.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '5'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst5 = 0xFFD5,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD6.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '6'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst6 = 0xFFD6,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD7.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Restart with modulo 8 count '7'</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Rst7 = 0xFFD7,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD8.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Start of image</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Soi = 0xFFD8,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFD9.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>End of image</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Eoi = 0xFFD9,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFDA.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Start of scan</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Sos = 0xFFDA,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFDB.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Define quantization table(s)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Dqt = 0xFFDB,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFDC.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Define number of line(s)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Dnl = 0xFFDC,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFDD.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Define restart interval</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Dri = 0xFFDD,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFDE.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Define hierarchical progression</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Dhp = 0xFFDE,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFDF.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Expand reference component(s)</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Exp = 0xFFDF,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE0.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App0 = 0xFFE0,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE1.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App1 = 0xFFE1,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE2.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App2 = 0xFFE2,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE3.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App3 = 0xFFE3,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE4.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App4 = 0xFFE4,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE5.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App5 = 0xFFE5,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE6.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App6 = 0xFFE6,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE7.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App7 = 0xFFE7,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE8.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App8 = 0xFFE8,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFE9.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App9 = 0xFFE9,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFEA.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App10 = 0xFFEA,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFEB.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App11 = 0xFFEB,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFEC.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App12 = 0xFFEC,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFED.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App13 = 0xFFED,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFEE.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App14 = 0xFFEE,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFEF.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for application segments</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        App15 = 0xFFEF,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF0.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg0 = 0xFFF0,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF1.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg1 = 0xFFF1,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF2.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg2 = 0xFFF2,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF3.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg3 = 0xFFF3,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF4.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg4 = 0xFFF4,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF5.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg5 = 0xFFF5,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF6.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg6 = 0xFFF6,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF7.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg7 = 0xFFF7,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF8.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg8 = 0xFFF8,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFF9.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg9 = 0xFFF9,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFFA.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg10 = 0xFFFA,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFFB.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg11 = 0xFFFB,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFFC.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg12 = 0xFFFC,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFFD.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg13 = 0xFFFD,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFFE.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Reserved for JPEG extensions</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Jpg14 = 0xFFFE,
        /// <summary>
        ///   A JPEG marker with the value being 0xFFFE.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>Comment</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Com = 0xFFFE,
        /// <summary>
        ///   A JPEG marker with the value being 0xFF01.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The description of this JPEG marker is as follows (as also specified in the ITU-T T.81 specification):
        ///   </para>
        ///   <para>
        ///     <em>For temporary private use in arithmetic coding</em> - from ITU-T T.81 specification.
        ///   </para>
        /// </remarks>
        Tem = 0xFF01,
    }
}

