using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Player_DamagedFXUpdater : DamageFXUpdater
{
    public Image damagedOverlay;
    public Color secondaryTint = Color.red;
    private Color nColor = Color.white;
    private float additionalAlpha = 0f;
    private PostProcessVolume m_Volume;
    private Vignette m_Vignette;
    protected override void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        nColor.a = additionalAlpha;
        secondaryTint.a = additionalAlpha;
        damagedOverlay.color = secondaryTint;
        // Create an instance of a vignette
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(0f);
        m_Vignette.color.Override(Color.red);
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }
    public override void InitializeFlash()
    {
        base.InitializeFlash(); // Call the base class method to flash the original renderer
        StartCoroutine(AdditionalFlashRoutine());
    }

    private IEnumerator AdditionalFlashRoutine()
    {
        additionalAlpha = 0.5f;
        m_Vignette.intensity.Override(0.2f);
        MaterialPropertyBlock additionalMaterialPropertyBlock = new MaterialPropertyBlock();

        while (additionalAlpha >= 0f)
        {
            additionalAlpha -= increment * 2f;
            secondaryTint.a = additionalAlpha;
            damagedOverlay.color = secondaryTint;
            m_Vignette.intensity.value = additionalAlpha * 0.8f;
            yield return new WaitForSeconds(increment);
        }
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}