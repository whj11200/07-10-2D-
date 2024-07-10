using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour
{
    private RectTransform _touchPad;
    private Vector3 _StartsPosition = Vector3.zero;
    public float _dragRadius = 122f;
    private int _touchPadId = -1; // 터치 인지 아닌지 판단
    private bool _isBtnPressed = false; // 버튼을 누른 상태인지 아닌지
    public Vector3 diff;
    [SerializeField] private RoketCtrl roketCtrl;


    void Start()
    {
       roketCtrl = GameObject.FindWithTag("Player").GetComponent<RoketCtrl>();
      _touchPad =GetComponent<RectTransform>();
      _StartsPosition = _touchPad.position;
    }

    public void OnButtonDown()
    {
        _isBtnPressed= true;
    }
    public void ButtonUp()
    {
        _isBtnPressed = false;
    }
    // 정확한 물리량을 따른 것을 구현 하거나 원하는 시간타임에 
    // 반드시 구현 것이라면 FixedUpdate 를 사용한다.
    // 고정플레이
    private void FixedUpdate()
    {
        
        if(Application.platform == RuntimePlatform.Android)
        {
            HandIeTouchInput();
        }
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            HandIeInput(Input.mousePosition);
        }


        
    }
    void HandIeTouchInput() //모바일용 패드 이동 함수
    {
        int i = 0;

        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                ++i;
                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);
                // 터치유형 == 터치가 이제 막 시작 되었다면 
                if(touch.phase == TouchPhase.Began )
                {
                    // x를 넣어야 float값으로 변환되어 비교할수 있음
                    // 터치를 했는데 원 범위 안에 있다면
                    if (touch.position.x <= (_StartsPosition.x + _dragRadius))
                    {
                        _touchPadId = i;
                    }
                    if (touch.position.y <= (_StartsPosition.y + _dragRadius))
                    {
                        _touchPadId = i;
                    }
                }
                // 터치 상태가 움직이고 있거나 멈춤 상태일때
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    // 원안에 있다면
                    if (_touchPadId == i)
                    {
                        // 여기 이 함수에서 실제로 움직였다.
                        HandIeInput(touchPos);
                    }
                }
                // 터치가 끝났다면
                if (touch.phase == TouchPhase.Ended)
                {
                   if(_touchPadId == i)
                    {
                        _touchPadId = -1;
                    }
                }
            }
        }
    }
    void HandIeInput(Vector3 input) // pc용 패드 이동 함수
    {
        if (_isBtnPressed)
        {                        // 터치 워치 - 스타트 워치 = 방향과 거리가 구해짐
            Vector3 diffVector = (input - _StartsPosition);
            // 전체 거리를 비교해서 원밖을 벗어났다면
            if(diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                diffVector.Normalize();
                _touchPad.position = _StartsPosition;
                // 터치한 마우스커서나 손가락 원밖을 나가도
                // 터치 패드는 원안에서 방향을 지한채 
            }
            else
            {
                // 원안에 있므녀 그대로 입력값에 따라 달라진다.
                _touchPad.position = input; 

            }
            
        }
        else
        { // 막았다면
            _touchPad.position = _StartsPosition;
        }
        Vector3 diff = _touchPad.position - _StartsPosition;

                                    
        Vector2 nomel0iff = new Vector2(diff.x/_dragRadius, diff.y/_dragRadius);
        if(roketCtrl !=  null)
        {
            roketCtrl.OnSickPos(nomel0iff);
        }
    }
    
}
