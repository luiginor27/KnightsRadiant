using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour 
{

	public Transform target;
	public bool followInX;
	public bool followInY;
	public float xOffset;
	public float yOffset;

	void Update() 
	{
        if(target == null)
            return;

		float posX = followInX ? target.position.x + xOffset : transform.position.x;
		float posY = followInY ? target.position.y + yOffset : transform.position.y;
		transform.position = new Vector3(posX,posY,transform.position.z);
	}
}
