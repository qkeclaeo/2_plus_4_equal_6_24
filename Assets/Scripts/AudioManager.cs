using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // �̱��� ó��

    [Header("BGM")]
    public AudioSource bgmSource; // bgm �ҽ�
    public AudioClip bgmClip; // bgm Ŭ��

    [Header("SFX")]
    public AudioSource[] sfxSource; // sfx �ҽ� �迭
    public AudioClip[] sfxClip; // sfx Ŭ�� �迭

    public enum Sfx // ȿ���� ����
    {
        jump, // ���� ȿ����
        sliding, // �����̵� ȿ����
        item_coin, // ���� ȹ�� ȿ����
        item_fast, // ���� ������ ȿ����
        item_slow, // ���� ������ ȿ����
        item_heal, // ȸ�� ������ ȿ����
        damage // ��ֹ� �浹 �� ȿ����
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
        bgmSource.clip = bgmClip; // Ŭ�� �Ҵ�

        bgmSource.Play(); // BGM ����
    }

    private void InitSFX() // SFX �ʱ�ȭ �޼���
    {
        sfxSource = new AudioSource[sfxClip.Length]; // sfxSource �迭 ũ�� �ʱ�ȭ

        for (int i = 0; i < sfxSource.Length; i++) // sfxSource ���� & SFX �ʱ�ȭ
        {
            sfxSource[i] = new GameObject("SFX_" + i).AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ���� ������Ʈ ����
            sfxSource[i].transform.SetParent(transform); // ������ ������Ʈ�� AudioManager�� �ڽ����� ��ġ
            sfxSource[i].playOnAwake = false; // ���� ���� ������ ��Ȱ��ȭ ����
            sfxSource[i].loop = false; // SFX ���� X
        }
    }

    public void PlaySFX(Sfx idx) // ȿ���� Ŭ�� ���
    {
        sfxSource[(int)idx].PlayOneShot(sfxClip[(int)idx]);
    }

    public void SetBGMVolume(float volume) // �����̴� ���� ���� BGM ���� ���� �� �ʱ�ȭ
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume) // �����̴� ���� ���� SFX ���� ���� �� �ʱ�ȭ
    {
        for(int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i].volume = volume;
        }
    }
}

