using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DamageFXUpdater : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    private float alpha = 0f;
    private float increment = 0.05f;
    // This code is not optimal! We should re-implement it after the other parts are finalized.
    // Possible approach: render a white overlay of variable alpha using a spritemask on main object whose shape is updated on each increment to match animation
    // Better approach: use custom white-overlay shader controlled by script.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SingleFlashRoutine()
    {
        Color updColor = Color.white;
        while(alpha>=0f)
        {
            alpha -= increment;
            updColor.a = increment;
            mySpriteRenderer.color = updColor;
            yield return new WaitForSeconds(increment);
        }
    }

    public void InitializeFlash()
    {
        alpha = 1f;
        StartCoroutine(SingleFlashRoutine());
    }
}
