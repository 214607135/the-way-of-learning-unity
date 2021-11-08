using System;
using System.Numerics;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public int maxHealth = 5;
    private int currentHealth;
    public int speed = 3;

    public int Health {get {return currentHealth;}}

    
    private bool isInvicible;
    private float  invicibleTime = 2.0f;
    private float  curInvicible;

    private UnityEngine.Vector2 lookDirection = new UnityEngine.Vector2(1, 0);
    private Animator animator;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
        isInvicible = false;
        curInvicible = invicibleTime;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");
        
        UnityEngine.Vector2 move = new UnityEngine.Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed",  move.magnitude);

        UnityEngine.Vector2 position = transform.position;
        position += speed * move * Time.deltaTime;
        // transform.position = position;

        rigidbody2d.MovePosition(position);

        if (isInvicible) {
            curInvicible = curInvicible - Time.deltaTime;
            if (curInvicible < 0) {
                isInvicible = false;
            }
        }
    }

    public void ChangeHealth(int amount) 
    {
        if (amount < 0) {
            if (isInvicible) {
                return;
            }
            isInvicible = true;
            curInvicible = invicibleTime;
        }
        currentHealth = Mathf.Clamp(currentHealth+amount, 0, maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position, UnityEngine.Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
    }
}
