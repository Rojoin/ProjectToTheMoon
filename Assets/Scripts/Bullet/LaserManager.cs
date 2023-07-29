using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private AskForLaserChannel askForLaserChannel;
    [Header("Transform")]
    [SerializeField] private Laser laser;
    private LaserFactory factory = new LaserFactory();

    public void Awake()
    {
        askForLaserChannel.Subscribe(SpawnLaser);
    }

    public void OnDestroy()
    {
        askForLaserChannel.Unsubscribe(SpawnLaser);
    }

    private void SpawnLaser(Transform pos, string layer, LaserConfiguration laserConfig, Transform parent,
        Quaternion rotation)
    {
        factory.CreateLaser(laser, pos, layer, laserConfig, parent, rotation);
    }
}