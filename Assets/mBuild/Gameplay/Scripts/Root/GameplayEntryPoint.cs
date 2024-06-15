using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _sceneRootBinder;

    public void Run()
    {
        Debug.Log("Gamplay scene loaded");
    }
}
