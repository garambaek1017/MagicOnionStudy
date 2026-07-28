using Packets;
using Shared;
using Shared.Util;
using UnityEngine;
using Util;

public partial class HubClient
{
    public void OnForceClose(ErrorCode errorCode)
    {
        Debug.Log($"[OnForceClose] {errorCode}");
    }

    public void OnSendReceiver(string message)
    {
        var pkt = message.ToObject<BroadCastPacket>();
        if (pkt == null)
        {
            Debug.LogWarning($"[OnSendReceiver] Failed to parse packet: {message}");
            return;
        }

        Debug.Log($"[OnSendReceiver] {pkt.Sender} : {pkt.BroadCastMessage}");
        GameManager.Instance.ShowMessage(pkt.Sender, pkt.BroadCastMessage);
    }
}
