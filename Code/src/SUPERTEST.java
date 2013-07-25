import opennlp.tools.cmdline.parser.ParserTool;
import opennlp.tools.parser.Parse;
import opennlp.tools.parser.Parser;
import opennlp.tools.parser.ParserFactory;
import opennlp.tools.parser.ParserModel;
import opennlp.tools.sentdetect.SentenceDetector;
import opennlp.tools.sentdetect.SentenceDetectorME;
import opennlp.tools.sentdetect.SentenceModel;
import opennlp.tools.tokenize.Tokenizer;
import opennlp.tools.tokenize.TokenizerME;
import opennlp.tools.tokenize.TokenizerModel;

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
        String fakeParagrah = "jkjfka;jdsajf";
        SentenceDetector sentenceDetector;
        Tokenizer tokenizer;
        InputStream modelSD = new FileInputStream("src/en-sent.bin");
        InputStream modelTK = new FileInputStream("src/en-token.bin");
        InputStream modelParser = new FileInputStream("src/en-parser-chunking.bin");
        ParserModel parserModel = new ParserModel(modelParser);
        Parser parser = ParserFactory.create(parserModel);
        Parse parse[] = ParserTool.parseLine(fakeParagrah,parser,1);
        SentenceModel sentenceModel = new SentenceModel(modelSD);
        sentenceDetector = new SentenceDetectorME(sentenceModel);
        TokenizerModel tokenizerModel = new TokenizerModel(modelTK);
        tokenizer = new TokenizerME(tokenizerModel);

        String[] strings1 = sentenceDetector.sentDetect(fakeParagrah);
        String[] strings2 = tokenizer.tokenize(paragraph);

        for(String s: strings1)
        {
            System.out.println(s);
        }

        for(String s: strings2)
        {
            System.out.println(s);
        }



        modelSD.close();
        modelTK.close();
    }
}
