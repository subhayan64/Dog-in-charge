using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep : MonoBehaviour
{
    public Transform resetPos;
    public Transform targetPos;
    public float speed=4f;
    bool playerNear;
    bool atTarget;
    bool atRest;
    bool contactPlayer;
    public Transform player;

    public float range = 0f;

    public float time;
    float timeStore;

    Animator animator;
    Rigidbody2D rb;

    Vector3 pos, velocity;

    bool facingRight = false;

    float currenthealth;
    public float maxhealth = 100f;
    public healthbar healthbar;

    public Transform wolf;

    private AudioManager2 audiomanager;

    public gamemaster game;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("fence"))
        {
            animator.SetTrigger("jump");
        }
        else if (collision.CompareTag("wolf"))
        {
            //rb.velocity = Vector3.zero;
            audiomanager.Play("sheep");
            speed = 0f;            
        }

    }
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("wolf"))
        {
            //rb.velocity = Vector3.zero;
            speed = 2f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        transform.position = resetPos.position;
        atTarget = false;
        atRest = true;
        timeStore = time;
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;

        currenthealth = maxhealth;
        healthbar.MaxHealth(maxhealth);

        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }

        
    }

    void Update()
    {
        animator.SetFloat("horizontal", velocity.x);
        move();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkPlayerPresence();
        animator.SetBool("run", !rb.IsSleeping());


        velocity = (transform.position - pos) / Time.fixedDeltaTime;
        pos = transform.position;

        if (currenthealth < 0)
        {
            //Destroy(gameObject);
            Debug.Log("gameover");
            game.gameover();

        }


        if (atRest)
        {
            if (!playerNear)
            {
                if (contactPlayer)
                {
                    goToReset();
                }
                else
                {
                    goToTarget();
                }
                
            }
            else
            {
                goToReset();
            }
        }
        else if (atTarget)
        {
            if (playerNear)
            {
                goToReset();
            }
        }


        if (transform.position == targetPos.position)
        {
            atTarget = true;
            atRest = false;
        }
        else if (transform.position == resetPos.position)
        {

            audiomanager.Play("sheep");
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                 
                time = timeStore;
                //Do Stuff   
                atRest = true;
                atTarget = false;
                contactPlayer = false;

            }
        }

        

    }

    void move()
    {
        if (facingRight == true && velocity.x > 0)
        {
            Flip();
        }
        else if (facingRight == false && velocity.x < 0)
        {
            Flip();
        }
    }
    void checkPlayerPresence()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if ((distToPlayer < range))
        {
            playerNear = true;
            contactPlayer = true;
            audiomanager.Play("dog");
            audiomanager.Play("sheep");
        }
        else
        {
            if (atRest)
            {
                playerNear = false;
            }
        }

        float distTowolf = Vector2.Distance(transform.position, wolf.position);
        if ((distTowolf < range)) {

            currenthealth -= 5 * Time.deltaTime;
            healthbar.SetHealth(currenthealth);
            //Debug.Log(currenthealth);
        }
        else
        {
            currenthealth = maxhealth;
            healthbar.SetHealth(currenthealth);
        }

    }
    void goToTarget()
    {
       transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.fixedDeltaTime);             
    }
    void goToReset()
    {
        transform.position = Vector3.MoveTowards(transform.position, resetPos.position, speed * Time.fixedDeltaTime);
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scalar = transform.localScale;
        scalar.x *= -1;
        transform.localScale = scalar;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
