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

	private void Awake() {
        m_controller = GetComponent<CharacterController2D>();
		m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        m_horizontalMove = Input.GetAxisRaw("Horizontal") * playerMoveSpeed;
        m_verticalMove = Input.GetAxisRaw("Vertical") * playerMoveSpeed;

        moveClamped = new Vector2(m_horizontalMove, m_verticalMove);
        moveClamped = Vector2.ClampMagnitude(moveClamped, playerMoveSpeed);
    }

    void FixedUpdate(){
        m_controller.Move(moveClamped * Time.fixedDeltaTime);
    }
}
