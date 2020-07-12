using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {

    [SerializeField] private Troublemaker troublemaker;
    [SerializeField] private SpriteRenderer m_leftArmRenderer;
    [SerializeField] private SpriteRenderer m_rightArmRenderer;
    [SerializeField] private Animator m_leftArmAnim;
    [SerializeField] private Animator m_rightArmAnim;

    [SerializeField] private float attackDelay;
    [SerializeField] private float timeBetweenPunches;
    private float coolDownElapsed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damageDealt;
    [SerializeField] private float knockbackForce;

    void Update() {
        if (coolDownElapsed > 0) {
			coolDownElapsed -= Time.deltaTime * 1;
		}
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && troublemaker.isCausingTrouble && coolDownElapsed < attackCooldown){
            StartCoroutine(Attack(other));
        }
    }

    IEnumerator Attack(Collider2D col) {
        while (col.gameObject.tag == "Player" && troublemaker.isCausingTrouble && coolDownElapsed < attackCooldown) {
            coolDownElapsed = attackCooldown;
            yield return new WaitForSeconds(attackDelay);
            m_leftArmAnim.SetTrigger("Punch");
            yield return new WaitForSeconds(timeBetweenPunches);
            m_rightArmAnim.SetTrigger("Punch");
            PlayerStats.Instance.TakeDamage(damageDealt, gameObject, knockbackForce);
        }
    }
}
