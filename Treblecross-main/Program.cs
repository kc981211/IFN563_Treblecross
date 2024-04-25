namespace Treblecross
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProgramEngine pe = new ProgramEngine();
            Delegate fn = pe.DisplayMenu();
            if (fn == null) return;
            // var opt = fn.DynamicInvoke() ?? new TreblecrossOperator();
            var opt = fn.DynamicInvoke();
            GameOperator gameOp =  (GameOperator) opt;
            gameOp.Start();
        }
    }
}
