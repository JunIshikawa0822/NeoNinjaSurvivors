using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DamageFXUpdater : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    private float alpha = 0f;
    private float increment = 0.05f;
    private MaterialPropertyBlock materialPropertyBlock;
    private static readonly int OverlayAlphaID = Shader.PropertyToID("_OverlayAlpha");
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        setOverlayAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setOverlayAlpha(float nalpha)
    {
        mySpriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat(OverlayAlphaID, nalpha);
        mySpriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    private IEnumerator SingleFlashRoutine()
    {
        print("ienum singleflashroutine has been initialized");
        while(alpha>=0f)
        {
            alpha -= increment*2f;
            setOverlayAlpha(alpha);
            yield return new WaitForSeconds(increment);
        }
    }

    public void InitializeFlash()
    {
        print("initializeflash has been called");
        alpha = 1f;
        StartCoroutine(SingleFlashRoutine());
    }
}
