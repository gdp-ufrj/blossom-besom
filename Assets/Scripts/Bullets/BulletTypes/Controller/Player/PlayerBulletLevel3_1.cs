using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletLevel3_1", menuName = "Bullets/Player Bullet L3_1", order = 0)]
public class PlayerBulletLevel3_1 : BulletMovementScriptable
{
    public override Vector2 Move(float t)
    {
        return new Vector2(3 * t, 3 * t / 2 + 10);
    }
}
