using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BluePlayer : PlayerStats
{
    public GameObject blueDamge;
    public GameObject blueDeath;

   new void Start(){
        base.Start();
        PutShieldOn();
    }
    

    override protected void Damage(){
         if(SceneManager.GetActiveScene().name == "Play"){
            Health -= 10;
            SliderHealth.value = Health;
        }
        if(Health <= 0)
        {
            GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerStats>().Winner(Color.yellow);
            Instantiate(blueDeath, this.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Instantiate(blueDamge, this.gameObject.transform.position, Quaternion.identity);
    }
    override protected void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "bullet2")
        {
            Damage();
        }
    }
    override protected void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "rocket2" )
        {
            Damage();
        }
    }
    override protected void PutShieldOn(){
        if(!hasShield){
            GameObject Shield = Instantiate(Resources.Load("prefap/Shield"), this.gameObject.transform.position, Quaternion.identity) as GameObject;
            hasShield=true;
            Shield.GetComponent<ShieldController>().brokenShield =  Resources.Load("prefap/BlueBulletDestroyed") as GameObject;
            Shield.GetComponent<ShieldController>().enemyBulletTag="bullet2";
            Shield.GetComponent<ShieldController>().myParentStates=base.gameObject.GetComponent<PlayerStats>();
            Shield.GetComponent<SpriteRenderer>().color=Color.cyan;
            Shield.transform.parent = gameObject.transform;
            Shield.GetComponent<Transform>().position += new Vector3 (-1.3f,-0.2f,0.0f);
            Shield.GetComponent<Transform>().eulerAngles = new Vector3(0.0f,180.0f,0.0f);
        }
    }
}
