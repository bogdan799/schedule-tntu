using System;
using Xamarin.Forms;

namespace Schedule.App.Core.Tools
{
    public sealed class RandomColorGenerator
    {
        #region Declarations

        private readonly int _seed;

        #endregion

        #region Constructors

        public RandomColorGenerator(int seed)
        {
            _seed = seed;
        }

        #endregion

        #region Public Methods

        public Color Generate(double opacity)
        {
            var colorHue = GenerateColorHue(_seed);

            return ApplyColorStyle(colorHue, opacity);
        }

        #endregion

        #region Private Methods

        private Color ApplyColorStyle(int hue, double opacity)
        {
            var h = hue / 360.0;
            return Color.FromHsla(h, 0.7, 0.7, opacity);
        }

        private int GenerateColorHue(int seed)
        {
            // Multiplying to a random prime number to avoid similar ID color collisions
            seed *= 23522316;

            var rnd = new Random(seed);

            return rnd.Next(0, 25) * 15;
        }

        #endregion
    }
}
