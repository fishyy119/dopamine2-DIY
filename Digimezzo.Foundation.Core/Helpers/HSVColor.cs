using System;
using System.Windows.Media;

namespace Digimezzo.Foundation.Core.Helpers
{
    public class HSVColor
    {
        private double hue;
        private double saturation;
        private double mValue;

        public int Hue
        {
            get
            {
                var t = (int)(this.hue * 60d);

                if (t > 0)
                {
                    while (t > 360)
                    {
                        t -= 360;
                    }
                }

                else
                {
                    while (t < 0)
                    {
                        t += 360;
                    }
                }

                return t;
            }
            set
            {
                var t = value;

                if (t > 0)
                {
                    while (t > 360)
                    {
                        t -= 360;
                    }
                }

                else
                {
                    while (t < 0)
                    {
                        t += 360;
                    }
                }

                this.hue = t / 60d;
            }
        }

        public int Saturation
        {
            get { return (int)(this.saturation * 100d); }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Saturation", "Saturation only can be set between 0 and 100");
                }
                else
                {
                    this.saturation = value / 100d;
                }
            }
        }

        public int Value
        {
            get { return (int)(this.mValue * 100d); }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Value", "Value only can be set between 0 and 100");
                }
                else
                {
                    this.mValue = value / 100d;
                }
            }
        }

        public HSVColor(int H, int S, int V)
        {
            this.Hue = H;
            this.Saturation = S;
            this.Value = V;
        }

        private HSVColor(double h, double s, double v)
        {
            this.hue = h;
            this.saturation = s;
            this.mValue = v;
        }

        public Color ToRgb()
        {
            byte r, g, b;
            double tr, tg, tb;

            int h = this.Hue / 60;
            double f = this.Hue / 60d - h;
            double v = this.mValue;
            double s = this.saturation;
            double p = v * (1 - s);
            double q = v * (1 - f * s);
            double t = v * (1 - (1 - f) * s);

            switch (h)
            {
                case 0: tr = v; tg = t; tb = p; break;
                case 1: tr = q; tg = v; tb = p; break;
                case 2: tr = p; tg = v; tb = t; break;
                case 3: tr = p; tg = q; tb = v; break;
                case 4: tr = t; tg = p; tb = v; break;
                case 5: tr = v; tg = p; tb = q; break;
                default: tr = tg = tb = 0; break;
            }

            r = (byte)Math.Round(tr * 255d);
            g = (byte)Math.Round(tg * 255d);
            b = (byte)Math.Round(tb * 255d);

            return Color.FromRgb(r, g, b);
        }

        public HSVColor MoveNext(int step)
        {
            Hue += step;
            return this;
        }

        public static HSVColor GetFromRgb(Color color)
        {
            return GetFromRgb(color.R, color.G, color.B);
        }

        public static HSVColor GetFromRgb(byte R, byte G, byte B)
        {
            double r = R / 255d, g = G / 255d, b = B / 255d;
            double max = Math.Max(Math.Max(r, g), b), min = Math.Min(Math.Min(r, g), b);
            var delta = max - min;

            double h = 0, s = 0, v = max;

            if (delta != 0)
            {
                if (r == max)
                {
                    h = (g - b) / delta;
                }
                else if (g == max)
                {
                    h = 2d + (b - r) / delta;
                }
                else if (b == max)
                {
                    h = 4d + (r - g) / delta;
                }
            }

            if (max != 0)
            {
                s = 1 - min / max;
            }

            return new HSVColor(h, s, v);
        }

        public static Color Normalize(Color color, int value)
        {
            if (value < 0 | value > 100)
            {
                return color;
            }

            HSVColor HSVColor = HSVColor.GetFromRgb(color);

            if (HSVColor.mValue < value)
            {
                HSVColor.mValue = value;
            }
            else if (HSVColor.mValue > 100 - value)
            {
                HSVColor.mValue = 100 - value;
            }

            return HSVColor.ToRgb();
        }
    }
}
