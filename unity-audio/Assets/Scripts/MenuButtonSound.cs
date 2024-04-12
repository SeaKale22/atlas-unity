using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource buttonHover;

    // public AudioSource buttonClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // void OnPointerDown(PointerEventData eventData)
        // {
        //     Debug.Log($"{this.name} was clicked");
        //     buttonClick.Play();
        // }
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse hovered over button");
        buttonHover.Play();
    }
    
}
