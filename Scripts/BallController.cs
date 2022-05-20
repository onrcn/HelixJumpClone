using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Rigidbody rb;
    public float impulseForce = 5.0f;

    private Vector3 startPos; // Start position of the ball
    public int perfectPass = 0; // 0 = no, 1 = yes, 2 = yes and ball is in the goal
    private bool ignoreNextCollision; // ignore the next collision, prevent the double jump
    public bool isSuperSpeedActive; // is super speed active

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision == true)
        {
            return;
        }

        DeathPart deathPart = collision.gameObject.GetComponent<DeathPart>();
        if (deathPart)
            deathPart.HitDeathPart();

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // Add instant force to the ball
        ignoreNextCollision = true;
        Invoke("AllowNextCollision", 0.2f);
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBallPosition()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {

    }

}