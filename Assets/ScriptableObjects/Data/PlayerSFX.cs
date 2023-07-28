using UnityEngine;

[CreateAssetMenu(menuName = "SFXControllers/PlayerSFX", fileName = "PlayerSFX")]
class PlayerSFX : SFXSO
{
    [Header("Sounds")]
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] [Range(0, 1)] private float explosionVolume;
    [SerializeField] private AudioClip inpactSound;
    [SerializeField] [Range(0, 1)] private float inpactVolume;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] [Range(0, 1)] private float shootVolume;
    [SerializeField] private AudioClip laserClip;
    [SerializeField] [Range(0, 1)] private float laserVolume;
    [SerializeField] private AudioClip prepareLaserClip;
    [SerializeField] [Range(0, 1)] private float prepareLaserVolume;
    [SerializeField] private AudioClip barrelRollSound;
    [SerializeField] [Range(0, 1)] private float barrelRollVolume;

    public void ExplosionSound()
    {
        SFXChannel.RaiseEvent(explosionSound, explosionVolume);
    }

    public void InpactSound()
    {
        SFXChannel.RaiseEvent(inpactSound, inpactVolume);
    }

    public void ShootSound()
    {
        SFXChannel.RaiseEvent(shootClip, shootVolume);
    }

    public void ShootLaserSound()
    {
        SFXChannel.RaiseEvent(laserClip, laserVolume);
    }

    public void PrepareLaserSound()
    {
        SFXChannel.RaiseEvent(prepareLaserClip, prepareLaserVolume);
    }

    public void BarrelRollSound()
    {
        SFXChannel.RaiseEvent(barrelRollSound, barrelRollVolume);
    }
}