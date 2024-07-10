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
    private int _touchPadId = -1; // ��ġ ���� �ƴ��� �Ǵ�
    private bool _isBtnPressed = false; // ��ư�� ���� �������� �ƴ���
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
    // ��Ȯ�� �������� ���� ���� ���� �ϰų� ���ϴ� �ð�Ÿ�ӿ� 
    // �ݵ�� ���� ���̶�� FixedUpdate �� ����Ѵ�.
    // �����÷���
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
    void HandIeTouchInput() //����Ͽ� �е� �̵� �Լ�
    {
        int i = 0;

        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                ++i;
                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);
                // ��ġ���� == ��ġ�� ���� �� ���� �Ǿ��ٸ� 
                if(touch.phase == TouchPhase.Began )
                {
                    // x�� �־�� float������ ��ȯ�Ǿ� ���Ҽ� ����
                    // ��ġ�� �ߴµ� �� ���� �ȿ� �ִٸ�
                    if (touch.position.x <= (_StartsPosition.x + _dragRadius))
                    {
                        _touchPadId = i;
                    }
                    if (touch.position.y <= (_StartsPosition.y + _dragRadius))
                    {
                        _touchPadId = i;
                    }
                }
                // ��ġ ���°� �����̰� �ְų� ���� �����϶�
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    // ���ȿ� �ִٸ�
                    if (_touchPadId == i)
                    {
                        // ���� �� �Լ����� ������ ��������.
                        HandIeInput(touchPos);
                    }
                }
                // ��ġ�� �����ٸ�
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
    void HandIeInput(Vector3 input) // pc�� �е� �̵� �Լ�
    {
        if (_isBtnPressed)
        {                        // ��ġ ��ġ - ��ŸƮ ��ġ = ����� �Ÿ��� ������
            Vector3 diffVector = (input - _StartsPosition);
            // ��ü �Ÿ��� ���ؼ� ������ ����ٸ�
            if(diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                diffVector.Normalize();
                _touchPad.position = _StartsPosition;
                // ��ġ�� ���콺Ŀ���� �հ��� ������ ������
                // ��ġ �е�� ���ȿ��� ������ ����ä 
            }
            else
            {
                // ���ȿ� �ֹǳ� �״�� �Է°��� ���� �޶�����.
                _touchPad.position = input; 

            }
            
        }
        else
        { // ���Ҵٸ�
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
