using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{

    public long applicationID;

    public string details = "Testing the prototype version";
    public string state = "Current aim points:";

    public string largeImage = "aim_logo";
    public string largeText = "Aimazing - Aim Trainer";

    private long time;

    private static bool instanceExists;
    private Discord.Discord discord;

    private void Awake()
    {
        if(instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if(FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        discord = new Discord.Discord(applicationID, (ulong)CreateFlags.NoRequireDiscord);
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    void Update()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = state + " " +  GameManager.points,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText,
                },
                Timestamps =
                {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch
        {
            Destroy(gameObject);
        }
    }
}
