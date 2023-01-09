using System.Threading;


namespace CreditCards.UITests
{
    internal static class DemoHelper
    {
        ///// <summary>
        ///// Brief delay to slow down browser 
        ///// interactions to see each step
        ///// </summary>

        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}
