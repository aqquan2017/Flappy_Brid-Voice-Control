using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

public class Bird_Control : MonoBehaviour
{
    public static Bird_Control instance;
    // Start is called before the first frame update
    Rigidbody2D rig;
    Animator anim;
    bool startMove = false;

    private AudioSource audioSource;

    [Header("Audio")]
    public AudioClip hit;
    public AudioClip jump;
    public AudioClip score;

    [Range(0,1)]
    public float speed;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!startMove && (Input.GetKeyDown(KeyCode.Space) || 
            VoiceRecognition.instance.playerSpeech == "Start"))
        {
            GameController.instance.init.gameObject.SetActive(false);
            VoiceRecognition.instance.playerSpeech = "";
            rig.simulated = true;
            GameController.instance.startCreatingObj();
            VoiceRecognition.instance.RandomQuestion();
            startMove = true;
            audioSource.PlayOneShot(jump, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.isDead)
        {
            Move();

            if(transform.position.y > 8)
            {      
                Dead();
            }
        }
        else
        {
            //reset game
            if ((Input.GetKeyDown(KeyCode.R) || VoiceRecognition.instance.playerSpeech == "Restart")
                && GameController.instance.isDead)
            {
                GameController.instance.repeatGame();
            }

            if ((Input.GetKeyDown(KeyCode.Q) || VoiceRecognition.instance.playerSpeech == "Quit")
    && GameController.instance.isDead)
            {
                Application.Quit();
            }
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
    VoiceRecognition.instance.playerSpeech == VoiceRecognition.instance.questionText.text && startMove)
        {
            VoiceRecognition.instance.answerResult = true;
            StartCoroutine(MoveToSaferPlace());
            VoiceRecognition.instance.playerSpeech = "";
            //anim.SetTrigger("Flap");
            audioSource.PlayOneShot(jump, 1f);
            //rig.velocity = Vector2.zero;
            //rig.AddForce(speed * Vector2.up);
        }
    }

    IEnumerator MoveToSaferPlace()
    {
        float desPos = 0;
        if (GameController.instance.objPos.Count > 0)
        {
            desPos = GameController.instance.objPos.Dequeue();
        }
        else StopCoroutine(MoveToSaferPlace());
        
        float currentTime = Time.time;
        Vector2 currentPos = transform.position;

        while(transform.position.y != desPos)
        {
            anim.SetTrigger("Flap");
            transform.position = Vector2.Lerp(currentPos , Vector2.up*desPos, Time.time - currentTime);
            yield return new WaitForSeconds(0.06f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!GameController.instance.isDead)
            Dead();
    }

    public void scoreSound()
    {
        audioSource.PlayOneShot(score, 1f);
    }

    public void hitSound()
    {
        audioSource.PlayOneShot(hit, 1f);
    }

    public void Dead()
    {
        StopAllCoroutines();
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
        rig.isKinematic = true;
        rig.velocity = Vector2.down * 5;
        GameController.instance.isDead = true;
        anim.SetTrigger("Die");
        hitSound();
        GameController.instance.isDead = true;
        GameController.instance.End();
    }
}
