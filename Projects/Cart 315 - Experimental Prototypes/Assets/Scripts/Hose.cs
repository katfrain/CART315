using System.Collections;
using UnityEngine;

public class Hose : MonoBehaviour
{
    public float defaultAngle = 90;
    public float finalAngle = 180;
    public float hoseSpeed;
    public KeyCode upKey;
    public KeyCode shootKey;
    public GameObject waterDrop;
    public Transform waterDropSpawn;
    public float fireRate;
    public bool facingRight;
    
    private Quaternion targetRotation;
    private float angle;
    private GameObject waterDropInst;
    private Coroutine fireCoroutine;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angle = transform.eulerAngles.z;
        Debug.Log(angle);
        Debug.Log($"Final: {finalAngle}, Default: {defaultAngle}, Current: {angle}");
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();
        handleShooting();

    }

    private void updatePosition()
    {
        
        if (facingRight)
        {
            if (Input.GetKey(upKey) && angle < finalAngle)
            {
                angle += hoseSpeed * Time.deltaTime;;
            }
            else if (angle > defaultAngle)
            {
                angle -= hoseSpeed * Time.deltaTime;;
            }
        }
        else
        {
            if (Input.GetKey(upKey) && angle > finalAngle)
            {
                angle -= hoseSpeed * Time.deltaTime;;
            }
            else if (angle < defaultAngle)
            {
                angle += hoseSpeed * Time.deltaTime;;
            }
        }
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void handleShooting()
    {
        if (Input.GetKey(shootKey) && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        targetRotation = transform.rotation;
        if (facingRight)
        {
            targetRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + 90);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90);
        }
        waterDropInst = Instantiate(waterDrop, waterDropSpawn.position, targetRotation);  
        yield return new WaitForSeconds(fireRate);
        fireCoroutine = null;
    }
}

