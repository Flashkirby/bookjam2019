using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Feature : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void Start()
        {

        }
        void Update()
        {

        }
        void FixedUpdate()
        {

        }
    }
}
