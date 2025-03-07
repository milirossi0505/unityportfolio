using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadroSelector : MonoBehaviour
{
    public int value;
    CuadroManager cuadroManager;

    // Start is called before the first frame update
    void Start()
    {
        cuadroManager = FindObjectOfType<CuadroManager>();      
    }

    public void OnMouseDown()
    {
        cuadroManager.ChangeMaterial(value);
    }
}
