using BIGFOOT.MatrixViz.DriverInterfacing;
using System;

namespace BIGFOOT.MatrixViz.Visuals.Constants
{
    public static class MatrixGridTileColors
    {
        // A color palette's configurable properties
        // You can adjust these to change possible colors within a palette

        private const sbyte contrast = 124;             // [0 to 127]
        private const double contrast_ratio = 0.5;     // [0.0 to 1.0]

        private const byte r_hue = 50;                  // [0 to 255]
        private const byte g_hue = 0;                 // [0 to 255]
        private const byte b_hue = 0;                  // [0 to 255]


        // Static color palette definitions
        // Using the config values above, we define rules making up our color palette to create colors from

        private static byte palette_black = (byte) (Math.Floor(contrast * contrast_ratio));
        private static byte palette_white = (byte) (byte.MaxValue - Math.Floor(contrast * contrast_ratio));

        private static byte r_hueShift = (byte) Math.Floor(r_hue * contrast_ratio);
        private static byte g_hueShift = (byte) Math.Floor(g_hue * contrast_ratio);
        private static byte b_hueShift = (byte) Math.Floor(b_hue * contrast_ratio);


        // Set of colors created using our palette (aka, a color theme)
        // 
        // They all come from the same palette and thus relate in a consistent, meaningful way
        // Visual representations made from colors within a theme can be exploited to encode addition info and communicate it effictively 

        public static Color BLOCK = new Color(palette_black + r_hueShift, palette_black + g_hueShift, palette_black + b_hueShift);
        public static Color EMPTY = new Color(palette_white + r_hueShift, palette_white + g_hueShift, palette_white + b_hueShift);
        
        public static Color VISITED = Color.ShiftLuminance(BLOCK, (palette_white - palette_black) / 2);

        public static Color TARGET = new Color(VISITED.R, palette_white, VISITED.B); 
        public static Color START = new Color(palette_white, VISITED.G, VISITED.B); 
    
        public static Color SELECTOR = new Color(palette_white, palette_white, palette_white);
    }
}
