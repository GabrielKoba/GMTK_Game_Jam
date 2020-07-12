using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Knockable knockable;
    private bool invincible = false;
    [SerializeField] float invincibilityTime = 0.1f;
    [SerializeField] float shakeDurationWhenHit;

    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg, GameObject damageDealer, float knockbackForce)
    {
        if (!invincible) {
            StartCoroutine(Invulnerability());
            
            CameraShake.Instance.shakeDuration = shakeDurationWhenHit;
            
            health -= dmg;

            knockable.TakeKnockback(damageDealer, knockbackForce);

            if(health == 0) {
                Die();
            }
        }

        ClampHealth();
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;
        }
        
        if (onHealthChangedCallback != null)
        onHealthChangedCallback.Invoke();
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    void Die() {
        Menu.Instance.RestartGame();
    }

    IEnumerator Invulnerability() {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}
