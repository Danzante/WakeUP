using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class gameController : MonoBehaviour
{

    public static float[] gCordx, gCordz;
    public static bool paused { get; private set; }
    public static int gLen { get; private set; }
    public static int[] gELen;
    public static int[][] gEdge;
    static GameObject PM;
    static bool active;
    public static bool inited = false;

    void Start()
    {
        PM = GameObject.Find("/PauseMenu");
        PM.SetActive(false);
        active = false;
    }

    // Use this for initialization
    public static void Start2()
    {
        inited = true;
        gLen = 21;
        gCordx = new float[gLen];
        gCordz = new float[gLen];
        gELen = new int[gLen];
        gEdge = new int[gLen][];
        gELen[0] = 1;
        gEdge[0] = new int[gELen[0]];
        gCordx[0] = 80;
        gCordz[0] = 110;
        gEdge[0][0] = 1;
        gELen[1] = 3;
        gEdge[1] = new int[gELen[1]];
        gCordx[1] = 80;
        gCordz[1] = 70;
        gEdge[1][0] = 0;
        gEdge[1][1] = 2;
        gEdge[1][2] = 3;
        gELen[2] = 3;
        gEdge[2] = new int[gELen[2]];
        gCordx[2] = 80;
        gCordz[2] = 30;
        gEdge[2][0] = 1;
        gEdge[2][1] = 5;
        gEdge[2][2] = 7;
        gELen[3] = 3;
        gEdge[3] = new int[gELen[3]];
        gCordx[3] = 70;
        gCordz[3] = 70;
        gEdge[3][0] = 1;
        gEdge[3][1] = 4;
        gEdge[3][2] = 9;
        gELen[4] = 3;
        gEdge[4] = new int[gELen[4]];
        gCordx[4] = 70;
        gCordz[4] = 60;
        gEdge[4][0] = 3;
        gEdge[4][1] = 5;
        gEdge[4][2] = 6;
        gELen[5] = 3;
        gEdge[5] = new int[gELen[5]];
        gCordx[5] = 70;
        gCordz[5] = 30;
        gEdge[5][0] = 2;
        gEdge[5][1] = 4;
        gEdge[5][2] = 8;
        gELen[6] = 1;
        gEdge[6] = new int[gELen[6]];
        gCordx[6] = 60;
        gCordz[6] = 60;
        gEdge[6][0] = 4;
        gELen[7] = 2;
        gEdge[7] = new int[gELen[7]];
        gCordx[7] = 80;
        gCordz[7] = 20;
        gEdge[7][0] = 2;
        gEdge[7][1] = 8;
        gELen[8] = 2;
        gEdge[8] = new int[gELen[8]];
        gCordx[8] = 70;
        gCordz[8] = 20;
        gEdge[8][0] = 5;
        gEdge[8][1] = 7;
        gELen[9] = 3;
        gEdge[9] = new int[gELen[9]];
        gCordx[9] = 20;
        gCordz[9] = 70;
        gEdge[9][0] = 3;
        gEdge[9][1] = 10;
        gEdge[9][2] = 11;
        gELen[10] = 1;
        gEdge[10] = new int[gELen[10]];
        gCordx[10] = 10;
        gCordz[10] = 70;
        gEdge[10][0] = 9;
        gELen[11] = 4;
        gEdge[11] = new int[gELen[11]];
        gCordx[11] = 20;
        gCordz[11] = 60;
        gEdge[11][0] = 9;
        gEdge[11][1] = 12;
        gEdge[11][2] = 16;
        gEdge[11][3] = 17;
        gELen[12] = 2;
        gEdge[12] = new int[gELen[12]];
        gCordx[12] = 30;
        gCordz[12] = 60;
        gEdge[12][0] = 11;
        gEdge[12][1] = 13;
        gELen[13] = 3;
        gEdge[13] = new int[gELen[13]];
        gCordx[13] = 30;
        gCordz[13] = 50;
        gEdge[13][0] = 12;
        gEdge[13][1] = 14;
        gEdge[13][2] = 15;
        gELen[14] = 1;
        gEdge[14] = new int[gELen[14]];
        gCordx[14] = 50;
        gCordz[14] = 50;
        gEdge[14][0] = 13;
        gELen[15] = 1;
        gEdge[15] = new int[gELen[15]];
        gCordx[15] = 30;
        gCordz[15] = 40;
        gEdge[15][0] = 13;
        gELen[16] = 1;
        gEdge[16] = new int[gELen[16]];
        gCordx[16] = 10;
        gCordz[16] = 60;
        gEdge[16][0] = 11;
        gELen[17] = 2;
        gEdge[17] = new int[gELen[17]];
        gCordx[17] = 20;
        gCordz[17] = 20;
        gEdge[17][0] = 11;
        gEdge[17][1] = 18;
        gELen[18] = 3;
        gEdge[18] = new int[gELen[18]];
        gCordx[18] = 0;
        gCordz[18] = 20;
        gEdge[18][0] = 17;
        gEdge[18][1] = 19;
        gEdge[18][2] = 20;
        gELen[19] = 1;
        gEdge[19] = new int[gELen[19]];
        gCordx[19] = 0;
        gCordz[19] = 40;
        gEdge[19][0] = 18;
        gELen[20] = 1;
        gEdge[20] = new int[gELen[20]];
        gCordx[20] = 0;
        gCordz[20] = 0;
        gEdge[20][0] = 18;
    }

    void Update()
    { }


    public static void Pause()
    {
        active = !active;
        PM.SetActive(active);
        paused = !paused;
    }

    // Update is called once per frame
    public static void Update2()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }
}
