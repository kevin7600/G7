using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine2D : MonoBehaviour
{

    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected EdgeCollider2D m_EdgeCollider2D;
    [SerializeField]
    protected Camera m_Camera;
    protected List<Vector2> m_Points;

    public virtual LineRenderer lineRenderer
    {
        get
        {
            return m_LineRenderer;
        }
    }

    public virtual bool addCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    public virtual EdgeCollider2D edgeCollider2D
    {
        get
        {
            return m_EdgeCollider2D;
        }
    }

    public virtual List<Vector2> points
    {
        get
        {
            return m_Points;
        }
    }
    private Joystick joystick;
    protected virtual void Awake()
    {
        joystick =  FindObjectOfType<Joystick>();
        if (m_LineRenderer == null)
        {
            Debug.LogWarning("DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer.");
            CreateDefaultLineRenderer();
        }
        if (m_EdgeCollider2D == null)
        {
            Debug.LogWarning("DrawLine: Edge Collider 2D not assigned, Adding and Using default Edge Collider 2D.");
            CreateDefaultEdgeCollider2D();
        }
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2>();
    }
    private ShootButton shootButton;
    protected virtual void Update()
    {
        shootButton = FindObjectOfType<ShootButton>();
        if (Input.GetMouseButtonDown(0) && !(System.Math.Abs(joystick.Horizontal) > 0.0 && System.Math.Abs(joystick.Vertical) > 0.0))
        {
            Reset();
        }
        if (System.Math.Abs(joystick.Horizontal) > 0.0 && System.Math.Abs(joystick.Vertical) > 0.0) {
            Debug.Log("joy stick pressed");
        } /*else if (shootButton.AttemptFire()) {
            Debug.Log("attemp fire");
        }*/ else {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
                if (!m_Points.Contains(mousePosition))
                {
                    m_Points.Add(mousePosition);
                    m_LineRenderer.positionCount = m_Points.Count;
                    m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, mousePosition);
                    if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
                    {
                        m_EdgeCollider2D.points = m_Points.ToArray();
                    }
                }
            }

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                || Input.GetMouseButton(0))
            {
                Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
                if (!m_Points.Contains(mousePosition))
                {
                    m_Points.Add(mousePosition);
                    m_LineRenderer.positionCount = m_Points.Count;
                    m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, mousePosition);
                    if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
                    {
                        m_EdgeCollider2D.points = m_Points.ToArray();
                    }
                }
            }
        }

       
    }

    protected virtual void Reset()
    {
        if (m_LineRenderer != null)
        {
            m_LineRenderer.positionCount = 0;
        }
        if (m_Points != null)
        {
            m_Points.Clear();
        }
        if (m_EdgeCollider2D != null && m_AddCollider)
        {
            m_EdgeCollider2D.Reset();
            Debug.Log("reset check");
        }

    }

    protected virtual void CreateDefaultLineRenderer()
    {
        m_LineRenderer = gameObject.AddComponent<LineRenderer>();
        m_LineRenderer.positionCount = 0;
        //m_LineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        m_LineRenderer.startColor = Color.white;
        m_LineRenderer.endColor = Color.white;
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;
        m_LineRenderer.useWorldSpace = true;
    }

    protected virtual void CreateDefaultEdgeCollider2D()
    {
        m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
    }

}