using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrow;
    public GameObject spawnArrow;
    public float interval;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            Instantiate(arrow, spawnArrow.transform.position, Quaternion.Euler(0, 0, -90));
            timer -= interval;
        }
    }
}
