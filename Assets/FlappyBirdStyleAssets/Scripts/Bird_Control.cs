using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Bird_Control : MonoBehaviour
{

    public static Bird_Control instance;
    // Start is called before the first frame update
    Rigidbody2D rig;
    Animator anim;
    bool startMove = false;

    [HideInInspector] public AudioSource audio;

    [Header("Audio")]
    public AudioClip hit;
    public AudioClip jump;
    public AudioClip score;

    public float speed;

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !startMove)
        {
            rig.simulated = true;
            GameController.instance.startCreatingObj();
            startMove = true;
            audio.PlayOneShot(jump, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.isDead)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    anim.SetTrigger("Flap");
                    rig.velocity = Vector2.zero;
                    rig.AddForce(speed * Vector2.up);
                    audio.PlayOneShot(jump, 1f);
                    StartCoroutine(touchReady());
                   
              
                
                
                

            }

            if(transform.position.y > 6.5)
            {
                GameController.instance.isDead = true;
                GameController.instance.end();
                đead();
            }
        }
        else
        {
            if (Input.touchCount == 1)
            {
                GameController.instance.repeatGame();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.instance.isDead = true;
        đead();
    }

    public void scoreSound()
    {
        audio.PlayOneShot(score, 1f);
    }

    public void hitSound()
    {
        audio.PlayOneShot(hit, 1f);
    }

    void đead()
    {
        anim.SetTrigger("Die");
        GameController.instance.end();
        hitSound();
    }

    IEnumerator touchReady()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
