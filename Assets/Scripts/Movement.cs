using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust = 1000f;
    [SerializeField] float torque = 1000f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem thrustPaticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    Rigidbody rigidbody;
    AudioSource source;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }
    void ProcessMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetKey(KeyCode.Space) ? 1f : 0f;

        x *= torque * Time.deltaTime;
        y *= thrust * Time.deltaTime;

        rigidbody.AddRelativeTorque(Vector3.forward * (-x), ForceMode.Impulse);
        rigidbody.AddRelativeForce(Vector3.up * y, ForceMode.Impulse);

        if (x != 0f || y != 0f)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(mainEngine);
            }

            if (x > 0f && !rightThrustParticle.isPlaying)
            {
                rightThrustParticle.Play();
            }
            else if (x < 0f && leftThrustParticle.isPlaying)
            {
                leftThrustParticle.Play();
            }

            if (y != 0f && !thrustPaticle.isPlaying)
            {
                thrustPaticle.Play();
            }
        }
        else
        {
            if (x == 0)
            {
                leftThrustParticle.Stop();
                rightThrustParticle.Stop();
            }
            if (y == 0)
            {
                thrustPaticle.Stop();
            }

            source.Stop();
        }
    }
}
