using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Equipment equipped = null;

    public void UpdateEquipmentLogic() 
    {
        if(equipped != null)
        {
            equipped.CheckMainUse();
            equipped.CheckAltUse();
        }
    }

    public void Equip(Equipment equipment)
    {
        equipped = equipment;
        equipment.transform.SetParent(this.transform);
    }

    /// <summary>
    /// Check if the hand is holding something
    /// </summary>
    public bool IsArmed()
    {
        return equipped != null;
    }
}
