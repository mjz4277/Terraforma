using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    float speed = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 f = transform.rotation * Vector3.forward;
        f = Vector3.ProjectOnPlane(f, Vector3.up);
        f.Normalize();
        Vector3 r = transform.rotation * Vector3.right;
        r = Vector3.ProjectOnPlane(r, Vector3.up);
        r.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + f, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - r, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - f, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + r, speed * Time.deltaTime);
        }
	}
}
