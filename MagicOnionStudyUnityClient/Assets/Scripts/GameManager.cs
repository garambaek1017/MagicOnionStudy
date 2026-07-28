using System.Collections.Generic;
using Uitility;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingletonTemplate<GameManager>
{
    public Dictionary<string, GameObject> Players = new Dictionary<string, GameObject>();
    
    void Awake()
    {
        MyLogger.Log("GameManager Awake, gamePlayer Set Active false");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowGamePlayer(string userName, float x , string color)
    {
        GameObject canvas = GameObject.Find("Canvas");
        
        if (Players.TryGetValue(userName, out var player) == false)
        {
            // GamePlayer 프리팹 로드
            var gamePlayerPrefab = Resources.Load<GameObject>("GamePlayer");
            // GamePlayer 인스턴스화
            var gamePlayer = Instantiate(gamePlayerPrefab, new Vector3(x, 0, 0), Quaternion.identity);
            
            // GamePlayer를 Canvas의 자식으로 설정
            gamePlayer.transform.SetParent(canvas.transform, false);
            
            Image playerImage = gamePlayer.GetComponentInChildren<Image>();
            if (ColorUtility.TryParseHtmlString(color, out var newColor))
            {
                playerImage.color = newColor;
            }

            Players.Add(userName, gamePlayer);
            
            // Canvas 하위의 TXT_UserName 오브젝트 찾기
            var userNameTransform = gamePlayer.transform.Find("TXT_UserName");

            if (userNameTransform != null)
            {
                var usernameText = userNameTransform.GetComponent<Text>();
                
                if (usernameText != null)
                {
                    usernameText.text = userName;
                }
            }
        }
    }

    public void ShowMessage(string userName, string message)
    {
        if(Players.TryGetValue(userName, out var player) == true)
        {
            var gamePlayer = player.GetComponent<GamePlayer>();
            StartCoroutine(gamePlayer.ShowMessage(message));
        }
    }
}
