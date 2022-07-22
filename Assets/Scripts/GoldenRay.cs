using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRay : MonoBehaviour
{
    LineRenderer rend;
    EdgeCollider2D col;

    public List<Vector2> linePoints = new List<Vector2>();

    private void Start()
    {
        rend = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        linePoints[0] = rend.GetPosition(0);
        linePoints[1] = rend.GetPosition(1);
        col.SetPoints(linePoints);
    }
}
