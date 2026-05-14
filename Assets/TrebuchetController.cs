using UnityEngine;

public class TrebuchetController : MonoBehaviour
{
    [Header("Ссылки")]
    public Animator animator;
    public GameObject projectile;
    public Transform releasePoint;
    public Transform startPosition;
    public TrajectoryLine trajectoryLine;
    public GhostTrajectory ghostTrajectory;
    public CameraFollow cameraFollow;
    public UIManager uiManager;                    // ← Новый UI менеджер

    [Header("Сила броска")]
    [Range(15f, 65f)]
    public float currentForce = 38f;

    [Header("Угол броска")]
    [Range(-10f, 70f)]
    public float currentAngle = 35f;
    public float angleSpeed = 25f;

    private Rigidbody rbProjectile;
    private Projectile projectileScript;

    private Vector3 startPos;
    private Quaternion startRot;
    private Transform originalParent;

    private bool launched = false;

    void Start()
    {
        if (projectile != null)
        {
            rbProjectile = projectile.GetComponent<Rigidbody>();
            projectileScript = projectile.GetComponent<Projectile>();

            if (projectileScript != null)
                projectileScript.Init(this);

            originalParent = projectile.transform.parent;

            startPos = startPosition != null ? startPosition.position : projectile.transform.position;
            startRot = startPosition != null ? startPosition.rotation : projectile.transform.rotation;

            ResetProjectilePhysics();
        }

        if (trajectoryLine != null)
            trajectoryLine.Show();
    }

    void Update()
    {
        // Рисуем текущую траекторию
        if (!launched && trajectoryLine != null && projectile != null)
        {
            Vector3 startPosCurrent = projectile.transform.position;
            Vector3 velocity = GetLaunchVelocity();
            trajectoryLine.DrawTrajectory(startPosCurrent, velocity);
        }

        // Управление углом
        float angleChange = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) angleChange += angleSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) angleChange -= angleSpeed * Time.deltaTime;

        if (angleChange != 0f)
        {
            currentAngle = Mathf.Clamp(currentAngle + angleChange, -10f, 70f);
            ApplyAngle();
        }

        // Управление силой
        if (Input.GetKey(KeyCode.E)) 
            currentForce = Mathf.Min(currentForce + 30f * Time.deltaTime, 65f);
        if (Input.GetKey(KeyCode.Q)) 
            currentForce = Mathf.Max(currentForce - 30f * Time.deltaTime, 15f);

        // Обновление UI каждый кадр
        if (uiManager != null)
            uiManager.UpdateUI(currentForce, currentAngle);

        // Запуск
        if (Input.GetKeyDown(KeyCode.Space) && !launched)
            Launch();

        // Сброс
        if (Input.GetKeyDown(KeyCode.R))
            ResetProjectile();
    }

    private void ApplyAngle()
    {
        if (releasePoint != null)
        {
            Vector3 rot = releasePoint.localEulerAngles;
            rot.x = -currentAngle;
            releasePoint.localEulerAngles = rot;
        }
    }

    private Vector3 GetLaunchVelocity()
    {
        return releasePoint != null ? releasePoint.forward * currentForce : transform.forward * currentForce;
    }

    public void Launch()
    {
        launched = true;

        if (animator != null)
        {
            animator.Rebind();
            animator.Play("Armature|ArmatureAction", 0, 0f);
        }

        // Сохраняем призрачную траекторию
        if (ghostTrajectory != null && trajectoryLine != null)
        {
            Vector3 start = projectile.transform.position;
            Vector3 vel = GetLaunchVelocity();
            Vector3[] points = trajectoryLine.GetTrajectoryPoints(start, vel);
            ghostTrajectory.ShowTrajectory(points);
        }

        if (trajectoryLine != null)
            trajectoryLine.Hide();

        if (cameraFollow != null && projectile != null)
            cameraFollow.SetTarget(projectile.transform);
    }

    public void ReleaseProjectile()
    {
        if (projectile == null || rbProjectile == null) return;

        projectile.transform.SetParent(null);
        rbProjectile.isKinematic = false;
        rbProjectile.WakeUp();
        rbProjectile.linearVelocity = GetLaunchVelocity();
    }

    public void ResetProjectile()
    {
        launched = false;

        if (projectile == null) return;

        if (projectileScript != null)
            projectileScript.StopProjectile();

        projectile.transform.SetParent(originalParent);
        projectile.transform.position = startPos;
        projectile.transform.rotation = startRot;

        ResetProjectilePhysics();

        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }

        if (trajectoryLine != null)
            trajectoryLine.Show();

        if (cameraFollow != null)
            cameraFollow.ResetToDefault();
    }

    private void ResetProjectilePhysics()
    {
        if (rbProjectile == null) return;
        rbProjectile.isKinematic = true;
        rbProjectile.Sleep();
    }
}