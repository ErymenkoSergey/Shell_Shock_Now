//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Mirror;
//using UnityEngine;

//public class PlayerCharacter : NetworkBehaviour
//{

//    public static PlayerCharacter local;

//    [Header("References")]
//    [SerializeField] new Rigidbody rigidbody;

//    [Header("Prefabs")]
//    [SerializeField] GameObject droppingPrefab;

//    [SerializeField] GameObject cameraPrefab;

//    [Header("Settings")]
//    [SerializeField, Range(0, 50)] float moveSpeed = 20;

//    //InputController inputController;
//    //PlayerCamera playerCamera;

//    [Header("Interaction")]
//    [SerializeField] LayerMask interactionLayer;
//    //[SerializeField] List<InteractableBox> interactableBoxes = new List<InteractableBox>();
//    //InteractableBox holdingInteractableBox;

//    public override void OnStartClient()
//    {
//        Debug.Log($"OnStartClient");
//    }

//    public override void OnStartLocalPlayer()
//    {
//        Debug.Log($"OnStartLocalPlayer");

//        local = this;
//        //playerCamera = Instantiate(cameraPrefab).GetComponent<PlayerCamera>();

//        //inputController = new InputController();
//        //inputController.Enable();
//        RegisterInputs();
//    }

//    public override void OnStopLocalPlayer()
//    {
//        DeregisterInputs();
//        inputController.Dispose();
//    }

//    public override void OnStartServer()
//    {
//        Debug.Log($"OnStartServer");
//    }

//    /*
//        INPUTS
//    */

//    void RegisterInputs()
//    {
//        InputController.OnDroppingPerformed.AddListener(Dropping);
//        InputController.OnInteractPerformed.AddListener(Interact);
//    }

//    void DeregisterInputs()
//    {
//        InputController.OnDroppingPerformed.RemoveListener(Dropping);
//        InputController.OnInteractPerformed.RemoveListener(Interact);
//    }

//    /* 
//        CONTROLLER
//    */

//    void FixedUpdate()
//    {
//        if (isLocalPlayer)
//        {
//            if (InputController.moveVector != Vector2.zero) Move();
//        }
//    }

//    void Move()
//    {
//        Vector3 moveVelocity = ((Vector3.right * InputController.moveVector.x + Vector3.forward * InputController.moveVector.y) * 0.1f * moveSpeed) + (Vector3.up * rigidbody.angularVelocity.y);
//        moveVelocity = new Vector3(moveVelocity.z, moveVelocity.y, -moveVelocity.x);

//        rigidbody.angularVelocity = playerCamera.virtualCamera.transform.TransformDirection(moveVelocity);
//    }

//    void Dropping()
//    {
//        if (holdingInteractableBox != null)
//        {
//            //Throw the box
//            DropPickup();
//        }
//        else
//        {
//            Debug.Log($"Dropping");
//            CmdDropping();
//        }
//    }

//    [Command]
//    void CmdDropping()
//    {
//        GameObject newDropping = Instantiate(droppingPrefab);
//        newDropping.transform.position = transform.position - (transform.forward * 0.5f);

//        NetworkServer.Spawn(newDropping);
//    }

//    /* 
//        INTERACTION TRIGGER
//    */

//    void OnTriggerEnter(Collider other)
//    {
//        if (isLocalPlayer)
//        {
//            CheckInteractableBox(true, other);
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (isLocalPlayer)
//        {
//            CheckInteractableBox(false, other);
//        }
//    }

//    [Client]
//    void CheckInteractableBox(bool enter, Collider other)
//    {
//        if (interactionLayer == (interactionLayer | (1 << other.gameObject.layer)))
//        {
//            if (other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out InteractableBox interactableBox))
//            {
//                if (enter && !interactableBoxes.Contains(interactableBox))
//                {
//                    Debug.Log($"Interactable Box Added");
//                    interactableBoxes.Add(interactableBox);
//                }
//                else if (!enter && interactableBoxes.Contains(interactableBox))
//                {
//                    Debug.Log($"Interactable Box Removed");
//                    interactableBoxes.Remove(interactableBox);
//                }
//            }
//        }
//    }

//    [Client]
//    void Interact()
//    {
//        InteractableBox closestBox = null;
//        float closestBoxDist = Mathf.Infinity;
//        for (int i = 0; i < interactableBoxes.Count; i++)
//        {
//            float distSqr = (interactableBoxes[i].transform.position - transform.position).sqrMagnitude;
//            if (distSqr < closestBoxDist)
//            {
//                closestBoxDist = distSqr;
//                closestBox = interactableBoxes[i];
//            }
//        }

//        closestBox?.Interact(this);
//    }

//    [Client]
//    public void Pickup(InteractableBox interactableBox)
//    {
//        holdingInteractableBox = interactableBox;
//        CmdPickup(interactableBox.netIdentity);
//    }

//    [Command]
//    void CmdPickup(NetworkIdentity networkIdentity)
//    {
//        if (networkIdentity.AssignClientAuthority(connectionToClient))
//        {
//            networkIdentity.GetComponent<NetworkTransform>().clientAuthority = true;

//            networkIdentity.GetComponent<Rigidbody>().isKinematic = true;
//            if (networkIdentity.TryGetComponent(out InteractableBox interactableBox))
//            {
//                interactableBox.PickedUp();
//            }
//            TargetPickedUp(networkIdentity);
//        }
//    }

//    [TargetRpc]
//    void TargetPickedUp(NetworkIdentity networkIdentity)
//    {
//        if (networkIdentity.TryGetComponent(out InteractableBox interactableBox))
//        {
//            Debug.Log($"Picked Up");
//            StartCoroutine(PickupFollowPlayer(interactableBox));
//        }
//    }

//    [Client]
//    IEnumerator PickupFollowPlayer(InteractableBox interactableBox)
//    {

//        while (interactableBox.hasAuthority)
//        {
//            interactableBox.transform.position = transform.position + (Vector3.up * 2);
//            yield return null;
//        }
//    }

//    [Client]
//    void DropPickup()
//    {
//        CmdDropPickup(holdingInteractableBox.gameObject, InputController.moveVector);
//        holdingInteractableBox = null;
//    }

//    [Command]
//    void CmdDropPickup(GameObject holdingInteractableBoxGO, Vector3 moveVector)
//    {
//        holdingInteractableBoxGO.GetComponent<Rigidbody>().isKinematic = false;
//        holdingInteractableBoxGO.GetComponent<NetworkTransform>().clientAuthority = false;

//        holdingInteractableBoxGO.GetComponent<NetworkIdentity>().RemoveClientAuthority();
//        InteractableBox interactableBox = holdingInteractableBoxGO.GetComponent<InteractableBox>();

//        interactableBox.Dropped();
//        interactableBox.YeetTheBox(moveVector, 5);
//    }

//}