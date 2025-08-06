

using System;
using UnityEngine;

namespace UtilityScripts
{
    public static class VectorUtilities
    {
        public static bool TooCloseXY(Vector3 t, Vector3 o)
        {
            return Math.Abs(t.x - o.x) < 10 ||
                   Math.Abs(t.y - o.y) < 10;
        }
    }
}