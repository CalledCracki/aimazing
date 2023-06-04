using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void Go()
    {
        GameManager.started = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
