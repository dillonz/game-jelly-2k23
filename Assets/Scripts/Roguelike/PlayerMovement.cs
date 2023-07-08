using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * 5;
        float yMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * 5;

        this.transform.position += new Vector3(xMove, yMove, 0);
    }
}
