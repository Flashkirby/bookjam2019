using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Config;

public class FactBook : MonoBehaviour
{
    //19 line limit per page
    public Text pageOne;
    public Text pageTwo;
    public Text testPage;

    public List<string> clueList;

    public bool activeSelf { get { return gameObject.activeSelf; } }


    // Start is called before the first frame update
    void Start()
    {
        pageOne.text = "";
        pageTwo.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        printCluesToFactBook();
    }

    public void OpenFactBook()
    {

        if (Game.S.detective.identifyingSoundPlaying)
        {
            Game.S.detective.identifyingSoundPlaying = false;
            Game.S.globalAudioPlayer.Stop();
        }

        gameObject.SetActive(true);
        Game.S.detective.inMenu = true;
    }

    public void CloseFactBook()
    {
        gameObject.SetActive(false);
        Game.S.detective.inMenu = false;
    }

    public void ToggleFactBook()
    {
        if (activeSelf)
        {
           CloseFactBook();
        }
        else
        {
           OpenFactBook();
        }
    }

    int pageLines(Text page)
    {
        Canvas.ForceUpdateCanvases();
        return page.cachedTextGenerator.lines.Count;

    }

    public void addClue(string clueString)
    {
        if (clueList.Contains(clueString) || clueString == "USELESS")
        {
            // You already have this clue message?
        } else
        {
            clueList.Add(clueString);
        }
        
    }

    void printCluesToFactBook()
    {
        pageOne.text = "";
        pageTwo.text = "";

        Text[] pages = { pageOne, pageTwo };
        int whichPage = 0;

        string testPageText = "";
        foreach (string clue in clueList)
        {
            string markedClue = "* " + clue;
            string newLineAndMarkedClue = "\n" + markedClue;


            if (testPageText == "")
            {
                testPageText += markedClue;
            }
            else
            {
                testPageText += newLineAndMarkedClue;
            }
            testPage.text = testPageText;


            if (pageLines(testPage) >= 18 ){
                whichPage = 1;
            }

            if (whichPage == 0)
            {
                if(pageOne.text == "")
                {
                    pageOne.text += markedClue;
                } else
                {
                    pageOne.text += newLineAndMarkedClue;
                }
            } else
            {
                if (pageTwo.text == "")
                {
                    pageTwo.text += markedClue;
                }
                else
                {
                    pageTwo.text += newLineAndMarkedClue;
                }
            }
        }
    }
}
