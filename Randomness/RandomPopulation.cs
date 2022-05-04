using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPopulation : MonoBehaviour
{
    public GameObject prefab;
    public int numRows;
    public int numCols;
    
    void Start()
    {
        var total = numRows * numCols;
        
        for (int i = 0; i < total; i++)
        {
            var row = i / numCols;
            var col = i % numCols;
            
            var randHeight = Rand.NextGaussian(1, 0.1f, 0.8f, 1.2f);
            var person = Instantiate(prefab, new Vector3(2f * col - numCols, 1 * randHeight, 2f * row - numRows), Quaternion.identity);
            person.transform.localScale *= randHeight;
        }
    }

    void Update()
    {
        
    }
}
