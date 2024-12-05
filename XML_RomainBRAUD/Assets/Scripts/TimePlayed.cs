using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePlayed : MonoBehaviour
{
    public float TimePlaying;

    private void Update()
    {
        TimePlaying += Time.deltaTime;
    }

    public float Timed()
    {
        return TimePlaying;
    }

}