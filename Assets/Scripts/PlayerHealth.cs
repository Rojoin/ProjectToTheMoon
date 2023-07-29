public class PlayerHealth : HealthSystem
{
    public override void Deactivate()
    {
        onDeath.Invoke();
        transform.gameObject.SetActive(false);
    }
}