using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf : MonoBehaviour
{
    public Transform resetPos;
    Transform targetPos;
    public float speed = 4f;
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

    public Transform[] sheep;

    Vector3 pos, velocity;

    bool facingRight = false;

    //public sheepsarray[] sheep;

    private AudioManager2 audiomanager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("fence"))
        {
            animator.SetTrigger("jump");
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
        selectSheep();
        pos = transform.position;

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
                selectSheep();

            }
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
        }
        else
        {
            if (atRest)
            {
                playerNear = false;
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
    void goToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.fixedDeltaTime);
    }
    void goToReset()
    {
        transform.position = Vector3.MoveTowards(transform.position, resetPos.position, speed * Time.fixedDeltaTime);
    }

    void selectSheep()
    {
        int t = Random.Range(0, sheep.Length);
        
        if (sheep[t] == null)
        {
            Debug.LogWarning("sheep not present in scene");
            goToReset();
            selectSheep();
            //return;
        }
        else
        {
            targetPos = sheep[t];
        }
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
