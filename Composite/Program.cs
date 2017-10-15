using CommonClientLib;

namespace Composite
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private const string INVALID_CHOICE_MESSAGE = "You entered an invalid choice. I'm not mad; just disappointed. Let's try again I guess.\n";

        static void Main(string[] args)
        {
            TxtPrinter.PrintAppTitle("WELCOME TO THE COMPOSITE PROGRAM -- WHICH IS A SOMEWHAT INTERESTING PROGRAM");

            var keepLooping = true;
            while (keepLooping)
            {
                keepLooping = ContinuationDeterminer.GoAgain();
            }
        }
    }
}
