using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Background : MonoBehaviour
{
    Sprite desertSprite;
    Sprite mountainSprite;
    void Awake()
    {
        mountainSprite= Resources.Load<Sprite>("sprites/Mountain");
        desertSprite = Resources.Load<Sprite>("sprites/Desert");
    }
    void Start()
    {
        if(PlayerPrefs.GetString("BackGround","Mountain")== "Mountain"){
            this.GetComponent<SpriteRenderer>().sprite  = mountainSprite;
        }
        else{
            this.GetComponent<SpriteRenderer>().sprite = desertSprite;
        }
    }
    public void ChooseDesert()
    {
        this.GetComponent<SpriteRenderer>().sprite = desertSprite;
        PlayerPrefs.SetString("BackGround","Desert");
    }
    public void ChooseMountain()
    {
        this.GetComponent<SpriteRenderer>().sprite = mountainSprite;
        PlayerPrefs.SetString("BackGround","Mountain");
    }
}
