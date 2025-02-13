using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    private AudioSource audioSource;

    [SerializeField] private GameObject pauseMenuUI;
    private bool isPause = false;

    [SerializeField] private GameObject exitUI;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(clickSound);
        exitUI.SetActive(false);
        SceneManager.LoadScene(0);
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

    public void RestartGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void PlayClickPause()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            Invoke(nameof(PauseGame), 0.3f);
        }
        else
            PauseGame();
    }

    private void PauseGame()
    {
        if(!isPause)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f; // หยุดเวลาในเกม
            isPause = true;
        }
    }

    public void PlayClickResume()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            Invoke(nameof(ResumeGame), 0.3f);
        }
        else
            ResumeGame();
    }

    public void ResumeGame()
    {
        audioSource.PlayOneShot(clickSound);

        if (isPause)
        {
            Debug.Log("Resuming game...");
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPause = false;
        }
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(clickSound);
        exitUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void NotExitGame()
    {
        audioSource.PlayOneShot(clickSound);
        exitUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
