import Tree.SmallSentenceTree1;
import opennlp.tools.sentdetect.SentenceDetector;
import opennlp.tools.sentdetect.SentenceDetectorME;
import opennlp.tools.sentdetect.SentenceModel;
import opennlp.tools.tokenize.TokenizerModel;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;

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

    public void checkTrees(String sentence)
    {
       SmallSentenceTree1 smallSentenceTree = new SmallSentenceTree1();

       boolean isSentence = smallSentenceTree.isSentence(sentence);

       if(isSentence)
       {
           smallSentenceTree.Reply(sentence);
       }
    }
}
