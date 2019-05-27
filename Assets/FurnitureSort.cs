using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriter in spriteRenderers)
        { spriter.sortingOrder = Config.SORT_ORDER_FURNITURE; }
    }
}
