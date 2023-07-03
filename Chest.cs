using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    public AudioSource GetitemSound;

    protected override void OnCollect()
    {
        if(!collected)
        {
            GetitemSound.Play();
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " Coin!", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
        }
    }
}
