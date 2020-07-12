using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour {

    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private Troublemaker troublemaker;
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private AIPath path;
    [SerializeField] private Animator m_legsAnim;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.

    void Update(){
        if (path.TargetReached)
            m_legsAnim.SetBool("Moving", false);
        else if (path.steeringTarget != transform.position) {
            m_legsAnim.SetBool("Moving", true);
        }

        // If the input is moving the player right and the player is facing left...
		if (path.steeringTarget.x > transform.position.x && !m_FacingRight) {
			// ... flip the player.
			troublemaker.Flip();
            // Switch the way the player is labelled as facing.
		    m_FacingRight = !m_FacingRight;
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (path.steeringTarget.x < transform.position.x && m_FacingRight) {
			// ... flip the player.
			troublemaker.Flip();
            // Switch the way the player is labelled as facing.
		    m_FacingRight = !m_FacingRight;
		}
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && troublemaker.isCausingTrouble){
            destinationSetter.target = other.transform;
        }
        else {
            destinationSetter.target = null;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            destinationSetter.target = null;
        }
    }
}
