package Tree.SmallSentence;

import Tree.Tree;
import opennlp.tools.cmdline.parser.ParserTool;
import opennlp.tools.parser.Parse;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 8/8/13
 * Time: 4:08 PM
 * To change this template use File | Settings | File Templates.
 */
public class SmallSentenceTree4 extends Tree
{
    public SmallSentenceTree4()
    {
        super();
    }

    @Override
    public void Reply(String sentence)
    {
        try
        {
            String stringVerb = "";
            String stringNoun = "";
            Parse parse[] = ParserTool.parseLine(sentence, parser, 1);
            Parse[] parserChildren = parse[0].getChildren();
            stringNoun = grabNoun(parserChildren);
            stringVerb = grabVerb(parserChildren);
            String reply = "No, I can't " + stringVerb + " the " + stringNoun;
            System.out.println(reply);
        }

        catch(Exception e)
        {
            e.printStackTrace();
        }
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
