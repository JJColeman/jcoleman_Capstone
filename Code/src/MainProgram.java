import java.io.IOException;
import java.util.Scanner;

/**
 * Created with IntelliJ IDEA.
 * User: jcoleman
 * Date: 7/25/13
 * Time: 1:17 PM
 * To change this template use File | Settings | File Templates.
 */
public class MainProgram
{
    public static void main(String [] args) throws IOException
    {
        JokeBot jokeBot = new JokeBot();

        boolean done = false;
        System.out.println("Welcome to Jokebot");
        System.out.println("type in exit if you are done");
        Scanner scan = new Scanner(System.in);

        while(!done)
        {
            String userInput = scan.nextLine();

            if(userInput.equals("exit") || userInput.equals("Exit"))
            {
                done = true;
            }

            else
            {
                jokeBot.replyToUser(userInput);
            }
        }
    }
}
