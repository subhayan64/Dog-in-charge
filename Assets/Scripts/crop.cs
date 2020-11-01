using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crop : MonoBehaviour
{
    float currenthealth;
    public float maxhealth = 100f;
    public healthbar healthbar;
    public float range;

    public Transform sheep;

    public gamemaster game;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        healthbar.MaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (currenthealth < 0)
        {
            Debug.Log("Gameover");
            game.gameover();
        }
        
        
        
        float distTowolf = Vector2.Distance(transform.position, sheep.position);
        if ((distTowolf < range))
        {

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
