using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uICondition;

    Condition health { get { return uICondition.health; } }
    Condition stamina { get { return uICondition.stamina; } }


    public event Action onTakeDamage;

    private void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }


    public void Die()
    {
        Debug.Log("ав╬З╢ы.");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }

    public IEnumerator SpeedBuf(float value)
    {
        CharactorManager.Instance.Player._playerData.moveSpeed += value;
        yield return new WaitForSeconds(5);
        CharactorManager.Instance.Player._playerData.moveSpeed -= value;
    }
    public IEnumerator JumpBuf(float value)
    {
        CharactorManager.Instance.Player._playerData.jumpPower += value;
        yield return new WaitForSeconds(5);
        CharactorManager.Instance.Player._playerData.jumpPower -= value;
    }
}
