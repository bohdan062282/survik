using UnityEngine;

public class BulletPool
{

    private GameObject _bulletPrefab;

    private GameObject[] pool;

    private int _size;
    private int _nextIndex;


    public BulletPool(int size)
    {
        _bulletPrefab = Resources.Load<GameObject>("Bullet/Bullet");

        _size = size;

        pool = new GameObject[size];
        
        for (int i = 0; i < size; i++)
        {
            pool[i] = GameObject.Instantiate(_bulletPrefab);
        }

        _nextIndex = 0;

    }
    public void releaseBullet(Vector3 position, Vector3 direction, float speed, float damage, int hittingID)
    {
        pool[_nextIndex].GetComponent<BulletScript>().release(position, direction, speed, damage, hittingID);

        _nextIndex += 1;

        if (_nextIndex == _size) _nextIndex = 0;

    }
    
}
