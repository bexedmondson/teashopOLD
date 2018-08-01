using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

	public List<AudioClip> clips;

    void Awake ()
    {
		//singleton time!
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);


		//SetupAudioEventListeners();
    }

	/*private void SetupAudioEventListeners()
	{
		EventManager.StartListening(AudioEvent.ShopBell, 
	}*/
	//TODO: make this work nicer please

    
    
    //Used to play single sound clips.
    public void PlaySingle(string clipName)
    {
		foreach (AudioClip clip in clips)
		{
			if (clip.name == clipName)
			{
				//Set the clip of our efxSource audio source to the clip passed in as a parameter.
				efxSource.clip = clip;

				//Play the clip.
				efxSource.Play();
				return;
			}
		}

		Debug.LogError("Clip " + clipName + " not found!");
    }
}