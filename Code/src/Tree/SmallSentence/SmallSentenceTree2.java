package Tree.SmallSentence;

import Tree.Tree;
import opennlp.tools.cmdline.parser.ParserTool;
import opennlp.tools.parser.Parse;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 8/8/13
 * Time: 4:04 PM
 * To change this template use File | Settings | File Templates.
 */
public class SmallSentenceTree2 extends Tree
{
    public SmallSentenceTree2()
    {
        super();
    }

    @Override
    public void Reply(String sentence)
    {
        try
        {
            String stripProNoun = "";
            String stringAdjective = "";
            String stringNoun = "";
            Parse parse[] = ParserTool.parseLine(sentence, parser, 1);
            Parse[] parserChildren = parse[0].getChildren();
            stripProNoun = grabProNoun(parserChildren);
            stringAdjective = grabAdjective(parserChildren);
            stringNoun = grabNoun(parserChildren);
            String reply = "Weird, not sure why " + stripProNoun + " would have " + stringAdjective + " " + stringNoun;
            System.out.println(reply);
        }

        catch(Exception e)
        {
            e.printStackTrace();
        }
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

    public String grabProNoun(Parse[] children)
    {
        String grabbedProNoun = "";
        boolean isNotFound = true;
        int nextChild = 0;

        while(isNotFound)
        {
            if(children[nextChild].getType().equals("NP"))
            {
                Parse[] parseGrandChildren = children[nextChild].getChildren();

                for(int i = 0; i < parseGrandChildren.length;i++)
                {
                    if(parseGrandChildren[i].getType().equals("PRP"))
                    {
                        grabbedProNoun = parseGrandChildren[i].toString();
                        isNotFound = false;
                    }
                }
            }

            else if((nextChild + 1) == children.length)
            {
                isNotFound = false;
                for(int i = 0; i < children.length;i++)
                {
                    grabbedProNoun = grabProNoun(children[i].getChildren());
                    if(!grabbedProNoun.equals(""))
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

        return grabbedProNoun;
    }
}
