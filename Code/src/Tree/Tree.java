package Tree;

import opennlp.tools.cmdline.parser.ParserTool;
import opennlp.tools.parser.Parse;
import opennlp.tools.parser.Parser;
import opennlp.tools.parser.ParserFactory;
import opennlp.tools.parser.ParserModel;

import java.io.FileInputStream;
import java.io.InputStream;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 8/1/13
 * Time: 4:35 PM
 * To change this template use File | Settings | File Templates.
 */
public class Tree
{
    public Node startingNode;
    public Parser parser;

    public Tree()
    {

    }

    public Node getStartingNode()
    {
        return startingNode;
    }

    public void setStartingNode(Node startingNode)
    {
        this.startingNode = startingNode;
    }

    public boolean isParsable(String sentence)
    {
        boolean foundSentence = false;

        try
        {
            Parse parse[] = ParserTool.parseLine(sentence, parser, 1);
            String s = parse[0].getChildren()[0].getType();

            if(s.equals("S"))
            {
                foundSentence = true;
            }
        }

        catch (Exception e)
        {
            e.printStackTrace();
        }

        return foundSentence;
    }

    public void Reply(String sentence)
    {

    }
}
