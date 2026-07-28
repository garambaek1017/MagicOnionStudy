using Uitility;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private bool _isConnected = false;

    public InputField MessageField;
    public InputField NameField;

    // Start is called before the first frame update
    void Start()
    {
        InitFields();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Initialize ui Fields
    /// </summary>
    private void InitFields()
    {
        if (MessageField == null)
        {
            MessageField = GameObject.Find("IF_Message").GetComponent<InputField>();
        }

        if (NameField == null)
        {
            NameField = GameObject.Find("Input_UserName").GetComponent<InputField>();
        }
    }
    
    /// <summary>
    /// Client -> Server, Connect
    /// </summary>
    public async void Connect()
    {
        if (IsConnected() == true)
            return;

        var serverUrl = "http://localhost:5000";
        
        _isConnected = await HubClient.Instance.ConnectAsync(serverUrl);
        
        MyLogger.Log($"[Connection Result]:{_isConnected}, {serverUrl}");
    }

    /// <summary>
    /// Client -> server, Join Group
    /// </summary>
    public async void Join()
    {
        // if (IsConnected() == false)
        //     return;

        var userName = NameField.text;
        
        await HubClient.Instance.JoinAsync(userName);
    }

    /// <summary>
    /// Client -> server, SendMessage
    /// </summary>
    public async void SendMessage()
    {
        var userName = NameField.text;
        var message = MessageField.text;

        await HubClient.Instance.SendMessageAsync(userName, message);
        
       
    }

    /// <summary>
    /// Client -> server, disconnect
    /// </summary>
    public async void Dispose()
    {
        if (IsConnected() == false)
            return;
        
        await HubClient.Instance.DisposeAsync();
        
        _isConnected = false;
    }

    /// <summary>
    /// Connection Check
    /// </summary>
    /// <returns></returns>
    private bool IsConnected()
    {
        if (_isConnected == true)
        {
            MyLogger.Log("Already Connected to Server");
            return true;
        }
        else
        {
            MyLogger.Log("Not connected to Server");
            return false;
        }
    }
}