using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jcoleman_Capstone_JokeBot
{
    class Converter
    {
        public Converter()
        {
        }

        public bool checkForReduce(Stack<Symbols> stack)
        {
            return
                (stack.Contains(Symbols.Subject) && stack.Contains(Symbols.Predicate)) ||
                (stack.Contains(Symbols.Noun) && stack.Contains(Symbols.Article)) ||
                (stack.Contains(Symbols.Noun)) ||
                (stack.Contains(Symbols.Verb) && stack.Contains(Symbols.Preposition)) ||
                (stack.Contains(Symbols.Verb));
        }

        public bool checkForSentence(string[] seperatedWords)
        {
            bool foundSentence = false;
            Stack<Symbols> stack = new Stack<Symbols>();
            int stringsleft = 0;
            while (stringsleft < seperatedWords.Length)
            {
                bool notReduceOnce = true;
                while (checkForReduce(stack) || notReduceOnce)
                {
                    bool isVerb = Enum.IsDefined(typeof(Verbs), seperatedWords[stringsleft]);
                    bool isNoun = Enum.IsDefined(typeof(Nouns), seperatedWords[stringsleft]);
                    bool isArticle = Enum.IsDefined(typeof(Articles), seperatedWords[stringsleft]);

                    if (stack.Count > 0)
                    {
                        if (stack.Peek() == Symbols.Predicate)
                        {
                            stack.Pop();

                            if (stack.Peek() == Symbols.Subject)
                            {
                                stack.Pop();
                                stack.Push(Symbols.Sentence);
                            }

                            else
                            {
                                stack.Push(Symbols.Predicate);
                            }
                        }

                        else if (stack.Peek() == Symbols.Noun)
                        {
                            stack.Pop();

                            if (stack.Peek() == Symbols.Article)
                            {
                                stack.Pop();
                                stack.Push(Symbols.Subject);
                            }

                            else
                            {
                                stack.Push(Symbols.Noun);
                            }
                        }

                        else if (stack.Peek() == Symbols.Noun)
                        {
                            stack.Pop();
                            stack.Push(Symbols.Subject);
                        }

                        else if (stack.Peek() == Symbols.Verb)
                        {
                            stack.Pop();
                            stack.Push(Symbols.Predicate);
                        }

                        else if (isArticle && stack.Count < 1)
                        {
                            stack.Push(Symbols.Article);
                            notReduceOnce = false;
                        }

                        else if ((isVerb &&
                            stack.Peek() == Symbols.Noun || stack.Peek() == Symbols.Subject))
                        {
                            stack.Push(Symbols.Verb);
                            notReduceOnce = false;
                        }

                        else if (isNoun)
                        {
                            stack.Push(Symbols.Noun);
                            notReduceOnce = false;
                        }

                        else
                        {
                            notReduceOnce = false;
                        }
                    }

                    else if (isArticle && stack.Count < 1)
                    {
                        stack.Push(Symbols.Article);
                        notReduceOnce = false;
                    }

                    else if (isNoun)
                    {
                        stack.Push(Symbols.Noun);
                        notReduceOnce = false;
                    }

                    else
                    {
                        notReduceOnce = false;
                    }
                }
                stringsleft++;
            }

            if (stack.Contains(Symbols.Sentence) && stack.Count == 1)
            {
                foundSentence = true;
            }

            return foundSentence;
        }
    }
}
