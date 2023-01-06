using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlay : MonoBehaviour
{
    GameObject playingKey;
    AudioSource audioSource;
    float runTime; 
    static int keyCount = 25;
    int crtCounts;

    [Header("떳다떳다 비행기 음노트 25개")]
    public GameObject[] pianoKey = new GameObject[keyCount];
    public float[] playTime =  new float[keyCount];

    public void MusicPlay()
    {
        if (crtCounts == 0 || crtCounts == keyCount)
        {
            runTime = 0f;
            for(int i = 0; i < keyCount; i++)
            {   
                runTime += playTime[i];
                playingKey = GameObject.Find(pianoKey[i].name);
                playingKey.GetComponent<AudioSource>().Stop();
                backToOriginalColor();  
                StartCoroutine(PlayCoroutine(i, playingKey));
            }        
        }
        else
        {
            StopAllCoroutines();
            backToOriginalColor();
            crtCounts = 0;
        }
    }

    IEnumerator PlayCoroutine(int i, GameObject playingKey)
    {
        yield return new WaitForSeconds(runTime);
        crtCounts += 1;
        playingKey.GetComponent<AudioSource>().Play();
        playingKey.GetComponent<Image>().color = Color.green;
        Invoke("backToOriginalColor",0.2f);
    }
    void backToOriginalColor()
    {
        GameObject[] whiteKeys = GameObject.FindGameObjectsWithTag("whiteKey");
        GameObject[] blackKeys = GameObject.FindGameObjectsWithTag("blackKey");
        foreach(GameObject key in whiteKeys)
        {
            key.GetComponent<Image>().color = Color.white;
        }
        foreach(GameObject key in blackKeys)
        {
            key.GetComponent<Image>().color = Color.black;
        }
    }
}
