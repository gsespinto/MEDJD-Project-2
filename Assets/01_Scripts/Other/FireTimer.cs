using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTimer : MonoBehaviour
{
    [SerializeField, Min(0)] private float fireTimer;
    private float currentFireTimer;
    [SerializeField] private Slider fireSlider;
    [SerializeField] private int badEndingLevelIndex = 0;
    bool hasEnded;

    // Start is called before the first frame update
    void Start()
    {
        currentFireTimer = fireTimer;
    }

    // Update is called once per frame
    void Update()
    {
        TickTimer();
    }

    void TickTimer()
    {
        if (hasEnded)
            return;

        currentFireTimer -= Time.deltaTime;

        if (currentFireTimer <= 0)
        {
            hasEnded = true;
            LoadBadEnding();
        }

        UpdateVisuals();
    }

    void LoadBadEnding()
    {
        LoadingFunctionLibrary.LoadScene(badEndingLevelIndex);
    }

    void UpdateVisuals()
    {
        if (!fireSlider)
        {
            return;
        }

        fireSlider.value = currentFireTimer / fireTimer;
    }
}
