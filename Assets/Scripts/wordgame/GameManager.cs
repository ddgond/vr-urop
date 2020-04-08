﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameQuestion
{
    public string beforeText;
    public string question;
    public string answer;
    public string afterText;

    public GameQuestion(string beforeText, string question, string answer, string afterText)
    {
        this.beforeText = beforeText;
        this.question = question;
        this.answer = answer;
        this.afterText = afterText;
    }

    public string getFullQuestion()
    {
        return beforeText + "(" + question + ")" + afterText;
    }

    public string getFullAnswer()
    {
        return beforeText + answer + afterText;
    }

    public string getAnswer()
    {
        return this.answer;
    }
}
/*
 * Main class for word game
 */
public class GameManager : MonoBehaviour
{
    private GameQuestion[] questions = {
        new GameQuestion("教科書を", "と", "閉", "じてください"),
        new GameQuestion("会社に", "もど", "戻", "ります"),
        new GameQuestion("私はパンよりご", "はん", "飯", "が好きだ"),
        new GameQuestion("この部屋は", "さむ", "寒", "いです"),
        new GameQuestion("彼女はとても", "よろこ", "喜", "びました")
    };
    public TextMeshPro question;
    private int qIndex = 0;

    // JPLT N4, N3 kanji
    private string options = "不世主乗事京仕代以低住体作使便借働元兄光写冬切別力勉動区医去台合同味品員問回図地堂場声売夏夕夜太好妹姉始字室家寒屋工市帰広度建引弟弱強待心思急悪意所持教文料方旅族早明映春昼暑暗曜有服朝村林森業楽歌止正歩死民池注洋洗海漢牛物特犬理産用田町画界病発県真着知短研私秋究答紙終習考者肉自色英茶菜薬親計試説貸質赤走起転軽近送通進運遠都重野銀門開院集青音頭題顔風飯館首験鳥黒丁両丸予争交他付令仲伝位例係信倉倍候停健側億兆児全公共兵具典内冷刀列初利刷副功加助努労勇勝包化卒協単博印原参反取受史号司各向君告周命和唱商喜器囲固園坂型塩士変夫央失委季孫守完官定実客宮害宿察寺対局岩岸島州巣差希席帯帳平幸底府庫庭康式弓当形役径徒得必念息悲想愛感成戦戸才打投折拾指挙改放救敗散数整旗昔星昨昭景晴曲最望期未末札材束松板果柱栄根案梅械植極様標横橋機欠次歯歴残殺毒毛氏氷求決汽油治法波泣泳活流浅浴消深清温港湖湯満漁灯炭点無然焼照熱牧玉王球由申畑番登的皮皿直相省矢石礼祝神票祭福科秒種積章童競竹笑笛第筆等算管箱節米粉糸紀約級細組結給絵続緑線練置羊美羽老育胃脈腸臣航船良芸芽苦草荷落葉虫血街衣表要覚観角訓記詩課調談議谷豆象貝負貨貯費賞路身軍輪辞農辺返追速連遊達選郡部配酒里量鉄録鏡関陸陽隊階雪雲静面順願類飛養馬鳴麦黄鼻";



    // Start is called before the first frame update
    void Start()
    {
        question.text = questions[qIndex].getFullQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool SubmitAnswer(string text)
    {
        Debug.Log("Submitted: " + text);
        if (text == questions[qIndex].getAnswer())
        {
            if (qIndex == questions.Length-1)
            {
                question.text = "win";
                return true;
            }

            question.text = questions[++qIndex].getFullQuestion();
            return true;
        }

        return false;
    }

    // Request some text to be applied to a ball
    public string RequestText()
    {
        if (Random.Range(0, 6) == 0)
        {
            return questions[qIndex].getAnswer(); ;
        }

        return options[Random.Range(0, options.Length)].ToString();
    }
}