using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveScript : MonoBehaviour
{

	[SerializeField]
	private float ratio = 33.75f; // 60 / (screenHeight/Screen.width)
    // Start is called before the first frame update
    void Start()
    {
		Camera.main.fieldOfView = ratio * ((float)Screen.height / (float)Screen.width) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
