using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
