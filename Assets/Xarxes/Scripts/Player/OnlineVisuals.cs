using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlineVisuals : MonoBehaviour
{
    public List<Material> fontMaterials_list = new List<Material>();
    public TMP_Text nameText;

    [SerializeField] private PlayerData cs_PlayerData;
    [SerializeField] private PlayerOnline cs_PlayerOnline;

    // Start is called before the first frame update
    void Start()
    {
        cs_PlayerData = cs_PlayerOnline.GetPlayerData();

        if (cs_PlayerData != null)
        {
            nameText.text = cs_PlayerData.name;
            nameText.material = fontMaterials_list[cs_PlayerData.playerNum - 1];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
