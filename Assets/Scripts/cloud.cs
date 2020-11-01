using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    public float speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed* Time.deltaTime);
        if (transform.position.x < -15f)
        {
            transform.position = new Vector3(15f, Random.Range(0,3), 0);
        }
    }
}
