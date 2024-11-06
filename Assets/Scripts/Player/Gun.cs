using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject gun;

    // Update is called once per frame
    void Update()
    {
        gun.transform.position = this.transform.position;
    }
}
