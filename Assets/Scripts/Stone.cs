using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private SoulType soulType = SoulType.NONE;
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
            if (Vector3.Distance(characterTransform.position, transform.position) < 0.2f)
            {
                GameManager.Instance.StoneCounter[soulType] += 1;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();

        if (character && !isCollision)
        {
            isCollision = true;
            characterTransform = character.transform;
        }
    }
}
