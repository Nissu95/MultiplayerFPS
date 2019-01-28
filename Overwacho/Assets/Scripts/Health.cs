using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SerializeField] RectTransform healthBar;
    [SerializeField] float respawnTime;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    Player playerScript;
    float defaultSizeDeltaX;
    

    private void Start()
    {
        defaultSizeDeltaX = healthBar.sizeDelta.x;
        playerScript = GetComponent<Player>();
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            RpcDeath();
            currentHealth = maxHealth;
        }
    }

    [ClientRpc]
    void RpcDeath()
    {
        playerScript.CreateRagdoll();
        playerScript.DisablePlayer();
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        playerScript.EnablePlayer();
    }

    void OnChangeHealth(int health)
    {
        Vector2 healthbarSize = new Vector2((defaultSizeDeltaX * health) / maxHealth, healthBar.sizeDelta.y);

        healthBar.sizeDelta = healthbarSize;
    }


}
