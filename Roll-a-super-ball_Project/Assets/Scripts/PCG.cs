using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCG : MonoBehaviour {
    public int matrixSize = 100;
    public GameObject box;
    public GameObject container;
    int[,] matrix;
    int x = 7;
    int y = 0;

    string facing = "up";

    bool StopCondition(int index, int directionSign, int size, string direction, int currentX, int currentY) {
        //(yy, directionSign, i) => 
        //direction === 'up' ? 
        //yy <= y + directionSign * i
        // yy >= y + directionSign * i;
        //Debug.Log("condition for direction: " + direction);
        switch (direction) {
            case "up":
                return index <= currentY + directionSign * size;
            case "down":
                return index >= currentY + directionSign * size;
            case "right":
                return index <= currentX + directionSign * size;
            default: // left
                return index >= currentX + directionSign * size;
        }
    }

    bool IsIndexOutOfBound(int index) {
        return index < 0 || index >= matrixSize;
    }

    bool CanPutARoomOfSize(int currentX, int currentY, int i, string direction) {
        int sideMovement = (int)Mathf.Floor(i / 2); // |_ i/2 _|
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            for (int xx = currentX - sideMovement; xx <= currentX + sideMovement; xx++) {
                if (IsIndexOutOfBound(xx)) {
                    return false; // x out of bound
                }
                for (int yy = currentY + directionSign; StopCondition(yy, directionSign, i, direction, currentX, currentY); yy += directionSign) {
                    if (IsIndexOutOfBound(yy)) return false; // y out of bound
                    if (matrix[xx, yy] != 0) return false; // there's already something in the cell
                }
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            for (int xx = currentX + directionSign; StopCondition(xx, directionSign, i, direction, currentX, currentY); xx += directionSign) {
                if (IsIndexOutOfBound(xx)) return false; // x out of bound
                for (int yy = currentY - sideMovement; yy <= currentY + sideMovement; yy++) {
                    if (IsIndexOutOfBound(yy)) return false; // y out of bound
                    if (matrix[xx, yy] != 0) return false; // there's already something in the cell
                }
            }
        }
        // if you never went out of bound AND you never encountered a filled cell
        // you can put a room of size i there
        return true;
    }

    void PutARoomOfSize(int currentX, int currentY, int i, string direction) {
        int sideMovement = (int)Mathf.Floor(i / 2); // |_ i/2 _|
        int distanceToCenter = (int)Mathf.Ceil((float)i / 2); // |_ i/2 _|
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(currentX, 1, currentY + directionSign * distanceToCenter), Quaternion.identity, container.transform);
            obj.transform.localScale = new Vector3(i, 1, i);
            for (int xx = currentX - sideMovement; xx <= currentX + sideMovement; xx++) {
                for (int yy = currentY + directionSign; StopCondition(yy, directionSign, i, direction, currentX, currentY); yy += directionSign) {
                    matrix[xx, yy] = 1;
                }
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(currentX + directionSign * distanceToCenter, 1, currentY), Quaternion.identity, container.transform);
            obj.transform.localScale = new Vector3(i, 1, i);
            for (int xx = currentX + directionSign; StopCondition(xx, directionSign, i, direction, currentX, currentY); xx += directionSign) {
                for (int yy = currentY - sideMovement; yy <= currentY + sideMovement; yy++) {
                    matrix[xx, yy] = 1;
                }
            }
        }
    }

    bool CanPutABridgeOfSize(int currentX, int currentY, int i, string direction) {
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            for (int yy = currentY + directionSign; StopCondition(yy, directionSign, i, direction, currentX, currentY); yy += directionSign) {
                if (IsIndexOutOfBound(yy)) return false; // x out of bound
                if (matrix[currentX, yy] != 0) return false; // there's already something in the cell
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            for (int xx = currentX + directionSign; StopCondition(xx, directionSign, i, direction, currentX, currentY); xx += directionSign) {
                if (IsIndexOutOfBound(xx)) return false; // x out of bound
                if (matrix[xx, currentY] != 0) return false; // there's already something in the cell
            }
        }
        // if you never went out of bound AND you never encountered a filled cell
        // you can put a bridge of size i there
        return true;
    }

    void PutABridgeOfSize(int currentX, int currentY, int i, string direction) {
        int distanceToCenter = (int)Mathf.Ceil((float)i / 2); // |_ i/2 _|
        if (direction == "up" || direction == "down") {
            int directionSign = direction == "up" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(currentX, 1, currentY + directionSign * distanceToCenter), Quaternion.identity, container.transform);
            obj.transform.localScale = new Vector3(1, 1, i);
            for (int yy = currentY + directionSign; StopCondition(yy, directionSign, i, direction, currentX, currentY); yy += directionSign) {
                matrix[currentX, yy] = 2;
            }
        } else {
            int directionSign = direction == "right" ? 1 : -1;
            GameObject obj = Instantiate(box, new Vector3(x + directionSign * distanceToCenter, 1, currentY), Quaternion.identity, container.transform);
            obj.transform.localScale = new Vector3(i, 1, 1);
            for (int xx = currentX + directionSign; StopCondition(xx, directionSign, i, direction, currentX, currentY); xx += directionSign) {
                matrix[xx, currentY] = 2;
            }
        }
    }


    int GetNewY(int y, string facing, int size, bool alreadyInCenter) {
        Debug.Log("GetNewY called with" + y + facing + size + alreadyInCenter.ToString());
        int distanceToCenter = (int)Mathf.Ceil((float)size / 2);
        if (alreadyInCenter) {
            distanceToCenter -= 1;
        }
        if (facing == "up" || facing == "down") {
            y += facing == "up" ? distanceToCenter : -distanceToCenter;
        }

        return y;
    }


    int GetNewX(int x, string facing, int size, bool alreadyInCenter) {
        Debug.Log("GetNewX called with" + x + facing + size + alreadyInCenter.ToString());
        int distanceToCenter = (int)Mathf.Ceil((float)size / 2);
        if (alreadyInCenter) {
            distanceToCenter -= 1;
        }
        Debug.Log("Distance to center " + distanceToCenter);
        if (facing == "left" || facing == "right") {
            x += facing == "right" ? distanceToCenter : -distanceToCenter;
        }

        return x;
    }

    int[] LookAhead(int startingX, int startingY, string nowFacing) {
        int[] sizes;
        for (int bridgeSize = 7; bridgeSize >= 3; bridgeSize -= 2) {
            int currentX = startingX, currentY = startingY;
            Debug.Log("Can put BRIDGE of size " + bridgeSize + "?");
            if (CanPutABridgeOfSize(currentX, currentY, bridgeSize, nowFacing)) {
                Debug.Log("yes, putting BRIDGE of size " + bridgeSize);
                if (nowFacing == "up" || nowFacing == "down") {
                    currentY += nowFacing == "up" ? bridgeSize : -bridgeSize;
                } else {
                    currentX += nowFacing == "right" ? bridgeSize : -bridgeSize;
                }
                Debug.Log("Moved lookahead agento to " + currentX + " - " + currentY);
                for (int platformSize = 7; platformSize >= 3; platformSize -= 2) {
                    Debug.Log("Can put room of size " + platformSize + "?");
                    if (CanPutARoomOfSize(currentX, currentY, platformSize, nowFacing)) {
                        Debug.Log("yes, putting room of size " + platformSize);
                        sizes = new int[2];
                        sizes[0] = bridgeSize;
                        sizes[1] = platformSize;
                        return sizes;
                    }
                }

            }
        }
        return null;
    }

    void ShuffleStrings(string[] strings, int length) {
        for (int i = 0; i < length; i++) {
            int index = (int)Mathf.Floor(Random.value * length);
            string temp = strings[i];
            strings[i] = strings[index];
            strings[index] = temp;
        }
    }

    public void Generate() {
        Debug.Log("Started");

        foreach (Transform t in container.transform) {
            Destroy(t.gameObject);
        }

        matrix = new int[matrixSize, matrixSize];
        x = 7;
        y = 0;

        //bool canPutARoom = false, canPutABridge = false;
        string newFacing;
        int lastRoomSize = 0;
        string[] availableDirection = { "up", "down", "left", "right" };
        // put first room
        for (int size = 7; size >= 3; size -= 2) {
            Debug.Log("Can put room of size " + size + "?");
            if (CanPutARoomOfSize(x, y, size, facing)) {
                Debug.Log("yes, putting room of size " + size);
                PutARoomOfSize(x, y, size, facing);
                //const [newX, newY] = getXY(x, y, facing, newFacing, size)

                //center of the platform
                x = GetNewX(x, facing, size, false);
                y = GetNewY(y, facing, size, false);
                lastRoomSize = size;
                //x = GetNewX(x, facing, size, true);
                //y = GetNewY(y, facing, size, true);
                Debug.Log("Moved to x" + x + " y " + y);
                // console.log('moved agent to', x, y);
                break;
            }
        }
        bool didAMove = false;
        do {
            didAMove = false;
            Debug.Log("Starting from position x " + x + " y " + y + " and facing" + facing);
            ShuffleStrings(availableDirection, availableDirection.Length);
            string str = "----------";
            foreach (string dir in availableDirection) {
                str += " " + dir;
            }
            Debug.Log(str);
            foreach (string lookingAt in availableDirection) {
                //newFacing = facing == "up" ? "right" : "up";
                int tempX = GetNewX(x, lookingAt, lastRoomSize, true);
                int tempY = GetNewY(y, lookingAt, lastRoomSize, true);
                Debug.Log(lookingAt + " Moved to x" + tempX + " y " + tempY);
                //facing = newFacing;


                int[] sizes = LookAhead(tempX, tempY, lookingAt);
                if (sizes == null) {
                    Debug.Log("Can't put a bridge and a room in this direction: continue! " + lookingAt);
                    continue;
                    //return;
                } else {
                    Debug.Log(sizes.ToString() + sizes[0] + sizes[1]);
                }
                x = tempX;
                y = tempY;

                PutABridgeOfSize(x, y, sizes[0], lookingAt);
                if (lookingAt == "up" || lookingAt == "down") {
                    y += lookingAt == "up" ? sizes[0] : -sizes[0];
                } else {
                    x += lookingAt == "right" ? sizes[0] : -sizes[0];
                }

                PutARoomOfSize(x, y, sizes[1], lookingAt);
                x = GetNewX(x, lookingAt, sizes[1], false);
                y = GetNewY(y, lookingAt, sizes[1], false);
                lastRoomSize = sizes[1];
                didAMove = true;
                break;
            }

            if (!didAMove) {
                Debug.Log("Can't put a bridge and a room in every direction: finishing! ");
                //break;
            }

            //for (int size = 7; size >= 3; size -= 2) {
            //    Debug.Log("Can put room of size " + size + "?");
            //    if (CanPutARoomOfSize(size, facing)) {
            //        Debug.Log("yes, putting room of size " + size);
            //        canPutARoom = true;
            //        PutARoomOfSize(size, facing);
            //        newFacing = facing == "up" ? "right" : "up";
            //        //const [newX, newY] = getXY(x, y, facing, newFacing, size)
            //        x = GetNewX(x, facing, newFacing, size);
            //        y = GetNewY(y, facing, newFacing, size);
            //        Debug.Log("Moved to x" + x + " y " + y);
            //        facing = newFacing;
            //        // console.log('moved agent to', x, y);
            //        break;
            //    }
            //}
            //for (int size = 7; size >= 3; size -= 2) {
            //    Debug.Log("Can put BRIDGE of size " + size + "?");
            //    if (CanPutABridgeOfSize(size, facing)) {
            //        Debug.Log("yes, putting BRIDGE of size " + size);
            //        // console.log('putting a bridge of size', size, 'in', x, y);
            //        canPutABridge = true;

            //        Debug.Log("Moved to x" + x + " y " + y);
            //        break;
            //    }
            //}
        } while (didAMove);
    }


    //public void Generate() {
    //    Debug.Log("Started");
    //    bool canPutARoom = false, canPutABridge = false;
    //    string newFacing;
    //    do {
    //        Debug.Log("Starting from position x " + x + " y " + y + " and facing" + facing);
    //        canPutARoom = false;
    //        canPutABridge = false;
    //        for (int size = 7; size >= 3; size -= 2) {
    //            Debug.Log("Can put room of size " + size + "?");
    //            if (CanPutARoomOfSize(size, facing)) {
    //                Debug.Log("yes, putting room of size " + size);
    //                canPutARoom = true;
    //                PutARoomOfSize(size, facing);
    //                newFacing = facing == "up" ? "right" : "up";
    //                //const [newX, newY] = getXY(x, y, facing, newFacing, size)
    //                x = GetNewX(x, facing, newFacing, size);
    //                y = GetNewY(y, facing, newFacing, size);
    //                Debug.Log("Moved to x" + x + " y " + y);
    //                facing = newFacing;
    //                // console.log('moved agent to', x, y);
    //                break;
    //            }
    //        }
    //        for (int size = 7; size >= 3; size -= 2) {
    //            Debug.Log("Can put BRIDGE of size " + size + "?");
    //            if (CanPutABridgeOfSize(size, facing)) {
    //                Debug.Log("yes, putting BRIDGE of size " + size);
    //                // console.log('putting a bridge of size', size, 'in', x, y);
    //                canPutABridge = true;
    //                PutABridgeOfSize(size, facing);
    //                if (facing == "up" || facing == "down") {
    //                    y += facing == "up" ? size : -size;
    //                } else {
    //                    x += facing == "right" ? size : -size;
    //                }
    //                Debug.Log("Moved to x" + x + " y " + y);
    //                break;
    //            }
    //        }
    //    } while (canPutARoom || canPutABridge);
    //}
}
