using UnityEngine;

public class DiscordMain : MonoBehaviour
{
    public string newDetails = "Testing the prototype version";
    public string newState = "Current aim points: " + GameManager.points;

    public string newLargeImage = "aim_logo";
    public string newLargeText = "Aimazing - Aim Trainer";

    void Start()
    {
        DiscordController discord = GameObject.Find("DiscordController").GetComponent<DiscordController>();

        discord.details = newDetails;
        discord.state = newState;

        discord.largeImage = newLargeImage;
        discord.largeText = newLargeText;
    }
}