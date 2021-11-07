using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SteganographyExample
{
    class Program
    {
        private static string workingFolder;

        static void Main(string[] args)
        {
            workingFolder = Path.Combine(
               Environment.GetFolderPath(
                   Environment.SpecialFolder.LocalApplicationData), "SteganographyExample");

            Directory.CreateDirectory(workingFolder);

            /*
             * Embeds a wav file in a copy of an image
             */
            CreateStegoFile();

            /*
             * Reads a wav file out of an encoded image.
             */
            ExtractWAVfile();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Operation complete!");
            Console.ResetColor();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void ExtractWAVfile()
        {
            List<byte> wavBytes = new List<byte>();

            Bitmap final = (Bitmap)Bitmap.FromFile(Path.Combine(workingFolder, "final.png"));

            //This is cheating a little because normally you wouldn't know exactly how many bytes
            //the .wav file is. Some trial and error would be necessary to determine that. You would
            //start looking around where all pixels go back to full opacity (255).
            byte[] wav = new byte[683672];

            int h = final.Height;
            int w = final.Width;

            for (int i = 1; i < wav.Length + 1; i++)
            {
                Point next = GetPixelCoordinate(i, w);

                var current = final.GetPixel(next.X, next.Y);

                wavBytes.Add(current.A);
            }

            string outputFilePath = Path.Combine(workingFolder, "result.wav");

            File.WriteAllBytes(outputFilePath, wavBytes.ToArray());

            final.Dispose();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The extracted .wav file has been written to: {outputFilePath}");
            Console.ResetColor();
        }

        static void CreateStegoFile()
        {
            //Get all the bytes of the wav file you want to embed in the image
            byte[] original_wav_file = File.ReadAllBytes(Path.Combine("resources", "Original.wav"));

            //Get the original image. It will be read pixel-by-pixel to generate a new image with the .wav data embedded.
            Bitmap orig = (Bitmap)Bitmap.FromFile(Path.Combine("resources", "before.png"));

            //Initialize the output image with same size as original.
            Bitmap resultFile = new Bitmap(orig.Width, orig.Height, PixelFormat.Format32bppArgb);

            int h = orig.Height;
            int w = orig.Width;

            int currentPxl = 0;

            //set alpha channel pixel to value of wav file, pixel-by-pixel
            for (int i = 1; i < original_wav_file.Length - 1; i++)
            {
                Point next = GetPixelCoordinate(i, w);

                var px = orig.GetPixel(next.X, next.Y);

                //Set Alpha component to the current .wav file byte. Set RGB components to same as original image.
                //In a real-world scenario, you would distribute one or two bytes among alternating color components to make it undetectable.
                Color clr = Color.FromArgb(original_wav_file[i - 1], px.R, px.G, px.B);

                resultFile.SetPixel(next.X, next.Y, clr);

                currentPxl = i - 1;
            }

            //complete the rest of the image
            for (int i = currentPxl; i < (orig.Width * orig.Height); i++)
            {
                Point next = GetPixelCoordinate(i, w);

                var px = orig.GetPixel(next.X, next.Y);

                Color clr = Color.FromArgb(255, px.R, px.G, px.B);

                resultFile.SetPixel(next.X, next.Y, clr);
            }

            string outputFilePath = Path.Combine(workingFolder, "final.png");

            resultFile.Save(outputFilePath, ImageFormat.Png);

            orig.Dispose();
            resultFile.Dispose();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The new image has been written to: {outputFilePath}");
            Console.ResetColor();
        }

        /// <summary>
        /// A little bit of math gymnastics required to get pixel coordinates since
        /// pixels are in columns and rows.
        /// </summary>
        /// <param name="pixelNumber">Which pixel you want to translate into image coordinates</param>
        /// <param name="imageWidth">The width in pixels of the original image</param>
        /// <returns>Point</returns>
        private static Point GetPixelCoordinate(int pixelNumber, int imageWidth)
        {
            int y = pixelNumber % imageWidth == 0 ? (pixelNumber / imageWidth) : (pixelNumber / imageWidth) + 1;
            int x = pixelNumber % imageWidth == 0 ? imageWidth : pixelNumber - ((y - 1) * imageWidth);

            return new Point(x - 1, y - 1);
        }
    }
}
