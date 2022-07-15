using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetro : MonoBehaviour
{
    public Transform [] creatParts;

    void Start()
    {
        Instantiate(creatParts[Random.Range(0, 7)], transform.position, Quaternion.identity);
    }

    public void proxPart()
    {
        Instantiate(creatParts[Random.Range(0, 7)], transform.position, Quaternion.identity);
    }
}
