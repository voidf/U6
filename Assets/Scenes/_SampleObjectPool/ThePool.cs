using UnityEngine;
using UnityEngine.Pool;

public class ThePool : SingletonMono<ThePool>
{
    // 子弹预制体
    public BulletOrSomethingSpawnFrequently bulletPrefab;

    // 对象池
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
    void ShootBullet(Vector3 position, Vector3 direction)
    {
        BulletOrSomethingSpawnFrequently BulletOrSomethingSpawnFrequently = bulletPool.Get();
        BulletOrSomethingSpawnFrequently.transform.position = position;
        BulletOrSomethingSpawnFrequently.Ctor(direction);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 vector3 = new Vector3(mousePos.x, mousePos.y, 0);
            ShootBullet(vector3, Vector3.up);
        }
    }
}
