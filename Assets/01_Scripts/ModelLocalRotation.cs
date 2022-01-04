using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLocalRotation : MonoBehaviour
{
    [SerializeField] private Transform transToFollow;
    [SerializeField] private float effectValue;
    private Quaternion offset;

    void Start()
    {
        offset = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transToFollow)
            return;

        this.transform.localRotation = Quaternion.Lerp(offset, Quaternion.Euler(0, 0, transToFollow.localRotation.eulerAngles.z), effectValue);
    }
}

