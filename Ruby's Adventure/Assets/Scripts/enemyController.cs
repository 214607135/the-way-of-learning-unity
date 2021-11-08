using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public int speed = 3;
    public bool vertical = false;
    private int direction = 1;
    public float changeTime = 3.0f;
    private float timer;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;

        animator = GetComponent<Animator>();
        PlayerMoveAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;

        timer = timer - Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            PlayerMoveAnimation();
            timer = changeTime;
        }

        if (vertical)
        {
            position.y += speed * Time.deltaTime * direction;
        }
        else
        {
            position.x += speed * Time.deltaTime * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController rubyController = collision.gameObject.GetComponent<RubyController>();

        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }

    private void PlayerMoveAnimation()
    {
        if (vertical) {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", direction);
        } else {
            animator.SetFloat("moveX", direction);
            animator.SetFloat("moveY", 0);
        }
    }
}
