using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cartii = 0;
    private int ok = 0;
    [SerializeField] private Text cartiiText;
    [SerializeField] private AudioSource cartiiAudioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cartii"))
        {
            cartiiAudioSource.Play();
            Destroy(collision.gameObject);
            cartii++;
             cartiiText.text = "Carti: " + cartii;
            ok = 1;
        }

        if (collision.gameObject.CompareTag("Scroll"))
        {
            if(ok == 1)
            {
                cartiiText.text = "";
                cartii = 0;
                cartiiAudioSource.Play();
                Destroy(collision.gameObject);
                cartii++;
                cartiiText.text = "Pagini invatate: " + cartii;
                ok = 2;
            }
            else
            {
                
                cartiiAudioSource.Play();
                Destroy(collision.gameObject);
                cartii++;
                cartiiText.text = "Pagini invatate: " + cartii;
            }
            
        }
    }
}
