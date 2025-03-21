using System;
using System.Windows.Media;

using Colourful;
using CLabColor = Colourful.LabColor;
using RGBws = Colourful.RGBWorkingSpaces;
using LABws = Colourful.Illuminants;
using RGB2LabConverter = Colourful.IColorConverter<Colourful.RGBColor, Colourful.LabColor>;
using Lab2RGBConverter = Colourful.IColorConverter<Colourful.LabColor, Colourful.RGBColor>;

namespace Digimezzo.Foundation.Core.Helpers
{
    /// <summary>
    /// 对于Colourful的LabColor的一个封装，使得外部的调用方式更为统一
    /// </summary>
    public class LABColor
    {
        private static readonly RGB2LabConverter
            rgbToLab = new ConverterBuilder().FromRGB(RGBws.sRGB).ToLab(LABws.D50).Build();
        private static readonly Lab2RGBConverter
            labToRgb = new ConverterBuilder().FromLab(LABws.D50).ToRGB(RGBws.sRGB).Build();

        private readonly CLabColor colourfulObject;

        public double L => this.colourfulObject.L;
        public double A => this.colourfulObject.a;
        public double B => this.colourfulObject.b;

        public LABColor(double L, double a, double b)
        {
            this.colourfulObject = new CLabColor(L, a, b);
        }

        public LABColor(CLabColor cLabColor)
        {
            this.colourfulObject = cLabColor;
        }

        public Color ToRgb()
        {
            var result_rgb = labToRgb.Convert(this.colourfulObject);
            byte r = (byte)(result_rgb.R * 255d);
            byte g = (byte)(result_rgb.G * 255d);
            byte b = (byte)(result_rgb.B * 255d);
            return Color.FromRgb(r, g, b);
        }

        public static LABColor GetFromRgb(Color color)
        {
            return GetFromRgb(color.R, color.G, color.B);
        }

        public static LABColor GetFromRgb(byte R, byte G, byte B)
        {
            var rgbColor = new RGBColor(R / 255.0, G / 255.0, B / 255.0);
            return new LABColor(rgbToLab.Convert(rgbColor));
        }
    }
}
