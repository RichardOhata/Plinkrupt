using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingShop : MonoBehaviour
{
    private Volume volume;
    private ScreenSpaceLensFlare lensFlare;
    private ShadowsMidtonesHighlights smh;
    public float minScale = 1.5f;
    public float maxScale = 3f;
    public float speed = 1f;
    public int reverseColorTrigger = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = GetComponent<Volume>();

        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out lensFlare);
            volume.profile.TryGet(out smh);

            if (lensFlare != null)
            {
                lensFlare.scale.overrideState = true;
                lensFlare.scale.value = minScale;
            }

            if (smh != null)
            {
                smh.shadows.overrideState = true;
                smh.highlights.overrideState = true;
            }
        }
        else
        {
            Debug.LogWarning("Volume or VolumeProfile missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimateLensFlare();
        AdjustShadowsBasedOnValue();
    }

        void AnimateLensFlare()
    {
        if (lensFlare == null) return;

        float t = Mathf.PingPong(Time.time * speed, 1f);
        lensFlare.scale.value = Mathf.Lerp(minScale, maxScale, t);
    }

        void AdjustShadowsBasedOnValue()
    {
        if (smh == null) return;

        // Unity uses Vector4 for shadows and highlights (R, G, B, A channels)
        if (reverseColorTrigger > 0)
        {
            smh.shadows.value = new Vector4(3.5f, 3.5f, 3.5f, 0f); // Boosted shadows
            smh.highlights.value = new Vector4(1f, 1f, 1f, 0f); // Default

        }
        else if (reverseColorTrigger == 0)
        {
            smh.shadows.value = new Vector4(1f, 1f, 1f, 0f); // Default
            smh.highlights.value = new Vector4(1f, 1f, 1f, 0f); // Default
        }
        else
        {
            smh.shadows.value = new Vector4(0f, 0f, 0f, 0f); // Dimmed shadows
            smh.highlights.value = new Vector4(0.13f, 0.13f, 0.13f, 0f); // 
        }
    }
}
