using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    private float despawnTimer = 0.0f;
    private bool isTiming = false;

    public float Timer
    {
        get { return despawnTimer; }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            despawnTimer += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void ResetTimer()
    {
        isTiming= false;
        despawnTimer = 0.0f;
    }
}
