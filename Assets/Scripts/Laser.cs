using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Damage { get; set; } 
    public float TimeUntilDeath { get; set; } 
    public float Distance { get; set; } 
    
    private IEnumerator Start()
    {
        Destroy(gameObject, TimeUntilDeath);
        List<HealthSystem> obj = new List<HealthSystem>();
        while (gameObject.activeSelf)
        {
            if (CheckLaserHitBox(out var hit) && hit.collider.TryGetComponent<HealthSystem>(out var health))
            {
               if(obj.Contains(health))
               {
                   yield return null;
               }
               else
               {
                   obj.Add(health);
                   health.ReceiveDamage(Damage);
                   if (!health.IsAlive())
                   {
                       health.Deactivate();
                   }

               }
            }
            yield return null;
        }
    }
    /// <summary>
    /// Check if Ray hit Something
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    private bool CheckLaserHitBox(out RaycastHit hit)
    {
        var transform1 = transform;
        var position = transform1.position;
        var forward = transform1.forward;
        return Physics.Raycast(position, forward, out hit, Distance) ||
               Physics.Raycast(position + Vector3.up / 2, forward, out hit, Distance) ||
               Physics.Raycast(position + Vector3.down / 2, forward, out hit, Distance) ||
               Physics.Raycast(position + Vector3.right / 2, forward, out hit, Distance) ||
               Physics.Raycast(position + Vector3.left / 2, forward, out hit, Distance);
    }

  
}
