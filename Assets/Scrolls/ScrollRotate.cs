using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ScrollRotate : MonoBehaviour
{
    public GameObject scroll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scroll.transform.Rotate(0, 3, 0, Space.World);
    }
}
