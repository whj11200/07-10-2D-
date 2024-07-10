using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 오브젝트 프리팹 2. 생성시 시간 간격 3. 크기가 랜덤 4. y축 자표 상하 위치
public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    [Header("Spawn 관련변수")]
    public GameObject AseteroidPrefad;
    private float TimePrev;
    private float YminValue = -3.0f;
    private float YmaxValue = 3.5f;

    [Header("CamerShake 관련변수")]
    public Vector3 PosCamera; //카메라가 흔들리기 전 원래 카메라 위치를 받는 변수
    public float HitBeginTime; // 로켓이 운석이랑 부딪칠때 시간을 저장 하는 변수
    private bool is_Hit = false; // 로켓이 운석이랑 부딪쳤는지 아닌지 판단하는 변수
    public bool Is_Gameover = false;
    
    void Start()
    {
        // 현재 시간 할당
        TimePrev = Time.time;
        Instance = this;
    }

    
    void Update()
    {
        

        if (Time.time - TimePrev > 2.5f)
        {
            TimePrev =Time.time;
            SpawnAsterroid();
        }
        if(is_Hit)
        {
            float x = Random.Range(-0.01f, 0.01f);
            float y = Random.Range(-0.01f, 0.01f);
            Camera.main.transform.position += new Vector3(x, y, 0f);
            if (Time.time - HitBeginTime > 0.3f)
            {
                is_Hit = false;
                Camera.main.transform.position = PosCamera;
            }

        }
    }

    public void TurnOn()
    {
        is_Hit = true; // 충돌했다가 true
        PosCamera = Camera.main.transform.position;
                  // 카메라가 흔들리긴 전 위치값을 대입
        HitBeginTime = Time.time;
    }
    void SpawnAsterroid()
    {
        float RandomYpos = Random.Range(YminValue, YmaxValue);
        float _Scale = Random.Range(1f, 2.5f);
        AseteroidPrefad.transform.localScale = Vector3.one * _Scale;
        // x,y,z 가 동일하게 
        Instantiate(AseteroidPrefad, new Vector3(10.0f, RandomYpos, AseteroidPrefad.transform.position.z), Quaternion.identity);
    }
}
