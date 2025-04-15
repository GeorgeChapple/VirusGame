using Cinemachine;
using StarterAssets;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera cinemachineCamera;
    public float BobbingSpeed = 14f;
    private float bobbingAmount;
    public float walkBobbingAmount = 0.05f;
    public float sprintBobbingAmount = 0.1f;
    public StarterAssetsInputs inputs;
    public FirstPersonController controller;

    private float targetDutch;
    private float currentDutch;

    float defaultPosY = 0;
    float timer = 0;

    public bool cameraShake;

    void Start()
    {
        defaultPosY = transform.localPosition.y;//default position to go back to if player is not moving
    }
    void Update()
    {
        if (inputs.sprint) { bobbingAmount = sprintBobbingAmount; }//changes bobbing speed to sprint
        else if (inputs.sprint == false) { bobbingAmount = walkBobbingAmount; }//changes bobbing speed to walk

        if (cameraShake)//changed by another script
        {
            targetDutch = Random.Range(-40, 40);//chooses random number
            cinemachineCamera.m_Lens.Dutch = currentDutch + (targetDutch - currentDutch) * Time.deltaTime * 15;
            //lerps to it
        }

        if (controller.Grounded)//only bobs when player is on ground
        {
            if (inputs.move.y != 0 || inputs.move.x != 0)//if player moving
            {
                timer += Time.deltaTime * BobbingSpeed; //uses time to change value
                transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
                //moves camera root object up and down
            }
            else
            {
                timer = 0;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (defaultPosY - transform.localPosition.y) * Time.deltaTime * BobbingSpeed, transform.localPosition.z);
                //stops moving camera root object up and down and lerps its position to the default
            }
            if (inputs.move.x > 0.1)
            {
                targetDutch = -3;//change target dutch to lerp to
            }
            else if (inputs.move.x < -0.1)
            {
                targetDutch = 3;//change target dutch to lerp to
            }
            else
            {
                targetDutch = 0;//change target dutch to lerp to
            }
        }//makes camera shift up and down on movement
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (defaultPosY - transform.localPosition.y) * Time.deltaTime * BobbingSpeed, transform.localPosition.z);
            targetDutch = 0;
            //resets everything
        }

        currentDutch = cinemachineCamera.m_Lens.Dutch;
        cinemachineCamera.m_Lens.Dutch = currentDutch + (targetDutch - currentDutch) * Time.deltaTime * 7;//lerp to the target dutch
    }
}
