using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int PlayerNumber;
    public int Speed;
    public Vector3 Move;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetAxisRaw("P" + PlayerNumber + "H") < 0)
        {
            //transform.position = new Vector2(transform.position.x - (Time.deltaTime * Speed), transform.position.y);
            Move = new Vector3(-1, Move.y, 0);
        }
        else if (Input.GetAxisRaw("P" + PlayerNumber + "H") > 0)
        {
            //transform.position = new Vector2(transform.position.x + (Time.deltaTime * Speed), transform.position.y);
            Move = new Vector3(1, Move.y, 0);
        }
        else
        {
            Move = new Vector3(0, Move.y, 0);
        }

        if (Input.GetAxisRaw("P" + PlayerNumber + "V") < 0)
        {
            //transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * Speed));
            Move = new Vector3(Move.x, -1, 0);
        }
        else if (Input.GetAxisRaw("P" + PlayerNumber + "V") > 0)
        {
            //transform.position = new Vector2(transform.position.x, transform.position.y + (Time.deltaTime * Speed));
            Move = new Vector3(Move.x, 1, 0);
        }
        else
        {
            Move = new Vector3(Move.x, 0, 0);
        }
        transform.position += Move * Speed * Time.deltaTime;

    }
}
