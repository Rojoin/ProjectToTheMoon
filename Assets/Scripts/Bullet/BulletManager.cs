using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class BulletManager : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [Header("Transform")]
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Transform world;
    [FormerlySerializedAs("bullet")] [SerializeField] private Bullet bulletPrefab;
    private BulletFactory factory = new BulletFactory();
    private ObjectPool<Bullet> _pool;
  

    public void Awake()
    {
        askForBulletChannel.Subscribe(SpawnBullet);
        _pool = new ObjectPool<Bullet>(() => Instantiate(bulletPrefab, bulletParent),
            bullet => { bullet.gameObject.SetActive(true); }, bullet => { bullet.gameObject.SetActive(false); },
            bullet => { Destroy(bullet.gameObject); }, false, 20, 100);
    }

    public void OnDisable()
    {
        askForBulletChannel.Unsubscribe(SpawnBullet);
    }

    public void KillBullet(Bullet bul)
    {
        _pool.Release(bul);
    }
    private void SpawnBullet(Transform pos, string layer, BulletConfiguration bulletConfig, Quaternion rotation)
    {
        var newBullet = _pool.Get();
        factory.ConfigureBullet(ref newBullet, pos, layer, bulletConfig, world, bulletParent, rotation);
        newBullet.Init(KillBullet);
        newBullet.StartBullet();
    }

   
}

