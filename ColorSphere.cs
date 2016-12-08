using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FirNewYear
{
    public static class ColorSphere
    {
        //public static Color[,] Colors = 
        //{ 
        //    {new Color(), }
        //};

        public static string[,] Colors =
        {
            {"#FF0000", "#FFFF4747", "#FFFFEFEF"},
            {"#FFFFE800", "#FFF7FF4E", "#FFFEFFEF"},
            {"#FF23FF00", "#FF50FF58", "#FFEFFFF0"},
            {"#FF00FFE8", "#FF50FFFF", "#FFEFFEFF"},
            {"#FF0080FF", "#FF5088FF", "#FFEFF3FF"},
            {"#FF8000FF", "#FFA050FF", "#FFF7EFFF"},
            {"#FFFF00F3", "#FFFF50EF", "#FFFFEFFE"},
            {"#FFFF8000", "#FFFFAF50", "#FFFFF8EF"}
        };

        public static Color[,] ObjColors = new Color[8, 3];

        public static void CreateColors()
        {
            for (int i = 0; i < ObjColors.GetLength(0); i++)
            {
                for (int j = 0; j < ObjColors.GetLength(1); j++)
                {
                    ObjColors[i, j] = (Color)ColorConverter.ConvertFromString(Colors[i, j]);
                }
            }
        }
    }

}
