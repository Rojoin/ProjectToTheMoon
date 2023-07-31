using UnityEngine;

[CreateAssetMenu(menuName = "DirHandler/TurretDirection", fileName = "TurretDirection", order = 0)]
public class TurretDirection : DirectionHandler
{
    public override Vector3 GetDirection(Transform transform, Transform world)
    {
        return transform.forward;
    }
}