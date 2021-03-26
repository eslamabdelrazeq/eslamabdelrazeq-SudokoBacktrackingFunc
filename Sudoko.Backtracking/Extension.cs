using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko.Backtracking
{
    public static class Extension
    {
        public static List<Func<int[,], int, int, int, bool>> AssignBehavior(this List<Func<int[,], int, int, int, bool>> obj, Func<int[,], int, int, int, bool> behavior)
        {
            obj.Add(behavior);
            return obj;
        }
    }
}
