using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_DamagedFXUpdater : DamageFXUpdater
{
    public Image damagedOverlay;
    public Color secondaryTint = Color.red;
    private Color nColor = Color.white;
    private float additionalAlpha = 0f;
    protected override void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        nColor.a = additionalAlpha;
        damagedOverlay.color = nColor;
    }
    public override void InitializeFlash()
    {
        base.InitializeFlash(); // Call the base class method to flash the original renderer
        StartCoroutine(AdditionalFlashRoutine());
    }

    private IEnumerator AdditionalFlashRoutine()
    {
        additionalAlpha = 0.5f;
        MaterialPropertyBlock additionalMaterialPropertyBlock = new MaterialPropertyBlock();

        while (additionalAlpha >= 0f)
        {
            additionalAlpha -= increment * 2f;
            nColor.a = additionalAlpha;
            damagedOverlay.color = nColor;
            yield return new WaitForSeconds(increment);
        }
    }
}