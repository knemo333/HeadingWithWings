using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUIController : MonoBehaviour
{
    public Button[] slotButtons;
    public Image[] slotEquipImages;
    public GameObject[] firstSlotLevelImages;
    public GameObject[] secondSlotLevelImages;
    public GameObject[] thirdSlotLevelImages;
    public List<GameObject[]> levels = new List<GameObject[]>();
    public TMPro.TextMeshProUGUI[] slotEquipName;
    //public GameObject[] slotBan;
    public TMPro.TextMeshProUGUI[] info;
    public TMPro.TextMeshProUGUI[] detailInfo;
    // [SerializeField] private GameObject[] firstSlotInfos;
    // [SerializeField] private GameObject[] secondSlotInfos;
    // [SerializeField] private GameObject[] thirdSlotInfos;
    //public List<GameObject[]> slotInfos = new List<GameObject[]>();
    public Button[] infoButtons;
    public GameObject[] detailInfoUI;
    public Button rerollButton;
    public GameObject rerollBan;
    public Button skipButton;
    
    public void Init(){
        // slotInfos.Add(firstSlotInfos);
        // slotInfos.Add(secondSlotInfos);
        // slotInfos.Add(thirdSlotInfos);
        levels.Add(firstSlotLevelImages);
        levels.Add(secondSlotLevelImages);
        levels.Add(thirdSlotLevelImages);
        GetComponent<AudioSource>().Play();
    }

    public void DetailInfoOpen(int index)
    {
        detailInfoUI[index].SetActive(true);
    }

    public void DetailInfoExit(int index)
    {
        detailInfoUI[index].SetActive(false);
    }

    public void SetLevel(int index, int level)
    {
        for(int i = 0; i < level; i++)
        {
            levels[index][i].SetActive(true);
        }
    }

    public void BanReroll()
    {
        rerollButton.gameObject.SetActive(false);
        rerollBan.SetActive(true);
    }
    // public void SetSlotBan(int index)
    // {
    //     slotBan[index].SetActive(true);
    // }
    // public void ClearSlotBan()
    // {
    //     foreach(var ban in slotBan)
    //     {
    //         ban.SetActive(false);
    //     }
    // }
    
    public void PickUIExit()
    {
        // int count = firstSlotInfos.Length;
        // for(int i = 0; i < count; i++)
        // {
        //     firstSlotInfos[i].SetActive(false);
        //     secondSlotInfos[i].SetActive(false);
        //     thirdSlotInfos[i].SetActive(false);
        // }
        // for(int i = 0; i < 3; i++)
        // {
        //     slotBan[i].SetActive(false);
        // }
        for(int i = 0; i < 3; i++)
        {
            detailInfoUI[i].SetActive(false);
            foreach(GameObject level in levels[i])
            {
                level.SetActive(false);
            }
        }
        rerollButton.gameObject.SetActive(true);
        rerollBan.SetActive(false);
        gameObject.SetActive(false);
    }
}
