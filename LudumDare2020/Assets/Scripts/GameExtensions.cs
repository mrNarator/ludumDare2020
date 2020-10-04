using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class GameExtensions
{
    public static Movement.MovementType ToMoveDirection(this Vector3 vec)
    {
        if(Mathf.Approximately(vec.z, 0f))
        {
            goto LeftRight;
        }
        var ratio = vec.x / vec.z;
        if(Mathf.Abs(ratio) > 1)
        {
            goto LeftRight;
        }
        else
        {
            if(vec.z > 0)
            {
                return Movement.MovementType.Forward;
            }
            return Movement.MovementType.Backward;
        }

        LeftRight:

        if (vec.x > 0)
        {
            return Movement.MovementType.Left;
        }
        return Movement.MovementType.Right;
    }
}
