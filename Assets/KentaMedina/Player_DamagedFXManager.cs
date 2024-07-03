using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DamagedFXManager : MonoBehaviour
{
    SpriteRenderer mySpriteRenderer;
    float alpha = 0f;
    float maxAlpha = 0.5f;
    float increment = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SingleFlashRoutine()
    {
        Color nColor = Color.red;
        nColor.a = maxAlpha;
        while(alpha>=0f)
        {
            alpha -= increment*2f;
            nColor.a = alpha;
            mySpriteRenderer.color = nColor;
            yield return new WaitForSeconds(increment);
        }
    }

    public void SetDeath()
    {
        StopAllCoroutines();
        Color nColor = Color.red;
        nColor.a = 0.5f;
        mySpriteRenderer.color = nColor;
    }

    public void CallFlash()
    {
        StartCoroutine(SingleFlashRoutine());
    }
}