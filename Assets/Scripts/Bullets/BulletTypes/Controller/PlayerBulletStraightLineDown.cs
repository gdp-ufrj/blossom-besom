using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletStraightLineDown : BulletController
{
    public override Vector2 Move(float t)
    {
        return new Vector2(t, 10);
    }
}