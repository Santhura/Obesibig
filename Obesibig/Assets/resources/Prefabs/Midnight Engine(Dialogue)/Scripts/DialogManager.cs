using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public GameObject textBox; //the textbox, a simple transparent box to display letters on.

    public Text theText; //the text script that were using to type down the txt. file

    public TextAsset textFile; //the txt. file
    public string[] textLines;

    public int currentLine; //the current line that is being displayed/typed out.
    public int endAtLine; //a public integer that allows the user to decide at which line the dialog window closes

    public bool isActive;

    public bool isTyping = false; //detects whether this script is typing out the text
    private bool cancelTyping = false; //cancels the typing.

    public float typeSpeed; //the speed at which the letter are typed out
    public int letterCount; //counts the amount of letters that have been typed out.

    void Start () {

        if(textFile != null)
        {
            textLines = (textFile.text.Split('\n')); // if you have the textfile, the lines will be split by pressing enter. this way you get multiple number of lines.
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

      if (isActive)
        { EnableTextBox();  }
      else
        { DisableTextBox(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        { return; }

        //theText.text = textLines[currentLine];


        if (Input.GetButtonUp("Fire1") /*&& currentLine <= endAtLine*/) //if you tap or click, the following will be done, depending on conditions.
        {
            if (!isTyping)
            {
                currentLine += 1; //if the text is not typing, increase the number of the current line

                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }

            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }

    }

    private IEnumerator TextScroll (string lineOfText)
    {
        int letter = 0;
        letterCount = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;

        //in here, we type out the letters 1 by 1 until the amount of letters is equal to the amount of text.
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) 
        {
            theText.text += lineOfText[letter];
            letter += 1;
            letterCount += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    } 


    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        StartCoroutine(TextScroll(textLines[currentLine]));
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }
}