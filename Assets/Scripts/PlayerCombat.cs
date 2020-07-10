using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    [SerializeField] private CharacterController2D m_controller;

    [SerializeField] private SpriteRenderer m_leftArmRenderer;
    [SerializeField] private SpriteRenderer m_rightArmRenderer;

    [SerializeField] private Animator m_leftArmAnim;
    [SerializeField] private Animator m_rightArmAnim;

    void Update() {
        // Play punch animation & logic when mouse clicked
		if (Input.GetButtonDown("Fire1")) {
            if (m_controller.m_FacingRight){
			    m_leftArmAnim.SetTrigger("Punch");
                m_rightArmRenderer.sortingOrder  = 3;	
            }
            else {
                m_rightArmAnim.SetTrigger("Punch");
                m_rightArmRenderer.sortingOrder  = 3;	
            }
		}
        else if (Input.GetButtonDown("Fire2")) {
            if (m_controller.m_FacingRight){
			    m_rightArmAnim.SetTrigger("Punch");
                m_leftArmRenderer.sortingOrder  = 2;	
            }
            else {
                m_leftArmAnim.SetTrigger("Punch");
                m_leftArmRenderer.sortingOrder  = 2;	
            }
        }
    }
}
