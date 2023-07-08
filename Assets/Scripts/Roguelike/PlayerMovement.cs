using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int speed;

    void Start()
    {
        
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float yMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        this.transform.position += new Vector3(xMove, yMove, 0);
    }
}
