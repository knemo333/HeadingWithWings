using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpChange : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.playerInfo.MaxHealthPoint += GameManager.playerInfo.MaxHealthPoint * (changeMultiplier / 100f);
    }
}
