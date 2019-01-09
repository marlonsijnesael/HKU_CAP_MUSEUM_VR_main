using UnityEngine;

public class F_Template : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string f_event;
    [FMODUnity.EventRef]
    public string[] _events;

    [SerializeField]
    public FMOD.Studio.EventInstance f_Instance;

    private void Awake()
    {
        f_Instance = FMODUnity.RuntimeManager.CreateInstance(f_event);
        f_Instance.start();

    }

  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeSound();
        }
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(f_Instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    private void ChangeSound()
    {
        f_Instance = FMODUnity.RuntimeManager.CreateInstance(_events[Random.Range(0,_events.Length)]);
        f_Instance.start();
    }
   
}
