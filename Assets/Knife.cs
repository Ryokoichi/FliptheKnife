using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knife : MonoBehaviour
{
    public Rigidbody rb;
    public float force;
    public float torque = 20f;

    private Vector2 startSwipe;
    private Vector2 endSwipe;
    private float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0)){
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
    }

    void Swipe()
    {
        rb.isKinematic = false;
        Vector2 swipe = force * ( endSwipe - startSwipe);
        Debug.Log(swipe);
        rb.AddForce(swipe ,ForceMode.Impulse);
        rb.AddTorque(0f, 0f, -torque, ForceMode.Impulse);
        timeDelay = Time.time;
    }

    void OnTriggerEnter()
    {
        rb.isKinematic = true;
    }

    private void OnCollisionEnter()
    {
        if (!rb.isKinematic)
        {
            float timeInAir = Time.time - timeDelay;
            if (!rb.isKinematic && timeDelay >= .1f)
            {
                Restart();
            }
        }
        Debug.Log("fail");
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
