using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    [SerializeField] private CharacterController2D m_controller;
    [SerializeField] private Rigidbody2D m_rigidBody2D;

    [SerializeField] private SpriteRenderer m_leftArmRenderer;
    [SerializeField] private SpriteRenderer m_rightArmRenderer;

    [SerializeField] private Animator m_leftArmAnim;
    [SerializeField] private Animator m_rightArmAnim;

    private Vector2 moveDirection;
    [SerializeField] private int damageDealt;
    [SerializeField] private float knockbackForce;

    void Update() {
        // Play punch animation & logic when mouse clicked
		if (Input.GetButtonDown("Fire1")) {
            if (m_controller.m_FacingRight){
			    m_leftArmAnim.SetTrigger("Punch");
                m_rightArmRenderer.sortingOrder  = 4;	
            }
            else {
                m_rightArmAnim.SetTrigger("Punch");
                m_rightArmRenderer.sortingOrder  = 4;	
            }
		}
        else if (Input.GetButtonDown("Fire2")) {
            if (m_controller.m_FacingRight){
			    m_rightArmAnim.SetTrigger("Punch");
                m_leftArmRenderer.sortingOrder  = 3;	
            }
            else {
                m_leftArmAnim.SetTrigger("Punch");
                m_leftArmRenderer.sortingOrder  = 3;	
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            if (Input.GetButtonDown("Fire1") || (Input.GetButtonDown("Fire2"))) {
                other.gameObject.GetComponent<Troublemaker>().TakeDamage(damageDealt, gameObject, knockbackForce);
            }
        }
    }
}
