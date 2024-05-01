using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask enemie;
    public LayerMask unWalk;
    public LayerMask player;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    public MeshFilter viewMeshFilter;

    Mesh viewMesh;

   
    
    public int edgeResolveIteration;
    public float edgeDstThreshold;

    public float maskCutaWayDst = .1f;

    void Start()
    {
        
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("FindTargetWithDelay", .2f);
    }



    IEnumerator FindTargetWithDelay(float Delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            FindVisibleTarget();
        }
    }

    private void LateUpdate()
    {
        DrawFiledOfView();
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, player);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, unWalk))
                {
                    IdleState idleState = gameObject.GetComponent<IdleState>();
                    idleState.isChaseRange = true;
                    Debug.Log("player");
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void DrawFiledOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoint = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            // Debug.DrawLine(transform.position, transform.position + DigFromAngle(angle, true) * viewRadius, Color.red);
            ViewCastInfo newViewCast = ViewCast(angle);


            if(i>0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if(oldViewCast.hit != newViewCast.hit ||(oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA!=Vector3.zero)
                    {
                        viewPoint.Add(edge.pointA);
                    }
                    if(edge.pointB != Vector3.zero)
                    {
                        viewPoint.Add(edge.pointB);
                    }
                }
            }

            viewPoint.Add(newViewCast.point);
            oldViewCast = newViewCast;
            
        }
        int vertexCount = viewPoint.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoint[i])+Vector3.forward*maskCutaWayDst;

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }


    EdgeInfo FindEdge(ViewCastInfo minViewCast,ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i = 0;i<edgeResolveIteration;i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - maxViewCast.dst) > edgeDstThreshold;

            if (newViewCast.hit == minViewCast.hit&& !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;

            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DigFromAngle(globalAngle, true);
        RaycastHit hit;
     
       
        if (Physics.Raycast(transform.position,dir,out hit,viewRadius,unWalk))
        {
            
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
       //if (Physics.Raycast(transform.position, dir, out hit, viewRadius, player))
       // {
            
       //     Debug.Log("Player");
       //     return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);

       // }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
       
    }
  
    public Vector3 DigFromAngle(float angleInDig,bool angleInGlobal)
    {
        if(!angleInGlobal)
        {
            angleInDig += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDig * Mathf.Deg2Rad),0, Mathf.Cos(angleInDig * Mathf.Deg2Rad));
    }
public struct ViewCastInfo
{

        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;
//Cons
        public ViewCastInfo(bool _hit,Vector3 _point,float _dst,float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
}

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA,Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
