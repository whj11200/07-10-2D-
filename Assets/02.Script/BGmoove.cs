using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmoove : MonoBehaviour
{
    // 배경의 이동 속도
    public float speed;

    // 배경의 Transform 컴포넌트
    private Transform tr;

    // 배경 이미지의 너비
    private float width;

    // 배경의 Collider2D 컴포넌트
    BoxCollider2D collider2D;

    // 게임 시작 시 실행되는 코루틴
    IEnumerator Start()
    {
        // 배경의 초기 이동 속도 설정
        speed = 8f;

        // BoxCollider2D 컴포넌트 가져오기
        collider2D = GetComponent<BoxCollider2D>();

        // Transform 컴포넌트 가져오기
        tr = GetComponent<Transform>();

        // 배경 이미지의 x축 길이 계산
        width = collider2D.size.x;

        // 한 프레임을 기다린 후, 배경 무한 반복 코루틴 실행
        yield return null;
        StartCoroutine(BackGroundLoop());
    }

    // 배경 무한 반복을 처리하는 코루틴
    IEnumerator BackGroundLoop()
    {
        // 게임이 종료 상태가 아닌 동안 반복
        while (GameManger.Instance.Is_Gameover == false)
        {
            // 배경을 왼쪽으로 이동
            tr.Translate(Vector3.left * speed * Time.deltaTime);

            // 배경이 화면 왼쪽 끝을 지나치면 다시 배치
            if (tr.position.x <= -2 * width)
            {
                RePosition();
            }

            // 매우 짧은 시간 동안 대기 후 다음 프레임 처리
            yield return new WaitForSeconds(0.002f);
        }
    }

    // 배경을 다시 배치하는 함수
    void RePosition()
    {
        // 배경을 오른쪽으로 이동시켜 새로운 위치에 배치
        Vector2 offset = new Vector3(width * 3f, 0f, tr.position.z);
        tr.position = (Vector2)tr.position + offset;
    }
}
