using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Generic
{
    public interface IDeepDisposable : IDisposable
    {
        void Dispose(bool deep);
    }

public static class Extensions
{
    public static Vector3 MemberDivide(this Vector3 dividend, Vector3 divider)
    {
        dividend.x /= divider.x;
        dividend.y /= divider.y;
        dividend.z /= divider.z;
        return dividend;
    }

    public static Vector3 MemberMult(this Vector3 multiplicant, Vector3 multiplier)
    {
        multiplicant.x *= multiplier.x;
        multiplicant.y *= multiplier.y;
        multiplicant.z *= multiplier.z;
        return multiplicant;
    }

    public static Vector3 MemberAprox0(this Vector3 src)
    {
        if (Mathf.Approximately(src.x, 0f)) src.x = 0f;
        if (Mathf.Approximately(src.y, 0f)) src.y = 0f;
        if (Mathf.Approximately(src.z, 0f)) src.z = 0f;
        return src;
    }

    public static void Swap<T>(this IList<T> source, int id1, int id2)
    {
        var temp = source[id1];
        source[id1] = source[id2];
        source[id2] = temp;
    }

    public static float FrameLerp(this float source, float target, float smoothing, float dt)
    {
        return Mathf.Lerp(source, target, 1 - Mathf.Pow(smoothing, dt));
    }

    public static Vector3 FrameLerp(this Vector3 source, Vector3 target, float smoothing, float dt)
    {
        return Vector3.Lerp(source, target, 1 - Mathf.Pow(smoothing, dt));
    }

    /// <summary>
    /// Gets Vector x Plane interection point, or null, if vector is paraller to plane
    /// </summary>
    public static Vector3? IntersectPlane(this Vector3 vectorPoint, Vector3 vectorDirection, Vector3 planePoint, Vector3 planeNormal)
    {
        vectorDirection.Normalize();
        planeNormal.Normalize();
        var d1 = Vector3.Dot(vectorDirection, planeNormal);
        if (Mathf.Approximately(d1, Mathf.Epsilon))
            return null;
        var d2 = Vector3.Dot(planePoint, planeNormal) - Vector3.Dot(vectorPoint, planeNormal);
        return vectorDirection * (d2 / d1) + vectorPoint;
    }

    public static void Dispose<T>(this IList<T> self) where T : IDisposable
    {
        foreach (var disp in self)
        {
            disp.Dispose();
        }
        self.Clear();
    }

    public static void Dispose<T>(this IList<T> self, bool deep = false) where T : IDeepDisposable
    {
        foreach (var disp in self)
        {
            disp.Dispose(deep);
        }
        self.Clear();
    }

    public static void RemoveRange<T>(this IList<T> self, IList<T> toRemove)
    {
        for (int i = 0; i < toRemove.Count; i++)
        {
            self.Remove(toRemove[i]);
        }
    }

    public static void RemoveRange<T>(this IList<T> self, IEnumerable<T> toRemove)
    {
        foreach (var rem in toRemove)
        {
            self.Remove(rem);
        }
    }

    public static IList<T> ShuffleFY<T>(this IList<T> self)
    {
        var limit = self.Count;
        for (int i = 0; i < limit - 2; i++)
        {
            var idx = UnityEngine.Random.Range(i, limit);
            (self[idx], self[i]) = (self[i], self[idx]);
        }
        return self;
    }

    public static bool TryGetComponent<T>(this Transform go, out T result) where T : Component
    {
        result = go.GetComponent<T>();
        return result != null;
    }

    public static bool TryGetComponent<T>(this GameObject go, out T result) where T : Component
    {
        result = go.GetComponent<T>();
        return result != null;
    }

    public static Transform GetRoot(this Transform tr)
    {
        if (tr?.parent == null)
        {
            return tr;
        }

        return tr.parent.GetRoot();
    }

    public static List<TEnum> GetAllValues<TEnum>(params TEnum[] except)
        where TEnum : Enum
    {
        var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Except(except).ToList();
        return values;
    }

    public static T GetRandomValue<T>(this IList<T> self)
    {
        var random = UnityEngine.Random.Range(0, self.Count);
        return self[random];
    }

    public static Vector3 ToVec3NormUp(this Vector2 vec2, float yVal = 0f)
    {
        return new Vector3(vec2.x, yVal, vec2.y);
    }
}
}
