using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject brokenShield;
    public PlayerStats myParentStates;
    public string enemyBulletTag;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == enemyBulletTag){
            print(brokenShield.gameObject.name);
            Instantiate(brokenShield, this.gameObject.transform.position, Quaternion.identity);
            myParentStates.hasShield = false;
            //Destroy(this.gameObject);
        }
    }
}
