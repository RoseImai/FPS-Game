using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    private Camera _camera;
    private PlayerUI _playerUI;
    private InputManager _inputManager;
    
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask _mask;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<PlayerLook>()._camera;
        _playerUI = GetComponent<PlayerUI>();
        _inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerUI.UpdateText(string.Empty);
        Ray _ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Debug.DrawRay(_ray.origin, _ray.direction * distance);
        RaycastHit hitInfo;

        if (Physics.Raycast(_ray, out hitInfo, distance, _mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable _interactable = hitInfo.collider.GetComponent<Interactable>();
                _playerUI.UpdateText(_interactable.promptMessage);
                if (_inputManager._onFootActions.Interact.triggered)
                {
                    _interactable.BaseInteract();
                }
            }
        }
    }
}
