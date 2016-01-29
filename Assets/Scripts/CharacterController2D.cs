using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    public enum CollisionFlags
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Right = 4,
        Left = 8
    }

    struct RaycastPoint
    {
        public CollisionFlags Flag;
        public Vector2 Point;
    }

    public float MaxStepHeight = 0.1f;
    [Range(0, 90)]
    public float MaxSlopeAngle = 45f;
    public int RaycastsPerSide = 10;
    public float RaycastSafeOffset = 0.01f;
    public LayerMask CollisionMask;

    private BoxCollider2D mCollider;

    private List<RaycastPoint> mRaycastPoints;

    public CollisionFlags Move(Vector2 delta)
    {
        var col = CollisionFlags.None;
        if (delta == Vector2.zero)
            return col;

        var pos = transform.position;
        var newDeltaY = delta.y;

        var newDeltaX = delta.x;
        foreach (var rp in mRaycastPoints)
        {
            if (rp.Flag == CollisionFlags.Right || rp.Flag == CollisionFlags.Left)
            {
                var deltaX = new Vector2(delta.x, 0);

                var origin = (Vector2)pos + rp.Point + (deltaX.normalized * RaycastSafeOffset);
                var direction = deltaX.normalized;
                var distance = deltaX.magnitude;

                Debug.DrawLine(origin, origin + direction * distance);
                var hit = Physics2D.Raycast(origin, direction, distance, CollisionMask);
                if (hit)
                {
                    col |= rp.Flag;
                    var d = hit.point - origin;
                    if (Mathf.Abs(d.x) < Mathf.Abs(newDeltaX))
                        newDeltaX = d.x;
                }
            }
        }
        pos.x += newDeltaX;
        transform.position = pos;

        foreach (var rp in mRaycastPoints)
        {
            if (rp.Flag == CollisionFlags.Top || rp.Flag == CollisionFlags.Bottom)
            {
                var deltaY = new Vector2(0, delta.y);

                var origin = (Vector2)pos + rp.Point + (deltaY.normalized * RaycastSafeOffset);
                var direction = deltaY.normalized;
                var distance = deltaY.magnitude;

                Debug.DrawLine(origin, origin + direction * distance);
                var hit = Physics2D.Raycast(origin, direction, distance, CollisionMask);
                if (hit)
                {
                    col |= rp.Flag;
                    var d = hit.point - origin;
                    if (Mathf.Abs(d.y) < Mathf.Abs(newDeltaY))
                        newDeltaY = d.y;
                }
            }
        }
        pos.y += newDeltaY;
        transform.position = pos;

        return col;
    }

    void Start()
    {
        mCollider = GetComponent<BoxCollider2D>();
        GenerateRaycastPoints();
    }

    void GenerateRaycastPoints()
    {
        var extentX = mCollider.size.x / 2;
        var extentY = mCollider.size.y / 2;
        var offsetX = mCollider.offset.x;
        var offsetY = mCollider.offset.y;
        mRaycastPoints = new List<RaycastPoint>();

        var minPointX = offsetX - extentX;
        var maxPointX = offsetX + extentX;
        var deltaX = (maxPointX - minPointX) / RaycastsPerSide;
        for (int i = 0; i <= RaycastsPerSide; i++)
        {
            var pointX = minPointX + deltaX * i;
            mRaycastPoints.Add(new RaycastPoint() { Flag = CollisionFlags.Top, Point = new Vector2(pointX, offsetY + extentY) });
            mRaycastPoints.Add(new RaycastPoint() { Flag = CollisionFlags.Bottom, Point = new Vector2(pointX, offsetY - extentY) });
        }

        var minPointY = offsetY - extentY;
        var maxPointY = offsetY + extentY;
        var deltaY = (maxPointY - minPointY) / RaycastsPerSide;
        for (int i = 0; i <= RaycastsPerSide; i++)
        {
            var pointY = minPointY + deltaY * i;
            mRaycastPoints.Add(new RaycastPoint() { Flag = CollisionFlags.Right, Point = new Vector2(offsetX + extentX, pointY) });
            mRaycastPoints.Add(new RaycastPoint() { Flag = CollisionFlags.Left, Point = new Vector2(offsetX - extentX, pointY) });
        }
    }
}
