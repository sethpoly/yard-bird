using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Equipment equipped;

    public void UpdateEquipmentLogic() 
    {
        equipped.CheckMainUse();
        equipped.CheckAltUse();
    }
}
