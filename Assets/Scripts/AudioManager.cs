using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // �̱��� ó��

    public AudioSource audioSource;
    public AudioClip clip;

    private void Awake() // �̱��� ó��
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Scene ��ȯ �ÿ��� ����
        }
        //else
        //{
        //    // �ٸ� Scene�� AudioManager�� ������ ù �̱��游 ���� �� ������ �ı�
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

