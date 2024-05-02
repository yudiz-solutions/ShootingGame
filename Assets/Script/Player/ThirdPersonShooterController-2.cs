using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using JetBrains.Annotations;
public class ThirdPersonShooterController : MonoBehaviour
{
   [SerializeField] private Rig aimRig;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    private StarterAssetsInputs starterAssets;
    [SerializeField]
    private float normalSensitivity = 1;
    [SerializeField]
    private float aimSensitivity = 0.5f;
    private ThirdPersonController thirdPersonController;
    [SerializeField] private LayerMask aimColliderLayer=new LayerMask();
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform aimTarget;
    private float aimRigWeight;
    private void Start()
    {   
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssets = GetComponent<StarterAssetsInputs>();
      //  animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
         Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        Transform hitTransform = null;
     
        if(Physics.Raycast(ray, out RaycastHit hit, 1000,aimColliderLayer))
        {
           aimTarget.position = hit.point;
           mouseWorldPosition = hit.point;
           hitTransform = hit.transform;
        }   
        if (starterAssets.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20);
            
            animator.SetLayerWeight(1,Mathf.Lerp(animator.GetLayerWeight(1),1,Time.deltaTime*10));
            aimRigWeight = 1f;
            

        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1,Mathf.Lerp(animator.GetLayerWeight(1),0,Time.deltaTime*10));
            aimRigWeight = 0f;
        }

           aimRig.weight = Mathf.Lerp(aimRig.weight,aimRigWeight,Time.deltaTime*10);

        if(starterAssets.shoot && starterAssets.aim)
        {
            animator.SetBool("Shoot",true);
           Vector3 aimDir =(mouseWorldPosition - bulletSpawnPoint.position).normalized;
           Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir,Vector3.up));
           starterAssets.shoot = false;
           Invoke("StopAnimation",0.4f);
         
        }

    
       


         
       
    }
     
     private void StopAnimation()
     {
         animator.SetBool("Shoot",false);
     }

}
