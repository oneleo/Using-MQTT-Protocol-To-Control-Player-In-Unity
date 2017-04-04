//using UnityEngine;
//using System.Collections;


public class CharacterAnimator : UnityEngine.MonoBehaviour
{

    private UnityEngine.Animator animatorCharacter;
    public bool boolWalk, boolJumpUp;
    public float walkAnimatorSpeed, jumpUpAnimatorSpeed;

    // Use this for initialization
    void Start ()
    {

        //
        animatorCharacter = this.GetComponent<UnityEngine.Animator>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        animatorCharacter.SetBool("BoolWalk",boolWalk);
        animatorCharacter.SetFloat("FloatWalkSpeed", walkAnimatorSpeed);

        animatorCharacter.SetBool("BoolJumpUp", boolJumpUp);
        animatorCharacter.SetFloat("FloatJumpUpSpeed", jumpUpAnimatorSpeed);
    }



}
