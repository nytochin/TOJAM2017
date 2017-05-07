using UnityEngine;
using System.Collections;

public class ShakeCubes : MonoBehaviour {

    public float hardLimit;
    public float movementPower;
    public float smoothness;

    private float xMove;
    private float yMove;
    private float zMove;
    private Vector3 origPos;
    void Start()
    {
        origPos = transform.localPosition;
        xMove = origPos.x;
        yMove = origPos.y;
        zMove = origPos.z;
    }
    // Update is called once per frame
    void Update () {

        xMove += 1 / smoothness * (Random.value * 2 * movementPower - movementPower);
        yMove += 1 / smoothness * (Random.value * 2 * movementPower - movementPower);
        zMove += 1 / smoothness * (Random.value * 2 * movementPower - movementPower);

        if (xMove > origPos.x + hardLimit) xMove = origPos.x + hardLimit;
        if (yMove > origPos.y + hardLimit) yMove = origPos.y + hardLimit;
        if (zMove > origPos.z + hardLimit) zMove = origPos.z + hardLimit;

        if (xMove < origPos.x - hardLimit) xMove = origPos.x - hardLimit;
        if (yMove < origPos.y - hardLimit) yMove = origPos.y - hardLimit;
        if (zMove < origPos.z - hardLimit) zMove = origPos.z - hardLimit;

        transform.localPosition = new Vector3(xMove, yMove, zMove);

    }
}
