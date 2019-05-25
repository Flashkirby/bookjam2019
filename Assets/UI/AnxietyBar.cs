using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour
{

    public Slider slider;
    public Text displayText;


    // Create a property to handle the slider's value
    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
            displayText.text = (slider.value * 100).ToString("0.00") + "%";
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        CurrentValue += 0.0043f;
    }
}
