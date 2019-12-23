using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisonController : MonoBehaviour
{
    
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject BISON;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate bison.
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                GameObject bison = Instantiate(BISON, new Vector3(i, 1, j), Quaternion.identity);
                bison.transform.parent = gameObject.transform;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
