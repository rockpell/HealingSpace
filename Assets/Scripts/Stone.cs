using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private Transform characterTransform;
    private float speed = 1f;
    private bool isCollision = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollision)
        {
            transform.Translate((characterTransform.position - transform.position) * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character)
        {
            isCollision = true;
            characterTransform = character.transform;
            Debug.Log("this is Character pos: " + character.transform.position);
        }
    }

}
