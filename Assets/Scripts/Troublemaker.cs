using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Troublemaker : MonoBehaviour {

    private ScreenIndicator screenIndicator;
    [SerializeField] private Enemy enemy;
    private Knockable knockable;
    public bool isCausingTrouble;
    public float timeTillTroubleCaused;
    public int health;
    private bool invincible = false;
    [SerializeField] float reputationDamage;
    [SerializeField] float invincibilityTime = 0.1f;
    [SerializeField] float shakeDurationWhenHit;

    void Awake() {
        screenIndicator = gameObject.GetComponent<ScreenIndicator>();
        knockable = gameObject.GetComponent<Knockable>();
    }

    void Update() {
        if (isCausingTrouble) {
            StartCoroutine(CausingTrouble());
        }
        else {
            screenIndicator.enabled = false;
        }
    }

    public void TakeDamage(int damageAmmount, GameObject damageDealer, float knockbackForce) {
        if (health >= 1 && !invincible) {
            if (isCausingTrouble) {
                CameraShake.Instance.shakeDuration = shakeDurationWhenHit;
                knockable.TakeKnockback(damageDealer, knockbackForce);
                health -= damageAmmount;
            }

            StartCoroutine(Invulnerability());

            if (health <= 0) {
                Die();
            }
        }
    }

    IEnumerator Invulnerability() {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    IEnumerator CausingTrouble() {
        screenIndicator.enabled = true;

        while(isCausingTrouble) {
            yield return new WaitForSeconds(timeTillTroubleCaused);
            CausedTrouble();
        }
    }

    public void Flip() {
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public void CausedTrouble() {
        ScoreTracker.Instance.score --;
        PlayerStats.Instance.TakeDamage(reputationDamage, gameObject, 0);
        isCausingTrouble = false;
    }

    public void Die() {
        ScoreTracker.Instance.score ++;
        PlayerStats.Instance.AddHealth();
        isCausingTrouble = false;
        gameObject.SetActive(false);
    }
}
