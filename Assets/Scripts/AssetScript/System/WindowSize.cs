using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Window Size Class

[System.Serializable]
public class Size
{
    public int width;
    public int height;

    public Size()
    {
        width = 0;
        height = 0;
    }

    public Size(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public string GetResolution() { return $"{width}x{height}"; }
}

#endregion

[CreateAssetMenu(fileName = "Window Size", menuName = "System Data/Window Size", order =1)]
public class WindowSize : ScriptableObject
{
    public Size[] size;

    public Size this[int select] => size[select];

    public int Count => size.Length;
}
