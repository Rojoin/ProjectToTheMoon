using UnityEngine;

[CreateAssetMenu(menuName = "VFXControllers/PlayerVFX", fileName = "VFXPlayer")]
public class PlayerVFX : VFXSO
{
    [SerializeField] protected ParticleSystem prefireLaser;

    private ParticleSystem prefireLaserGameObject;

    public void PlayPreFireLaserAnimation(Transform transform)
    {
        if (!prefireLaserGameObject)
        {
            prefireLaserGameObject = Instantiate(prefireLaser, transform.position, Quaternion.identity, transform);
            prefireLaserGameObject.Play();
        }
        else
        {
            prefireLaserGameObject.Play();
        }
    }

    public void StopPrefireLaser()
    {
        prefireLaserGameObject.Stop();
    }
}