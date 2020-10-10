using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    private MonsterCreator monsterCreator = null;
    private SceneController sceneController = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();

        if (character)
        {
            // monsterCreator.IsMonster = false;
            Destroy(this.gameObject);
            SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        }
    }

    public void SetMonsterCreator(MonsterCreator monsterCreator)
    {
        Debug.Log("mo::  " + monsterCreator);
        this.monsterCreator = monsterCreator;
    }
}
