using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SerializeField] RectTransform healthBar;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    float defaultSizeDeltaX;

    private void Start()
    {
        defaultSizeDeltaX = healthBar.sizeDelta.x;
    }

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
        Vector2 healthbarSize = new Vector2((defaultSizeDeltaX * health) / maxHealth, healthBar.sizeDelta.y);

        healthBar.sizeDelta = healthbarSize;
    }


}
