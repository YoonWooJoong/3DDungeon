using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;


    // Start is called before the first frame update
    void Start()
    {
        CharactorManager.Instance.Player._playerCondition.uICondition = this;
    }
}
