using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class collisionscript : MonoBehaviour
{
    [SerializeField] float delaytime = 1.5f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sfxdestroy;
    [SerializeField] AudioClip sfxfinish;
    [SerializeField] ParticleSystem vfxcrash;
    [SerializeField] ParticleSystem vfxfinish;
    bool iscrashed = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            nextlevel();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            iscrashed = !iscrashed;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (iscrashed)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "friendly":
                Debug.Log("friendly");
            break;
            case "fuel":
                Debug.Log("fuel");
            break;
            case "Finish":
                Startfinishsequence();
            break;
                default:
                Startcrashsequence();
                break;  
        }

    }
    private void Startfinishsequence()
    {
        iscrashed = true;
        vfxfinish.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(sfxfinish);
        GetComponent<movement>().enabled = false;
        Invoke("nextlevel", delaytime);

    }
    private void Startcrashsequence()
    {
        iscrashed = true;
        vfxcrash.Play(); 
        audioSource.PlayOneShot(sfxdestroy);
        GetComponent<movement>().enabled = false;
        Invoke("reload", delaytime);
        
    }
    private void nextlevel()
    {
        int currentindex = SceneManager.GetActiveScene().buildIndex;
        int nextindex = currentindex + 1;
        if (currentindex == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextindex = 0;
        }
            
        SceneManager.LoadScene(nextindex);
    }
    private void reload()
    {
        int currentindex= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentindex);
    }

}
