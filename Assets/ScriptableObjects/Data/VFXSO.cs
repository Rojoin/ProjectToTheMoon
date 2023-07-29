using UnityEngine;

[CreateAssetMenu(menuName = "VFXControllers/VFX", fileName = "VFX")]
public class VFXSO : ScriptableObject
{
    [SerializeField] protected ParticleSystem impactPrefab;
    [SerializeField] protected ParticleSystem boom;

    public void PlayImpactAnimation(Transform transform)
    {
        var inpact = Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
        inpact.Play();
        Destroy(inpact.gameObject, 1f);
    }
    
    public void PlayBoomAnimation(Transform transform)
    {
        var inpact = Instantiate(boom, transform.position, Quaternion.identity);
        inpact.Play();
        Destroy(inpact.gameObject, 2f);
    }
}