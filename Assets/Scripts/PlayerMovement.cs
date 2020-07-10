using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private CharacterController2D m_controller;
    [SerializeField] private Rigidbody2D m_rigidbody2D;

    [SerializeField] float playerMoveSpeed;
    private float m_horizontalMove = 0f;
    private float m_verticalMove = 0f;

    private Vector2 moveClamped;

    [SerializeField] private Animator m_legsAnim;

	private void Awake() {
        m_controller = GetComponent<CharacterController2D>();
		m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        m_horizontalMove = Input.GetAxisRaw("Horizontal") * playerMoveSpeed;
        m_verticalMove = Input.GetAxisRaw("Vertical") * playerMoveSpeed;

        moveClamped = new Vector2(m_horizontalMove, m_verticalMove);
        moveClamped = Vector2.ClampMagnitude(moveClamped, playerMoveSpeed);

        // Play moving legs animation if Moving
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
			m_legsAnim.SetBool("Moving", true);
		}
        else {
        	m_legsAnim.SetBool("Moving", false);
        }
    }

    void FixedUpdate(){
        m_controller.Move(moveClamped * Time.fixedDeltaTime);
    }
}
