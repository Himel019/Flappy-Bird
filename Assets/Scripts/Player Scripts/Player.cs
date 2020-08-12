using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    [SerializeField]
    private float forwardSpeed = 3f;
    [SerializeField]
    private float bounceSpeed = 4f;
    private bool didFlap;
    private bool isAlive;

    private Button flapButton;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        if(instance == null) {
            instance = this;
        }

        isAlive = true;

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

            if(didFlap) {
                didFlap = false;
                myRigidbody.velocity = new Vector2(0f, bounceSpeed);
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

    public bool IsAlive() {
        return isAlive;
    }

    public void FlapTheBird() {
        didFlap = true;
    }
}
