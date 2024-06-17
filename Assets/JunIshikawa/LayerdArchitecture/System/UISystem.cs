using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UIElements;

public class UISystem : SystemBase, IOnLateUpdate
{
    public override void SetUp()
    {
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.playerPrimeDemandExp);
        SliderMaxValueSet(gameStat.playerHpSlider, gameStat.playerDataList[0].playerMaxHp);

        EnterButtonInit(gameStat.levelUpEnterButton);
        DebugButtonInit(gameStat.DebugButton);

        for(int panelNum = 0; panelNum < gameStat.selectPanelsList.Count; panelNum++)
        {
            RewardSelectPanelInit(gameStat.selectPanelsList[panelNum], panelNum);
        }
    }

    public void OnLateUpdate()
    {
        SliderMaxValueSet(gameStat.playerExpSlider, gameStat.playerExpSliderMaxValue);
        SliderValueSet(gameStat.playerExpSlider, gameStat.playerExpSliderProgressValue);

        SliderValueSet(gameStat.playerHpSlider, gameStat.player.GetEntityHp);
        TextSet(gameStat.playerLevelText, gameStat.playerLevel.ToString());

        UIDisplay(gameStat.levelUpPanel, gameStat.isLevelUp);
    }

    private void SliderMaxValueSet(UnityEngine.UI.Slider _slider, int _value)
    {
        _slider.maxValue = _value;
    }

    private void SliderValueSet(UnityEngine.UI.Slider _slider, int _value)
    {
        _slider.value = _value;
    }

    private void UIDisplay(GameObject _UI, bool _UIDisplayTrigger)
    {
        if (_UIDisplayTrigger)
        {
            _UI.SetActive(true);
        }
        else
        {
            _UI.SetActive(false);
        }
    }

    private void TextSet(TextMeshProUGUI _TMPText, string _text)
    {
        _TMPText.text = _text;
    }

    //levelUpPanelのEnterButton初期化
    private void EnterButtonInit(GameObject _buttonUI)
    {
        ButtonBase button = _buttonUI.GetComponent<ButtonBase>();
        if (button == null) return;
        button.ButtonInit();

        TriggerInsert(button, EventTriggerType.PointerEnter).callback.AddListener((eventDate) => { button.PointerOverEvent(); });
        TriggerInsert(button, EventTriggerType.PointerExit).callback.AddListener((eventDate) => { button.PointerExitEvent(); });
        TriggerInsert(button, EventTriggerType.PointerDown).callback.AddListener((eventDate) => { button.PointerDownEvent(); });

        button.pointerOverEvent += () => button.buttonImage.color = Color.red;
        button.pointerExitEvent += () => button.buttonImage.color = Color.white;

        button.pointerDownEvent += () => {
            if (gameStat.isPanelSelected == true)
                gameStat.isLevelUp = false;
                gameStat.isPanelSelected = false;
        };
    }

    //levelUpPanelのPanels初期化
    private void RewardSelectPanelInit(GameObject _buttonUI, int _panelNum)
    {
        ButtonBase button = _buttonUI.GetComponent<ButtonBase>();
        if (button == null) return;
        button.ButtonInit();

        TriggerInsert(button, EventTriggerType.PointerEnter).callback.AddListener((eventDate) => { button.PointerOverEvent(); });
        TriggerInsert(button, EventTriggerType.PointerExit).callback.AddListener((eventDate) => { button.PointerExitEvent(); });
        TriggerInsert(button, EventTriggerType.PointerDown).callback.AddListener((eventDate) => { button.PointerDownEvent(); });

        button.pointerOverEvent += () => button.buttonImage.color = Color.black;
        button.pointerExitEvent += () => button.buttonImage.color = Color.white;
        button.pointerDownEvent += () => gameStat.isPanelSelected = true;
        button.pointerDownEvent += () => gameStat.selectedPanelNumber = _panelNum;
    }

    private void DebugButtonInit(GameObject _buttonUI)
    {
        ButtonBase button = _buttonUI.GetComponent<ButtonBase>();
        if (button == null) return;
        button.ButtonInit();

        TriggerInsert(button, EventTriggerType.PointerDown).callback.AddListener((eventDate) => { button.PointerDownEvent(); });

        button.pointerDownEvent += () => gameStat.playerTotalExp++;
    }

    private Entry TriggerInsert(ButtonBase button, EventTriggerType _eventTriggerType)
    {
        Entry entryTrigger = new Entry();
        entryTrigger.eventID = _eventTriggerType;

        button.buttonEventTrigger.triggers.Add(entryTrigger);

        return entryTrigger;
    }
}
