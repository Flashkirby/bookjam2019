﻿using System.Collections;
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

    void Awake()
    {

    }

    void Start()
    {
        Frame = 1;
    }

    void Update()
    {
        if(isWalking)
        {
            animTime += Time.deltaTime;
            if (animTime > animDelay)
            {
                animTime -= animDelay;
                nextWalkFrame();
            }
        }
        else
        {
            animTime = 0;
            Frame = 0;
        }
    }

    void FixedUpdate()
    {

    }

    public void nextWalkFrame()
    {
        switch(Frame)
        {
            case 1:
                Frame = 2;
                break;
            case 2:
                Frame = 1;
                break;
            default:
                Frame = 0;
                break;
        }
    }
}
