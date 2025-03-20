using UnityEngine;
using UnityEngine.UIElements;

public class InfoExample : MonoBehaviour
{
    [Info("Info")]
    public string Info;

    [Info("Warning", messageType: HelpBoxMessageType.Warning)]
    public string Warning;

    [Info("Error", fontSize: 24, messageType: HelpBoxMessageType.Error)]
    public string[] Error;
}