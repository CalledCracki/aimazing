using UnityEngine;

public class BringUpSettings : MonoBehaviour
{
    public GameObject setting;
    public bool issettingactive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (issettingactive == false)
            {
                Pause();
            }

            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        setting.SetActive(true);
        issettingactive = true;
        this.GetComponent<PlayerMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        setting.SetActive(false);
        issettingactive = false;
        this.GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}