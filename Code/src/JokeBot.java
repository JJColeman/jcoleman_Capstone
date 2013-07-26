import opennlp.tools.sentdetect.SentenceDetector;
import opennlp.tools.sentdetect.SentenceDetectorME;
import opennlp.tools.sentdetect.SentenceModel;
import opennlp.tools.tokenize.Tokenizer;
import opennlp.tools.tokenize.TokenizerME;
import opennlp.tools.tokenize.TokenizerModel;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Random;
import java.util.Scanner;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 7/25/13
 * Time: 1:16 PM
 * To change this template use File | Settings | File Templates.
 */
public class JokeBot
{
    public JokeBot(){}

    private String userName;
    private boolean hasHi = false;
    private boolean hasHowAreYou = false;
    private boolean hasBeenUpTo = false;
    private boolean hasAskName = false;

    public String replyToUser(String userinput) throws IOException {
        String reply = "";
        SentenceDetector sentenceDetector;
        Tokenizer tokenizer;
        InputStream modelSD = new FileInputStream("src/en-sent.bin");
        InputStream modelTK = new FileInputStream("src/en-token.bin");
        SentenceModel sentenceModel = new SentenceModel(modelSD);
        sentenceDetector = new SentenceDetectorME(sentenceModel);
        TokenizerModel tokenizerModel = new TokenizerModel(modelTK);
        tokenizer = new TokenizerME(tokenizerModel);

        String[] sentences = sentenceDetector.sentDetect(userinput);

        for(int i = 0;i < sentences.length;i++)
        {
            String[] words = tokenizer.tokenize(sentences[i]);
            for(int o = 0;o < words.length;o++)
            {
               reply = findReply(words[o],reply);
            }
        }

        if(reply.equals(""))
        {
            reply = "I don't understand what you are saying to me";
        }

        modelSD.close();
        modelTK.close();

        return reply;
    }

    public String findReply(String userInputWord, String reply)
    {
        if(reply.equals(""))
        {
            if(isGreeting(userInputWord))
            {
                hasHi = true;
                reply = "Hey there, how are you doing?";
            }

            else if(isHowAreYou(userInputWord))
            {
                hasHowAreYou = true;
                if(userInputWord.equals("good"))
                {
                    reply = "Good to here, glad you are ok,by the way, what is your name";
                }

                else if(userInputWord.equals("bad"))
                {
                    reply = "Sorry to hear that, hope you feel better,by the way, what is your name";
                }

                else
                {
                    reply = "Nice to hear, by the way, what is your name";
                }
            }

            else if(!hasAskName && hasHowAreYou)
            {
                hasAskName = true;
                userName = userInputWord;
                reply = userName + " is a nice name, so " + userName + ", what have you been up to lately";
            }

            else if(!hasBeenUpTo && hasAskName)
            {
                hasBeenUpTo = true;
                reply = "Interesting " + userName + " , want to hear a joke?";
            }

            else if(hasBeenUpTo)
            {
                KnockKnock();
                userKnockKnock();

                reply = "Pretty funny, you got me";
            }
        }

        return reply;
    }

    public void KnockKnock()
    {
        System.out.println("knock knock");
        Scanner scan = new Scanner(System.in);
        String userInput = scan.nextLine();
        if(userInput.equals("whos there"))
        {
            Random random = new Random();
            int i = random.nextInt(3);
            String[] replyKnockKnock1 = {"Cash","Doris","Honey bee"};
            String[] replyKnockKnock2 = {"No thanks, but I would like a peanut instead","Doris locked, that's why I'm knocking!","Honey bee a dear and get me a soda!"};
            System.out.println(replyKnockKnock1[i]);
            String userInputWho = scan.nextLine();
            if(userInputWho.equals(replyKnockKnock1[i] + " who"))
            {
                System.out.println(replyKnockKnock2[i]);
                System.out.println("Funny right?, now you try a knock knock joke");
            }

            else
            {
                System.out.println("You mess up the joke -_-, well you try then...say knock knock");
            }
        }
    }

    public void userKnockKnock()
    {
        Scanner scan = new Scanner(System.in);
        String userInput = scan.nextLine();
        if(userInput.equals("knock knock"))
        {
            System.out.println("whos there");
        }
        String userWhoThereReply = scan.nextLine();
        System.out.println(userWhoThereReply + " who");
        String userLastReply = scan.nextLine();
    }

    public boolean isGreeting(String userInputWord)
    {
        return userInputWord.equals("hi") ||
                userInputWord.equals("hello") ||
                userInputWord.equals("hey") ||
                userInputWord.equals("sup") ||
                userInputWord.equals("meet") ||
                userInputWord.equals("you") ||
                userInputWord.equals("up");
    }

    public boolean isHowAreYou(String userInputWord)
    {
        return userInputWord.equals("good") ||
                userInputWord.equals("bad") ||
                userInputWord.equals("awesome") ||
                userInputWord.equals("fine") ||
                userInputWord.equals("same") ||
                userInputWord.equals("old") ||
                userInputWord.equals("seen");
    }

    public void isKnockKnock(String userInputWord)
    {

    }


}
