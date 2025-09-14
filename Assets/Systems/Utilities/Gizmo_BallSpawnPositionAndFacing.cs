using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024

public class Gizmo_BallSpawnPositionAndFacing : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // only allows rotation of the Y axis
        if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        Gizmos.matrix = this.transform.localToWorldMatrix;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 0.5f);

        Gizmos.color = new Color(0,0,1,0.5f);
        Gizmos.DrawCube(Vector3.zero + new Vector3(0.0f, 0.0f, 0.7f), new Vector3(0.05f, 0.05f, 0.5f));
        Gizmos.DrawSphere(Vector3.zero + new Vector3(0.0f, 0.0f, 1.05f), 0.06f);

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.forward * 1.1f;

        DrawArrowEnd(startPosition, endPosition);
    }

    private void DrawArrowEnd(Vector3 start, Vector3 end)
    {
        float arrowHeadLength = 0.2f;
        float arrowHeadHeight = 0.05f;
        float arrowHeadWidth = 0.065f;

        Vector3 direction = (end - start).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Calculates positions for the rectangular shapes of the arrowhead
        Vector3 right = rotation * Quaternion.Euler(0, 180 + 45, 0) * new Vector3(0, 0, arrowHeadLength);
        Vector3 left = rotation * Quaternion.Euler(0, 180 - 45, 0) * new Vector3(0, 0, arrowHeadLength);
        //Vector3 up = rotation * Quaternion.Euler(180 + 45, 0, 0) * new Vector3(0, 0, arrowHeadLength);
        //Vector3 down = rotation * Quaternion.Euler(180 - 45, 0, 0) * new Vector3(0, 0, arrowHeadLength);

        // Draws each side of the arrowhead
        DrawArrowRectangle(end, right, arrowHeadHeight, arrowHeadWidth);
        DrawArrowRectangle(end, left, arrowHeadHeight, arrowHeadWidth);
        //DrawArrowRectangle(end, up, arrowHeadWidth, arrowHeadThickness);
        //DrawArrowRectangle(end, down, arrowHeadWidth, arrowHeadThickness);
    }

    private void DrawArrowRectangle(Vector3 end, Vector3 direction, float width, float thickness)
    {
        Vector3 position = end + direction * 1.1f;
        Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.LookRotation(direction), new Vector3(thickness, width, direction.magnitude));
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }

}















