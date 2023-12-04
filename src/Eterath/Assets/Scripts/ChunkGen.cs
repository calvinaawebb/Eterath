using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour
{
    public DimensionalMapGen mapref;
    public GameObject chunk;
    public int chunkAmount = 9;
    // Start is called before the first frame update
    void Start()
    {
        mapref = new DimensionalMapGen();
        transform.position = new Vector3((mapref.xbound*1.5f)*-1, -50, (mapref.zbound*1.5f)*-1);
        for(int x=0;x<chunkAmount;x++) 
        {
            for(int z=0;z<chunkAmount;z++) 
            {
                GameObject o = Instantiate(chunk, transform);
                o.transform.localPosition = new Vector3(mapref.xbound*x,0,mapref.zbound*z);
                DimensionalMapGen gen = o.GetComponent<DimensionalMapGen>();
                gen.offset = new Vector2((mapref.xbound*x)/gen.scale, (mapref.zbound*z)/gen.scale);
                gen.GenDMap();
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
