using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager m_instance;
    AudioSource m_AudioSource;

    private void Awake()
    {
  
    }

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.Play();
    }

   
}
