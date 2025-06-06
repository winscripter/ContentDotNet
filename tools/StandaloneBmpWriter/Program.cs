using System;
using System.IO;

int width = 100;  // Image width
int height = 100; // Image height
string filename = "output.bmp";

using (FileStream fs = new FileStream(filename, FileMode.Create))
using (BinaryWriter writer = new BinaryWriter(fs))
{
    // BMP Header (14 bytes)
    writer.Write((byte)'B');
    writer.Write((byte)'M');
    writer.Write((int)(54 + width * height * 3)); // File size
    writer.Write((int)0); // Reserved
    writer.Write((int)54); // Pixel data offset

    // DIB Header (40 bytes)
    writer.Write((int)40); // Header size
    writer.Write((int)width);
    writer.Write((int)height);
    writer.Write((short)1); // Planes
    writer.Write((short)24); // Bits per pixel (RGB)
    writer.Write((int)0); // Compression
    writer.Write((int)(width * height * 3)); // Image size
    writer.Write((int)0); // Horizontal resolution
    writer.Write((int)0); // Vertical resolution
    writer.Write((int)0); // Colors in palette
    writer.Write((int)0); // Important colors

    // Pixel Data (BGR format)
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            writer.Write((byte)(x % 256)); // Blue
            writer.Write((byte)(y % 256)); // Green
            writer.Write((byte)0); // Red
        }
    }
}

Console.WriteLine("BMP file created!");
