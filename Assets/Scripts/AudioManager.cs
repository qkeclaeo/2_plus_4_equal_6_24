using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // �̱��� ó��

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource; // bgm �ҽ�
    public AudioClip bgmClip; // bgm Ŭ��
    public float bgmVolume = 1f; // bgm ����

    [Header("SFX")]
    [SerializeField] private AudioSource[] sfxSource; // sfx �ҽ� �迭
    public AudioClip[] sfxClip; // sfx Ŭ�� �迭
    public float sfxVolume = 1f; // sfx ����

    public enum Sfx // ȿ���� ����
    {
        SFX_jump, // ���� ȿ����
        SFX_sliding, // �����̵� ȿ����
        SFX_item_coin, // ���� ȹ�� ȿ����
        SFX_item_fast, // ���� ������ ȿ����
        SFX_item_slow, // ���� ������ ȿ����
        SFX_item_heal // ȸ�� ������ ȿ����
    }

    private void Awake() // �̱��� ó��
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitBGM(); // BGM �ʱ�ȭ �� ����
        InitSFX(); // SFX �ʱ�ȭ
    }

    private void InitBGM() // BGM �ʱ�ȭ �޼���
    {
        bgmSource = gameObject.GetComponent<AudioSource>();
        bgmSource.playOnAwake = true; // ���� ���۰� ���ÿ� Ȱ��ȭ - �ڵ� ����
        bgmSource.loop = true; // BGM ���� O
        bgmSource.volume = bgmVolume; // BGM ���� ���� �� �ʱ�ȭ
        bgmSource.clip = bgmClip; // Ŭ�� �Ҵ�

        bgmSource.Play(); // BGM ����
    }

    private void InitSFX() // SFX �ʱ�ȭ �޼���
    {
        sfxSource = new AudioSource[sfxClip.Length]; // sfxSource �迭 ũ�� �ʱ�ȭ

        for (int i = 0; i < sfxSource.Length; i++) // SFX ���� ���� �� �ʱ�ȭ
        {
            sfxSource[i] = new GameObject("SFX_" + i).AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ���� ������Ʈ ����
            sfxSource[i].transform.SetParent(transform); // ������ ������Ʈ�� AudioManager�� �ڽ����� ��ġ
            sfxSource[i].playOnAwake = false; // ���� ���� ������ ��Ȱ��ȭ ����
            sfxSource[i].loop = false; // SFX ���� X
            sfxSource[i].volume = sfxVolume; // SFX ���� ����
        }
    }

    public void PlaySFX(Sfx idx) // ȿ���� Ŭ�� ���
    {
        sfxSource[(int)idx].PlayOneShot(sfxClip[(int)idx]);
    }
}

