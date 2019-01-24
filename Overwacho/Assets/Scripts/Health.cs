using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SerializeField] RectTransform healthBar;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Muere");
        }
    }

    void OnChangeHealth(int health)
    {
        Debug.Log(health.ToString());
    }


}
