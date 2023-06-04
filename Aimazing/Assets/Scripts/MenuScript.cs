using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Main");
    }
}
