using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip cilckSound;
    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(cilckSound);
    }
}
