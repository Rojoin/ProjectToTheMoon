using UnityEngine;

public class LaserFactory
{
    public void CreateLaser(Laser laser, Transform pos, string layerName, LaserConfiguration laserConfig, Transform laserParent,Quaternion rotation)
    {
        var newBullet = GameObject.Instantiate(laser, pos.position, rotation, laserParent);
        Debug.Log(newBullet);
        newBullet.gameObject.layer = LayerMask.NameToLayer(layerName);
        newBullet.Damage = laserConfig.damage;
        newBullet.Distance = laserConfig.distance;
        newBullet.TimeUntilDeath = laserConfig.lifeTime;
    }
}