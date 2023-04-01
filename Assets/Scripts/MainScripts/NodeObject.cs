using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
    //the true GameObject
    private GameObject trueObject;

    //the enemy GameObject
    private GameObject enemyObject;

    //setter for the true gameObject
    public void setTrueObject(GameObject TrueObject) 
    {
        this.trueObject = TrueObject;
    }

    //setter for the enemy gameObject
    public void setEnemyObject(GameObject EnemyObject)
    {
        this.enemyObject = EnemyObject;
    }

    //getter for the true gameObject
    public GameObject getTrueObject()
    {
        return trueObject;
    }

    //getter for the enemy gameObject
    public GameObject getEnemyObject()
    {
        return enemyObject;
    }
}
