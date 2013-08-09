package Tree.Greetings;

import Tree.Tree;
import opennlp.tools.cmdline.parser.ParserTool;
import opennlp.tools.parser.Parse;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 8/8/13
 * Time: 3:55 PM
 * To change this template use File | Settings | File Templates.
 */
public class GreetingTree2 extends Tree
{
    public GreetingTree2()
    {
        super();
    }

    @Override
    public void Reply(String sentence)
    {
        System.out.println("Doing pretty good");
    }

    @Override
    public boolean isParsable(String sentence)
    {
        boolean foundSentence = false;

        try
        {
            Parse parse[] = ParserTool.parseLine(sentence, parser, 1);
            String s = parse[0].getChildren()[0].getType();

            if(s.equals("SBAR") || s.equals("SBARQ"))
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
}
