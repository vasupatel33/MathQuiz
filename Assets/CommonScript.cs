using UnityEngine;

public class CommonScript : MonoBehaviour
{
    public bool musicPlaying, soundPlaying;
    public static CommonScript instance;

    void Start()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {

    }
}
