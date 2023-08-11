using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PlayCard : MonoBehaviour
{
    private static int _leftSatelliteCards, _wonderCards, _rightSatelliteCards = 0;
    [SerializeField]
    private MeshRenderer _cardRenderer = null;
    [SerializeField]
    private GameObject _hologramObject = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Location") {
            _cardRenderer.material.color = Color.green;
            if(other.gameObject.name == "Left satellite") {
                float _xOffsetLeft = (other.transform.position.x - other.transform.lossyScale.x/4.0f) + (other.transform.lossyScale.x/4.0f * _leftSatelliteCards);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.position = new Vector3(_xOffsetLeft, 1.11f, 0.75f);
                transform.localScale = new Vector3(0.075f, 0.01f, 0.1125f);
                // GameObject.Instantiate(_hologramObject, transform.position + new Vector3(0.0f, 0.25f, 0.0f), Quaternion.identity);
                _leftSatelliteCards++;
            }
            else if(other.gameObject.name == "Right satellite") {
                float _xOffsetRight = (other.transform.position.x - other.transform.lossyScale.x/4.0f) + (other.transform.lossyScale.x/4.0f * _rightSatelliteCards);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.position = new Vector3(_xOffsetRight, 1.11f, 0.75f);
                transform.localScale = new Vector3(0.075f, 0.01f, 0.1125f);
                // GameObject.Instantiate(_hologramObject, transform.position + new Vector3(0.0f, 0.25f, 0.0f), Quaternion.identity);
                _rightSatelliteCards++;
            }
            else {
                float _xOffsetMid = (other.transform.position.x - other.transform.lossyScale.x/4.0f) + (other.transform.lossyScale.x/4.0f * _wonderCards);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.position = new Vector3(_xOffsetMid, 1.11f, 0.75f);
                transform.localScale = new Vector3(0.075f, 0.01f, 0.1125f);
                // GameObject.Instantiate(_hologramObject, transform.position + new Vector3(0.0f, 0.25f, 0.0f), Quaternion.identity);
                _wonderCards++;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
