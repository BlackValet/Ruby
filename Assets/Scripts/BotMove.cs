using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public float botMoveSpeedV = 2f;
    public float timeMoveBot = 3;
    public bool vertical;
    int direction = 1;
    private float timer;
    Rigidbody2D rigidbody2d;
    Animator animator;

    bool broken = true;

    public ParticleSystem smokeEffect;

    private Vector2 position;
    // Start is called before the first frame update

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        position = rigidbody2d.position;
        timer = timeMoveBot;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if(timer<0)
        {
            direction = -direction;
            timer = timeMoveBot;
        }

        Vector2 position = rigidbody2d.position;

         if (vertical)
            {
                position.y = position.y + botMoveSpeedV * Time.deltaTime*direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            }                       
       
        else 
        {
                position.x = position.x + botMoveSpeedV * Time.deltaTime*direction;
                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);
            
        }

        rigidbody2d.MovePosition(position);
                      
    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    
    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
         
    }
    
    
}
