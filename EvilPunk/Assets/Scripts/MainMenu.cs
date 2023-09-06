using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlayGame()
    {
        StartCoroutine(PlayAudioAndLoadScene());
    }

    IEnumerator PlayAudioAndLoadScene()
    {
        // Play the audio clip
        audioSource.clip = audioClip;
        audioSource.Play();

        // Wait for 5 seconds
        yield return new WaitForSeconds(7f);

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
