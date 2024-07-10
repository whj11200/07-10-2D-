using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. ������Ʈ ������ 2. ������ �ð� ���� 3. ũ�Ⱑ ���� 4. y�� ��ǥ ���� ��ġ
public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    [Header("Spawn ���ú���")]
    public GameObject AseteroidPrefad;
    private float TimePrev;
    private float YminValue = -3.0f;
    private float YmaxValue = 3.5f;

    [Header("CamerShake ���ú���")]
    public Vector3 PosCamera; //ī�޶� ��鸮�� �� ���� ī�޶� ��ġ�� �޴� ����
    public float HitBeginTime; // ������ ��̶� �ε�ĥ�� �ð��� ���� �ϴ� ����
    private bool is_Hit = false; // ������ ��̶� �ε��ƴ��� �ƴ��� �Ǵ��ϴ� ����
    public bool Is_Gameover = false;
    
    void Start()
    {
        // ���� �ð� �Ҵ�
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
        is_Hit = true; // �浹�ߴٰ� true
        PosCamera = Camera.main.transform.position;
                  // ī�޶� ��鸮�� �� ��ġ���� ����
        HitBeginTime = Time.time;
    }
    void SpawnAsterroid()
    {
        float RandomYpos = Random.Range(YminValue, YmaxValue);
        float _Scale = Random.Range(1f, 2.5f);
        AseteroidPrefad.transform.localScale = Vector3.one * _Scale;
        // x,y,z �� �����ϰ� 
        Instantiate(AseteroidPrefad, new Vector3(10.0f, RandomYpos, AseteroidPrefad.transform.position.z), Quaternion.identity);
    }
}
