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


    // Start is called before the first frame update
    void Start()
    {
        pageOne.text = "";
        pageTwo.text = "";
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("They are wearing a red hat.");
        //clueList.Add("They are either on the 3rd or 4th floor.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
        //clueList.Add("Their name is John Smith.");
    }

    // Update is called once per frame
    void Update()
    {
        CloseFactBook();
        printCluesToFactBook();
    }

    void CloseFactBook()
    {
        if (Input.GetButtonDown("Fire1") || Utils.isAxisActive(Input.GetAxisRaw("Horizontal")))
        {
            gameObject.SetActive(false);
            Game.S.detective.inMenu = false;
        };
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
