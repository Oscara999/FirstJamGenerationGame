using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("xd");
            StartCoroutine(GameManager.Instance.RestaureStartPosition());
        }
    }
}
