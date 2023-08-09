using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private BulletScriptable[] bulletScriptables;

    private ObjectPool<BulletController>[] bulletPools;

    private void Start()
    {
        bulletPools = new ObjectPool<BulletController>[bulletScriptables.Length];
        for (int i = 0; i < bulletScriptables.Length; i++)
        {
            bulletPools[i] = createObjectPool(bulletScriptables[i], i, 10, 20);
        }
    }

    private ObjectPool<BulletController> createObjectPool(BulletScriptable bulletScriptable, int bulletType, int size, int maxSize)
    {
        return new ObjectPool<BulletController>(() =>
        {
            var bullet = Instantiate(bulletScriptable.BulletPrefab);
            bullet.OnCreateObject((bullet, healthManager) => BulletHit(bullet, healthManager, bulletType));
            return bullet;
        }, (bullet) =>
        {
            bullet.gameObject.SetActive(true);
        }, (bullet) =>
        {
            bullet.gameObject.SetActive(false);
        }, (bullet) =>
        {
            Destroy(bullet.gameObject);
        }, false, size, maxSize);
    }

    public void Shoot(BulletScriptable bulletScriptable, Vector2 startPosition)
    {
        int bulletIndex = System.Array.IndexOf(bulletScriptables, bulletScriptable);
        BulletController bullet = bulletPools[bulletIndex].Get();
        bullet.OnReuseObject(startPosition);
    }

    private void BulletHit(BulletController bullet, HealthManager target, int bulletType)
    {
        bulletPools[bulletType].Release(bullet);
        string bulletName = bulletScriptables[bulletType].ToString();
        if (target == null)
        {
            Debug.Log($"{bulletName} missed");
            return;
        }
        Debug.Log($"{bulletName} hit {target.name}");

    }
}
