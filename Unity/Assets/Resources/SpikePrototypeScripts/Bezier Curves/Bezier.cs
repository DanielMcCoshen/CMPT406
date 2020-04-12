using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{

    public LineRenderer lineRenderer;

        [SerializeField]
    private int numPoints = 50;
    private Vector3[] positions;

    [Header("Transforms")]

    public Transform pointZero;
    public Transform pointOne;
    public Transform pointTwo;



    // Start is called before the first frame update
    void Start()
    {
        DrawCurve();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            DrawCurve();
        }
    }

    private void DrawCurve() {
        positions = new Vector3[numPoints];
        lineRenderer.positionCount = numPoints;
        //DrawLinearCurve();
        DrawQuadraticCurve();
    }

    private void DrawQuadraticCurve() {
        for (int i = 0; i < numPoints; i++) {
            float time = i / (float)numPoints;
            positions[i] = CalculateQuadraticBezierPoint(time, pointZero.position, pointOne.position, pointTwo.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private void DrawLinearCurve() {
        for (int i = 0; i < numPoints; i++) {
            float time = i / (float)numPoints;
            positions[i] = CalculateLinearBezierPoint(time, pointZero.position, pointOne.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateQuadraticBezierPoint(float time, Vector3 pointZero, Vector3 pointOne, Vector3 pointTwo) {

        float oneMinusTime = 1 - time;
        float timeSquared = time * time;
        float oneMinusTimeSquared = oneMinusTime * oneMinusTime;

        return ((oneMinusTimeSquared * pointZero) + (2 * oneMinusTime * time * pointOne) + (timeSquared * pointTwo));
    }

    private Vector3 CalculateLinearBezierPoint(float time, Vector3 pointZero, Vector3 pointOne) {
        return (pointZero + time * (pointOne - pointZero));
    }

    
}
