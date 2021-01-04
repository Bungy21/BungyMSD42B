using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    //the speed of thes crolling
    [SerializeField] float scrollSpeed = 0.02f;

    //the material from the texture
    Material myMaterial;

    //movement
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        //get the material of the background from the Renderer component
        myMaterial = GetComponent<Renderer>().material;

        //scroll in the y-axis at that speed
        offset = new Vector2(0f, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //move the texture by offset every frame
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
