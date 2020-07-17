using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObj : MonoBehaviour
{
    public float speed;
    Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(-speed ,0);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isDead)
            rig.velocity = Vector2.zero;
    }
}
