using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlineVisuals : MonoBehaviour
{
    public List<Color> fontMaterials_list = new List<Color>();
    public TMP_Text nameText;

    [SerializeField] private PlayerData cs_PlayerData;
    [SerializeField] private PlayerOnline cs_PlayerOnline;

    void Start()
    {
        cs_PlayerData = cs_PlayerOnline.GetPlayerData();

        if (cs_PlayerData != null)
        {
            nameText.text = cs_PlayerData.name;
            nameText.outlineColor = fontMaterials_list[cs_PlayerData.playerNum - 1];

            this.gameObject.GetComponent<SpriteRenderer>().material.color = cs_PlayerData.GetColorColor();
        }
    }
}
