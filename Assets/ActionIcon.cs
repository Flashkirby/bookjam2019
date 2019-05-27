using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ActionIcon : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite spriteMain;
    public Sprite spriteAlt;
    public List<Sprite> spritesLong;

    public bool hideIcon;
    public bool altIcon;
    public float sliderShow;
    public float SliderShow { get { return sliderShow; } set { sliderShow = Mathf.Clamp01(value); } }

    public void Awake()
    {
        spriteRenderer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(hideIcon)
        { spriteRenderer.enabled = false; }
        else
        {
            spriteRenderer.enabled = true;
            if (SliderShow == 0f)
            { spriteRenderer.sprite = altIcon ? spriteAlt : spriteMain; }
            else
            {
                int toFrame = (int)(Mathf.Clamp((spritesLong.Count * sliderShow + 0.5f), 0, spritesLong.Count - 1));
                spriteRenderer.sprite = spritesLong[toFrame];
            }
        }
    }
}
