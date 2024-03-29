﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public abstract class PlayerStats : MonoBehaviour
{
    public Slider SliderHealth,SliderRewind;
    public Text rewindCounter,rocketCounterText;
    public GameObject Death;
    protected bool isRewinding;
    public bool hasShield;
    protected int Health,Rewind,FrameCounter,RewindSeconds,RewindSecondsLimit,fps,rocketCounter;
    public GameObject Win,Win2;
    struct oldData
    {
        public int oldHealth;
        public Vector3 OldPosition;
        public Quaternion OldRotation;
    }
    List<oldData> oldDataList = new List<oldData>();
    List<oldData> SpareData = new List<oldData>();
    protected abstract void Damage();
    protected abstract void OnCollisionEnter2D(Collision2D collision);
    protected abstract void OnTriggerEnter2D(Collider2D collision);
    protected abstract void PutShieldOn();
    protected void Start(){
        hasShield=true;
        isRewinding = false;
        rocketCounter = 0;
        fps = 60;
        RewindSecondsLimit =3;
        Health = 100;
        Rewind = 0;
        RewindSeconds = 3;
        FrameCounter = 1;
        if(SceneManager.GetActiveScene().name == "Play"){
            rocketCounterText.text = rocketCounter.ToString();
            rewindCounter.text = Rewind.ToString()+"/3";
        }
    }

    void Update()
    {
        Application.targetFrameRate = fps;
        if(!isRewinding)
            Delay();

    }
    void Delay()
    {
        if (FrameCounter >= RewindSecondsLimit)
        {
            FrameCounter = 0;
            FillData();
        }
        FrameCounter++;
    }
    void FillData()
    {
        oldData temp = new oldData();
        temp.OldPosition = transform.position;
        temp.OldRotation = transform.rotation;
        temp.oldHealth = this.Health;
        if (oldDataList.Count >= 60)
        {
           
            SpareData = new List <oldData>(oldDataList);
            oldDataList.Clear();
        }
        oldDataList.Add(temp);
        
    }
    
    public void IncreaseRewindValue()
    {
        if(SceneManager.GetActiveScene().name == "Play"){
            if(Rewind <3){
                Rewind++;
            }
            SliderRewind.value = Rewind;
            rewindCounter.text = Rewind.ToString()+"/3";
        }
    }
    
    public void RewindPlayer()
    {
        isRewinding = true;
        StartCoroutine(rewindCourtine());
    }
    IEnumerator rewindCourtine()
    {
        int counter = 0;
        int SpareCounter = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        while (counter < 60)
        {
            if (oldDataList.Count - counter - 1 >= 0)
            {
                this.Health = oldDataList[oldDataList.Count-counter-1].oldHealth;
                this.transform.position = oldDataList[oldDataList.Count-counter-1].OldPosition;
                this.transform.rotation = oldDataList[oldDataList.Count - counter - 1].OldRotation;
                if(SceneManager.GetActiveScene().name == "Play"){
                    SliderHealth.value = Health;
                }
            }
            else
            {
                this.Health = SpareData[SpareData.Count - SpareCounter - 1].oldHealth;
                this.transform.position = SpareData[SpareData.Count - SpareCounter - 1].OldPosition;
                this.transform.rotation = SpareData[SpareData.Count - SpareCounter - 1].OldRotation;
                if(SceneManager.GetActiveScene().name == "Play"){
                    SliderHealth.value = Health;
                }
                SpareCounter++;
            }
            counter++;
            yield return new WaitForSeconds(0);
        }
        this.gameObject.GetComponent<PlayerController>().SetPlayerActive();
        Rewind = 0;
        if(SceneManager.GetActiveScene().name == "Play"){
            SliderRewind.value = Rewind;
            rewindCounter.text = Rewind.ToString()+"/3";
        }
        isRewinding = false;
    }
    public bool CanRewind()
    { 
        if(Rewind >= 3)
        {
            return true;
        }
        return false;
    }
    public void Winner(Color WinnerColor)
    {
        Win.GetComponent<Text>().color = WinnerColor;
        Win2.GetComponent<Text>().color = WinnerColor;
        Win.SetActive(true);
        Win2.SetActive(true);
    }
    public bool isThereRocket()
    {
        if (rocketCounter > 0)
            return true;
        return false;
    }
    public void DecreaseRockets()
    {
        rocketCounter--;
         if(SceneManager.GetActiveScene().name == "Play"){
            rocketCounterText.text = rocketCounter.ToString();
         }
    }
    public void IncreaseRocket(){
        rocketCounter++;
         if(SceneManager.GetActiveScene().name == "Play"){
            rocketCounterText.text = rocketCounter.ToString();
        }
    }
}
/*
    public void Damage()
    {
        Health -= 10;
        SliderHealth.value = Health;
        if (gameObject.tag == "player1")
        {
            if(Health <= 0)
            {
                GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerStats>().Winner(Color.yellow);
                Instantiate(blueDeath, this.gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            Instantiate(blueDamge, this.gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            if (Health <= 0)
            {
                GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerStats>().Winner(Color.cyan);
                Instantiate(YellowDeath, this.gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            Instantiate(yellowDamge, this.gameObject.transform.position, Quaternion.identity);
        }
    }*/
    /*protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet2" && gameObject.tag == "player1" )
        {
            Damge();
        }
        else if (collision.gameObject.tag == "bullet1" && gameObject.tag == "player2" )
        {
            Damge();
        }

    } */
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rocket2" && gameObject.tag == "player1")
        {
            Damge();
        }
        else if (collision.gameObject.tag == "rocket1" && gameObject.tag == "player2")
        {
            Damge();
        }
    } */