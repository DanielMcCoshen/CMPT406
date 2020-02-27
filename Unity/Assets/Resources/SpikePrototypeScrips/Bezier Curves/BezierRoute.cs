using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    private float curveMagnitude;

    private Vector2 gizmosPosition;

    private void Start() {
    }

    public void SetStartAndEnd(Transform start, Transform end) {
        controlPoints[0].position = start.position;
        
        Vector2 dir = (end.position - start.position);
        float distance = Vector2.Distance(start.position, end.position);
        Vector2 midpoint = dir/2;
        //Debug.Log(midpoint);

        controlPoints[3].position = new Vector2(start.position.x + distance, start.position.y);

        curveMagnitude = midpoint.x;

        controlPoints[1].localPosition = new Vector2(controlPoints[0].localPosition.x,  curveMagnitude);
        controlPoints[2].localPosition = new Vector2(controlPoints[3].localPosition.x, curveMagnitude);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Fix offset.

        Vector2 offset = (controlPoints[0].position - start.position);
        controlPoints[0].position = new Vector2(controlPoints[0].position.x + offset.x, controlPoints[0].position.y + offset.y);
    }

    private void OnDrawGizmos() {
        for (float t = 0; t <= 1; t += 0.05f) {
            gizmosPosition = Mathf.Pow(1-t,3) * controlPoints[0].position +
                3 * Mathf.Pow(1-t,2) * t * controlPoints[1].position +
                3 * (1-t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;
            
            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }    
        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
            new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
            new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
    }
}
