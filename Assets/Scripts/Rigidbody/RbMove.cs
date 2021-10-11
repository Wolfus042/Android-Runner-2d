using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMove : MonoBehaviour
{
    public Rigidbody rigidBody;
    public SphereCollider groundSphere;
    public LayerMask groundLayer;
    public float jumpForce;

    public bool isJumping = false;

    [SerializeField]private bool grounded = true;

    //public Vector3 constantForces;

    private void Update() {
        if (Physics.CheckSphere(groundSphere.transform.position + groundSphere.center, groundSphere.radius, groundLayer)) {
            if (!grounded) {
                isJumping = false;
                grounded = true;
            }
        }
        grounded = Physics.CheckSphere(groundSphere.transform.position + groundSphere.center, groundSphere.radius, groundLayer);

    }

    public bool Grounded() {
        return grounded;
    }
    public void Unground() {
        grounded = false;
    }
}
