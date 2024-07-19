using UnityEngine;

public class BulletOrSomethingSpawnFrequently : MonoBehaviour
{
    Vector3 orientation;

    public void Ctor(Vector3 _ori)
    {
        orientation = _ori;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += orientation * Time.fixedDeltaTime;
        // 如果超出屏幕范围，回收
        var cam = Camera.main;
        var min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        var max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        if (transform.position.x < min.x || transform.position.x > max.x || transform.position.y < min.y || transform.position.y > max.y)
        {
            ThePool.Instance.ReturnBullet(this);
        }
    }
}
