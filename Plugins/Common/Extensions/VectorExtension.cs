using UnityEngine;

public static class VectorExtention
{
    #region Clamp

    public static Vector2 Clamp(this Vector2 target, float min, float max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);

        return target;
    }

    public static Vector2 Clamp(this Vector2 target, Vector2 min, Vector2 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);

        return target;
    }

    public static Vector2Int Clamp(this Vector2Int target, int min, int max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);

        return target;
    }

    public static Vector2Int Clamp(this Vector2Int target, Vector2Int min, Vector2Int max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);

        return target;
    }

    public static Vector3 Clamp(this Vector3 target, float min, float max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);
        target.z = Mathf.Clamp(target.z, min, max);

        return target;
    }

    public static Vector3 Clamp(this Vector3 target, Vector3 min, Vector3 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = Mathf.Clamp(target.z, min.z, max.z);

        return target;
    }

    public static Vector3Int Clamp(this Vector3Int target, int min, int max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);
        target.z = Mathf.Clamp(target.z, min, max);

        return target;
    }

    public static Vector3Int Clamp(this Vector3Int target, Vector3Int min, Vector3Int max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = Mathf.Clamp(target.z, min.z, max.z);

        return target;
    }

    public static Vector4 Clamp(this Vector4 target, float min, float max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);
        target.z = Mathf.Clamp(target.z, min, max);
        target.w = Mathf.Clamp(target.w, min, max);

        return target;
    }

    public static Vector4 Clamp(this Vector4 target, Vector4 min, Vector4 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = Mathf.Clamp(target.z, min.z, max.z);
        target.w = Mathf.Clamp(target.w, min.w, max.w);

        return target;
    }

    #endregion

    #region Subdivision

    public static Vector2 xy(this Vector3 target)
    {
        return new Vector2(target.x, target.y);
    }

    public static Vector2 xz(this Vector3 target)
    {
        return new Vector2(target.x, target.z);
    }

    public static Vector2 yz(this Vector3 target)
    {
        return new Vector2(target.y, target.z);
    }

    public static Vector2Int xy(this Vector3Int target)
    {
        return new Vector2Int(target.x, target.y);
    }

    public static Vector2Int xz(this Vector3Int target)
    {
        return new Vector2Int(target.x, target.z);
    }

    public static Vector2Int yz(this Vector3Int target)
    {
        return new Vector2Int(target.y, target.z);
    }

    public static Vector2 xy(this Vector4 target)
    {
        return new Vector2(target.x, target.y);
    }

    public static Vector2 xz(this Vector4 target)
    {
        return new Vector2(target.x, target.z);
    }

    public static Vector2 xw(this Vector4 target)
    {
        return new Vector2(target.x, target.w);
    }

    public static Vector2 yz(this Vector4 target)
    {
        return new Vector2(target.y, target.z);
    }

    public static Vector2 yw(this Vector4 target)
    {
        return new Vector2(target.y, target.w);
    }

    public static Vector2 zw(this Vector4 target)
    {
        return new Vector2(target.z, target.w);
    }

    public static Vector3 xyz(this Vector2 target, float z)
    {
        return new Vector3(target.x, target.y, z);
    }

    public static Vector3 xyz(this Vector4 target)
    {
        return new Vector3(target.x, target.y, target.z);
    }

    public static Vector3 xyw(this Vector4 target)
    {
        return new Vector3(target.x, target.y, target.w);
    }

    public static Vector3 xzw(this Vector4 target)
    {
        return new Vector3(target.x, target.z, target.w);
    }

    public static Vector3 yzw(this Vector4 target)
    {
        return new Vector3(target.y, target.z, target.w);
    }

    #endregion

    #region Quaternion

    public static Quaternion ToQuaternion(this Vector4 target)
    {
        return new Quaternion(target.x, target.y, target.z, target.w);
    }

    public static Quaternion ToNormalizedQuaternion(this Vector4 target)
    {
        target = Vector4.Normalize(target);
        return new Quaternion(target.x, target.y, target.z, target.w);
    }

    public static Vector4 ToVector4(this Quaternion target)
    {
        return new Vector4(target.x, target.y, target.z, target.w);
    }

    #endregion
}
