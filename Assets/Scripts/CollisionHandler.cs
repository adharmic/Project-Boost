using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private AudioClip levelSuccess;
    [SerializeField] private AudioClip shipDestroyed;
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem crashParticles;
    private AudioSource audioSource;

    private bool isTransitioning = false;
    private bool collisionDisabled = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessCheats();
    }

    void ProcessCheats()
    {
        if(Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)) {
            Debug.Log("Collision Disabled");
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other) {
        if(!isTransitioning && !collisionDisabled) {
            switch(other.gameObject.tag) {
                case "Friendly":
                    Debug.Log("Hit friendly");
                    break;
                case "Finish":
                    LevelComplete();
                    break;
                default:
                    CrashSequence();
                    break;
            }
        }
    }

    void CrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(shipDestroyed);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void LevelComplete() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelSuccess);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
