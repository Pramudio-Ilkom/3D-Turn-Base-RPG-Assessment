using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    public Volume volume;
    public Bloom bloom;
    public Vignette vignette;
    public ColorAdjustments colorAdjustments;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() 
    {
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out colorAdjustments);
    }
    void Start()
    {
        // test
                // TemporaryEffect(2f, 5f,"Bloom");
                // TemporaryEffect(2f, 5f,"Vignette");
                // TemporaryEffect(2f, 5f,"ColorAdjustmentsRed");
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void dyingEffect(float bloomIntensity, 
                     float vignetteIntensity, 
                     Color colorAdjustmentsValue)
    {
        StartCoroutine(dyingEffectCoroutine(bloomIntensity, 
                                            vignetteIntensity, 
                                            colorAdjustmentsValue));
    }

    private IEnumerator dyingEffectCoroutine(float bloomIntensity, 
                                             float vignetteIntensity, 
                                             Color colorAdjustmentsValue)
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
            bloom.intensity.value = Mathf.Lerp(initialBloomIntensity, 
                                               bloomIntensity, 
                                               t);

            // Lerp the vignette intensity
            vignette.intensity.value = Mathf.Lerp(initialVignetteIntensity, 
                                                  vignetteIntensity, 
                                                  t);

            // Lerp the color adjustments
            colorAdjustments.colorFilter.value = Color.Lerp(initialColorAdjustmentsValue, 
                                                            colorAdjustmentsValue, 
                                                            t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        bloom.intensity.value = bloomIntensity;
        vignette.intensity.value = vignetteIntensity;
        colorAdjustments.colorFilter.value = colorAdjustmentsValue;
    }
    
    public void BloomChange(float bloomIntensity, 
                            float duration)
    {
        StartCoroutine(BloomChangeCoroutine(bloomIntensity, 
                                            duration));
    }

    private IEnumerator BloomChangeCoroutine(float bloomIntensity, 
                                             float duration)
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

    public void VignetteChange(float vignetteIntensity, 
                               float duration)
    {
        StartCoroutine(VignetteChangeCoroutine(vignetteIntensity, 
                                               duration));
    }

    private IEnumerator VignetteChangeCoroutine(float vignetteIntensity, 
                                                float duration)
    {
        float elapsedTime = 0f;
        float initialIntensity = vignette.intensity.value;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            vignette.intensity.value = Mathf.Lerp(initialIntensity, 
                                                  vignetteIntensity, 
                                                  t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.intensity.value = vignetteIntensity; // Ensure final value is set
    }

    public void ColorAdjustmentsChange(Color colorAdjustmentsValue, 
                                       float duration)
    {
        StartCoroutine(ColorAdjustmentsChangeCoroutine(colorAdjustmentsValue, 
                                                       duration));
    }

    private IEnumerator ColorAdjustmentsChangeCoroutine(Color colorAdjustmentsValue, 
                                                        float duration)
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

    public void TemporaryEffect(float Intensity, 
                                float duration, 
                                string effectType, 
                                float waitFrames=0f)
    {
        StartCoroutine(TemporaryEffectCoroutine(Intensity, 
                                                duration, 
                                                effectType, 
                                                waitFrames));
    }

    private IEnumerator TemporaryEffectCoroutine(float Intensity, 
                                                 float duration, 
                                                 string effectType, 
                                                 float waitFrames=0f)
    {
        float elapsedTime = 0f;
        float halfDuration = duration / 2f;
        float ReverseIntensity = 1f - Intensity;

        // Apply the effect based on the effectType
        switch (effectType)
            {
                // Bloom Setting //
                case "Bloom":
                    float initialBloomIntensity = bloom.intensity.value;
                    while (elapsedTime < halfDuration)
                    {
                        float t = elapsedTime / halfDuration;
                        bloom.intensity.value = Mathf.Lerp(initialBloomIntensity, 
                                                           Intensity, 
                                                           t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    bloom.intensity.value = Intensity; // Ensure final value is set
                    while (elapsedTime < duration)
                    {
                        float t = (elapsedTime - halfDuration) / halfDuration;
                        bloom.intensity.value = Mathf.Lerp(Intensity, 
                                                           initialBloomIntensity, 
                                                           t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    bloom.intensity.value = initialBloomIntensity; // Ensure final value is set
                    break;
                // Vignette Setting //
                case "Vignette":
                    float initialVignetteIntensity = vignette.intensity.value;
                    while (elapsedTime < halfDuration)
                    {
                        float t = elapsedTime / halfDuration;
                        vignette.intensity.value = Mathf.Lerp(initialVignetteIntensity, 
                                                              Intensity, 
                                                              t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    vignette.intensity.value = Intensity; // Ensure final value is set
                    while (elapsedTime < duration)
                    {
                        float t = (elapsedTime - halfDuration) / halfDuration;
                        vignette.intensity.value = Mathf.Lerp(Intensity, 
                                                              initialVignetteIntensity, 
                                                              t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    vignette.intensity.value = initialVignetteIntensity; // Ensure final value is set
                    break;
                // Color Adjustments Setting //
                case "ColorAdjustmentsIntensity":
                    Color initialColorIntensity = colorAdjustments.colorFilter.value;
                    Color newColorIntensity = new Color(1f, 1f, 1f,Intensity);
                    while (elapsedTime < halfDuration)
                    {
                        float t = elapsedTime / halfDuration;
                        elapsedTime += Time.deltaTime;
                        colorAdjustments.colorFilter.value = Color.Lerp(initialColorIntensity, 
                                                                        newColorIntensity, 
                                                                        t);
                        yield return null;
                    }
                    colorAdjustments.colorFilter.value = newColorIntensity; // Ensure final value is set
                    while (elapsedTime < duration)
                    {
                        float t = (elapsedTime - halfDuration) / halfDuration;
                        elapsedTime += Time.deltaTime;
                        colorAdjustments.colorFilter.value = Color.Lerp(newColorIntensity, 
                                                                        initialColorIntensity, 
                                                                        t);
                        yield return null;
                    }
                    colorAdjustments.colorFilter.value = initialColorIntensity; // Ensure final value is set
                    break;
                // Color Adjustments Red Setting //
                case "ColorAdjustmentsRed":
                    Color initialColorRed = colorAdjustments.colorFilter.value;
                    Color newColorRed = new Color(1f, 
                                                  ReverseIntensity, 
                                                  ReverseIntensity, 
                                                  0f);
                    while (elapsedTime < halfDuration)
                    {
                        float t = elapsedTime / halfDuration;
                        colorAdjustments.colorFilter.value = Color.Lerp(initialColorRed, 
                                                                        newColorRed, 
                                                                        t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    colorAdjustments.colorFilter.value = newColorRed; // Ensure final value is set
                    while (elapsedTime < duration)
                    {
                        float t = (elapsedTime - halfDuration) / halfDuration;
                        colorAdjustments.colorFilter.value = Color.Lerp(newColorRed, 
                                                                        initialColorRed, 
                                                                        t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    colorAdjustments.colorFilter.value = initialColorRed; // Ensure final value is set
                    break;
                // Color Adjustments Green Setting //
                case "ColorAdjustmentsGreen":
                    Color initialColorGreen = colorAdjustments.colorFilter.value;
                    Color newColorGreen = new Color(ReverseIntensity, 
                                                    1f, 
                                                    ReverseIntensity, 
                                                    0f);
                    while (elapsedTime < halfDuration)
                    {
                        float t = elapsedTime / halfDuration;
                        colorAdjustments.colorFilter.value = Color.Lerp(initialColorGreen, 
                                                                        newColorGreen, 
                                                                        t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    colorAdjustments.colorFilter.value = newColorGreen; // Ensure final value is set
                    while (elapsedTime < duration)
                    {
                        float t = (elapsedTime - halfDuration) / halfDuration;
                        colorAdjustments.colorFilter.value = Color.Lerp(newColorGreen, 
                                                                        initialColorGreen, 
                                                                        t);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    colorAdjustments.colorFilter.value = initialColorGreen; // Ensure final value is set
                    break;
                default:
                    Debug.LogWarning("Unknown effect type: " + effectType);
                    break;
            }
            yield return new WaitForSeconds(waitFrames); // Wait for the specified number of frames
    }
}