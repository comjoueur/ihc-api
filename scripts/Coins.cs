using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public static int coins = 0;
    public static int total = 200;
    Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponent<Text>();
        coins = Global.count;
    }

    // Update is called once per frame
    void Update()
    {
//         coinText.text = "" + coins + "/" + total;
        coinText.text = coins.ToString();
    }

    public void setCoins(int value) {
        coins += value;
        Global.count += value;
        total -= 10;
        //        coinText.text = "" + coins + "/" + total;
        coinText.text = coins.ToString();
    }
}
