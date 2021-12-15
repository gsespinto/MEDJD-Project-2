using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopLayer : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Transform endTrans;
    [SerializeField] private GameObject loopLayerPrefab;
    
    [Header("Loop")]
    [SerializeField] private bool isFirstLayer = false;
    [SerializeField] private int aheadLoops = 1;
    [SerializeField] private float spawnDistanceThreshold = 5f;
    [SerializeField] private float distanceToDestroyLayer = 40f;
    public bool spawnedNextLayer = false;
    private GameObject bird;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get player ref
        bird = GameObject.FindGameObjectWithTag("Bird");
        
        // If this is the first layer of the loop
        if (isFirstLayer)
        {
            // Spawns ahead layers
            for (int i = 0; i < aheadLoops; i++)
            {
                SpawnLayer(i);
            }

            spawnedNextLayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bird)
        {
            bird = GameObject.FindGameObjectWithTag("Bird");
            return;
        }
        
        SpawnNextLayer();
        DestroyCurrentLayer();
    }
    
    /// <summary> Spawns the next layer at the correct moment </summary>
    void SpawnNextLayer()
    {
        // Only spawn one layer
        if (spawnedNextLayer)
            return;
        
        // Once the player is close enough to the end of this layer
        Vector3 flooredLayerPos = this.transform.position; flooredLayerPos.y = 0;
        Vector3 flooredPlayerPos = bird.transform.position; flooredPlayerPos.y = 0;
        if (Vector3.Distance(flooredPlayerPos, flooredLayerPos) > spawnDistanceThreshold)
            return;

        // Spawn next layer
        SpawnLayer(aheadLoops - 1);

        // Only spawn one layer
        spawnedNextLayer = true;
    }
    
    LoopLayer SpawnLayer(int _aheadLoops)
    {
        return GameObject.Instantiate(loopLayerPrefab, endTrans.position + endTrans.localPosition * _aheadLoops,
            loopLayerPrefab.transform.rotation).GetComponent<LoopLayer>();
    }

    void DestroyCurrentLayer()
    {
        Vector3 flooredLayerPos = this.transform.position; flooredLayerPos.y = 0;
        Vector3 flooredPlayerPos = bird.transform.position; flooredPlayerPos.y = 0;
        
        if (!spawnedNextLayer || Vector3.Distance(flooredPlayerPos, flooredLayerPos) < distanceToDestroyLayer)
            return;
        
        Destroy(this.gameObject);
    }
}
