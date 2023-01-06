using System.Collections; //배열, 배열리스트 등의 구조체 활용을 위한 네임스페이스
using System.Collections.Generic; //리스트, 딕셔너리 등의 구조체 활용을 위한 네임스페이스 (이 프로젝트에선 활용X)
using UnityEngine; //Unity를 활용하기 위한 네임스페이스
using UnityEngine.UI; //UI를 활용하기 위한 네임스페이스

public class AutoPlay : MonoBehaviour
{
    GameObject playingKey; //연주할 건반을 할당하기 위한 변수 선언 
    float runTime;  //현재 연주 시간 추적을 위한 변수 선언
    static int keyCount = 25; //연주에 필요한 총 노트 수를 할당할 변수 선언 및 25로 초기화
    int crtCounts; //코루틴 함수 개수를 할당하며 추적하기 위한 변수 선언

    [Header("비행기 음노트 25개")] //Unity에디터에 제목으로 나타낼 내용 표시를 위한 선언
    public GameObject[] pianoKey = new GameObject[keyCount]; //연주할 노트를 할당할 배열 변수 선언 및 초기화
    public float[] playTime =  new float[keyCount]; //연주할 노트 시작 전, 지연 시간을 주기 위한 선언 및 초기화

    public void MusicPlay() //함수 선언 및 Unity에디터에서의 접근을 위한 공개
    {
        //코루틴 수가 0개, 25개 일 때 작동(자동연주 시작전, 종료시에만 작동)
        if (crtCounts == 0 || crtCounts == keyCount) 
        {
            runTime = 0f; //자동연주 시작 시 연주 시간 0초로 초기화
            for(int i = 0; i < keyCount; i++) //연주할 노트 개수 만큼 반복 실행
            {   
                //연주 시간에 사용자가 Unity에디터에서 할당한 노트별 시간을 반복 시마다 누가
                runTime += playTime[i]; 

                //연주할 건반에 사용자가 Unity에디터에서 할당한 건반 객체 할당
                playingKey = GameObject.Find(pianoKey[i].name);

                //연주할 건반 연주 중단(이전에 실행 중인 것이 있을 시 중단 목적) 
                playingKey.GetComponent<AudioSource>().Stop(); 

                backToOriginalColor(); //건반을 원래 색상으로 돌려 놓기 위한 함수 실행(하단에 별도 선언) 

                //연주할 건반 연주가 순차적으로 이루어지도록 코루틴 함수 실행(하단에 별도 선언)
                StartCoroutine(PlayCoroutine(i, playingKey));             
            }        
        }
        else //코루틴 수가 1개~24개일 때 작동(자동연주 중일 때 작동)
        {
            StopAllCoroutines(); //예약에 들어간 모든 코루틴 함수 중단
            backToOriginalColor(); //건반을 원래 색상으로 돌려 놓기 위한 함수 실행(하단에 별도 선언) 
            crtCounts = 0; //코루틴 개수 0으로 초기화
        }
    }

    IEnumerator PlayCoroutine(int i, GameObject playingKey) //순차 연주용 코루틴 함수 선언
    {
        yield return new WaitForSeconds(runTime); //runTime만큼 기다렸다가 하단의 코드 실행
        crtCounts += 1; //코루틴 함수가 추가로 예약될 때마다 수치 증가
        playingKey.GetComponent<AudioSource>().Play(); //연주할 건반 연주 실행
        playingKey.GetComponent<Image>().color = Color.green; //연주할 건반 색상에 초록색 할당
        Invoke("backToOriginalColor",0.2f); //0.2초 뒤 건반을 원래대로 돌려놓는 함수 실행(하단에 별도 선언)
    }
    void backToOriginalColor() //건반을 원래대로 돌려놓는 함수 선언
    {
        //흰색 건반을 모두 담을 배열 선언 및 "whiteKey" 태그가 붙은 건반 객체 모두 할당 
        GameObject[] whiteKeys = GameObject.FindGameObjectsWithTag("whiteKey"); 

        //검은색 건반을 모두 담을 배열 선언 및 "blackKey" 태그가 붙은 건반 객체 모두 할당
        GameObject[] blackKeys = GameObject.FindGameObjectsWithTag("blackKey");

        foreach(GameObject key in whiteKeys) //해당하는 모든 건반에 원래 색상인 흰색 할당
        {
            key.GetComponent<Image>().color = Color.white; 
        }
        foreach(GameObject key in blackKeys) //해당하는 모든 건반에 원래 색상인 검은색 할당
        {
            key.GetComponent<Image>().color = Color.black;
        }
    }
}
