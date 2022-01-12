using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLocalRotation : MonoBehaviour
{
    [SerializeField] private Transform transToFollow; // Transform with target rotation
    [SerializeField] private Vector3 axisValue; // How much each component of the rotation is supposed to follow target rotation
    [SerializeField, Min(0)] private float effectValue; // How much is the rotation supposed to follow target rotation as a whole
    private Quaternion offset;

    void Start()
    {
        offset = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        FollowRotation();
    }

    /// <summary> Follows target transform local rotation according to the axis and effect values </summary>
    void FollowRotation()
    {
        // Null ref protection
        if (!transToFollow)
            return;

        // Lerp this object's local rotation to targets scaled by the axis values with the effect amount
        this.transform.localRotation = Quaternion.Lerp(offset, Quaternion.Euler(Vector3.Scale(transToFollow.localRotation.eulerAngles, axisValue)), effectValue);
    }
}

