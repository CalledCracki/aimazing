using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int maxTargets = 6;
    public static int points = 0;
    public int totalShotsFired = 0;

    public GameObject targetPrefab;
    public GameObject mainTargetPrefab;
    public GameObject playerRef;
    public Text text;

    private Vector3 mainPos = new Vector3(0.23f, 0.77f, 8.29f);
    private Vector3 mainPosScreen;
    private Vector2 overshoot;
    private Vector3 targetPos;
    private Vector3 targetPosScreen;
    private GameObject targetRef;
    private GameObject mainRef;
    private float prevAdjus = 0f;
    public static bool started = false;
    public static bool gameRunning;

    public bool targetMode = false;
    private bool targetModeOnce = false;

    public void Awake()
    {
        gameRunning = true;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public void Update()
    {
        // Shoot Target
        if (Input.GetMouseButtonDown(0) && started)
        {
            totalShotsFired++;
            FindObjectOfType<AudioManager>().Play("Gun");
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            UpdateAccuracy();
            if (Physics.Raycast(ray, out raycastHit, 100f, LayerMask.GetMask("Target")))
            {
                if (raycastHit.transform != null)
                {
                    ClickedGameObject(raycastHit.transform.gameObject);
                }

                return;
            }
            else if (targetMode)
            {
                targetPosScreen = Camera.main.WorldToScreenPoint(targetRef.transform.position);
                mainPosScreen = Camera.main.WorldToScreenPoint(mainPos);

                // overshoot based on crosshair pos
                overshoot = new Vector2((Screen.width / 2) - targetPosScreen.x, (Screen.height / 2) - targetPosScreen.y);

                GenerateMainTarget();
                Destroy(targetRef);
            }
            else if (targetModeOnce)
            {
                mainPosScreen = Camera.main.WorldToScreenPoint(targetPos);
                targetPosScreen = Camera.main.WorldToScreenPoint(mainPos);

                // overshoot based on crosshair pos
                overshoot = new Vector2((Screen.width / 2) - targetPosScreen.x, (Screen.height / 2) - targetPosScreen.y);
                GenerateTarget();
                Destroy(mainRef);

            }


        }
    }

    private void ClickedGameObject(GameObject go)
    {

        FindObjectOfType<AudioManager>().Play("Pop");
        points++;

        if (go.tag == "MainTarget")
        {
            GenerateTarget();
        }
        else
        {
            GenerateMainTarget();

        }

        Destroy(go);
    }

    public void GenerateTarget()
    {
        // Generate some random spot infront of the player, generate after a target is destroyed
        float x, y, z;
        float px = playerRef.transform.position.x;
        float py = playerRef.transform.position.y;
        float pz = playerRef.transform.position.z;

        x = Random.Range(-7f, 7f) + px;
        y = Random.Range(-.5f, 4f) + py;
        z = Random.Range(2f, 8f);

        targetRef = Instantiate(targetPrefab, new Vector3(x, y, z), Quaternion.identity);
        overshoot = Vector2.zero;
        targetPos = targetRef.transform.position;
        targetMode = true;
        targetModeOnce = true;
    }

    public void GenerateMainTarget()
    {
        mainRef = Instantiate(mainTargetPrefab, mainPos, Quaternion.identity);
        targetMode = false;
    }

    private void OnApplicationQuit()
    {
        gameRunning = false;
    }

    private void UpdateAccuracy()
    {
        // Calculate accuracy as a percentage
        float accuracyPercentage = 0f;
        if (totalShotsFired > 0)
        {
            accuracyPercentage = (float)points / totalShotsFired * 100f;
        }

        // Display or use the accuracy percentage as needed
        text.text = "Accuracy: " + (accuracyPercentage.ToString("F2") + "%");
    }

}