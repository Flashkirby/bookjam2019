using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static Vector2 SetX(this Vector2 position, float x = 0)
    {
        position.x = x;
        return position;
    }

    public static Vector2 SetY(this Vector2 position, float y = 0)
    {
        position.y = y;
        return position;
    }

    public static int Dir(float x)
    {
        if (x > 0) return 1;
        else if (x < 0) return -1;
        else { return 0; }
    }
}