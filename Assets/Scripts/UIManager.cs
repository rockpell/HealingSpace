using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject statusBar = null;
    [SerializeField] private Text name = null;
    [SerializeField] private Text level = null;
    [SerializeField] private Image love = null;
    [SerializeField] private Slider hp = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleStatusBar(bool value)
    {
        statusBar.SetActive(value);
    }

    public void RefreshStatusBar(Character character)
    {
        name.text = character.NickName;
        level.text = character.Level.ToString();
        love.fillAmount = character.Amour / 100;
        hp.value = character.Hp / 100;
    }
}