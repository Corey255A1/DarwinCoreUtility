using System.Collections.Generic;

namespace DarwinCoreUtility.Utils
{
    public static class ColorIterator
    {
        private static List<Color> baseColors = new List<Color>()
        {
            new Color(255,0,0),
            new Color(255,255,0),
            new Color(0,255,0),
            new Color(0,255,255),
            new Color(0,0,255),
            new Color(255,0,255),
        };

        private static int colorIdx = 0;
        private static int nextColor = 0;
        private static int colorIntensity = 0;
        //public static Color NextPrimaryColor(int step)
        //{
        //    colorIdx = (nextColor) % baseColors.Count;
        //    colorIntensity = nextColor / baseColors.Count;
        //    var c = baseColors[colorIdx].AddValue(-step * colorIntensity);
        //    nextColor += 1;
        //    return c;
        //}

        public static Color NextColor(int step)
        {
            Color ret = baseColors[(colorIdx % baseColors.Count)];
            bool next = false;
            switch (colorIdx % baseColors.Count)
            {
                case 0:
                    ret = ret.AddValue(0, nextColor, 0);
                    next = (ret.Green >= 255);
                    break;
                case 1:
                    ret = ret.AddValue(-nextColor, 0, 0);
                    next = (ret.Red <= 0);
                    break;
                case 2:
                    ret = ret.AddValue(0, 0, nextColor);
                    next = (ret.Blue >= 255);
                    break;
                case 3:
                    ret = ret.AddValue(0, -nextColor, 0);
                    next = (ret.Green <= 0);
                    break;
                case 4:
                    ret = ret.AddValue(0, nextColor, 0);
                    next = (ret.Green >= 255);
                    break;
                case 5:
                    ret = ret.AddValue(nextColor, 0, 0);
                    next = (ret.Red >= 255);
                    break;
            }
            if (next)
            {
                colorIdx++;
                colorIntensity = 0;
                nextColor = (colorIdx % 123);
            }
            else
            {
                colorIntensity++;
                nextColor = (step * colorIntensity) + (colorIdx % 123);
            }
            return ret;

        }

        public static void Reset() { colorIdx = 0; nextColor = 0; colorIntensity = 0; }


    }

    public class Color
    {
        public byte Red { get; set; }
        public byte Blue { get; set; }
        public byte Green { get; set; }
        public byte Alpha { get; set; }

        private byte clamp(int c) => c >= 0 ? (c <= 255 ? (byte)c : (byte)255) : (byte)0;

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            Red = r;
            Green = g;
            Blue = b;
            Alpha = a;
        }
        public Color(int r, int g, int b, int a = 255)
        {
            Red = clamp(r);
            Green = clamp(g);
            Blue = clamp(b);
            Alpha = clamp(a);
        }

        public string HexABGR
        {
            get => Alpha.ToString("X2") + Blue.ToString("X2") + Green.ToString("X2") + Red.ToString("X2");
        }

        public Color AddValue(byte v)
        {
            return new Color(Red + v, Blue + v, Green + v, Alpha);

        }
        public Color AddValue(int v)
        {
            return new Color(Red + v, Green + v, Blue + v, Alpha);

        }
        public Color AddValue(int r, int g, int b)
        {
            return new Color(Red + r, Green + g, Blue + b, Alpha);

        }


    }
}
