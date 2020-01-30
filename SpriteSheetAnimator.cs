using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSheetAnimator : MonoBehaviour
{
    public Sprite[] sprites;

    public bool isAnimating;
    public bool isLooping = false;
    public bool backwards = false;
    //public SpriteRenderer spriteRenderer;
    public Image spriteRenderer;

    public int frameRate = 2;
    public int frameCounter;
    public int currentIndex;

    public void Initialize(Sprite[] sprites , int index)
    {
        this.sprites = sprites;
        spriteRenderer.sprite = sprites[index];
    }

    public void Run()
    {
        _Reset();
        isAnimating = true;
    }

    public void Stop()
    {
        _Reset();
        isAnimating = false;
        gameObject.SetActive( false );
    }

    void _Reset()
    {
        currentIndex = backwards ? (sprites.Length - 1) : 0;
        frameCounter = 0;
        spriteRenderer.sprite = backwards ? sprites[sprites.Length - 1] : sprites[0];
    }

    void Update()
    {
        if (isAnimating)
        {
            frameCounter++;

            if (frameCounter >= frameRate)
            {
                frameCounter = 0;

                currentIndex += backwards ? -1 : +1;

                if (currentIndex >= sprites.Length || currentIndex < 0)
                {
                    _Reset();

                    if (!isLooping)
                    {
                        Stop();
                        return;
                    }
                }

                spriteRenderer.sprite = sprites[currentIndex];
            }
        }
    }
}
