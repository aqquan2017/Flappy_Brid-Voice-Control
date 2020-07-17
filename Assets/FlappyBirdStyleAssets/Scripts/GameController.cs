using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text fpsText;
    public float deltaTime;

    [Header("Obstacle")]
    public GameObject objDown;
    public GameObject objUp;

    [Header("Range")]
    public float rangeMinimum;
    public float rangeMaximum;

    [Header("UI")]
    public Text score;
    public Text endGame;

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
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: "+ Mathf.Ceil(fps).ToString();
    }

    public void upScore()
    {
        point++;
        score.text = point.ToString();
    }

    public void end()
    {
        endGame.gameObject.SetActive(true);
    }

    public void repeatGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void startCreatingObj()
    {
        if(!isDead)
            InvokeRepeating("creatObstacle", 0, 1.5f);
    }
    void creatObstacle()
    {
        float randomRange = Random.Range(rangeMinimum, rangeMaximum);
        Vector2 posDown = new Vector2(objDown.transform.position.x , randomRange);
        Vector2 posUp = new Vector2(objDown.transform.position.x, randomRange+12.5f);
        Instantiate(objDown , posDown, objDown.transform.rotation);
        Instantiate(objUp , posUp , objUp.transform.rotation);     
    }
}
