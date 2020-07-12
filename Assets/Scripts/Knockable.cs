using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockable : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void TakeKnockback(GameObject collider, float knockbackForce) {
        moveDirection = transform.position - collider.transform.position;
        rb.velocity = (moveDirection.normalized * knockbackForce);
    }
}