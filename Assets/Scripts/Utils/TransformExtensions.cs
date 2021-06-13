using System.Runtime.CompilerServices;
using UnityEngine;

namespace Utils
{
    public static class TransformExtensions
    {
        public static void DestroyImmediateChildren(this Transform parent)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(parent.GetChild(i).gameObject);
            }
        }
    }
}