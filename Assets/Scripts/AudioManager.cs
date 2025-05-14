using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // 싱글톤 처리

    [Header("BGM")]
    public AudioSource bgmSource; // bgm 소스
    public AudioClip bgmClip; // bgm 클립

    [Header("SFX")]
    public AudioSource[] sfxSource; // sfx 소스 배열
    public AudioClip[] sfxClip; // sfx 클립 배열

    public enum Sfx // 효과음 종류
    {
        jump, // 점프 효과음
        sliding, // 슬라이딩 효과음
        item_coin, // 코인 획득 효과음
        item_fast, // 가속 아이템 효과음
        item_slow, // 감속 아이템 효과음
        item_heal, // 회복 아이템 효과음
        damage // 장애물 충돌 시 효과음
    }

    private void Awake() // 싱글톤 처리
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitBGM(); // BGM 초기화 및 실행
        InitSFX(); // SFX 초기화
    }

    private void InitBGM() // BGM 초기화 메서드
    {
        bgmSource = gameObject.GetComponent<AudioSource>();
        bgmSource.playOnAwake = true; // 게임 시작과 동시에 활성화 - 자동 실행
        bgmSource.loop = true; // BGM 루프 O
        bgmSource.clip = bgmClip; // 클립 할당

        bgmSource.Play(); // BGM 실행
    }

    private void InitSFX() // SFX 초기화 메서드
    {
        sfxSource = new AudioSource[sfxClip.Length]; // sfxSource 배열 크기 초기화

        for (int i = 0; i < sfxSource.Length; i++) // sfxSource 생성 & SFX 초기화
        {
            sfxSource[i] = new GameObject("SFX_" + i).AddComponent<AudioSource>(); // AudioSource 컴포넌트를 가진 오브젝트 생성
            sfxSource[i].transform.SetParent(transform); // 생성된 오브젝트를 AudioManager의 자식으로 배치
            sfxSource[i].playOnAwake = false; // 게임 시작 시점엔 비활성화 상태
            sfxSource[i].loop = false; // SFX 루프 X
        }
    }

    public void PlaySFX(Sfx idx) // 효과음 클립 재생
    {
        sfxSource[(int)idx].PlayOneShot(sfxClip[(int)idx]);
    }

    public void SetBGMVolume(float volume) // 슬라이더 값에 따라 BGM 볼륨 설정 및 초기화
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume) // 슬라이더 값에 따라 SFX 볼륨 설정 및 초기화
    {
        for(int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i].volume = volume;
        }
    }
}

