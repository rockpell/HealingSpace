using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCreator : MonoBehaviour
{
    [SerializeField] private GameObject heart = null;

    private Vector3 startPosition = Vector3.zero;
    private bool isHeartStart = false;
    private float time = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (isHeartStart)
        {
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                time -= 0.5f;
                HeartEffect(startPosition);
            }
            isHeartStart = false;
        }
    }

    public void StartHeartEffect(Vector3 position)
    {
        startPosition = position;
        isHeartStart = true;
    }

    public void HeartEffect(Vector3 position)
    {
        float randX = Random.Range(-0.5f, 0.5f);
        Vector3 offsetPos = new Vector3(randX, 0.5f, 0);
        GameObject heartObject = Instantiate(heart, position + offsetPos, Quaternion.identity, this.transform.parent);
        StartCoroutine(VanishHeart(heartObject));
    }

    private IEnumerator VanishHeart(GameObject heart)
    {
        SpriteRenderer heartSprite = heart.GetComponent<SpriteRenderer>();
        float accTime = 0;
        float waitTime = 0.05f;
        float upSpeed = 0.05f;
        float deltaAlpha = 0.05f;
        Color color = heartSprite.color;

        while (accTime < 1f)
        {
            heart.transform.Translate(new Vector3(0, upSpeed, 0));
            color.a -= deltaAlpha;
            heartSprite.color = color;
            yield return new WaitForSeconds(waitTime);
            accTime += waitTime;
        }
        Destroy(heart);
    }
}
