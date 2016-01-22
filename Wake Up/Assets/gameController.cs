using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

    public float[] gCordx, gCordz;
    public bool paused { get; private set; }
    public int gLen { get; private set; }
    public int[] gELen;
    public int[][] gEdge;

	// Use this for initialization
	void Start () {
        gLen = 21;
        gCordx = new float[gLen];
        gCordx = new float[gLen];
        gELen = new int[gLen];
        gEdge = new int[gLen][];
        gELen[0] = 1;
        gCordx[0] = 80;
        gCordz[0] = 110;
        gEdge[0][0] = 1;
        gELen[1] = 3;
        gCordx[1] = 80;
        gCordz[1] = 70;
        gEdge[1][0] = 0;
        gEdge[1][1] = 2;
        gEdge[1][2] = 3;
        gELen[2] = 3;
        gCordx[2] = 80;
        gCordz[2] = 30;
        gEdge[2][0] = 1;
        gEdge[2][1] = 5;
        gEdge[2][2] = 7;
        gELen[3] = 3;
        gCordx[3] = 70;
        gCordz[3] = 70;
        gEdge[3][0] = 1;
        gEdge[3][1] = 4;
        gEdge[3][2] = 9;
        gELen[4] = 3;
        gCordx[4] = 70;
        gCordz[4] = 60;
        gEdge[4][0] = 3;
        gEdge[4][1] = 5;
        gEdge[4][2] = 6;
        gELen[5] = 3;
        gCordx[5] = 70;
        gCordz[5] = 30;
        gEdge[5][0] = 2;
        gEdge[5][1] = 4;
        gEdge[5][2] = 8;
        gELen[6] = 1;
        gCordx[6] = 60;
        gCordz[6] = 60;
        gEdge[6][0] = 4;
        gELen[7] = 2;
        gCordx[7] = 80;
        gCordz[7] = 20;
        gEdge[7][0] = 2;
        gEdge[7][1] = 8;
        gELen[8] = 2;
        gCordx[8] = 70;
        gCordz[8] = 20;
        gEdge[8][0] = 5;
        gEdge[8][1] = 7;
        gELen[9] = 3;
        gCordx[9] = 20;
        gCordz[9] = 70;
        gEdge[9][0] = 3;
        gEdge[9][1] = 10;
        gEdge[9][2] = 11;
        gELen[10] = 1;
        gCordx[10] = 10;
        gCordz[10] = 70;
        gEdge[10][0] = 9;
        gELen[11] = 4;
        gCordx[11] = 20;
        gCordz[11] = 60;
        gEdge[11][0] = 9;
        gEdge[11][1] = 12;
        gEdge[11][2] = 16;
        gEdge[11][3] = 17;
        gELen[12] = 2;
        gCordx[12] = 30;
        gCordz[12] = 60;
        gEdge[12][0] = 11;
        gEdge[12][1] = 13;
        gELen[13] = 3;
        gCordx[13] = 30;
        gCordz[13] = 50;
        gEdge[13][0] = 12;
        gEdge[13][1] = 14;
        gEdge[13][2] = 15;
        gELen[14] = 1;
        gCordx[14] = 50;
        gCordz[14] = 50;
        gEdge[14][0] = 13;
        gELen[15] = 1;
        gCordx[15] = 30;
        gCordz[15] = 40;
        gEdge[15][0] = 13;
        gELen[16] = 1;
        gCordx[16] = 10;
        gCordz[16] = 60;
        gEdge[16][0] = 11;
        gELen[17] = 2;
        gCordx[17] = 20;
        gCordz[17] = 20;
        gEdge[17][0] = 11;
        gEdge[17][1] = 18;
        gELen[18] = 3;
        gCordx[18] = 0;
        gCordz[18] = 20;
        gEdge[18][0] = 17;
        gEdge[18][1] = 19;
        gEdge[18][2] = 20;
        gELen[19] = 1;
        gCordx[19] = 0;
        gCordz[19] = 40;
        gEdge[19][0] = 18;
        gELen[20] = 1;
        gCordx[20] = 0;
        gCordz[20] = 0;
        gEdge[20][0] = 18;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = !paused;
        }
    }
}
