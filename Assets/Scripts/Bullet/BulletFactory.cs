using Unity.VisualScripting;
using UnityEngine;


public class BulletFactory
{
  
 
    public Bullet CreateBullet(Bullet bullet, Transform pos, string layerName, BulletConfiguration bulletConfig, Transform world, Transform bulletParent,Quaternion rotation)
    {
        var newBullet = GameObject.Instantiate(bullet, pos.position,  rotation, bulletParent);
        newBullet.gameObject.layer = LayerMask.NameToLayer(layerName);
        newBullet.transform.SetParent(bulletParent);
        newBullet.transform.rotation = rotation;
        newBullet.transform.position = pos.position;
        newBullet.DirHandler = bulletConfig.directionHandler;
        newBullet.Damage = bulletConfig.damage;
        newBullet.Velocity = bulletConfig.velocity;
        newBullet.SetStartPosition(pos);
        newBullet.SetWorld(world);
        return newBullet;
    }
    public void ConfigureBullet(ref Bullet newBullet,Transform pos, string layerName, BulletConfiguration bulletConfig, Transform world, Transform bulletParent,Quaternion rotation)
    {
        newBullet.transform.SetParent(bulletParent);
        newBullet.transform.rotation = rotation;
        newBullet.transform.position = pos.position;
        newBullet.gameObject.layer = LayerMask.NameToLayer(layerName);
        newBullet.DirHandler = bulletConfig.directionHandler;
        newBullet.Damage = bulletConfig.damage;
        newBullet.Velocity = bulletConfig.velocity;
        newBullet.SetStartPosition(pos);
        newBullet.SetWorld(world);
    }
}