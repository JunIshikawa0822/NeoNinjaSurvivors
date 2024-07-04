using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFXUpdater : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    [Tooltip("敵がダメージを受け取ると、この色になります。")]
    public Color tint = Color.white;
    protected float alpha = 0f;
    protected float increment = 0.05f;
    protected MaterialPropertyBlock materialPropertyBlock;
    private  static readonly int OverlayAlphaID = Shader.PropertyToID("_OverlayAlpha");
    private static readonly int ColorID = Shader.PropertyToID("_Color");
    protected virtual void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setOverlayAlpha(float nalpha)
    {
        mySpriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat(OverlayAlphaID, nalpha);
        materialPropertyBlock.SetColor(ColorID, tint);
        mySpriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    private IEnumerator SingleFlashRoutine()
    {
        while(alpha>=0f)
        {
            alpha -= increment*2f;
            setOverlayAlpha(alpha);
            yield return new WaitForSeconds(increment);
        }
    }

    virtual public void InitializeFlash()
    {
        alpha = 1f;
        StartCoroutine(SingleFlashRoutine());
    }
}