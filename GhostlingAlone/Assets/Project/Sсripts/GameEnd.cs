using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameEnd : MonoBehaviour
{
    public ParticleSystem particleSystemToDisable;
    public string requiredTool = "Tools";
    public AudioSource escapeAudio;
    public Canvas escapeCanvas;
    public Text escapeText;
    public float delayBeforeLoad = 6f; 
    public bool enableCursorAfterDelay = true; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventorySystem.Instance.CheckForItem(requiredTool))
            {
                DisableParticleSystem();
                PlayEscapeAudio();
                ShowEscapeUI();
                StartCoroutine(LoadNextSceneWithDelay());
            }
        }
    }

    private void DisableParticleSystem()
    {
        if (particleSystemToDisable != null)
        {
            particleSystemToDisable.gameObject.SetActive(false);
        }
    }

    private void PlayEscapeAudio()
    {
        if (escapeAudio != null)
        {
            escapeAudio.Play();
        }
    }

    private void ShowEscapeUI()
    {
        if (escapeText != null)
        {
            escapeText.gameObject.SetActive(true);
            escapeText.text = "You escaped";
        }
    }

    private IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        if (enableCursorAfterDelay)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
