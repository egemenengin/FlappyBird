using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pipe:MonoBehaviour
{

    private Transform Head;
    private Transform Body;
    private float pipeMoveSpeed;


    public Pipe(Transform Head, Transform Body,float pipeMoveSpeed)
    {
        this.Body = Body;
        this.Head = Head;
        this.pipeMoveSpeed = pipeMoveSpeed;
    }

 

    public void pipeMove()
    {
        /* if (Head == null)
         {
             Destroy(Body.gameObject);
             pipeList.Remove(this);
         }
         else
         {*/
        Head.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;
        Body.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;
        // }

    }
    public float getXPosition()
    {
        return Head.transform.position.x;
    }
    public void DestroyItself()
    {

        Destroy(Head.gameObject);
        Destroy(Body.gameObject);
    }
}
