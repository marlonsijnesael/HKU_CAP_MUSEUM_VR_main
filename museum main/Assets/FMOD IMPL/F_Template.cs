using UnityEngine;

public class F_Template : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string f_event;

    [SerializeField]
    public FMOD.Studio.EventInstance f_Instance;

    private void Awake()
    {
        f_Instance = FMODUnity.RuntimeManager.CreateInstance(f_event);
        f_Instance.start();
    }

  
    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(f_Instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

   
}
