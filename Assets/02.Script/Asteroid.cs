using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float moveSpeed = 0f;
    private string EnemyTag = "Coin";
    public GameObject effect;
    public AudioClip cilp;
    public AudioSource source;


    private Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
        moveSpeed = Random.Range(10.0f, 15f);
        source = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        tr.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (tr.position.x <= -20f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(EnemyTag))
        {
            source.PlayOneShot(cilp, 1.0f);
            Destroy(other.gameObject);
            Destroy(gameObject);
            var Hiteffect = Instantiate(effect, other.transform.position, Quaternion.identity);
            Destroy(Hiteffect, 0.2f);
        }
    }
}
