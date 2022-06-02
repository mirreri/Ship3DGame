using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Simple cool down timer.
/// </summary>
[System.Serializable]
public class CoolDownTimer
{
    /// <summary>
    /// The amount of time to take to cool down.
    /// </summary>
    [SerializeField] public float coolDownAmount = 0.5f;

    /// <summary>
    /// The time that this cool down completed.
    /// </summary>
    private float m_coolDownCompleteTime;

    /// <summary> 
    /// Is the cool down complete.
    /// </summary>
    private bool isComplete => Time.time > m_coolDownCompleteTime;

    /// <summary> 
    /// The definition of main method.
    /// </summary>
    public delegate void Method();
    /// <summary> 
    /// The main execute method for calling.
    /// </summary>
    private Method m_methodToCall;

    /// <summary> 
    /// The check execute method for calling when fail (optional).
    /// </summary>
    private Method m_notCompleteMethod;
    /// <summary> 
    /// The definition of check method.
    /// </summary>
    public delegate bool CheckMethod();
    /// <summary> 
    /// The check execute method for calling (optional).
    /// </summary>
    private CheckMethod m_CheckMethodToCall;



    /// <summary> 
    /// Get remaining time of cool down.
    /// </summary>
    public float GetRemainingTime()
    {
        return Mathf.Max(m_coolDownCompleteTime - Time.time, 0);
    }
    private void StartCoolDown()
    {
        m_coolDownCompleteTime = Time.time + coolDownAmount;
    }   

    // Auto Repeat
    public void Build(Method method)
    {
        StartCoolDown();
        m_methodToCall = method;
    }

    public void Build(Method method, CheckMethod check)
    {
        StartCoolDown();
        m_methodToCall = method;
        m_CheckMethodToCall = check;
    }

    public void Build(Method method, Method coolDownNotCompleteMethod, CheckMethod check)
    {
        StartCoolDown();
        m_methodToCall = method;
        m_CheckMethodToCall = check;
        m_notCompleteMethod = coolDownNotCompleteMethod;
    }



    /// <summary> 
    /// Force reset the cool down.
    /// </summary>
    public void Reset()
    {
        StartCoolDown();
    }

    /// <summary> 
    /// Is the cool down complete.
    /// </summary>
    public bool Running {get; set;}

    public void SubCoolTime(float value)
    {
        m_coolDownCompleteTime -= value;
    }
    public void AddCoolTime(float value)
    {
        m_coolDownCompleteTime += value;
    }

    public void Update()
    {
        if( isComplete == true && Running == false )
        {
            if( m_CheckMethodToCall != null )
            {
                if( m_CheckMethodToCall() )
                {
                    StartCoolDown();
                    m_methodToCall();
                }
            }
            else
            {
                StartCoolDown();
                m_methodToCall();
            }
        }
        else
        {
            if( m_notCompleteMethod != null )
            {
                m_notCompleteMethod();
            }
        }
    }


}