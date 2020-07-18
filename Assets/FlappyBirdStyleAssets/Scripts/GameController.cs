using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Obstacle")]
    public GameObject objDown;
    public GameObject objUp;

    [HideInInspector]
    public Queue<float> objPos;

    [Header("Range")]
    public float rangeMinimum;
    public float rangeMaximum;

    [Header("UI")]
    public Text score;
    public Text init;
    public List<Text> endGame;

    int point =0;

    public bool isDead = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 60;
        Cursor.visible = false;
    }

    private void Start()
    {
        objPos = new Queue<float>();
    }

    public void upScore()
    {
        point++;
        score.text = point.ToString();
    }

    public void End()
    {
        for(int i=0; i<endGame.Count; i++)
            endGame[i].gameObject.SetActive(true);
    }

    public void repeatGame()
    {
        VoiceRecognition.instance.keywords = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void startCreatingObj()
    {
        if(!isDead)
            InvokeRepeating("creatObstacle", 0, 4f);
    }
    void creatObstacle()
    {
        if (!isDead)
        {
            float randomRange = Random.Range(rangeMinimum, rangeMaximum);
            Vector2 posDown = new Vector2(objDown.transform.position.x , randomRange);
            Vector2 posUp = new Vector2(objDown.transform.position.x, randomRange+8f);
            float middle = (posDown.y + posUp.y) / 2;
            objPos.Enqueue(middle);
        //    Debug.Log("ADD : " + middle + " NUM: " +objPos.Count);
            Instantiate(objDown , posDown, objDown.transform.rotation);
            Instantiate(objUp , posUp , objUp.transform.rotation);     
        }
    }
}
