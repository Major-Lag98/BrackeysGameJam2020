using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DrawTimeCircle : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer = new LineRenderer();


    public float MinDistanceBetweenPoints = 0.1f;

    private bool _dragging = false;
    private List<Vector3> _points = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var mouse = Input.mousePosition;
            _points.Add(Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 1))); // Record as world position
            _dragging = true;

            DrawLines(_points);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_dragging)
            {
                var circle = MakeCircle(_points);

                var worldCoord = circle.Item1; // Get the world coord of our screen click
                var results = new Collider2D[10];

                // Creates a physics overlap to check what is underneath
                Physics2D.OverlapCircle(new Vector2(worldCoord.x, worldCoord.y), circle.Item2, new ContactFilter2D().NoFilter(), results);

                foreach (var collider in results)
                {
                    if (collider == null) // If we hit null, break out of our loop cause we're done
                        break;

                    if(collider.gameObject.layer == LayerMask.NameToLayer("Projectile")) // If it's a projectile
                    {
                        collider.transform.parent = null; // Clear the parent so we're outside everything
                        collider.gameObject.GetComponent<Projectile>().Ownership = 1;
                    }
                }

                _points.Clear();
                DrawLines(_points);
                _dragging = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        
    }

    private void DrawLines(List<Vector3> points)
    {
        
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        
    }

    /// <summary>
    /// Calculates a circle from the passed in points. Returns a tuple with a Vector2 as a world coordinate and a radius
    /// </summary>
    /// <returns>A Tuple with the center Vector2 as a world coord and the radius</returns>
    private Tuple<Vector3, float> MakeCircle(List<Vector3> points)
    {
        var summed = points.Aggregate((source, aggregate) => source + aggregate); // Simply add all the points together
        var center = summed / new Vector2(points.Count, points.Count); // Divide by number of points to get center
        var radius = 0f;
        points.ForEach(v => radius += Vector3.Distance(v, center));
        radius /= points.Count;

        return new Tuple<Vector3, float>(center, radius);
    }
}
