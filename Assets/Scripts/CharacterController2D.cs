﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

	private Rigidbody2D m_rigidbody2D;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	private void Awake() {
		m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

	public void Move(Vector2 move) {
        // Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move.x * 10, move.y * 10);
		// And then smoothing it out and applying it to the character
		m_rigidbody2D.velocity = Vector3.SmoothDamp(m_rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (move.x > 0 && !m_FacingRight) {
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move.x < 0 && m_FacingRight) {
			// ... flip the player.
			Flip();
		}
    }
    
	private void Flip() {
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
