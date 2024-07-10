using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_NearMove : MonoBehaviour
{
    private float Speed = 7f;
    [SerializeField]
    private Transform Tr = null;
    private Vector2 offset = Vector2.zero;

    void Start()
    {
       Tr = GetComponent<Transform>();
        offset = Vector3.Lerp(new Vector3(-42.5f, Tr.position.y,Tr.position.z),new Vector3(-14.5f, Tr.position.y,Tr.position.z),Time.deltaTime*Speed);
    }

    
    void Update()
    {
        Tr.Translate(Vector3.left* Speed*Time.deltaTime);
        if (Tr.position.x <= -42.5f)
        {
           
            Tr.position = new Vector3(-14.5f, Tr.position.y,Tr.position.z);
        }

    }
}
