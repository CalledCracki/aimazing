using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void ChangeScene()
    {
        SceneManager.LoadScene("Main");
    }
}
