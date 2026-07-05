using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    public Volume volume;
    private Bloom bloom;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() 
    {
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out colorAdjustments);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void dyingEffect(float bloomIntensity, float vignetteIntensity, Color colorAdjustmentsValue)
    {
        StartCoroutine(dyingEffectCoroutine(bloomIntensity, vignetteIntensity, colorAdjustmentsValue));
    }

    private IEnumerator dyingEffectCoroutine(float bloomIntensity, float vignetteIntensity, Color colorAdjustmentsValue)
    {
        float duration = 10f; // Duration of the effect in seconds
        float elapsedTime = 0f;
        float initialBloomIntensity = bloom.intensity.value;
        float initialVignetteIntensity = vignette.intensity.value;
        Color initialColorAdjustmentsValue = colorAdjustments.colorFilter.value;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Lerp the bloom intensity
            bloom.intensity.value = Mathf.Lerp(initialBloomIntensity, bloomIntensity, t);

            // Lerp the vignette intensity
            vignette.intensity.value = Mathf.Lerp(initialVignetteIntensity, vignetteIntensity, t);

            // Lerp the color adjustments
            colorAdjustments.colorFilter.value = Color.Lerp(initialColorAdjustmentsValue, colorAdjustmentsValue, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        bloom.intensity.value = bloomIntensity;
        vignette.intensity.value = vignetteIntensity;
        colorAdjustments.colorFilter.value = colorAdjustmentsValue;
    }
    
    public void BloomChange(float bloomIntensity, float duration)
    {
        StartCoroutine(BloomChangeCoroutine(bloomIntensity, duration));
    }

    private IEnumerator BloomChangeCoroutine(float bloomIntensity, float duration)
    {
        float elapsedTime = 0f;
        float initialIntensity = bloom.intensity.value;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            bloom.intensity.value = Mathf.Lerp(initialIntensity, bloomIntensity, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bloom.intensity.value = bloomIntensity; // Ensure final value is set
    }

    public void VignetteChange(float vignetteIntensity, float duration)
    {
        StartCoroutine(VignetteChangeCoroutine(vignetteIntensity, duration));
    }

    private IEnumerator VignetteChangeCoroutine(float vignetteIntensity, float duration)
    {
        float elapsedTime = 0f;
        float initialIntensity = vignette.intensity.value;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            vignette.intensity.value = Mathf.Lerp(initialIntensity, vignetteIntensity, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.intensity.value = vignetteIntensity; // Ensure final value is set
    }

    public void ColorAdjustmentsChange(Color colorAdjustmentsValue, float duration)
    {
        StartCoroutine(ColorAdjustmentsChangeCoroutine(colorAdjustmentsValue, duration));
    }

    private IEnumerator ColorAdjustmentsChangeCoroutine(Color colorAdjustmentsValue, float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = colorAdjustments.colorFilter.value;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            colorAdjustments.colorFilter.value = Color.Lerp(initialColor, colorAdjustmentsValue, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        colorAdjustments.colorFilter.value = colorAdjustmentsValue; // Ensure final value is set
    }
}