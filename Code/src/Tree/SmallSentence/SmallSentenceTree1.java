package Tree.SmallSentence;
import Tree.Node;
import Tree.Tree;
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
 * Time: 5:44 PM
 * To change this template use File | Settings | File Templates.
 */
public class SmallSentenceTree1 extends Tree{

    public SmallSentenceTree1()
    {
        super();
    }

    @Override
    public void Reply(String sentence)
    {
        try
        {
            String stripNoun = "";
            String stringVerb = "";
            String stringAdjective = "";
            Parse parse[] = ParserTool.parseLine(sentence,parser,1);
            Parse[] parserChildren = parse[0].getChildren();
            stripNoun = grabNoun(parserChildren);
            stringVerb = grabVerb(parserChildren);
            stringAdjective = grabAdjective(parserChildren);
            String reply = "Interesting why a" + stripNoun + " " + stringVerb + " " + stringAdjective;

            System.out.println(reply);
        }

        catch(Exception e)
        {
            e.printStackTrace();
        }
    }

    public boolean checkReply(String stripNoun,String stripVerb,String stripAdjective)
    {
        return stripNoun.equals("") || stripAdjective.equals("") || stripVerb.equals("");
    }

    public String grabAdjective(Parse[] children)
    {
        String grabbedAdjective = "";
        boolean isNotFound = true;
        int nextChild = 0;

        while(isNotFound)
        {
            if(children[nextChild].getType().equals("ADJP"))
            {
                String[] NounSymbols = new String[]{"JJ","JJR","JJS"};

                Parse[] parseGrandChildren = children[nextChild].getChildren();

                for(int i = 0; i < parseGrandChildren.length;i++)
                {
                    for(String symbols: NounSymbols)
                    {
                        if(parseGrandChildren[i].getType().equals(symbols))
                        {
                            grabbedAdjective = parseGrandChildren[i].toString();
                            isNotFound = false;
                        }
                    }
                }
            }

            else if((nextChild + 1) == children.length)
            {
                isNotFound = false;
                for(int i = 0; i < children.length;i++)
                {
                    if(children[i].getChildren().length == 0)
                    {

                    }

                    else
                    {
                        grabbedAdjective = grabAdjective(children[i].getChildren());
                    }

                    if(!grabbedAdjective.equals(""))
                    {
                        i = children.length;
                    }
                }
            }

            else
            {
                nextChild++;
            }
        }

        return grabbedAdjective;
    }

    public String grabNoun(Parse[] children)
    {
        String grabbedNoun = "";
        boolean isNotFound = true;
        int nextChild = 0;

        while(isNotFound)
        {
            if(children[nextChild].getType().equals("NP"))
            {
                String[] NounSymbols = new String[]{"NN","NNS","NNP","NNPS"};

                Parse[] parseGrandChildren = children[nextChild].getChildren();

                for(int i = 0; i < parseGrandChildren.length;i++)
                {
                    for(String symbols: NounSymbols)
                    {
                        if(parseGrandChildren[i].getType().equals(symbols))
                        {
                            grabbedNoun = parseGrandChildren[i].toString();
                            isNotFound = false;
                        }
                    }
                }
            }

            else if((nextChild + 1) == children.length)
            {
                isNotFound = false;
                for(int i = 0; i < children.length;i++)
                {
                    grabbedNoun = grabNoun(children[i].getChildren());
                    if(!grabbedNoun.equals(""))
                    {
                        i = children.length;
                    }
                }
            }

            else
            {
                nextChild++;
            }
        }

        return grabbedNoun;
    }

    public String grabVerb(Parse[] children)
    {
        String grabbedVerb = "";
        boolean isNotFound = true;
        int nextChild = 0;

        while(isNotFound)
        {
            if(children[nextChild].getType().equals("VP"))
            {
                String[] VerbSymbols = new String[]{"VB","VBD","VBG","VBN", "VBP", "VBZ"};

                Parse[] parseGrandChildren = children[nextChild].getChildren();

                for(int i = 0; i < parseGrandChildren.length;i++)
                {
                    for(String symbols: VerbSymbols)
                    {
                        if(parseGrandChildren[i].getType().equals(symbols))
                        {
                            grabbedVerb = parseGrandChildren[i].toString();
                            isNotFound = false;
                        }
                    }
                }
            }

            else if((nextChild + 1)  == children.length)
            {
                isNotFound = false;
                for(int i = 0; i < children.length;i++)
                {
                    grabbedVerb = grabVerb(children[i].getChildren());
                    if(!grabbedVerb.equals(""))
                    {
                        i = children.length;
                    }
                }
            }

            else
            {
                nextChild++;
            }
        }

        return grabbedVerb;
    }

}
