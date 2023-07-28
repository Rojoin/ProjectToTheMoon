public class PlayerHealth : HealthSystem
{
    protected override void Deativate()
    {
        onDeath.Invoke();
        transform.gameObject.SetActive(false);
    }
}