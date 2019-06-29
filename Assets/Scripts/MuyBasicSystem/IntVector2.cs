using System;
using System.Text;
using UnityEngine;

[System.Serializable]
public struct IntVector2
{
    public int x;
    public int y;

    private static readonly IntVector2 zeroVector = new IntVector2(0, 0);
    public static IntVector2 IntVectorZero => zeroVector; // => as {get {return zeroVector;}}
    private static readonly IntVector2 downVector2Int = new IntVector2(0, -1);
    public static IntVector2 DownVector2Int => downVector2Int;
    private static readonly IntVector2 upVector2Int = new IntVector2(0, 1);
    public static IntVector2 UpVector2Int => upVector2Int;
    private static readonly IntVector2 leftVector2Int = new IntVector2(-1, 0);
    public static IntVector2 LeftVector2Int => leftVector2Int;
    private static readonly IntVector2 rightVector2Int = new IntVector2(1, 0);
    public static IntVector2 RightVector2Int => rightVector2Int;

    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public IntVector2(IntVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }

    public static IntVector2 operator +(IntVector2 v1, IntVector2 v2)
    {
        return new IntVector2(v1.x + v2.x, v1.y + v2.y);
    }
    public static IntVector2 operator -(IntVector2 v)
    {
        return new IntVector2(-v.x, -v.y);
    }
    public static IntVector2 operator -(IntVector2 v1, IntVector2 v2)
    {
        return new IntVector2(v1.x - v2.x, v1.y - v2.y);
    }
    public static IntVector2 operator *(IntVector2 v1, int x)
    {
        return new IntVector2(v1.x * x, v1.y * x);
    }

    public static bool operator ==(IntVector2 v1, IntVector2 v2)
    {
        return (v1.x == v2.x && v1.y == v2.y);
    }
    public static bool operator !=(IntVector2 v1, IntVector2 v2)
    {
        return (v1.x != v2.x || v1.y != v2.y);
    }

    // public static implicit operator Vector3(Vector2Int _self)
    // {
    //     return new Vector3(_self.x, _self.y, 0.0f);
    // }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("x:{0}, y{1}", x, y);
        return sb.ToString();
    }

    public override bool Equals(System.Object _other)
    {
        if (_other.GetType() != typeof(IntVector2))
            return false;

        IntVector2 other = (IntVector2)_other;
        return (this.x == other.x && this.y == other.y);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    // struct end
}
