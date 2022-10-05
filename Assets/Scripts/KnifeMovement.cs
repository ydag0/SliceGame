using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
[RequireComponent(typeof(Rigidbody))]
public class KnifeMovement : MonoBehaviour
{
    public static KnifeMovement Instance;
    #region Variables
    public Stopwatch stopWatch = new Stopwatch();

    Rigidbody rigidB;
    Vector3 backForceVector;
    Vector3 backTorqueVector;
    bool shouldJump;
    bool backJump;
    bool canMove = false;
    #endregion

    #region TagControl&Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (!this.CompareTag(Tags.knifeTag))
        {
            this.tag = Tags.knifeTag;
        }


    }
    #endregion
    private void Start()
    {
        backForceVector = new Vector3(-GameSettings.Instance.settings.forceVector.x, GameSettings.Instance.settings.forceVector.y, 
            GameSettings.Instance.settings.forceVector.z);
        backTorqueVector = new Vector3(GameSettings.Instance.settings.torqueVector.x, GameSettings.Instance.settings.torqueVector.y,
            -GameSettings.Instance.settings.torqueVector.z);
        rigidB = GetComponent<Rigidbody>();

        //set pivot
        rigidB.centerOfMass = Vector3.zero;
        Physics.gravity = GameSettings.Instance.settings.gravity * Vector3.down;
        //
        stopWatch.Start();
    }
    private void Update()
    {
        if (canMove)
        {
            CheckInput();
        }
        
        
    }
    private void LateUpdate()
    {
        transform.SetLocalEulerZ(transform.localEulerAngles.z);
    }
    private void FixedUpdate()
    {
        if (shouldJump)
        {
            Jump();
        }
#if UNITY_EDITOR
        if (backJump)
        {
            GoBackJump();
        }
#endif
    }
    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldJump = true;
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            backJump = true;
        }
#endif

    }
    void Jump()
    {
        stopWatch.Restart();
        rigidB.isKinematic = false;
        stopWatch.Restart();
        rigidB.AddForce(GameSettings.Instance.settings.forcePower * GameSettings.Instance.settings.forceVector, 
            GameSettings.Instance.settings.forceType);
        rigidB.AddTorque( GameSettings.Instance.settings.torqueVector ,
            GameSettings.Instance.settings.torqueForceType);
        shouldJump = false;

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.gameOverTag))
        {
            UIManager.Instance.Died();
        }
        else if (collision.gameObject.CompareTag(Tags.levelEndTag))
        {
            canMove = false;
            rigidB.isKinematic = true;
            UIManager.Instance.LevelEnd();
            this.gameObject.GetComponentInChildren<dispersePixels>().enabled = true;
            collision.gameObject.GetComponentInChildren<dispersePixels>().enabled = true;
        }
    }


    public void GoBackJump()
    {
        rigidB.velocity = Vector3.zero;
        rigidB.AddForce(GameSettings.Instance.settings.forcePower * backForceVector, GameSettings.Instance.settings.forceType);
        rigidB.AddTorque( backTorqueVector, GameSettings.Instance.settings.torqueForceType);
        backJump = false;
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
