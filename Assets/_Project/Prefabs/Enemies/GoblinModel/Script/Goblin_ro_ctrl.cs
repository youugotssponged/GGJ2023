using UnityEngine;
using System.Collections;

public class Goblin_ro_ctrl : MonoBehaviour {

	public enum AnimStates
	{
		walk,
		run
	}
	public AnimStates AnimationState;
	private Animator anim;
	// Use this for initialization
	void Start () {
		
		anim = gameObject.GetComponent<Animator>();
		anim.Play(AnimationState.ToString(), -1);

    }
}

