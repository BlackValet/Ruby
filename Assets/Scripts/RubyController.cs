using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
   public int health { get { return currentHealth; } }
    public int maxHealth = 5; 
    int currentHealth;
    public float moveSpeed = 3f;

    public float timeInvincible = 2;
    bool isInvincible;
    float invincibleTimer;

    public GameObject projectilePrefab;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;
        position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
        position.y = position.y + moveSpeed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        if (isInvincible)
            (invincibleTimer) -= Time.deltaTime;
        if (invincibleTimer < 0)
            isInvincible = false;
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }    

    }

    public void ChangeHealth(int amout)
    {
        if (amout<0)
        { if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amout, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        if (currentHealth <= 0)
            Debug.Log(currentHealth + " " +
                " ÏÈÇÄÀ ÒÂÎÅÌÓ ÐÛÆÅÌÓ ÅÁÀËÓ");
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");

    }
}
