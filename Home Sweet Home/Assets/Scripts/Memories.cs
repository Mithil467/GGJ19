﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Memories : MonoBehaviour
{
    public float memoryTimer;
    public float fadeRate;
    public Image image;
    public PPSVignette vg;
    public Region region;
    public CharacterMovement player;

    private float targetAlpha;

    public Text textBox;
    private bool done = false;
    string[] goatText = new string[] {
        /*Video Game Store*/
"Wow! The new game cup head is here!",
        "I have wanted to play this since so long!",
        "Ummm...",
        "It's been a long time, but feels just like yesterday...",
        "When I was waiting in queue to play Street Fighter...\nBut as soon as it was my turn to play...",
        "These bullies rushed in and kicked me away...",
"My brother was on the way... He saw me crying",
"He immediately rushed to save me and fought against them",
"They all ran away and we played the game together!",
"I really miss my brother, he had to leave home for college\nNow he lives in a hostel",
"Ever since these guys have started bullying me again...",
""
};

    string[] bakery = new string[] {
/*Bakery*/

"Haa smells real good",
"Mom, that cake looks delicious! Please get it for me na mom...", "Please!",
"Alright! Alright! But I have got a condition, you will help me at home. Will you?",
"Ohkay okay! I promise!",
"Are you sure?",
"Yes yes please!!",
"Okay lets take it..."
};

    string[] hotel = new string[] {
/*Hotel*/

"I am hungry. It has been quite long since I left home",
"Should have taken some food with me, before leaving",
"It has been a long time, since we have been to this restaurant",
"We were getting back home after playing in the park",
"'Mom am hungry!'",
"Mom : We are almost home, as soon as we reach\nI will cook tasty food for you",
"'Dad : You must be tired as well dear... Lets eat at this restaurant'",
"'Yeah! Hotel, I want juice, ice cream, noodles, soup!'",
"Mom : Okay!You can have all these, lets first get in!"
};

    string[] hospital = new string[] {
/*Hospital*/

"Hospital usually relates people getting hurt, or falling ill",
"But it reminds me of Detective Pikachu!",
"I wanted to watch the movie but Mom forced me to school",
"Feeling sad and dissappointed I came in for my math class",
"Then suddenly right in the middle of the class,\nwe heard an announcement",
"It was for me, I was called downstairs in Princial's Cabin",
"Dad was sitting in already",
"He was talking about some medical emmergency, I panicked",
"We got in the car, Dad was smiling",
"I was confused",
"Soon, Dad parked the car in front of the theatre,\nand I finally understood what's happening!",
    };

    string[] park = new string[] {
/*Park*/

"It's been quite long that I haven't been here",
"During weekends, I would come here with my brother early morning",
"Then we played a lot",
"Many friends would accompany us",
"But things have changed",
"I don't even know where many of them currently live...",
"Even my best friend drifted apart...",
"Life is boring",
"I always feel that hopefully one day when I wake up,\neverything becomes as it was earlier",
"Parents stop quarelling... Brother comes back home"
    };

    string[] bicycle = new string[] {
/*Bicycle*/
"Hey! This looks like!...",
"Yeah! This is my bicycle!",
"All my friends had a bicycle, and I also wanted one",
"I would like to ride my way to home from school",
"I decided to talk about it with Dad",
"'Dad! I want a bicycle, all my friends have one!'",
"Okay son, we will get you one... Wait for some days",
"'No dad,I want it tommorow, or else I will not talk with you'",
"Later that night, I heard my parents talking...",
"Dad : I think we should buy him a bicycle\nI will buy a laptop next month for me",
"Mom : But your laptop is important for your job!",
"",
"You said the laptop is broken and you have\nproblem to work right?",
"Dad : Yes, but he feels bad that he doesn't have a bicycle",
"Mom : Okay, we will get a good cycle for him",
"And.. Next day this cycle was standing at the door",
"I felt very happy"
};
//    string[] goatText ;

    int currentlyDisplayingText = 0, time = 0;

    public void Update()
    {
        if (Input.GetKeyDown("space"))
            SkipToNextText();
    }

    public void FadeOut()
    {
        //Debug.Log("image fade out");
        targetAlpha = 0.0f;
        StartCoroutine(Anim(1));
    }

    public void FadeIn()
    {
        //Debug.Log("image fade in");
        targetAlpha = 1.0f;
        StartCoroutine(Anim());
    }

    IEnumerator Anim(int a = 0)
    {
        Color currentColor = image.color;
        float alphaDiff = Mathf.Abs(currentColor.a - targetAlpha);

        while (alphaDiff > 0.01f)
        {
            currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, fadeRate * Time.deltaTime);
            image.color = currentColor;

            currentColor = image.color;
            alphaDiff = Mathf.Abs(currentColor.a - targetAlpha);

            if (alphaDiff < 0.01f)
            {
                if (a==0)
                {
                    PlayMemory();
                    currentColor.a = 1f;
                }
                else
                {
                    currentColor.a = 0;
                    image.enabled = false;
                }

                image.color = currentColor;
                yield break;
            }

            yield return null;
        }
    }

    private void PlayMemory()
    {
        
        StartCoroutine(AnimateText());
        Invoke("Exit", memoryTimer);
    }
    public void SkipToNextText()
    {
        StopAllCoroutines();
        currentlyDisplayingText++;
        if (currentlyDisplayingText >= goatText.Length)
        {
            done = true;
        }
        if (!done)
            StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {

        for (int i = 0; i < (goatText[currentlyDisplayingText].Length + 1); i++)
        {
            textBox.text = goatText[currentlyDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(.05f);
        }

        currentlyDisplayingText++;
        if (currentlyDisplayingText >= goatText.Length)
        {
            done = true;
            yield break;
        }
    }

    private void Exit()
    {
        //Debug.Log("exiting memory");
        FadeOut();
        region.isGrayScale = false;
        region.Colorify();
        player.disableMovement = false;
    }
}
