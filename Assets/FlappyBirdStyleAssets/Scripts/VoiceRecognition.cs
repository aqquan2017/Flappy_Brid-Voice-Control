using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    public static VoiceRecognition instance;

    public TextMeshProUGUI questionText;
    public string playerSpeech;

    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public string[] keywords = new string[] { };
    private PhraseRecognizer recognizer;

    public bool micState;

    public bool answerResult = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (keywords != null)
        {
            if(recognizer == null)
            {
                recognizer = new KeywordRecognizer(keywords, confidence);
            }
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
        micState = true;
    }

    //API
    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (micState)
        {
            playerSpeech = args.text;
            Debug.Log("You said: " + playerSpeech);
        }
        else Debug.Log("MIC OFF");
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }

    

    public void RandomQuestion()
    {
        questionText.text = keywords[Random.Range(3, keywords.Length)];
    }
}
