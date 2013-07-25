import opennlp.tools.sentdetect.SentenceDetector;
import opennlp.tools.sentdetect.SentenceDetectorME;
import opennlp.tools.sentdetect.SentenceModel;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 7/24/13
 * Time: 10:05 PM
 * To change this template use File | Settings | File Templates.
 */
public class SUPERTEST
{
    public static void main(String [] args) throws IOException {
        String paragraph =
                "Certainly elsewhere my do allowance at. The address farther six hearted hundred towards husband. Are securing off occasion remember daughter replying. Held that feel his see own yet. Strangers ye to he sometimes propriety in. She right plate seven has. Bed who perceive judgment did marianne. \n";
        SentenceDetector sentenceDetector;
        InputStream modelSD = new FileInputStream("src/en-sent.bin");
        SentenceModel sentenceModel = new SentenceModel(modelSD);
        sentenceDetector = new SentenceDetectorME(sentenceModel);

        String[] strings = sentenceDetector.sentDetect(paragraph);

        for(String s: strings)
        {
            System.out.println(s);
        }
        modelSD.close();
    }
}
