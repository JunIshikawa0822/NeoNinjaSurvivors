using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFXUpdater : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    [Tooltip("敵がダメージを受け取ると、この色になります。")]
    public Color tint = Color.white;
    protected float increment = 0.05f;
    protected MaterialPropertyBlock materialPropertyBlock;
    private  static readonly int OverlayAlphaID = Shader.PropertyToID("_OverlayAlpha");
    private static readonly int ColorID = Shader.PropertyToID("_Color");
    private bool singleFlashDisabled = false;
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
        float alpha = 1f;
        if(!singleFlashDisabled)
        {
            while(alpha>=0f)
            {
                alpha -= increment*2f;
                setOverlayAlpha(alpha);
                yield return new WaitForSeconds(increment);
            }
        }
    }

    private IEnumerator DeathFlashRoutine()
    {
        float deathTimer = 0f;
        while(deathTimer<=1f)
        {
            deathTimer += increment*4f;
            setOverlayAlpha(deathTimer);
            yield return new WaitForSeconds(increment);
        }
    }

    virtual public void InitializeFlash()
    {
        StartCoroutine(SingleFlashRoutine());
    }

    public void DeathAnimation()
    {
        singleFlashDisabled = true;
        transform.parent = null;
        StopAllCoroutines();
        // create particle effect
        Destroy(gameObject,1f);
    }
}