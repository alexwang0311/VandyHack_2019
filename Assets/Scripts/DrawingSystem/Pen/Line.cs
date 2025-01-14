﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour {
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    private List<Vector2> points;
    public float ink_consume_speed = 0.1f;

    private void Start()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        edgeCollider.offset = new Vector2(-x, -y);
    }

    public void UpdateLine(Vector2 point, Ink ink)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(point);
            return;
        }

        if (Vector2.Distance(points.Last(), point) > 0.1f)
        {
            ink.UseInk(Vector2.Distance(points.Last(), point) * ink_consume_speed);
            Debug.Log("Remaining ink: " + ink.GetInk());
            SetPoint(point);
        }
    }

    private void SetPoint(Vector2 point)
    {
        points.Add(point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        if (points.Count > 1) {
            edgeCollider.points = points.ToArray();
        }
    }
}
