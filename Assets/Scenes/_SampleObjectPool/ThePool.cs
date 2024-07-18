using UnityEngine;
using UnityEngine.Pool;

public class ThePool : SingletonMono<ThePool>
{
    // �ӵ�Ԥ����
    public BulletOrSomethingSpawnFrequently bulletPrefab;

    // �����
    private ObjectPool<BulletOrSomethingSpawnFrequently> bulletPool;

    private new void Awake()
    {
        base.Awake();
        bulletPool = new ObjectPool<BulletOrSomethingSpawnFrequently>(CreateBullet, OnTakeFromPool, OnReturnedToPool, OnDestroyBullet, maxSize: 200);
    }

    private BulletOrSomethingSpawnFrequently CreateBullet()
    {
        return Instantiate(bulletPrefab);
    }

    private void OnTakeFromPool(BulletOrSomethingSpawnFrequently BulletOrSomethingSpawnFrequently)
    {
        BulletOrSomethingSpawnFrequently.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(BulletOrSomethingSpawnFrequently BulletOrSomethingSpawnFrequently)
    {
        BulletOrSomethingSpawnFrequently.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(BulletOrSomethingSpawnFrequently BulletOrSomethingSpawnFrequently)
    {
        Destroy(BulletOrSomethingSpawnFrequently.gameObject);
    }
    public void ReturnBullet(BulletOrSomethingSpawnFrequently BulletOrSomethingSpawnFrequently)
    {
        bulletPool.Release(BulletOrSomethingSpawnFrequently);
    }
    public void ShootBullet(Vector3 position, Vector3 direction)
    {
        BulletOrSomethingSpawnFrequently BulletOrSomethingSpawnFrequently = bulletPool.Get();
        BulletOrSomethingSpawnFrequently.transform.position = position;
        BulletOrSomethingSpawnFrequently.Shoot(direction);
    }
}
