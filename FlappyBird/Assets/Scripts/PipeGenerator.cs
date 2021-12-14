using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    public float maxTime = 1.0f;
    private float timer = 0;
    public GameObject pipe;
    public float height;

    

    void Update()
    {
        if(timer > maxTime)
        {
            GameObject genepipe = Instantiate(pipe);
            genepipe.transform.position = transform.position + new Vector3(0,Random.Range(-height, height));
            Destroy(genepipe,20);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
