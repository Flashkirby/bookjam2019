using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 X(this Vector3 position, float x = 0)
    {
        return new Vector3(x, position.y, position.z);
    }
    public static Vector3 Y(this Vector3 position, float y = 0)
    {
        return new Vector3(position.x, y, position.z);
    }
    public static Vector3 Z(this Vector3 position, float z = 0)
    {
        return new Vector3(position.x, position.y, z);
    }
    public static Vector3 Set(this Vector3 position, float x = 0, float y = 0)
    {
        return new Vector3(x, y, position.z);
    }


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