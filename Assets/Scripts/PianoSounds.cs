using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //키보드, 마우스 및 터치입력 등과 관련된 네임스페이스
using UnityEngine.UI; //UI활용을 위한 네임스페이스

public class PianoSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Color originalKeyColor; //건반의 원래 색상 할당용 변수 선언
    AudioSource audioSource; //음원 할당용 오디오소스 변수 선언

    [SerializeField]     //Unity에디터에서 키보드 키를 key 문자열 변수(12줄) 할당하기 위한 변수 선언
    private string key; 
    
    private void Start() { // 앱을 시작할 때 작동
        originalKeyColor = gameObject.GetComponent<Image>().color; //변수에 건반의 원래 색상을 할당
        audioSource = GetComponent<AudioSource>(); //Audio소스 컴퍼넌트를 변수에 할당
    }

    private void Update() { // 앱 동작 중 매 프레임마다 작동
        if (key.Length>0) //키의 길이가 0보다 클 때(즉, 키 값이 있을 때) 작동 
        {
            if (Input.GetKeyDown(key)) //Unity 에디터에서 key에 등록한 키보드 키를 누르면 작동
            {
                AudioPlay(); //음원 재생 함수 실행
            } 
            if (Input.GetKeyUp(key)) //Unity 에디터에서 key에 등록한 키보드 키에서 손가락을 떼면 작동
            {
                Invoke("AudioStop",0.7f); //음원 종료 함수를 0.7초 뒤에 실행 예약
            }             
        }
    }

    public void OnPointerDown(PointerEventData eventData) //마우스나 터치 시작 시 작동
    {
        // Debug.Log("터치 시작"); 
        AudioPlay(); //음원 재생 함수 실행
    }

    public void OnPointerUp(PointerEventData eventData) //마우스나 터치 종료 시 작동
    {
        // Debug.Log("터치 종료");
        Invoke("AudioStop",0.7f); //음원 종료 함수를 0.7초 뒤에 실행 예약
    }

    void AudioPlay(){
        gameObject.GetComponent<Image>().color = Color.green; //건반에 초록색 할당
        CancelInvoke("AudioStop");  //음원 종료 함수의 0.7초 뒤 실행 예약기능을 취소
        audioSource.Play(); //음원 재생
    }

    void AudioStop(){
        gameObject.GetComponent<Image>().color = originalKeyColor; //건반에 원래 색상 할당
        audioSource.Stop();  //음원 종료
    }
}
