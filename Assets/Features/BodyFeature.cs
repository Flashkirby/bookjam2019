using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFeature : Feature
{
    public List<Sprite> sprites;
    public float animTime;
    public float animDelay;
    public int headPixelYOffset;

    private float HeadOffset { get { return headPixelYOffset * Config.PIXEL; } }
    private int _frame;
    public int Frame
    {
        get { return _frame; }
        set
        {
            spriteRenderer.sprite = sprites[_frame];
            _frame = value;
        }
    }
    
    public bool isWalking = false;

    public new void Awake()
    {
        base.Awake();
    }

    public new void Start()
    {
        //base.Start();
    }

    public new void Update()
    {
        base.Update();

        if (isWalking)
        {
            animTime -= Time.deltaTime;
            if (animTime <= 0)
            {
                nextWalkFrame();
                animTime += animDelay;
            }
        }
        else
        {
            animTime = 0;
            Frame = 0;
        }
    }

    public new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void nextWalkFrame()
    {
        switch(Frame)
        {
            case 1:
                Frame = 2;
                break;
            default:
                Frame = 1;
                break;
        }
    }
}
