using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    [Header("input action")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header("components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip thrustsound;

    [Header("power values")]
    [SerializeField] private float thrustpower;
    [SerializeField] private float rotationpower;

    [Header("sfx")]
    [SerializeField] private ParticleSystem sfxmain;
    [SerializeField] private ParticleSystem sfxleft;
    [SerializeField] private ParticleSystem sfxright;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioSource = rb.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void Update()
    {
        rotationprocess();
    }

    private void rotationprocess()
    {
        float rotationvalue = rotation.ReadValue<float>();

        if (rotationvalue != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(rotationpower * rotationvalue * Time.deltaTime * Vector3.forward);
            rb.freezeRotation = false;
        }

        if (rotationvalue > -1f)
        {
            sfxright.Play();
            sfxleft.Stop();
        }
        else if (rotationvalue < 1f)
        {
            sfxleft.Play();
            sfxright.Stop();
        }
        else
        {
            sfxright.Stop();
            sfxleft.Stop();
        }
    }

    private void FixedUpdate()
    {
        Thrustprocess();
    }

    private void Thrustprocess()
    {
        if (thrust.IsPressed())
        {
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(thrustsound);
            }

            if (!sfxmain.isPlaying)
            {
                sfxmain.Play();
            }

            rb.AddRelativeForce(Vector3.up * thrustpower * Time.deltaTime);
        }
        else
        {
            AudioSource.Stop();
        }
    }
}
