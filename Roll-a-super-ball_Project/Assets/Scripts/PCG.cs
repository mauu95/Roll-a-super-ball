using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCG : MonoBehaviour {
    static int matrixSize = 100;
    public GameObject box;
    int[,] matrix = new int[matrixSize, matrixSize];
    int x = 7;
    int y = 0;
    string facing = "up";

    bool StopCondition(int index, int directionSign, int size, string direction) {
        //(yy, directionSign, i) => 
        //direction === 'up' ? 
        //yy <= y + directionSign * i
        // yy >= y + directionSign * i;
        //Debug.Log("condition for direction: " + direction);
        switch (direction) {
            case "up":
                return index <= y + directionSign * size;
            case "down":
                return index >= y + directionSign * size;
            case "right":
                return index <= x + directionSign * size;
            default: // left
                return index >= x + directionSign * size;
        }
    }

    bool IsIndexOutOfBound(int index) {
        return index < 0 || index >= matrixSize;
    }

    bool CanPutARoomOfSize(int i, string direction) {
        int sideMovement = (int)Mathf.Floor(i / 2); // |_ i/2 _|
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            for (int xx = x - sideMovement; xx <= x + sideMovement; xx++) {
                if (IsIndexOutOfBound(xx)) {
                    return false; // x out of bound
                }
                for (int yy = y + directionSign; StopCondition(yy, directionSign, i, direction); yy += directionSign) {
                    if (IsIndexOutOfBound(yy)) return false; // y out of bound
                    if (matrix[xx, yy] != 0) return false; // there's already something in the cell
                }
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            for (int xx = x + directionSign; StopCondition(xx, directionSign, i, direction); xx += directionSign) {
                if (IsIndexOutOfBound(xx)) return false; // x out of bound
                for (int yy = y - sideMovement; yy <= y + sideMovement; yy++) {
                    if (IsIndexOutOfBound(yy)) return false; // y out of bound
                    if (matrix[xx, yy] != 0) return false; // there's already something in the cell
                }
            }
        }
        // if you never went out of bound AND you never encountered a filled cell
        // you can put a room of size i there
        return true;
    }

    void PutARoomOfSize(int i, string direction) {
        int sideMovement = (int)Mathf.Floor(i / 2); // |_ i/2 _|
        int distanceToCenter = (int)Mathf.Ceil((float)i / 2); // |_ i/2 _|
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(x, 1, y + directionSign * distanceToCenter), Quaternion.identity);
            obj.transform.localScale = new Vector3(i, 1, i);
            for (int xx = x - sideMovement; xx <= x + sideMovement; xx++) {
                for (int yy = y + directionSign; StopCondition(yy, directionSign, i, direction); yy += directionSign) {
                    matrix[xx, yy] = 1;
                }
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(x + directionSign * distanceToCenter, 1, y), Quaternion.identity);
            obj.transform.localScale = new Vector3(i, 1, i);
            for (int xx = x + directionSign; StopCondition(xx, directionSign, i, direction); xx += directionSign) {
                for (int yy = y - sideMovement; yy <= y + sideMovement; yy++) {
                    matrix[xx, yy] = 1;
                }
            }
        }
    }

    bool CanPutABridgeOfSize(int i, string direction) {
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            for (int yy = y + directionSign; StopCondition(yy, directionSign, i, direction); yy += directionSign) {
                if (IsIndexOutOfBound(yy)) return false; // x out of bound
                if (matrix[x, yy] != 0) return false; // there's already something in the cell
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            for (int xx = x + directionSign; StopCondition(xx, directionSign, i, direction); xx += directionSign) {
                if (IsIndexOutOfBound(xx)) return false; // x out of bound
                if (matrix[xx, y] != 0) return false; // there's already something in the cell
            }
        }
        // if you never went out of bound AND you never encountered a filled cell
        // you can put a bridge of size i there
        return true;
    }

    void PutABridgeOfSize(int i, string direction) {
        int distanceToCenter = (int)Mathf.Ceil((float)i / 2); // |_ i/2 _|
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(x, 1, y + directionSign * distanceToCenter), Quaternion.identity);
            obj.transform.localScale = new Vector3(1, 1, i);
            for (int yy = y + directionSign; StopCondition(yy, directionSign, i, direction); yy += directionSign) {
                matrix[x, yy] = 2;
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(x + directionSign * distanceToCenter, 1, y), Quaternion.identity);
            obj.transform.localScale = new Vector3(i, 1, 1);
            for (int xx = x + directionSign; StopCondition(xx, directionSign, i, direction); xx += directionSign) {
                matrix[xx, y] = 2;
            }
        }
    }


    int GetNewY(int y, string oldFacing, string newFacing, int size) {
        Debug.Log("GetNewY called with" + y + oldFacing + newFacing + size);
        int distanceToCenter = (int)Mathf.Ceil((float)size / 2);
        if (oldFacing == "up" || oldFacing == "down") {
            y += oldFacing == "up" ? distanceToCenter : -distanceToCenter;
        }

        distanceToCenter -= 1;
        Debug.Log("distanceToCenter -1" + distanceToCenter);
        //moveto the position you're facing
        if (newFacing == "up" || newFacing == "down") {
            y += newFacing == "up" ? distanceToCenter : +distanceToCenter;
        }
        // console.log('end up in',x,y);

        return y;
    }


    int GetNewX(int x, string oldFacing, string newFacing, int size) {
        Debug.Log("GetNewX called with" + x + oldFacing + newFacing + size);

        // console.log('called with',x,y,oldFacing,newFacing,size);
        //moveto center of the platform
        int distanceToCenter = (int)Mathf.Ceil((float)size / 2);
        if (oldFacing == "left" || oldFacing == "right") {
            x += oldFacing == "right" ? distanceToCenter : -distanceToCenter;
        }

        distanceToCenter -= 1;
        //moveto the position you're facing
        if (newFacing == "left" || newFacing == "right") {
            x += newFacing == "right" ? distanceToCenter : -distanceToCenter;
        }
        return x;
    }


    public void Generate() {
        Debug.Log("Started");
        bool canPutARoom = false, canPutABridge = false;
        string newFacing;
        do {
            Debug.Log("Starting from position x " + x + " y " + y + " and facing" + facing);
            canPutARoom = false;
            canPutABridge = false;
            for (int size = 7; size >= 3; size -= 2) {
                Debug.Log("Can put room of size " + size + "?");
                if (CanPutARoomOfSize(size, facing)) {
                    Debug.Log("yes, putting room of size " + size);
                    canPutARoom = true;
                    PutARoomOfSize(size, facing);
                    newFacing = facing == "up" ? "right" : "up";
                    //const [newX, newY] = getXY(x, y, facing, newFacing, size)
                    x = GetNewX(x, facing, newFacing, size);
                    y = GetNewY(y, facing, newFacing, size);
                    Debug.Log("Moved to x" + x + " y " + y);
                    facing = newFacing;
                    // console.log('moved agent to', x, y);
                    break;
                }
            }
            for (int size = 7; size >= 3; size -= 2) {
                Debug.Log("Can put BRIDGE of size " + size + "?");
                if (CanPutABridgeOfSize(size, facing)) {
                    Debug.Log("yes, putting BRIDGE of size " + size);
                    // console.log('putting a bridge of size', size, 'in', x, y);
                    canPutABridge = true;
                    PutABridgeOfSize(size, facing);
                    if (facing == "up" || facing == "down") {
                        y += facing == "up" ? size : -size;
                    } else {
                        x += facing == "right" ? size : -size;
                    }
                    Debug.Log("Moved to x" + x + " y " + y);
                    break;
                }
            }
        } while (canPutARoom || canPutABridge);
    }
}
