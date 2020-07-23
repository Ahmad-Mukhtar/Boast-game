using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcs=100f;
    [SerializeField] float mainthurst = 100f;
    [SerializeField] AudioClip mainengine;
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip death;
    Rigidbody mybody;
    AudioSource audiosource;
    enum State
    {
        Alive,
        Dead,
        trans
    }
    State state = State.Alive;
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
        if(state==State.Alive)
        {
            Thurst();
            Rotate();
        }
     
    }

    void Thurst()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mybody.AddRelativeForce(Vector3.up*mainthurst);
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(mainengine);
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
        if (state != State.Alive)
            return;


        switch(collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                Startsuccessequence();
                break;
            default:
                StartdeathSequence();
                break;

        }

    }

    private void StartdeathSequence()
    {
       // state = State.Dead;
      //  audiosource.Stop();
       // audiosource.PlayOneShot(death);
      //  Invoke("loadafterdead", 1f);
        //die
    }

    private void Startsuccessequence()
    {
        state = State.trans;
        audiosource.Stop();
        audiosource.PlayOneShot(succes);
        Invoke("Loadnxtlevel", 1f);
 
    }

   void loadafterdead()
    {
        // SceneManager.LoadScene(0);
    }
  void Loadnxtlevel()
    {

        SceneManager.LoadScene(1);
    }
}


