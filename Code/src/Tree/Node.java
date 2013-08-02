package Tree;
/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 8/1/13
 * Time: 4:31 PM
 * To change this template use File | Settings | File Templates.
 */
public class Node
{
    private String word;
    private Node leftBranch;
    private Node rightBranch;

    public Node(String word)
    {
        this.word = word;
    }

    public void setLeftBranch(Node leftNode)
    {
        this.leftBranch = leftNode;
    }

    public void setRightBranch(Node rightNode)
    {
        this.rightBranch = rightNode;
    }

    public void setWord(String word)
    {
        this.word = word;
    }

    public Node getLeftBranch()
    {
        return leftBranch;
    }

    public Node getRightBranch()
    {
        return rightBranch;
    }

    public String getWord()
    {
        return word;
    }
}
