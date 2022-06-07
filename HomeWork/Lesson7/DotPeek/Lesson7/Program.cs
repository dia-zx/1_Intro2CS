// Decompiled with JetBrains decompiler
// Type: Lesson7.Program
// Assembly: Lesson7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F225F41F-0E4C-4448-930F-330B3F84C8DD
// Assembly location: E:\my_work\!GeekBrains\1_Intro2CS\HomeWork\Lesson7\bin\Release\Lesson7.exe

using System;

namespace Lesson7
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      string str = "secret";
      Console.WriteLine("Enter password:");
      if (Console.ReadLine() == str)
        Console.WriteLine("Welcome!");
      else
        Console.WriteLine("Incorrect password!");
      Console.WriteLine("\nНажмите любую клавишу для выхода");
      Console.ReadKey();
    }
  }
}
