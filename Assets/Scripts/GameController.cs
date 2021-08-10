using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public event Action Finishing;
    [SerializeField] private GameObject[] _blocks;
    
   

    private void Awake()
    {
        if (!Instance) Instance = this;
    }
    public void Checkfinish()
    {
        for (int i = 0; i < _blocks.Length; i++)
        {
            if (_blocks[i].activeSelf) return;
        }
        Finishing?.Invoke();
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < _blocks.Length; i++)
        {
            _blocks[i].SetActive(true);
        }
    }
}
