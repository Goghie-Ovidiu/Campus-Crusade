using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class NPC2 : MonoBehaviour
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
    private string collect;
    private bool ok;
    public int notaExamen;
    // Update is called once per frame
    void Update()
    {
        if (playerIsClose)
        {
            indicator.SetActive(true);
        }
        else
            indicator.SetActive(false);
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            textt.SetActive(true); //zeroText();
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

        if (dialogText.text == dialog[index])
        {
            nextButton.SetActive(true);
            ok = true;
        }
        else
            ok=false;

        if(ok==true)
        {
            if (index == 0)
            {
                notaExamen = nota();
                if(notaExamen>=5)
                {
                    collect = textCollect();
                    dialogText.text = "";
                    dialogText.text = collect+". Felicitari pentru nota obtinuta!";
                }
                else
                {
                    collect = textCollect();
                    dialogText.text = "";
                    dialogText.text = collect+". Ne vedem in restanta.";
                }
                
            }
        }
        
    }

    public int nota()
    {

        string texts = string.Empty;
        texts = textt.GetComponent<Text>().text.ToString();
        string[] s = texts.Split(' ');
        int n = s.Length;
        int nota =0;
        for (int i = 1; i <= n; i++)
            if(i==n)
                nota = int.Parse(s[i-1]);
        return nota;
    }

    public string textCollect()
    {
        string texts = string.Empty;
        texts = textt.GetComponent<Text>().text.ToString();
        return texts;
    }
    public void zeroText()
    {
        dialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char c in dialog[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        nextButton.SetActive(false);

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
        if (other.CompareTag("Player"))
            playerIsClose = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

}
