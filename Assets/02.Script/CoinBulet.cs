using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinBulet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private GameObject Effect;
    [SerializeField]
    private float Speed = 2000f;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.right * Speed);
        Destroy(gameObject,1.5f);
        
    }
    
        
    


}
