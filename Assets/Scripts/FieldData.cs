using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData
{
    private int length;
    private int width;
    private int height;
    private float lengthGap;
    private float widthGap;

    //Свойства
    public int Length { get { return length; } }
    public int Width { get { return width; } }
    public int Height { get { return height; } }
    public float LengthGap { get { return lengthGap; } }
    public float WidthGap { get { return widthGap; } }

    //Конструктор
    public FieldData(int length, int width, int height, float lengthGap, float widthGap) 
    {
        this.length = length;
        this.width = width;
        this.height = height;
        this.lengthGap = lengthGap;
        this.widthGap = widthGap;
    }
}
