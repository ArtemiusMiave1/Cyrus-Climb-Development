using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCounter : MonoBehaviour
{
    public GameObject[] Collectables;
    public Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

 private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Animator.SetBool("Playing",true);
        }
    }
}
