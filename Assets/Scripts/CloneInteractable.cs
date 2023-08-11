using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

using Oculus.Interaction.HandGrab;

public class CloneInteractable : MonoBehaviour
{
    private bool _canGrabCard = true;
    [SerializeField]
    private GameObject _cardObject = null;
    [SerializeField]
    private MeshRenderer _deckRenderer = null;
    [SerializeField]
    private HandGrabInteractor _handGrabInteractor = null;
    [SerializeField]
    private Transform _handTransform;

    public CharacterData[] characterDatas;


    void Start()
    {
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "RightHandCollider") {
            _deckRenderer.material.color = Color.green;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "RightHandCollider") {
            _deckRenderer.material.color = Color.red;
        }
    }

    public void GrabCard() {
        StartCoroutine(DelayedDraw());
        _canGrabCard = true;
    }

    IEnumerator DelayedDraw() {
        if(_canGrabCard) {
            var _handPos = new Vector3(_handTransform.position.x, _handTransform.position.y, _handTransform.position.z);
            Quaternion _handRot = Quaternion.Euler(new Vector3(_handTransform.rotation.x, _handTransform.rotation.y, _handTransform.rotation.z));
            _canGrabCard = false;
            GameObject cards = GameObject.Instantiate(_cardObject, _handPos, _handRot);
            cards.GetComponent<CardBehaviour>().PopulateCard(characterDatas[Random.Range(0, characterDatas.Length)]);
        }
        _deckRenderer.material.color = Color.blue;
        _handGrabInteractor.ForceRelease();
        yield return new WaitForSeconds(0.5f);
    }
}
