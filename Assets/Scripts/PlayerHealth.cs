using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerHealth : HealthSystem
{
    public bool GodMode { get; set; }

    public void Awake()
    {
        GodMode = false;
    }

    public override void ReceiveDamage(float damage)
    {
        if (!GodMode)
        {
            base.ReceiveDamage(damage);
        }
    }

    public override void Deactivate()
    {
        onDeath.Invoke();
        transform.gameObject.SetActive(false);
    }
}