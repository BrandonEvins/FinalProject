using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class btnFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    private RectTransform rectTransform;
    private Vector3 originalScale;

    private void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();

        // Store the original scale for later use
        originalScale = rectTransform.localScale;
    }

    public void HoverSoundAndScaleUp()
    {
        myFx.PlayOneShot(hoverFx);

        // Scale up the image on hover
        rectTransform.localScale = originalScale * 1.1f; // You can adjust the scale factor
    }

    public void ResetScale()
    {
        // Reset the scale to the original size
        rectTransform.localScale = originalScale;
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}