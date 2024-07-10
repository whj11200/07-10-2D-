using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmoove : MonoBehaviour
{
    // ����� �̵� �ӵ�
    public float speed;

    // ����� Transform ������Ʈ
    private Transform tr;

    // ��� �̹����� �ʺ�
    private float width;

    // ����� Collider2D ������Ʈ
    BoxCollider2D collider2D;

    // ���� ���� �� ����Ǵ� �ڷ�ƾ
    IEnumerator Start()
    {
        // ����� �ʱ� �̵� �ӵ� ����
        speed = 8f;

        // BoxCollider2D ������Ʈ ��������
        collider2D = GetComponent<BoxCollider2D>();

        // Transform ������Ʈ ��������
        tr = GetComponent<Transform>();

        // ��� �̹����� x�� ���� ���
        width = collider2D.size.x;

        // �� �������� ��ٸ� ��, ��� ���� �ݺ� �ڷ�ƾ ����
        yield return null;
        StartCoroutine(BackGroundLoop());
    }

    // ��� ���� �ݺ��� ó���ϴ� �ڷ�ƾ
    IEnumerator BackGroundLoop()
    {
        // ������ ���� ���°� �ƴ� ���� �ݺ�
        while (GameManger.Instance.Is_Gameover == false)
        {
            // ����� �������� �̵�
            tr.Translate(Vector3.left * speed * Time.deltaTime);

            // ����� ȭ�� ���� ���� ����ġ�� �ٽ� ��ġ
            if (tr.position.x <= -2 * width)
            {
                RePosition();
            }

            // �ſ� ª�� �ð� ���� ��� �� ���� ������ ó��
            yield return new WaitForSeconds(0.002f);
        }
    }

    // ����� �ٽ� ��ġ�ϴ� �Լ�
    void RePosition()
    {
        // ����� ���������� �̵����� ���ο� ��ġ�� ��ġ
        Vector2 offset = new Vector3(width * 3f, 0f, tr.position.z);
        tr.position = (Vector2)tr.position + offset;
    }
}
