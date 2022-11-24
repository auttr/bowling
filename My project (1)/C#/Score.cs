using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] TMP_InputField inputArray;

    int[] inputAAA;
    //輸入瓶數
    int pinCnt;
    const int pinTotalCnt = 10;
    int rollTimes = 0;


    int round = 1;

    int strikeRoundTmp;
    int strikeTmp1;
    int strikeTmp2;
    int strikeTmp3;

    int spareRoundTmp;
    int spareTmp1;
    int spareTmp2;

    int totalScore;


    int tempRound;
    int tempSpare1;
    int tempSpare2;

    int spareCount;

    int[] pinCntList = new int[1];
    List<List<int>> strikeList = new List<List<int>>();
    List<List<int>> spareList = new List<List<int>>();

    //countervail
    List<List<int>> countervailListSpare = new List<List<int>>();
    List<List<int>> countervailListStrike = new List<List<int>>();

    bool isStrike;
    bool isSpare;
    bool isStrike2;


    string inputArrayString = "";
    string[] inputArrayString2 = new string[21];
    #region 
    [SerializeField] TextMeshProUGUI r1s1;
    [SerializeField] TextMeshProUGUI r1s2;
    [SerializeField] TextMeshProUGUI r1Total;

    [SerializeField] TextMeshProUGUI r2s1;
    [SerializeField] TextMeshProUGUI r2s2;
    [SerializeField] TextMeshProUGUI r2Total;

    [SerializeField] TextMeshProUGUI r3s1;
    [SerializeField] TextMeshProUGUI r3s2;
    [SerializeField] TextMeshProUGUI r3Total;

    [SerializeField] TextMeshProUGUI r4s1;
    [SerializeField] TextMeshProUGUI r4s2;
    [SerializeField] TextMeshProUGUI r4Total;

    [SerializeField] TextMeshProUGUI r5s1;
    [SerializeField] TextMeshProUGUI r5s2;
    [SerializeField] TextMeshProUGUI r5Total;

    [SerializeField] TextMeshProUGUI r6s1;
    [SerializeField] TextMeshProUGUI r6s2;
    [SerializeField] TextMeshProUGUI r6Total;

    [SerializeField] TextMeshProUGUI r7s1;
    [SerializeField] TextMeshProUGUI r7s2;
    [SerializeField] TextMeshProUGUI r7Total;

    [SerializeField] TextMeshProUGUI r8s1;
    [SerializeField] TextMeshProUGUI r8s2;
    [SerializeField] TextMeshProUGUI r8Total;

    [SerializeField] TextMeshProUGUI r9s1;
    [SerializeField] TextMeshProUGUI r9s2;
    [SerializeField] TextMeshProUGUI r9Total;

    [SerializeField] TextMeshProUGUI r10s1;
    [SerializeField] TextMeshProUGUI r10s2;
    [SerializeField] TextMeshProUGUI r10s3;
    [SerializeField] TextMeshProUGUI r10Total;

    [SerializeField] TextMeshProUGUI textTotalScore;

    [SerializeField] TextMeshProUGUI textMsg;
    #endregion
    private void Awake()
    {
        textMsg.text = "";

    }
   
    public void OnClick()
    {
        if (input == null) textMsg.text = "請輸入分數";
        else
        {
            pinCnt = int.Parse(input.text);
            rollTimes++;
            textMsg.text = "";
            if (round <= 12)
            {
                //第一次投
                if (rollTimes == 1)
                {
                    if (pinCnt > pinTotalCnt)
                    {
                        textMsg.text = "輸入大於10";
                        rollTimes = 0;
                        return;
                    }
                    else if (pinCnt == pinTotalCnt)
                    {
                        //if(spareCount>=1)spareCount++;
                        List<int> countervailSpare = new List<int>();
                        countervailListSpare.Add(countervailSpare);
                        foreach (var item in countervailListSpare)
                        {
                            item.Add(pinCnt);
                        }

                        // spareCount = 0;
                        if (round <= 10) RoundChooseRoll1(round).text = "X";
                        else if (round == 11)
                        {
                            if (strikeList.Count != 0) r10s2.text = "X";
                            else
                            {
                                r10s3.text = "X";
                            }
                        }
                        else if (round == 12) r10s3.text = "X";

                        textMsg.text = "Strike";
                        List<int> strikeCntList = new List<int>();
                        List<int> countervailStrike = new List<int>();
                        strikeCntList.Add(int.Parse(round.ToString()));
                        rollTimes = 0;

                        round++;

                        strikeList.Add(strikeCntList);
                        countervailListStrike.Add(countervailStrike);
                        if (strikeList.Count >= 1)
                        {
                            if (strikeList[0][0] != tempRound + 1) spareCount = 0;
                        }
                        foreach (var item in strikeList)
                        {
                            item.Add(pinCnt);
                            if (item.Count >= 4)
                            {
                                strikeRoundTmp = item[0];
                                strikeTmp1 = item[1];
                                strikeTmp2 = item[2];
                                strikeTmp3 = item[3];
                                StrikeFrameScore(strikeRoundTmp, strikeTmp1, strikeTmp2, strikeTmp3);
                                isStrike = true;
                            }
                        }
                     
                        if (isStrike)
                        {
                            isStrike = false;
                            if (strikeList.Count >= 1)
                            {
                                strikeList.RemoveAt(0);
                            }
                        }

                        foreach (var item in spareList)
                        {
                            item.Add(pinCnt);
                            if (item.Count >= 3)
                            {
                                spareRoundTmp = item[0];
                                spareTmp1 = item[1];
                                spareTmp2 = item[2];
                                SpareFrameScore(spareRoundTmp, spareTmp1, spareTmp2);
                                isSpare = true;
                            }
                        }



                        if (isSpare)
                        {
                            isSpare = false;
                            if (spareList.Count >= 1)
                            {
                                spareList.RemoveAt(0);
                            }
                        }

                    }
                    else if (pinCnt < pinTotalCnt)
                    {

                        List<int> countervailSpare = new List<int>();
                        countervailListSpare.Add(countervailSpare);
                        pinCntList[0] = pinCnt;
                        foreach (var item in countervailListStrike)
                        {
                            item.Add(pinCnt);
                        }
                        foreach (var item in countervailListSpare)
                        {
                            item.Add(pinCnt);
                        }
                        if (round <= 10) RoundChooseRoll1(round).text = "" + pinCnt;
                        if (round == 11)
                        {
                            if (strikeList.Count != 0) r10s2.text = "" + pinCnt;
                            else
                            {
                                r10s3.text = "" + pinCnt;
                            }
                        }
                        if (strikeList.Count>=1)
                        {
                            if (strikeList[0][0] != tempRound + 1) spareCount = 0;
                        }
                      
                        foreach (var item in strikeList)
                        {
                            item.Add(pinCnt);

                            if (item.Count >= 4)
                            {
                                strikeRoundTmp = item[0];
                                strikeTmp1 = item[1];
                                strikeTmp2 = item[2];
                                strikeTmp3 = item[3];
                                StrikeFrameScore(strikeRoundTmp, strikeTmp1, strikeTmp2, strikeTmp3);
                                isStrike = true;
                            }
                        }
                    
                        if (isStrike)
                        {
                            isStrike = false;
                            if (strikeList.Count >= 1)
                            {
                                strikeList.RemoveAt(0);
                            }
                        }

                        foreach (var item in spareList)
                        {
                            item.Add(pinCnt);

                            if (item.Count >= 3)
                            {
                                spareRoundTmp = item[0];
                                spareTmp1 = item[1];
                                spareTmp2 = item[2];
                                SpareFrameScore(spareRoundTmp, spareTmp1, spareTmp2);
                                isSpare = true;
                            }
                        }
                        if (isSpare)
                        {
                            isSpare = false;
                            if (spareList.Count >= 1)
                            {
                                spareList.RemoveAt(0);
                            }
                            return;
                        }
                        if (strikeList.Count == 0) isStrike2 = true;
                        if (isStrike2)
                        {
                            isStrike2 = false;
                            totalScore += pinCnt;
                        }

                        if (strikeList.Count != 0)
                        {
                            foreach (var item in strikeList)
                            {
                                if (item.Count != 3) spareCount = 0;
                            }

                        }
                        else
                        {
                            spareCount = 0;
                        }

                    }
                }
                //第二次投
                else if (rollTimes == 2)
                {

                    if (pinCnt > pinTotalCnt - pinCntList[0])
                    {
                        textMsg.text = $"輸入大於{pinTotalCnt - pinCntList[0]}，請重新輸入";
                        rollTimes = 1;
                        return;
                    }
                    //Spare
                    else if (pinCnt == pinTotalCnt - pinCntList[0])
                    {

                        if (round <= 10) RoundChooseRoll2(round).text = "/";
                        if (round == 11) r10s3.text = "" + pinCnt;
                        Array.Clear(pinCntList, 0, 0);
                        if (round <= 10) textMsg.text = "Spare";
                        List<int> spareCntList = new List<int>();

                        spareCntList.Add(int.Parse(round.ToString()));
                        spareList.Add(spareCntList);

                        round++;

                        if (strikeList.Count >= 1)
                        {
                            if (strikeList[0][0] != tempRound + 1) spareCount = 0;
                        }
                        foreach (var item in strikeList)
                        {
                            item.Add(pinCnt);
                            if (item.Count >= 4)
                            {
                                strikeRoundTmp = item[0];
                                strikeTmp1 = item[1];
                                strikeTmp2 = item[2];
                                strikeTmp3 = item[3];
                                StrikeFrameScore(strikeRoundTmp, strikeTmp1, strikeTmp2, strikeTmp3);
                                isStrike = true;
                            }
                        }

                       

                        if (isStrike)
                        {
                            isStrike = false;
                            if (strikeList.Count >= 1)
                            {
                                strikeList.RemoveAt(0);
                            }
                        }
                        foreach (var item in spareList)
                        {
                            item.Add(pinCnt);
                            if (item.Count >= 3)
                            {
                                spareRoundTmp = item[0];
                                spareTmp1 = item[1];
                                spareTmp2 = item[2];
                                SpareFrameScore(spareRoundTmp, spareTmp1, spareTmp2);
                                isSpare = true;
                            }
                        }
                        if (isSpare)
                        {
                            isSpare = false;
                            if (spareList.Count >= 1)
                            {
                                spareList.RemoveAt(0);
                            }
                        }
                        if (round == 11) rollTimes = 0;
                    }
                    else if (pinCnt < pinTotalCnt - pinCntList[0])
                    {

                        if (round <= 10) RoundChooseRoll2(round).text = "" + pinCnt;
                        if (round == 11) r10s3.text = "" + pinCnt;
                        Array.Clear(pinCntList, 0, 0);
                        if (strikeList.Count >= 1)
                        {
                            if (strikeList[0][0] != tempRound + 1) spareCount = 0;
                        }
                        foreach (var item in strikeList)
                        {
                            item.Add(pinCnt);
                            if (item.Count >= 4)
                            {
                                strikeRoundTmp = item[0];
                                strikeTmp1 = item[1];
                                strikeTmp2 = item[2];
                                strikeTmp3 = item[3];
                                StrikeFrameScore(strikeRoundTmp, strikeTmp1, strikeTmp2, strikeTmp3);
                                isStrike = true;
                            }
                        }
                     
                        if (isStrike)
                        {
                            isStrike = false;
                            if (strikeList.Count >= 1)
                            {
                                strikeList.RemoveAt(0);
                            }
                        }
                        if (strikeList.Count == 0) totalScore += pinCnt;

                        if (round <= 10) RoundChooseTotal(round).text = "" + totalScore;
                        if (strikeList.Count != 0)
                        {
                            spareCount = 0;
                        }


                        round++;
                        rollTimes = 0;
                        return;
                    }

                    if (round <= 10) rollTimes = 0;

                }
                //第三投
                else if (rollTimes == 3)
                {
                    r10s3.text = "" + pinCnt;
                    foreach (var item in spareList)
                    {
                        item.Add(pinCnt);
                    }
                    foreach (var item in strikeList)
                    {
                        item.Add(pinCnt);
                        if (item.Count >= 4)
                        {
                            strikeRoundTmp = item[0];
                            strikeTmp1 = item[1];
                            strikeTmp2 = item[2];
                            strikeTmp3 = item[3];
                            StrikeFrameScore(strikeRoundTmp, strikeTmp1, strikeTmp2, strikeTmp3);
                            isStrike = true;
                        }
                    }
                    if (isStrike)
                    {
                        isStrike = false;
                        if (strikeList.Count >= 1)
                        {
                            strikeList.RemoveAt(0);
                        }

                    }
                    round = 13;
                }
            }
            else
            {
                textMsg.text = "遊戲結束";
            }
        }
    }
    
    public void OnclickArray()
    {

        inputArrayString = inputArray.text;

        inputAAA = new int[21];
        inputArrayString2 = inputArrayString.Split(',');
        for (int i = 0; i < inputArrayString2.Length; i++)
        {
            inputAAA[i] = Int32.Parse(inputArrayString2[i]);
        }

        foreach (var item in inputAAA)
        {
            input.text = "" + item;
            OnClick();
        }
    }

    void StrikeFrameScore(int r, int s1, int s2, int s3)
    {
        if (spareCount >= 1) totalScore -= 10;



        switch (r)
        {
            case 1:
                totalScore = totalScore + s1 + s2 + s3;
                r1Total.text = "" + totalScore;
                break;
            case 2:
                totalScore = totalScore + s1 + s2 + s3;
                r2Total.text = "" + totalScore;
                break;
            case 3:
                totalScore = totalScore + s1 + s2 + s3;
                r3Total.text = "" + totalScore;
                break;
            case 4:
                totalScore = totalScore + s1 + s2 + s3;
                r4Total.text = "" + totalScore;
                break;
            case 5:
                totalScore = totalScore + s1 + s2 + s3;
                r5Total.text = "" + totalScore;
                break;
            case 6:
                totalScore = totalScore + s1 + s2 + s3;
                r6Total.text = "" + totalScore;
                break;
            case 7:
                totalScore = totalScore + s1 + s2 + s3;
                r7Total.text = "" + totalScore;
                break;
            case 8:
                totalScore = totalScore + s1 + s2 + s3;
                r8Total.text = "" + totalScore;
                break;
            case 9:
                totalScore = totalScore + s1 + s2 + s3;
                r9Total.text = "" + totalScore;
                break;
            case 10:
                totalScore = totalScore + s1 + s2 + s3;
                r10Total.text = "" + totalScore;

                textTotalScore.text = "" + totalScore;
                textMsg.text = "遊戲結束";
                round = 15;

                break;
            default:
                break;
        }
        if (s2 != 10) totalScore += s2;

        //spareCount --;
    }
    void SpareFrameScore(int r, int s1, int s2)
    {
        tempRound = r;
        spareCount++;
        foreach (var item in countervailListSpare)
        {
            if (item.Count >= 2)
            {
                tempSpare1 = item[0];
                tempSpare2 = item[1];
            }
        }
        if (countervailListSpare.Count >= 2) countervailListSpare.RemoveAt(0);

        totalScore -= tempSpare1;

        switch (r)
        {
            case 1:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r1Total.text = "" + totalScore;
                break;
            case 2:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r2Total.text = "" + totalScore;
                break;
            case 3:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r3Total.text = "" + totalScore;
                break;
            case 4:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r4Total.text = "" + totalScore;
                break;
            case 5:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r5Total.text = "" + totalScore;
                break;
            case 6:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r6Total.text = "" + totalScore;
                break;
            case 7:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r7Total.text = "" + totalScore;
                break;
            case 8:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r8Total.text = "" + totalScore;
                break;
            case 9:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r9Total.text = "" + totalScore;
                break;
            case 10:
                totalScore = totalScore + s1 + s2 + tempSpare1;
                r10Total.text = "" + totalScore;
                textTotalScore.text = "" + totalScore;
                textMsg.text = "遊戲結束";
                round = 15;

                break;
            default:
                break;
        }
        totalScore += tempSpare2;

    }
    TextMeshProUGUI RoundChooseRoll1(int r)
    {
        switch (r)
        {
            case 1:
                return r1s1;

            case 2:
                return r2s1;
            case 3:
                return r3s1;
            case 4:
                return r4s1;
            case 5:
                return r5s1;
            case 6:
                return r6s1;
            case 7:
                return r7s1;
            case 8:
                return r8s1;
            case 9:
                return r9s1;
            case 10:
                return r10s1;
            default:
                return null;
        }
    }
    TextMeshProUGUI RoundChooseRoll2(int r)
    {
        switch (r)

        {
            case 1:
                return r1s2;

            case 2:
                return r2s2;
            case 3:
                return r3s2;
            case 4:
                return r4s2;
            case 5:
                return r5s2;
            case 6:
                return r6s2;
            case 7:
                return r7s2;
            case 8:
                return r8s2;
            case 9:
                return r9s2;
            case 10:
                return r10s2;
            default:
                return null;
        }
    }
    TextMeshProUGUI RoundChooseTotal(int r)
    {
        switch (r)

        {
            case 1:
                return r1Total;

            case 2:
                return r2Total;
            case 3:
                return r3Total;
            case 4:
                return r4Total;
            case 5:
                return r5Total;
            case 6:
                return r6Total;
            case 7:
                return r7Total;
            case 8:
                return r8Total;
            case 9:
                return r9Total;
            case 10:
                textTotalScore.text = "" + totalScore;
                textMsg.text = "遊戲結束";
                round = 15;
                return r10Total;
            default:
                return null;
        }
    }

}
