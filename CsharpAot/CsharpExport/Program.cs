using System.Runtime.InteropServices;

namespace CsharpExport
{
    public class Export
    {
        [UnmanagedCallersOnly(EntryPoint = "Add")]
        public static int Add(int a, int b)
        {
            return a + b;
        }
    }
}