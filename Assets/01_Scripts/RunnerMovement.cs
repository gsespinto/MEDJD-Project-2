using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    /// <summary> Move character forward </summary>
    void Move()
    {
        Vector3 position = this.transform.position;
        position.z += movementSpeed * Time.deltaTime;
        this.transform.position = position;
    }
}
