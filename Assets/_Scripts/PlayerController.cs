using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameManager gm;
    float _baseSpeed = 10.0f;
    float _gravity = 9.8f;
    float _jump = 0;
    GameObject playerCamera;
    float cameraRotation;
    CharacterController characterController;
    Text catText, catFoundText, buttonText, startText;
    bool catFound = false, catStatusMessage = false, started = false;
    public AudioClip catSFX, teleportSFX;
    private CameraShake cameraShake;

    void Start()
    {
        startText = GameObject.Find("UI_StartText").GetComponent<Text>();
        catText = GameObject.Find("UI_CatText").GetComponent<Text>();
        catFoundText = GameObject.Find("UI_CatFoundText").GetComponent<Text>();
        buttonText = GameObject.Find("UI_ButtonText").GetComponent<Text>();

        startText.gameObject.SetActive(false);
        catText.gameObject.SetActive(false);
        catFoundText.gameObject.SetActive(false);
        buttonText.gameObject.SetActive(false);

        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        gm = GameManager.GetInstance();

        cameraRotation = 0.0f;
        cameraShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    void Update()
    {
        if (gm.gameState == GameManager.GameState.MENU) Reset();
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (Input.GetKeyDown(KeyCode.Escape)) gm.ChangeState(GameManager.GameState.PAUSE);
        if (!started)
        {
            startText.gameObject.SetActive(true);
            StartCoroutine(DelayRemove(startText.gameObject));
            started = true;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");
        float y = 0;

        if (!characterController.isGrounded) y = -_gravity;

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;
        cameraRotation += mouse_dY * -1;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

        if (Input.GetButtonDown("Jump") && characterController.isGrounded) _jump = 2.5f;

        if (Input.GetKeyDown(KeyCode.LeftShift) && (x != 0f || z != 0f))
        {
            _baseSpeed += 5.0f;
            cameraShake.Shake(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && (x != 0f || z != 0f))
        {
            _baseSpeed -= 5.0f;
            cameraShake.Shake(false);
        }
        _jump -= _gravity * Time.deltaTime;

        direction.y = _jump;
        characterController.Move(direction * _baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);

        catText.gameObject.SetActive(catFound);
        catFoundText.gameObject.SetActive(catStatusMessage);

        if (catFound)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                AudioManager.PlaySFX(catSFX);
                catStatusMessage = true;
                gm.catStatus = true;
                GameObject cat = GameObject.Find("Cat");
                Destroy(cat);
            }
        }
    }


    void LateUpdate()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Debug.DrawRay(origin, transform.forward * 20.0f, Color.green);
        if (Physics.Raycast(origin, transform.forward, out hit, 20.0f))
        {
            catFound = hit.collider.name == "Cat" ? true : false;
        }
        else
        {
            catFound = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            gm.lifes--;
            Reset();
            gm.ChangeState(GameManager.GameState.END);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (gm.catStatus && collision.collider.tag == "Button")
        {
            catStatusMessage = false;
            buttonText.gameObject.SetActive(true);
        }
        if (gm.catStatus && collision.collider.tag == "Button")
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Reset();
                AudioManager.PlaySFX(teleportSFX);
                gm.ChangeState(GameManager.GameState.END);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (gm.catStatus && collision.collider.tag == "Button")
        {

            catStatusMessage = true;
            buttonText.gameObject.SetActive(false);
        }
    }

    IEnumerator DelayRemove(GameObject obj)
    {
        yield return (new WaitForSeconds(3));
        obj.SetActive(false);
    }

    void Reset()
    {
        gm.catStatus = false;
        catStatusMessage = false;
        catFoundText.gameObject.SetActive(catStatusMessage);
        catFound = false;
        buttonText.gameObject.SetActive(false);
        started = false;
        transform.position = new Vector3(-15.48f, 23.64f, -12.81f);
    }
}