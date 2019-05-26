using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{

    public Text splashText;

    // Use this for initialization
    void Start()
    {
        //CloseSplashScreen();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSplashScreen()
    {
        gameObject.SetActive(true);
    }

    public void CloseSplashScreen()
    {
        gameObject.SetActive(false);
    }

}
