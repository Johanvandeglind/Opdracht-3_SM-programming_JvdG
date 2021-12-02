using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] Sight sight;
    void Awake()
    {
        sight.OnEnterVision += EnterVision;
        sight.OnExitVision += ExitVision;
    }

    void EnterVision(object sender, VisionEventArgs args)
    {
        Debug.Log($"{gameObject.name} sighted: {args.collider.gameObject.name}");
    }
    void ExitVision(object sender, VisionEventArgs args)
    {
        Debug.Log($"{args.collider.gameObject.name} has left sight of {gameObject.name}");
    }

    void FixedUpdate()
    {
        // TODO moving agent
        // transform.Translate(new Vector3(Time.fixedDeltaTime*2,0f,0f));
        UpdateState();
        Reason();
        Act();
    }
    void UpdateState()
    {
        foreach (Collider collider in sight.inSight)
        {
            // TODO update state (remember enemy)
        }
    }
    void Reason()
    {
        //TODO Reason (think about it)
    }
    void Act()
    {
        //TODO Act (invade,atack,run away)
    }
}
