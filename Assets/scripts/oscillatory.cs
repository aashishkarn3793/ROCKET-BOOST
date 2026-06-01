using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Vector3 movementvector;
    [SerializeField] private float speed;
    Vector3 startpos;
    Vector3 endpos;
    float movementfactor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position;
        endpos = startpos + movementvector;
    }

    // Update is called once per frame
    private void Update()
    {
        movementfactor = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startpos, endpos, movementfactor);

    }
}
