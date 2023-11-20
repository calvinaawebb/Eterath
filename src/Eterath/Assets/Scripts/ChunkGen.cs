using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour
{
    public GameObject chunk;
    public int chunkAmount = 9;
    // Start is called before the first frame update
    void Start()
    {
        for(int x=0;x<chunkAmount;x++) 
        {
            for(int z=0;z<chunkAmount;z++) 
            {
                GameObject o = Instantiate(chunk, transform);
                o.transform.position = new Vector3(255*x,0,255*z);
                DimensionalMapGen gen = o.GetComponent<DimensionalMapGen>();
                gen.offset = new Vector2((255*x)/gen.scale, (255*z)/gen.scale);
                gen.GenDMap();
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
