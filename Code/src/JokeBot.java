import Tree.Greetings.GreetingTree1;
import Tree.Greetings.GreetingTree2;
import Tree.Reply.ReplyTree;
import Tree.SmallSentence.*;
import opennlp.tools.parser.Parser;
import opennlp.tools.parser.ParserFactory;
import opennlp.tools.parser.ParserModel;
import opennlp.tools.sentdetect.SentenceDetector;
import opennlp.tools.sentdetect.SentenceDetectorME;
import opennlp.tools.sentdetect.SentenceModel;
import opennlp.tools.tokenize.TokenizerModel;

import java.io.BufferedInputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import Tree.*;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 7/25/13
 * Time: 1:16 PM
 * To change this template use File | Settings | File Templates.
 */
public class JokeBot
{
    private String userName;
    private boolean hasHi = false;
    private boolean hasHowAreYou = false;
    private boolean hasBeenUpTo = false;
    private boolean hasAskName = false;
    private Parser parser;
    private int jokeTime;

    public JokeBot()
    {
        try
        {
            InputStream modelParser = new FileInputStream("src/en-parser-chunking.bin");
            ParserModel parserModel = new ParserModel(modelParser);
            parser = ParserFactory.create(parserModel);
            jokeTime = 0;
        }

        catch(Exception e)
        {
            e.printStackTrace();
        }
    }

    public void replyToUser(String userinput) throws IOException
    {
        SentenceDetector sentenceDetector;
        InputStream modelSD = new FileInputStream("src/en-sent.bin");
        InputStream modelTK = new FileInputStream("src/en-token.bin");
        SentenceModel sentenceModel = new SentenceModel(modelSD);
        sentenceDetector = new SentenceDetectorME(sentenceModel);
        TokenizerModel tokenizerModel = new TokenizerModel(modelTK);

        String[] sentences = sentenceDetector.sentDetect(userinput);

        for(int i = 0;i < sentences.length;i++)
        {
            checkTrees(sentences[i]);
        }

        modelSD.close();
        modelTK.close();

    }

    public void addParsersToTree(Parser parser, Tree tree)
    {
        tree.parser = parser;
    }

    public void checkTrees(String sentence)
    {
        GreetingTree1 greetingTree1 = new GreetingTree1();
        addParsersToTree(parser,greetingTree1);
        GreetingTree2 greetingTree2 = new GreetingTree2();
        addParsersToTree(parser,greetingTree2);
        SmallSentenceTree1 smallSentenceTree1 = new SmallSentenceTree1();
        addParsersToTree(parser,smallSentenceTree1);
        SmallSentenceTree2 smallSentenceTree2 = new SmallSentenceTree2();
        addParsersToTree(parser,smallSentenceTree2);
        SmallSentenceTree3 smallSentenceTree3 = new SmallSentenceTree3();
        addParsersToTree(parser,smallSentenceTree3);
        SmallSentenceTree4 smallSentenceTree4 = new SmallSentenceTree4();
        addParsersToTree(parser,smallSentenceTree4);
        ReplyTree replyTree = new ReplyTree();
        addParsersToTree(parser,replyTree);

        boolean isGreeting1 = greetingTree1.isParsable(sentence);
        boolean isGreeting2 = greetingTree2.isParsable(sentence);
        boolean isSentence1 = smallSentenceTree1.isParsable(sentence);
        boolean isSentence2 = smallSentenceTree2.isParsable(sentence);
        boolean isSentence3 = smallSentenceTree3.isParsable(sentence);
        boolean isSentence4 = smallSentenceTree4.isParsable(sentence);
        boolean isReply = replyTree.isParsable(sentence);

        if(isGreeting1)
        {
           greetingTree1.Reply(sentence);
        }

        else if(isGreeting2)
        {
           greetingTree2.Reply(sentence);
        }

        else if(isReply)
        {
            replyTree.Reply(sentence);
        }

        else if(isSentence1)
        {
           smallSentenceTree1.Reply(sentence);
        }

        else if(isSentence2)
        {
            smallSentenceTree2.Reply(sentence);
        }

        else if(isSentence3)
        {
            smallSentenceTree3.Reply(sentence);
        }

        else if(isSentence4)
        {
            smallSentenceTree4.Reply(sentence);
        }

        else
        {
            System.out.println("I don't understand what you saying...");
        }

        if(jokeTime == 2)
        {
            System.out.println("Want to hear a joke?");
            jokeTime = 1;
        }

        else
        {
            jokeTime++;
        }
    }
}
