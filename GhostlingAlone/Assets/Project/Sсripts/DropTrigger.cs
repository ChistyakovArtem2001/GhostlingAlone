using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrigger : MonoBehaviour
{
    public AudioSource jumpScareAudio;
    public GameObject fallingPicture;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            PlayJumpScare();
        }
    }

    private void PlayJumpScare()
    {
        if (jumpScareAudio != null && fallingPicture != null)
        {
            fallingPicture.SetActive(false);

            jumpScareAudio.Play();
            hasPlayed = true;

            StartCoroutine(ActivateFallingPicture());
        }
        else
        {
            Debug.LogWarning("audio not assigned!");
        }
    }

    private IEnumerator ActivateFallingPicture()
    {

        yield return new WaitForSeconds(1.5f);

        fallingPicture.SetActive(true);

        Rigidbody pictureRigidbody = fallingPicture.GetComponent<Rigidbody>();


        if (pictureRigidbody != null)
        {
            pictureRigidbody.isKinematic = false;
        }
    }
}