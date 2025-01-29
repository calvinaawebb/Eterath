using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimReverse : MonoBehaviour
{
    public Animator back;
    public Animator cont;
    // Start is called before the first frame update
    public void closeOut()
    {
        back.GetComponent<Animation>()["back_fade_in"].speed = -1.0f;
        cont.GetComponent<Animation>()["menu_pop_out"].speed = -1.0f;
        cont.Play("menu_pop_out", 0, 0);
        back.Play("back_fade_in", 0, 0);
        back.GetComponent<Animation>()["back_fade_in"].speed = 1.0f;
        cont.GetComponent<Animation>()["menu_pop_out"].speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
