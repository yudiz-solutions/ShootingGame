using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
	public LineRenderer lineRenderer;

	public Transform target;
    public float speed = 10;
    public Vector3[] path;
	int targetIndex;
	Camera viewCamera;


	public Animator animatorPlayer;

	void Start()
	{
	
		viewCamera = Camera.main;
	}

   
      


    void Update()
	{
		// if (Input.GetMouseButton(0))
		// {
		// 	RaycastHit hit;


		// 	Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		// 	if (Physics.Raycast(ray, out hit))
		// 	{
		// 		target.transform.position =new Vector3(hit.point.x,hit.point.y,hit.point.z);
		// 		if (path != null)
		// 		{
		// 			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

		// 			animatorPlayer.SetBool("Walk", true);

		// 		}
		// 	}
        //     //Debug.DrawLine(transform.position, target.position, Color.red, 1);
         

            

            

		// }


        // lineRenderer.SetPosition(0, transform.position);
        // lineRenderer.SetPosition(1, target.transform.position);

    }


 //   private void OnTriggerEnter(Collider other)
 //   {
	//	if (other.gameObject.tag == "Enemy")

	//	{
	//		animatorPlayer.SetBool("Attack", true);
	//	}
	//}


    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful && newPath!=null)
		{
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
			

		}
	}

	IEnumerator FollowPath()
	{
		if (path != null)
		{
			Vector3 currentWaypoint = path[0];
			

			while (true)
			{


				if (transform.position == currentWaypoint)
				{
					targetIndex++;


					if (targetIndex >= path.Length)
					{
						yield break;
					}
					currentWaypoint = path[targetIndex];
					

				}

			
				transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

				if (transform.position == path[path.Length - 1])
				{
					animatorPlayer.SetBool("Walk", false);
				}

				
				yield return null;

			}
		}
	}

 


    public void OnDrawGizmos()
	{
		if (path != null)
		{

			
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.red;
				//Gizmos.DrawCube(path[i], Vector3.one);

				


				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
			
		}
		
	}
}