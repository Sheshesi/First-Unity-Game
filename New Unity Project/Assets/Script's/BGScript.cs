using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var height = Camera.main.orthographicSize * 2.5f;
        var width = height * Screen.width / Screen.height;

        if(gameObject.name == "Background")
        {
            transform.localScale = new Vector3(width, height, 0);
        }
    }

}
