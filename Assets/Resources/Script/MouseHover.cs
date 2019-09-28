using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseHover : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    float percent = 20.0f/100.0f;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData){
        this.GetComponent<Transform>().localScale += new Vector3 (this.GetComponent<Transform>().localScale.x*percent,this.GetComponent<Transform>().localScale.y*percent,0.0f);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData){
        this.GetComponent<Transform>().localScale -= new Vector3 (this.GetComponent<Transform>().localScale.x*percent,this.GetComponent<Transform>().localScale.y*percent,0.0f);
    }
}
