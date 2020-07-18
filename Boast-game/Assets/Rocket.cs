using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcs=100f;
    [SerializeField] float mainthurst = 100f;
    Rigidbody mybody;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        audiosource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Thurst();
        Rotate();
    }

    void Thurst()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mybody.AddRelativeForce(Vector3.up*mainthurst);
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
        else
        {
            audiosource.Stop();
        }
    }
    private void Rotate()
    {
        mybody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            float rotatethisframe = rcs * Time.deltaTime;
            transform.Rotate(Vector3.forward*rotatethisframe);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            float rotatethisframe = rcs * Time.deltaTime;
            transform.Rotate(Vector3.back * rotatethisframe);
          
           
        }
        mybody.freezeRotation = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Fuel":
                //refuel
                break;
            default:
                //die
                break;

        }

    }
}


