using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RewardSystem : SystemBase, IOnLateUpdate
{
    public override void SetUp()
    {
        
    }

    public void OnLateUpdate()
    {
        RewardSet(gameStat.isLevelUp, out gameStat.isLevelUpOnce, gameStat.selectPanelsList, gameStat.rewardTextures);
    }

    private void RewardSet(bool _isLevelUp, out bool _isLevelUpOnce, List<GameObject> _selectPanelsList, List<Texture2D> _rewardTextures)
    {
        if (_isLevelUp && gameStat.isLevelUpOnce)
        {
            SkillRandomSet(_selectPanelsList, _rewardTextures);
            // LevelUpの設定
            
            _isLevelUpOnce = false;
        }
        else if (_isLevelUp && !gameStat.isLevelUpOnce)
        {
            // 特に何も実行しない
            _isLevelUpOnce = gameStat.isLevelUpOnce;
        }
        else
        {
            
            _isLevelUpOnce = _isLevelUp;
        }
    }

    private void SkillRandomSet(List<GameObject> _selectPanelsList, List<Texture2D> _rewardTextures)
    {
        // テクスチャのリストをコピーして使用
        List<Texture2D> availableTextures = new List<Texture2D>(_rewardTextures);

        foreach (var selectPanel in _selectPanelsList)
        {
            if (availableTextures.Count == 0)
            {
                Debug.LogWarning("Not enough textures to assign to all select panels.");
                break;
            }

            // ランダムにテクスチャを選択
            int randomIndex = Random.Range(0, availableTextures.Count);
            Texture2D selectedTexture = availableTextures[randomIndex];

            // セレクトパネルのImageコンポーネントを取得
            Image panelImage = selectPanel.GetComponent<Image>();

            // テクスチャをスプライトに変換
            Sprite sprite = Sprite.Create(selectedTexture, new Rect(0, 0, selectedTexture.width, selectedTexture.height), new Vector2(0.5f, 0.5f));

            // Imageコンポーネントにスプライトを設定
            panelImage.sprite = sprite;

            // レベル表示
            TextMeshProUGUI selectedSkillText = selectPanel.GetComponentInChildren<TextMeshProUGUI>();
            switch(selectedTexture.name)
            {
                case "Aura":
                    selectedSkillText.text = "Level : " + gameStat.auraSkillLevel;
                    break;
                case "Bullet":
                    selectedSkillText.text = "Level : " + gameStat.bulletSkillLevel;
                    break;
                case "Shuriken":
                    selectedSkillText.text = "Level : " + gameStat.shurikenSkillLevel;
                    break;
                case "Thunder":
                    selectedSkillText.text = "Level : " + gameStat.thunderSkillLevel;
                    break;
                default:
                    Debug.LogWarning("異常なSkillLevel検知");
                    break;
            }

            // 使用したテクスチャをリストから削除
            availableTextures.RemoveAt(randomIndex);
        }

    }



}
