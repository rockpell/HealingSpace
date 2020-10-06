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
    [SerializeField] private Text darkSoulCount = null;
    [SerializeField] private Text blueSoulCount = null;
    [SerializeField] private Text redSoulCount = null;
    [SerializeField] private Text whiteSoulCount = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            UpdateSoulCount();
        }
    }

    public void ToggleStatusBar(bool value)
    {
        statusBar.SetActive(value);
    }

    public void RefreshStatusBar(Character character)
    {
        name.text = character.NickName;
        love.fillAmount = character.Love / 100;
        hp.value = character.Hp / 100;
    }

    public void UpdateSoulCount()
    {
        darkSoulCount.text = GameManager.Instance.StoneCounter[SoulType.DARK].ToString();
        redSoulCount.text = GameManager.Instance.StoneCounter[SoulType.RED].ToString();
        blueSoulCount.text = GameManager.Instance.StoneCounter[SoulType.BLUE].ToString();
        whiteSoulCount.text = GameManager.Instance.StoneCounter[SoulType.WHITE].ToString();
    }

}