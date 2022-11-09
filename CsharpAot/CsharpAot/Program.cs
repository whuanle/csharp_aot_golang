using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CsharpAot;


public class Program
{
    static void Main()
    {
        var result = Native.Start(1, 2);
        Console.WriteLine($"1 + 2 = {result}");
        Console.ReadKey();
    }
    /// <inheritdoc/>
    /// <exception cref="Win32Exception"></exception>
    public static MemoryStatusExE GetValue()
    {
        var memoryStatusEx = new MemoryStatusExE();
        // 重新初始化结构的大小
        memoryStatusEx.Refresh();
        // 刷新值
        if (!Native.GlobalMemoryStatusEx(ref memoryStatusEx)) throw new Win32Exception("无法获得内存信息");
        return memoryStatusEx;
    }
}


unsafe public static partial class Native
{
    [LibraryImport("main.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial Int32 Start(int a, int b);


    [LibraryImport("CsharpExport.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial Int32 Add(Int32 a, Int32 b);

    /// <summary>
    /// 检索有关系统当前使用物理和虚拟内存的信息
    /// </summary>
    /// <remarks><see href="https://docs.microsoft.com/zh-cn/windows/win32/api/sysinfoapi/nf-sysinfoapi-globalmemorystatusex"/></remarks>
    /// <param name="lpBuffer"></param>
    /// <returns></returns>
    [LibraryImport("Kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial Boolean GlobalMemoryStatusEx(ref MemoryStatusExE lpBuffer);
}
