using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;

public static class Utils
{
    static System.Random rnd = new System.Random();

    public static Vector3 SetX3(this Vector3 position, float x = 0)
    {
        position.x = x;
        return position;
    }

    public static Vector3 SetY3(this Vector3 position, float y = 0)
    {
        position.y = y;
        return position;
    }

    public static Vector3 SetZ3(this Vector3 position, float z = 0)
    {
        position.z = z;
        return position;
    }

    public static Vector3 Sum3(this Vector3 position, float x = 0, float y = 0, float z = 0)
    {
        return new Vector3(position.x + x, position.y + y, position.z + z);
    }

    public static Vector3 Mult3(this Vector3 position, float x = 1f, float y = 1f, float z = 1f)
    {
        return new Vector3(position.x * x, position.y * y, position.z * z);
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

    public static bool isAxisActive(float axis)
    {
        return axis > CONTROL_DEADZONE || axis < -CONTROL_DEADZONE;
    }

    public static T PickRandom<T>(this IList<T> source)
    {
        return source[rnd.Next(source.Count)];
    }
}