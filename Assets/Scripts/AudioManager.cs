using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // 싱글톤 처리

    public AudioSource audioSource;
    public AudioClip clip;

    private void Awake() // 싱글톤 처리
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Scene 전환 시에도 유지
        }
        //else
        //{
        //    // 다른 Scene에 AudioManager가 있으면 첫 싱글톤만 유지 및 나머지 파괴
        //    Destroy(gameObject); 
        //}
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.Play();
    }
}

