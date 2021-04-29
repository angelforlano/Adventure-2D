using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    
    [Range(1, 30)]
    public int count;
    
    [Range(0f, 10f)]
    public float delay;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }
}
