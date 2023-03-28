using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPictureColor
{
    Red, Blue, Green,
    Pink, Orange, Yellow,
    Black, White, Purple,
    Brown, Cyan, Lime
}

public class PictureColor : MonoBehaviour
{
    private static List<Color> colors = new List<Color>()
    {
        new Color(1f, 0f, 0f),
        new Color(0f, 0f, 1f),
        new Color(0f, 0.6f, 0f),
        new Color(1f, 0.298f, 0.902f),
        new Color(1f, 0.4f, 0f),
        new Color(1f, 0.922f, 0.016f),
        new Color(0f, 0f, 0f),
        new Color(1f, 1f, 1f),
        new Color(0.6f, 0f, 0.6f),
        new Color(0.698f, 0.2f, 0f),
        new Color(0f, 1f, 1f),
        new Color(0.102f, 1f, 0.102f)
    };

   public static Color GetColor(EPictureColor pictureColor) { return colors[(int)pictureColor]; }

    public static string GetColorName(Color _color)
    { 
        // RGBA 값 비교가 안되는 색깔은 우선 하드코딩으로 대체했음

        if(_color == colors[(int)EPictureColor.Red])
        {
            return "Red";
        }
        else if (_color == colors[(int)EPictureColor.Blue])
        {
            return "Blue";
        }
        else if (_color == colors[(int)EPictureColor.Green])
        {
            return "Green";
        }
        // Pink
        else if (((int)(_color.r * 1000) == 1000) && ((int)(_color.b * 1000) == 901)
            && ((int)(_color.g * 1000) == 298))
        {
            return "Pink";
        }
        else if (_color == colors[(int)EPictureColor.Orange])
        {
            return "Orange";
        }
        // Yellow
        else if (((int)(_color.r *1000) == 1000) && ((int)(_color.b * 1000) == 15)
            && ((int)(_color.g * 1000) == 921))
        {
            return "Yellow";
        }
        else if (_color == colors[(int)EPictureColor.Black])
        {
            return "Black";
        }
        else if (_color == colors[(int)EPictureColor.White])
        {
            return "White";
        }
        else if (_color == colors[(int)EPictureColor.Purple])
        {
            return "Purple";
        }
        // Brown
        else if (((int)(_color.r * 1000) == 698) && ((int)(_color.b * 1000) == 0)
            && ((int)(_color.g * 1000) == 200))
        {
            return "Brown";
        }
        else if (_color == colors[(int)EPictureColor.Cyan])
        {
            return "Cyan";
        }
        // Brown
        else if (((int)(_color.r * 1000) == 101) && ((int)(_color.b * 1000) == 101)
            && ((int)(_color.g * 1000) == 1000))
        {
            return "Lime";
        }

        Debug.Log((int)(_color.r * 1000));
        Debug.Log((int)((colors[(int)EPictureColor.Yellow]).r * 1000));

        Debug.Log((int)(_color.b * 1000));
        Debug.Log((int)((colors[(int)EPictureColor.Yellow]).b * 1000));

        Debug.Log((int)(_color.g * 1000));
        Debug.Log((int)((colors[(int)EPictureColor.Yellow]).g * 1000));

        return null;
    }
}
