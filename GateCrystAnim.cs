using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCrystAnim : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void OpenGate()
    {
        animator.SetTrigger("open");
    }
    public void KeepOpen()
    {
        animator.SetTrigger("keepopen");
    }
}
