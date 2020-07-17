using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    bool hitPoint = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -5)
        {
            Destroy(gameObject);
        }
        if(!hitPoint && transform.position.x < -2 && gameObject.CompareTag("ObjDown"))
        {
            hitPoint = true;
            GameController.instance.upScore();
            Bird_Control.instance.scoreSound();
        }
    }
}
