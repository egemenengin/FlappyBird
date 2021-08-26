//Egemen Engin
//https://github.com/egemenengin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float cameraOrthSize = 50f;
    [SerializeField] float pipeWidth = 7.8f;
    private const float pipeHeadHeight = 3.75f;


    [SerializeField] static float pipeMoveSpeed = 30f;
    [SerializeField] float pipeExplodePoint = -105f;
    [SerializeField] float pipeSpawnPoint = 105f;
    
   
    private  List<Pipe> pipeList = new List<Pipe>();
    [SerializeField] float pipeSpawnTimer;
    [SerializeField] float pipeSpawnTimerMax= 1.1f;
    [SerializeField] float gapSize;
   
    private  List<Transform> groundList=new  List<Transform>();
    [SerializeField] float groundChangePoint = -280f;
    private float groundWidth = 221f;

    
  
    public void setGapSize(float newGapsize)
    {
        gapSize = newGapsize;
    }
    public void pipeMovement()
    {
        /* foreach(Pipe pipe in pipeList)
       {
           pipe.pipeMove();
       }*/
        for (int i = 0; i < pipeList.Count; i++)

        {
            Pipe pipe = pipeList[i];
            pipe.pipeMove();
            if (pipe.getXPosition() < pipeExplodePoint)
            {
                //PIPE WILL BE DESTROYED HERE.
                //pipe.DestroyItself();
                pipeList.Remove(pipe);
                //i--;
            }

        }
    }


    ///////////Ground setups, create and move///////////////
    public void spawnGround()
    {
        Transform groundTransform;
        float groundY = -47f;
        groundTransform = Instantiate(GameAssets.getInstance().Groundpf, new Vector3(0, groundY, 0), Quaternion.identity);
       
        groundList.Add(groundTransform);
        groundTransform = Instantiate(GameAssets.getInstance().Groundpf, new Vector3(groundWidth, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
        groundTransform = Instantiate(GameAssets.getInstance().Groundpf, new Vector3(groundWidth * 2, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
    }
    public void handleGround()
    {
        foreach (Transform groundTransform in groundList)
        {
            groundTransform.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;
            if (groundTransform.position.x < groundChangePoint)
            {
                float rightMostXPosition = -100f;
                for (int i = 0; i < groundChangePoint; i++)
                {
                    if (groundList[i].position.x > rightMostXPosition)
                    {
                        rightMostXPosition = groundList[i].position.x;
                    }
                }

                groundTransform.position = new Vector3(rightMostXPosition + groundWidth, groundTransform.position.y, groundTransform.position.z);
            }
        }
    }




    //////////Pipe setups, create and move/////////////////
    public bool pipeSpawn()
    {
        pipeSpawnTimer -= Time.deltaTime;
        if (pipeSpawnTimer < 0)
        {
            //Time to spawn another Pipe
            pipeSpawnTimer += pipeSpawnTimerMax;
            float height = findHeightForPipe();
            createGapPipes(height, gapSize, pipeSpawnPoint);
            return true;
        }
        return false;
    }

    public float findHeightForPipe()
    {
        float heightLimit = 10f;
        float minHeight = gapSize * 0.5f + heightLimit;
        float maxHeight = cameraOrthSize * 2f - gapSize * 0.5f - heightLimit;

        return UnityEngine.Random.Range(minHeight, maxHeight);

    }


    private void createGapPipes(float gapY, float gapSize, float xPosition)
    {
        //Bottom Pipe
        createPip(gapY - gapSize * 0.5f, xPosition, false);
        //Top pipe
        createPip(cameraOrthSize * 2f - gapY - gapSize * 0.5f, xPosition, true);
        
        

    }

    private void createPip(float height, float xPosition, bool bottomCreated)
    {

        //HeadSetup
        Transform pipeHead = Instantiate(GameAssets.getInstance().pipeHeadPF);
        pipeHead = pipeHeadSetup(pipeHead, height, xPosition, bottomCreated); ;


        //Body Setup
        Transform pipeBody = Instantiate(GameAssets.getInstance().pipeBodyPF);
        pipeBody = pipeBodySetup(pipeBody, height, xPosition, bottomCreated);


        //Change sprite render according to height
        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(pipeWidth, height);


        //Change size and offset of Body collider according to height
        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(pipeWidth, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * .5f);



        Pipe newPipe = new Pipe(pipeHead, pipeBody,pipeMoveSpeed);
        pipeList.Add(newPipe);

    }

    private Transform pipeBodySetup(Transform pipeBody, float height, float xPosition, bool bottomCreated)
    {
        float pipeBodyYposition;
        if (bottomCreated)
        {

            pipeBodyYposition = cameraOrthSize;
            pipeBody.localScale = new Vector3(1, -1, 1);

        }
        else
        {
            pipeBodyYposition = -cameraOrthSize;
        }


        pipeBody.position = new Vector3(xPosition, pipeBodyYposition);
        return pipeBody;
    }

    private Transform pipeHeadSetup(Transform pipeHead, float height, float xPosition, bool bottomCreated)
    {
        float pipeHeadYposition;
        if (bottomCreated)
        {
            pipeHeadYposition = -height + pipeHeadHeight * .5f + cameraOrthSize;

        }
        else
        {
            pipeHeadYposition = height - pipeHeadHeight * .5f - cameraOrthSize;
        }
        //Head Setup

        pipeHead.position = new Vector3(xPosition, pipeHeadYposition);
        return pipeHead;


    }
 


}
