using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private Coroutine timerCoroutine;
    protected float currentChargeTime;
    private bool atkTimerDone = true;
    protected Rigidbody owner;

    [SerializeField] public float contactDamage;
    [SerializeField] public float chargeTime;
    [SerializeField] public float minCharge;

    public WaitForSeconds Cooldown;
    [SerializeField] public float cooldown;

    private void OnEnable()
    {
        Cooldown = new WaitForSeconds(cooldown);
    }

    protected abstract void Attack(float chargePercent);
    
    protected virtual bool CanAttack()
    {
        return atkTimerDone;
    }

    private void TryAttack(float percent)
    {
        if (percent < minCharge) 
        {
            return;
        }

        Attack(percent);
            StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        atkTimerDone = false;
        yield return Cooldown;
        atkTimerDone = true;
    }

    public void StartAttack()
    {
        timerCoroutine = StartCoroutine(HandleCharge());
    }

    private IEnumerator HandleCharge()
    {
        currentChargeTime = 0;
        print("StartCharge");
        yield return new WaitUntil(() => atkTimerDone);
        print("CooldownDone");

        while(currentChargeTime < chargeTime)
        {
            currentChargeTime += Time.deltaTime;
            yield return null;
        }
        print("Attack Completed");
        TryAttack(1);
        timerCoroutine = StartCoroutine(HandleCharge());
    }

    public void EndAttack()
    {
        
    }
}
