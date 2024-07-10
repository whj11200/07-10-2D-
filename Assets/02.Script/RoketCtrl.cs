using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class RoketCtrl : MonoBehaviour
{
    public GameObject HitParticyer;
    public AudioSource auiosource;
    public AudioClip cilp;
    private float MoveSpeed = 4f;
    [SerializeField] private Transform tr;
    float h= 0;
    float v = 0;
    public string EnmeyTag = "Asteroid";
    private float halfHeight = 0f;
    private float halfWidth = 0f;
    public Transform Firepos;
    public GameObject CoinBullet;
    private Vector3 MoverVector;
    [SerializeField]
    private TouchPad pad;
    void Start()
    {
      auiosource = GetComponent<AudioSource>();
      tr = GetComponent<Transform>();
      halfHeight = Screen.height * 0.5f;
      halfWidth = Screen.width * 0.5f;
        Firepos = GameObject.Find("Firepos").transform;
        pad = GameObject.Find("ZoyStickpad").GetComponent<TouchPad>();
    }
    public void OnSickPos(Vector3 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

  
    void Update()
    {
        // 실시간 런타임 플렛폼이 안드로이드 인가
        if(Application.platform == RuntimePlatform.Android)

        {
            if (GetComponent<Rigidbody2D>())
            {
                Vector2 Speed = GetComponent<Rigidbody2D>().velocity;
                Speed.x = 4 * h;
                Speed.y = 4 * v;
                GetComponent<Rigidbody2D>().velocity = Speed;
            }
            //모바일에서 터치가 되었다면 카운트 수 를 저장 하고 있다.
            if (Input.touchCount > 0) // 터치가 되었다면
          {

            //float deltaPosX = Input.GetTouch(0).position.x-halfWidth;
            //float deltaPosY = Input.GetTouch(0).position.y-halfHeight;
            //    // 현재 위치값
            //float Xpos = deltaPosX-tr.position.x;
            //float Ypos = deltaPosY-tr.position.y;
             
            //tr.Translate(MoveSpeed * Time.deltaTime * Xpos* 0.05f,MoveSpeed* Time.deltaTime,Ypos* 0.05f ,0f);
          }
        }
        void QuitAPP()
        {
            // 모바일에서 뒤로가기 를 누르면 게임이 종료된다.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
           
        }
        // 실시간 런타임 플렛폼이 윈도우인가?
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            #region pc에서 조종
            //h = Input.GetAxis("Horizontal");
            //v = Input.GetAxis("Vertical");
            //Vector2 MoveDir = (h * Vector2.right) + (v * Vector2.up);
            //tr.Translate(MoveDir.normalized * MoveSpeed * Time.deltaTime);
            //CamerOutLimit();
            //QuitAPP();
            #endregion
            if (GetComponent<Rigidbody2D>())
            {
                Vector2 Speed = GetComponent<Rigidbody2D>().velocity;
                Speed.x = 4 * h;
                Speed.y = 4 * v;
                GetComponent<Rigidbody2D>().velocity = Speed;
            }
        }
        // 실시간 런타임 플렛폼이 IOS인가?
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {

        }
    }
    public void Fire()
    {
        Instantiate(CoinBullet,Firepos.transform.position, Quaternion.identity);
        
    }
    public void OnAnimatorMove()
    {
        tr.Translate(MoverVector * MoveSpeed * Time.deltaTime);
    }
   

    private void CamerOutLimit()
    {
        #region 카메라 밖을 못 나가게 하는 로직 첫번째
        if (tr.position.x >= 6.14f)
        {
            tr.position = new Vector3(6.14f, tr.position.y, tr.position.z);
        }
        else if (tr.position.x <= -8.19f)
        {
            tr.position = new Vector3(-8.19f, tr.position.y, tr.position.z);
        }
        if (tr.position.y >= 4.92f)
        {
            tr.position = new Vector3(tr.position.x, 4.92f, tr.position.z);
        }
        else if (tr.position.y <= -3.99)
        {
            tr.position = new Vector3(tr.position.x, -3.99f, tr.position.z);
        }
        #endregion
        #region 카메라 밖을 못나가게 하는 로직 두번째
        //tr.position = new Vector3(Mathf.Clamp(tr.position.x, 6.14f, -8.19f), Mathf.Clamp(tr.position.y, 4.92f, -3.99f));
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EnmeyTag))
        {
            Destroy(other.gameObject);
            GameObject effect = Instantiate(HitParticyer, other.transform.position, Quaternion.identity);
            Destroy(effect,0.5f);
            GameManger.Instance.TurnOn();
            auiosource.PlayOneShot(cilp, 1.0f);
        }
    }
}
