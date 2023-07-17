using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{
    public GameObject dialogPanel;
    public Text dialogText;
    public string[] dialog;
    private int index;
    public GameObject indicator;
    public GameObject nextButton;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject textt;
    // Update is called once per frame
    void Update()
    {
        if(playerIsClose)
        {
            indicator.SetActive(true);
        }
        else
        indicator.SetActive(false);
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose) 
        {
            textt.SetActive(true);
            if (dialogPanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            { 
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            
            }
        }

        if(dialogText.text == dialog[index])
        {
            nextButton.SetActive(true);
        }
    }
    
    public void zeroText()
    {
        dialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char c in dialog[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        nextButton.SetActive(false) ;

        if (index < dialog.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        { zeroText(); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { playerIsClose = false;
            zeroText();
        }
    }

}
