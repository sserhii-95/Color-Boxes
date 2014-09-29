using UnityEngine;
using System.Collections;

/**
 * Static class for work with colors
 */ 
public static class Colors {

    private static bool[] locks = { false, false, false, false};

    public static Color[] customColors = {
            new Color(0.50f, 0.50f, 0.50f),
            new Color(0.51f, 0.51f, 0.51f),
            new Color(0.52f, 0.52f, 0.52f),
            new Color(0.53f, 0.53f, 0.53f),
            new Color(0.54f, 0.54f, 0.54f),
            };

    private static Color[] colors = {
          Color.red,
          Color.blue,
          Color.green};

    /**
     * default array of colors
     */
    public static Color[] colorsS = new Color[]{
          Color.blue,
          Color.red,
          Color.cyan,
          Color.yellow,
          Color.green,
          Color.magenta};

    public static Color[] colorsM = new Color[]{
        Color255(255, 255,  82),
        Color255(252,  66, 123),
        Color255(209, 241, 239),
        Color255(243,  90,   0),
        Color255(58,  185, 124)};

    public static Color[] colorsG = new Color[]{
        Color255(255, 215, 0),
        Color255(192, 192, 192),
        Color255(184, 115, 51),
        Color255(98, 226, 254),
    };
    

    public static Color Color255(int r, int g, int b) {
        return new Color(r / 255f, g / 255f, b / 255f);
    }



    public static Color[] GetColors() { 
        Color[] _colors = new Color[colors.Length];
        for (int i = 0; i < _colors.Length; i++)
            _colors[i] = colors[i];
        return _colors;
    }

    /**
     * Changes array of colors
     */
    public static void Init(Color[] _colors) {
        colors = new Color[_colors.Length];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = _colors[i];
    }

    public static void Init(int index) {
        switch (index) { 
            case 0:
                Init(colorsS);
                break;
            case 1:
                Init(colorsM);
                break;
            case 2:
                Init(customColors);
                break;
            case 3:
                Init(colorsG);
                break;
        }
    }

    /**
     * Returns color, wich has index = randomIndex
     */
    public static Color GetColor(int randomIndex) {
        return colors[randomIndex % colors.Length];
    }

    /**
     * Returns color, wich has index = randomIndex
     * if color with this index equal color in parameters, retunrs color with index = randomIndex + 1
     */
    public static Color GetDiffColor(this Color color, int randomIndex) {
        Color newColor = GetColor(randomIndex);
        if (newColor.Equals(color))
            return GetColor(randomIndex + 1);
        else
            return newColor;
    }

    public static bool isLocked(int idx){
        return locks[idx];
    }
}
