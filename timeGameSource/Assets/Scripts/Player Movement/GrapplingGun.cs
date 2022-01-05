using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;
    public timeStop getFrozen;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    public CharacterController2D controller;
    private Rigidbody2D platform;
    bool movingPlatform = false;

    private float gravValue;
    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private bool grappleRight;

    public bool wasFrozen = false;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        gravValue = m_rigidbody.gravityScale;

    }

    private void FixedUpdate() {
        if(movingPlatform) {
            grapplePoint+=.02f*platform.velocity;//multiplied by .02 to go from /sec to /frame
           if(!getFrozen.frozen&&launchToPoint && grappleRope.isGrappling)
                m_rigidbody.gameObject.transform.position+=new Vector3(.02f*platform.velocity.x, .02f*platform.velocity.y, 0);
        }
    }
    private void Update()
    {
        if(!getFrozen.frozen) {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetGrapplePoint();
                if(controller.m_FacingRight == !grappleRight) {
                    controller.Flip();
                }
                wasFrozen=false;
            }
            else if (Input.GetKey(KeyCode.Mouse0) || wasFrozen)
            {
                if (grappleRope.enabled)
                {
                    RotateGun(grapplePoint, false);
                }
                else
                {
                    Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                    RotateGun(mousePos, true);
                }

                if (launchToPoint && grappleRope.isGrappling)
                {
                    if (launchType == LaunchType.Transform_Launch)
                    {
                        Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                        Vector2 targetPos = grapplePoint - firePointDistnace;
                        m_rigidbody.gravityScale = 0;
                        gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                grappleRope.enabled = false;
                m_springJoint2D.enabled = false;
                m_rigidbody.gravityScale = gravValue;
                movingPlatform = false;
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }
        }
        if(!grappleRope.enabled) {
            wasFrozen = false;
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        Vector3 distanceVector2 = lookPoint - gunHolder.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        float angleFromPlayer = Mathf.Atan2(distanceVector2.y, distanceVector2.x) * Mathf.Rad2Deg;
        if(!Input.GetKey(KeyCode.Mouse0)&&!grappleRope.enabled&&!getFrozen.frozen) {
            if(angleFromPlayer >= -90 && angleFromPlayer <= 90) {
                //face right
                gunPivot.transform.localPosition = new Vector3(4.69f, 0, 0);
                grappleRight = true;
                
            }
            else {
                //face left
                gunPivot.transform.localPosition = new Vector3(-4.69f, 0, 0);
                grappleRight = false;
            }
        }
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else if(!getFrozen.frozen)
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
            }
            if(_hit.transform.gameObject.tag.Equals("pusher")) {
                movingPlatform = true;
                platform = _hit.rigidbody;
            }
            else {
                movingPlatform = false;
                platform = null;
            }
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }

}