using UnityEngine;
using System.Collections;

/**
 * Static class for work with colors
 */ 
public static class Colors {

    /**
     * default array of colors
     */
    private static Color[] colors = new Color[]{   Color.blue,
          Color.red,
          Color.cyan,
          Color.yellow,
          Color.green,
          Color.magenta};

    /**
     * Changes array of colors
     */
    public static void Init(Color[] _colors) {
        colors = new Color[_colors.Length];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = _colors[i];
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
}
