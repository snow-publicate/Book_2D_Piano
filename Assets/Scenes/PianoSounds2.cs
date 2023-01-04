using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PianoSounds2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Color originalKeyColor;

    [SerializeField]
    private string key;

    private void Start() {
        originalKeyColor = gameObject.GetComponent<Image>().color;
    }

    private void Update() {
        if (key.Length>0)
        {
            if (Input.GetKeyDown(key))
            {
                gameObject.GetComponent<Image>().color = Color.green;
            } 
            if (Input.GetKeyUp(key))
            {
                gameObject.GetComponent<Image>().color = originalKeyColor;
            }             
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("터치 시작");
        gameObject.GetComponent<Image>().color = Color.green;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Debug.Log("터치 종료");
        gameObject.GetComponent<Image>().color = originalKeyColor;
    }
}
