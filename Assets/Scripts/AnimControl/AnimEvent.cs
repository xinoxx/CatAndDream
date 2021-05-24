using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class AnimEvent : MonoBehaviour
{

    public GameObject lockAnim;
    public GameObject house;
    public GameObject screenFader;

    [SerializeField] private float outerSpeed = 4.0f;

    private bool isLighting = false;
    private bool isSpiraling = false;

    void Update()
    {
        if (isLighting && house != null)
        {
            float offset = outerSpeed * Time.deltaTime;
            Light2D light = house.transform.GetChild(0).GetComponent<Light2D>();
            Vector3 scale = light.transform.localScale;

            if (light.intensity < 0.5f) light.intensity += Time.deltaTime * outerSpeed;
            else light.intensity += Time.deltaTime;
            light.transform.localScale = new Vector3(scale.x + offset, scale.y + offset, scale.z);
            light.transform.localEulerAngles = new Vector3(0.0f, 0.0f, light.transform.localEulerAngles.z + offset * 15);
            if (light.intensity > 2.5f)
            {
                isLighting = false;
                Destroy(CatController.instance.GetComponent<DragCat>());
                screenFader.GetComponent<ScreenFader>().FadeTo("white");
            }
        }
    }

    public void CloseLockAnim()
    {
        if (lockAnim != null)
        {
            lockAnim.SetActive(false);
            Animator anim = house.GetComponent<Animator>();
            anim.SetBool("spiraling", true);
            anim.SetBool("openDoor", true);
        }
    }

    public void HouseLighting()
    {
        if (!isSpiraling)
        {
            isSpiraling = true;
            isLighting = true;
        }
    }
}
