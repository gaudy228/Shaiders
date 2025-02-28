using UnityEngine;
public class Wobble : MonoBehaviour
{
    private Renderer renderer;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 AngularVelocity;
    [SerializeField] private float MaxWobble = 0.03f;
    [SerializeField] private float SpeedWobble = 1f;
    [SerializeField] private float recovery = 1f;

    private float wobbleAmountX;
    private float wobbleAmountZ;
    private float wobbleAmountToAddX;
    private float wobbleAmountToAddZ;
    private float pulse;
    private float time = 0.5f;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        time += Time.deltaTime;

        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * recovery);
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * recovery);


        pulse = 2 * Mathf.PI * SpeedWobble;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);

        renderer.material.SetFloat("_WobbleX", wobbleAmountX);
        renderer.material.SetFloat("_WobbleZ", wobbleAmountZ);

        velocity = (lastPos - transform.position) / Time.deltaTime;
        AngularVelocity = transform.rotation.eulerAngles - lastRot;



        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (AngularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (AngularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }
}
