using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickPlayGame()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            Invoke(nameof(StartGame), 0.3f);
        }
        else
            StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
