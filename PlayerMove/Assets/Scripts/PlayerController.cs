using System.Collections.ObjectModel;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    public float moveSpeed;
    public float jumpSpeed;
    private float xMove, yMove;
    private UnityEngine.Vector3 dir;
    public float gravity;
    private UnityEngine.Vector3 velocity;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    public bool isGround;
    public int jumpCount = 0;
    private void Start() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);
        if (isGround && velocity.y < 0) {
            velocity.y = -2f;
            jumpCount = 0;
        }

        xMove = Input.GetAxis("Horizontal") * moveSpeed;
        yMove = Input.GetAxis("Vertical") * moveSpeed;

        dir = transform.forward * yMove + transform.right * xMove;
        cc.Move(dir * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumpCount >= 0 && jumpCount < 2) {
            velocity.y = jumpSpeed;
            jumpCount++;
        } else if (jumpCount == 2) {
            jumpCount = -1;
        }

        velocity.y -= gravity *  Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
