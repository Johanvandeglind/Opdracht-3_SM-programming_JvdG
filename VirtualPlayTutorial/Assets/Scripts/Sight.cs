using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VisionEventArgs
{
    public Collider collider;
}
public class Sight : MonoBehaviour
{
    const Modality MODALITY = Modality.SIGHT;
    [SerializeField] float angle = 60f;
    [SerializeField] float distance = 20f;
    [SerializeField] float aspect = 2f;
    [SerializeField] Color gizmoColor = Color.red;
    [SerializeField] LayerMask layerMask;
    internal HashSet<Collider> inSight;
    internal HashSet<Transform> visibleTargets;
    HashSet<Collider> lastInSight;
    float coneRadius;
    Collider[] overlaps;
    public delegate void VisionEventHandler(object Sender, VisionEventArgs args);
    public event VisionEventHandler OnEnterVision;
    public event VisionEventHandler OnExitVision;

    void Awake()
    {
        inSight = new HashSet<Collider>();
        coneRadius = Mathf.Tan(Mathf.Deg2Rad * angle/2f)*distance;
    }

    RaycastHit hitInfo;
    RaycastHit coneHits;
    Collider coneHitCollider;
    void FixedUpdate()
    {
        
        lastInSight = new HashSet<Collider>(inSight);
        overlaps = Physics.OverlapSphere(transform.position,distance,layerMask);

        // foreach(Collider coneHitCollider in overlaps)
        // {
        //     if (Physics.Raycast(transform.position,coneHitCollider.transform.position - transform.position, out hitInfo,distance,layerMask)
        //         && hitInfo.collider == coneHitCollider)
        //     {
        //         if(!inSight.Contains(coneHitCollider))
        //         {
        //             inSight.Add(coneHitCollider);
        //             OnEnterVision.Invoke(this,new VisionEventArgs() {collider = coneHitCollider});

        //         }
        //         else
        //         {
        //             lastInSight.Remove(coneHitCollider);
        //         }
        //     }


            

        // }
        // if (inSight != null)
        // {inSight.Clear();}
        
        foreach(Collider target in overlaps) {
            Vector3 dirToTarget = target.transform.position - transform.position;
            if ((Vector3.Angle (transform.forward, dirToTarget) < angle && Vector3.Angle (transform.forward, dirToTarget) > 0)
              ||(Vector3.Angle (transform.forward, dirToTarget) >angle && Vector3.Angle (transform.forward, dirToTarget) < 0)) {
                float dstToTarget = Vector3.Distance (transform.position, target.transform.position);
                if (Physics.Raycast (transform.position, dirToTarget, out hitInfo, layerMask)&& hitInfo.collider == target) {
                    inSight.Add(target);
                    OnEnterVision.Invoke(this,new VisionEventArgs() {collider = target});
                }
            }
        }
        foreach (Collider collider in lastInSight)
        {
            OnExitVision.Invoke(this,new VisionEventArgs() {collider = collider});
            inSight.Remove(collider);
            
        }
        
    }

    void OnDrawGizmos()
    {
        if(inSight != null)
        {
            Gizmos.color = Color.magenta;
            foreach(Collider collider in inSight)
            {
                Gizmos.DrawLine(transform.position,collider.transform.position);
            }
        }

        Gizmos.color = gizmoColor;
        Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation,transform.lossyScale);
        Gizmos.DrawFrustum(Vector3.zero, angle ,distance,.5f,aspect);
    }
}
