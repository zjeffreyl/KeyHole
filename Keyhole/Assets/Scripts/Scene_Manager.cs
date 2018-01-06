using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Manager : MonoBehaviour
{

    public void jump_to(string level_name)
    {
        Application.LoadLevel(level_name);
    }

}
