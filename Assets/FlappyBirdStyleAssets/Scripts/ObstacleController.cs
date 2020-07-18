using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    bool hitPoint = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -5)
        {
            Destroy(gameObject);
        }
        if(!hitPoint && transform.position.x < -0.3f && gameObject.CompareTag("ObjDown"))
        {
            //ge╕t score
            if (VoiceRecognition.instance.answerResult)
            {
                hitPoint = true;
                GameController.instance.upScore();
                Bird_Control.instance.scoreSound();
                VoiceRecognition.instance.RandomQuestion();
            }
            else
            {
                Bird_Control.instance.Dead();
                hitPoint = true;
            }
        }
    }
}
