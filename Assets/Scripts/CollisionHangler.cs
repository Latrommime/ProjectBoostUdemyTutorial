using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHangler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] AudioClip finishAudio;
    [SerializeField] AudioClip crashAudio;

    [SerializeField] ParticleSystem finisParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource source;

    bool isTransitioning = false;
    bool isCollisionEnabled = true;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Application.isEditor) HandleDebugInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !isCollisionEnabled) return;

        switch (collision.gameObject.tag)
        {
            case "friendly":
                break;
            case "finish":
                StartSuccessSequence();
                break;
            case "obstacle":
                StartCrashSequence();
                break;
        }
    }
    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
            isCollisionEnabled = !isCollisionEnabled;
        if (Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();
    }
    void StartSuccessSequence()
    {
        source.Pause();
        source.PlayOneShot(finishAudio);
        finisParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        isTransitioning = true;
    }
    void StartCrashSequence()
    {
        source.Stop();
        source.PlayOneShot(crashAudio);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        isTransitioning = true;
    }
    void LoadNextLevel()
    {
        int nextLevel = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextLevel);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
