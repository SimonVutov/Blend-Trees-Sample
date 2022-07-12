using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    Vector2 speedPercent;

    public bool grounded = true;
    public Vector3 rbVelocity = Vector3.zero;

    void Update()
    {
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, GetComponent<CapsuleCollider>().height / 2f + 0.2f);

        rbVelocity = GetComponent<Rigidbody>().velocity;

        if (grounded) speedPercent = new Vector2(Mathf.Clamp(transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).x, -1f, 1f),
                Mathf.Clamp( transform.InverseTransformDirection(rbVelocity).z, -1f, 1f ) );
        else speedPercent = new Vector2(10f, 10f);
        animator.SetFloat("Xaxis", speedPercent.x, 0.1f, Time.deltaTime);
        animator.SetFloat("Yaxis", speedPercent.y, 0.1f, Time.deltaTime);
    }
}
