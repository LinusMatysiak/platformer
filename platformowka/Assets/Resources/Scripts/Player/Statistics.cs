using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static int[] stats;
    void Start()
    {
        ResetStatistics();
        //0 = enemieskilled
        //1 = times jumped
        //2 = stars collected
        //3 = coinscollected
        //4 = timeshealed
    }
    public static void ResetStatistics()
    {
        stats = new int[5];
    }
}
