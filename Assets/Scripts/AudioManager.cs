using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip jump1;
    public static AudioClip jump2;
    public static AudioClip jump3;
    public static AudioClip long_jump;
    public static AudioClip goombaDead;
    public static AudioClip coin;
    public static AudioClip upgrade;
    public static AudioClip punch1;
    public static AudioClip punch2;
    public static AudioClip kick;
    public static AudioClip take_damage;
    public static AudioClip grab;
    static AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        jump1 = Resources.Load<AudioClip>("jump1");
        jump2 = Resources.Load<AudioClip>("jump2");
        jump3 = Resources.Load<AudioClip>("jump3");
        long_jump = Resources.Load<AudioClip>("long_jump");
        goombaDead= Resources.Load<AudioClip>("goombaDead");
        coin = Resources.Load<AudioClip>("coin");
        upgrade = Resources.Load<AudioClip>("upgrade");
        punch1 = Resources.Load<AudioClip>("punch1");
        punch2 = Resources.Load<AudioClip>("punch2");
        kick = Resources.Load<AudioClip>("kick");
        take_damage = Resources.Load<AudioClip>("take_damage");
        grab = Resources.Load<AudioClip>("grab");
    }
    
        

        public static void PlaySound(string clip)
    {
        switch (clip)
        {   
            case "jump1":
                audioSrc.PlayOneShot(jump1, 0.5f);
                break;
            case "jump2":
                audioSrc.PlayOneShot(jump2, 0.3f);
                break;
            case "jump3":
                audioSrc.PlayOneShot(jump3, 0.4f);
                break;
            case "long_jump":
                audioSrc.PlayOneShot(long_jump, 0.4f);
                break;
            case "goombaDead":
                audioSrc.PlayOneShot(goombaDead, 0.4f);
                break;
            case "coin":
                audioSrc.PlayOneShot(coin, 0.4f);
                break;
            case "upgrade":
                audioSrc.PlayOneShot(upgrade, 0.4f);
                break;
            case "punch1":
                audioSrc.PlayOneShot(punch1, 0.4f);
                break;
            case "punch2":
                audioSrc.PlayOneShot(punch2, 0.4f);
                break;
            case "kick":
                audioSrc.PlayOneShot(kick, 0.4f);
                break;
            case "take_damage":
                audioSrc.PlayOneShot(take_damage, 0.4f);
                break;
            case "grab":
                audioSrc.PlayOneShot(grab, 0.4f);
                break;

        }
    }
}
