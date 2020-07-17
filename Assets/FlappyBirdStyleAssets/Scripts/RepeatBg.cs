using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBg : MonoBehaviour
{
    Vector2 startPos;
    float col;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        col = GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x - col)
        {
            transform.position = startPos;
        }
    }
}
