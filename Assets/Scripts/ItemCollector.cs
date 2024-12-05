using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    int coins = 0;

    [SerializeField] Text coinsText;

    [SerializeField] AudioSource collectionSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            coinsText.text = "Computer: " + coins; // Sets text to coin collection numbers
            Debug.Log("Computer: " + coins);
            collectionSound.Play();
        }
        if (other.gameObject.CompareTag("finalDetect"))
        {
            coins++;
            Debug.Log("Work!");
        }

    }
}
