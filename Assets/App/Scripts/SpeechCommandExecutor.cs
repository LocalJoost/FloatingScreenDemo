using HoloToolkitExtensions.Messaging;
using UnityEngine;

public class SpeechCommandExecutor : MonoBehaviour
{
    public void OpenHelpScreen()
    {
        Messenger.Instance.Broadcast(new ShowHelpMessage());
    }
}
