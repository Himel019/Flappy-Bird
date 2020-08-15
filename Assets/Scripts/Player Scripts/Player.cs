using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private AudioSource audioSource;

    [SerializeField]
    private float forwardSpeed = 3f;
    [SerializeField]
    private float bounceSpeed = 4f;

    [SerializeField]
    private AudioClip flapClip;
    [SerializeField]
    private AudioClip pointClip;
    [SerializeField]
    private AudioClip diedClip;

    private bool didFlap;
    private bool isAlive;

    private Button flapButton;

    private int score;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if(instance == null) {
            instance = this;
        }

        isAlive = true;
        score = 0;

        SetCameraOffsetX();

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => FlapTheBird());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAlive) {
            /// Vector2 speed = new Vector2(4, myRigidbody.velocity.y);
            /// myRigidbody.velocity = speed;

            Vector2 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if(Input.GetKeyDown(KeyCode.Space)) {
                didFlap = true;
            }

            if(didFlap) {
                didFlap = false;
                myRigidbody.velocity = new Vector2(0f, bounceSpeed);
                audioSource.PlayOneShot(flapClip);
                myAnimator.SetTrigger("Flap");
            }

            if(myRigidbody.velocity.y >= 0f) {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            } else {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidbody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }

    public float GetPositionX() {
        return transform.position.x;
    }

    private void SetCameraOffsetX() {
        CameraMovement.offsetX = (Camera.main.transform.position.x - GetPositionX()) - 1f;
    }

    public bool IsAlive() {
        return isAlive;
    }

    public void FlapTheBird() {
        didFlap = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Pipe") {
            if(isAlive) {
                isAlive = false;
                audioSource.PlayOneShot(diedClip);
                myAnimator.SetTrigger("Death");
                GameplayController.instance.PlayerDiedShowScore(score);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PipeHolder") {
            score++;
            GameplayController.instance.SetScore(score);
            audioSource.PlayOneShot(pointClip);
        }
    }

    public int GetScore() {
        return score;
    }
}
