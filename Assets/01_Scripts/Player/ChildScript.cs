using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildScript : MonoBehaviour
{
    private bool hasFruit = false;
    [SerializeField] Image fruitIcon;

    private void Start()
    {
        HasFruit = false;
    }

    public bool HasFruit 
    {
        get { return hasFruit; }
        
        set {
            hasFruit = value;
            fruitIcon.gameObject.SetActive(hasFruit);
        } 
    }
}
