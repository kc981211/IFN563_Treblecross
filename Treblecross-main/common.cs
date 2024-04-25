namespace Treblecross
{
    public static class Common<T>
    {
        public static T[] Convert1dArray(T[,] array){
            T[] ret = new T[array.GetLength(1)];
            for (int j = 0; j < array.GetLength(1); j++) {
                ret[j] = array[0, j];
            }
            return ret;
        }

        public static List<List<T>> Convert2DArrayTo2DList(T[,] ary2D) {
            List<List<T>> list2D =new List<List<T>>();
            for (int i = 0; i < ary2D.GetLength(0); i++) {
                List<T> innerList = new List<T>();
                for (int j = 0; j < ary2D.GetLength(1); j++)
                {
                    innerList.Add(ary2D[i, j]);
                }

                list2D.Add(innerList);

            }

            return list2D;
        
        }

    }

    public static class Log
    {
        public static void Info(string msg)
        {
            Info("System", msg);
        }

        public static void Info(string tag, string msg) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[{0}] {1}", tag, msg);
            Console.ResetColor();
        } 
        
        public static void Warning(string msg) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[System] {0}", msg); 
            Console.ResetColor();
        }

        public static void Error(string msg, Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[System] {0}, Error: {1}", msg, ex.Message); 
            Console.ResetColor();
        }
    }

}