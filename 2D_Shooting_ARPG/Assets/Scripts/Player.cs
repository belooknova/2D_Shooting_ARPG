using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{

    private ActionModule action;


    // Start is called before the first frame update
    void Start()
    {
        action = GetComponent<ActionModule>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //移動
    public void Move(InputAction.CallbackContext context)
    {
        var v2 = context.ReadValue<Vector2>();
        action.valueMove = new Vector2(v2.x, 0);
        Debug.Log(string.Format("Move: {0}", v2));
    }

    //ジャンプ
    public void Jump(InputAction.CallbackContext context)
    {
        
        //if (context.ReadValueAsButton())
        {
            action.Jump();
        }
    }

}
