using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PianoSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Color originalKeyColor;
    AudioSource audioSource;

    [SerializeField]
    private string key;
    
    private void Start() {
        originalKeyColor = gameObject.GetComponent<Image>().color;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (key.Length>0)
        {
            if (Input.GetKeyDown(key))
            {
                AudioPlay();
            } 
            if (Input.GetKeyUp(key))
            {
                Invoke("AudioStop",0.7f);
            }             
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("터치 시작");
        AudioPlay();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("터치 종료");
        Invoke("AudioStop",0.7f);
    }

    void AudioPlay(){
        gameObject.GetComponent<Image>().color = Color.green;
        CancelInvoke("AudioStop");
        audioSource.Play();
    }

    void AudioStop(){
        gameObject.GetComponent<Image>().color = originalKeyColor;
        audioSource.Stop();
    }
}
