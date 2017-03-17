using UnityEngine;
using System.Collections;

public class Zoombie : MonoBehaviour {
    GameObject hero;
    Transform target;
    float distance;
    public float moveSpeed = 0.5f;
    float rotationSpeed = 3f;
    // Use this for initialization
    void Start () {
        hero = GameObject.FindWithTag("Player");
        target = hero.transform;
    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(target.position, transform.position);
    //    transform.rotation = Quaternion.Slerp(transform.rotation,
    //Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += (target.position - transform.position) * moveSpeed * Time.deltaTime;
    }
}
