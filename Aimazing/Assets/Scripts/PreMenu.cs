using UnityEngine;
using UnityEngine.SceneManagement;

public class PreMenu : MonoBehaviour
{

    private void Start()
    {
        Invoke("ChangeScene", 3f);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Menu2");
    }
}
