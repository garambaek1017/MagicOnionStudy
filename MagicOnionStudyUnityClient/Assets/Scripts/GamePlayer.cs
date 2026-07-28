using System.Collections;
using Uitility;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : MonoBehaviourSingletonTemplate<GamePlayer>
{
    public Text UserName;
    public Text Message;
    private float MoveSpeed = 50.0f;

    void Awake()
    {
        if (UserName == null)
        {
            UserName = GameObject.Find("TXT_UserName").GetComponent<Text>();
        }

        if (Message == null)
        {
            Message = GameObject.Find("TXT_Message").GetComponent<Text>();
        }

        MyLogger.Log("GamePlayer Awake");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Start()
    {
        
    }
    
    public IEnumerator ShowMessage(string message)
    {
        Message.text = message;
        yield return new WaitForSeconds(3);
        Message.text = "";
    }
}