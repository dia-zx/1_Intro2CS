
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0




// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly Lesson7
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- Следующий настраиваемый атрибут добавлен автоматически, не следует отменять преобразование в комментарий -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 02 00 00 00 00 00 ) 

  .custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string) = ( 01 00 07 4C 65 73 73 6F 6E 37 00 00 )             // ...Lesson7..
  .custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string) = ( 01 00 07 4C 65 73 73 6F 6E 37 00 00 )             // ...Lesson7..
  .custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string) = ( 01 00 12 43 6F 70 79 72 69 67 68 74 20 C2 A9 20   // ...Copyright .. 
                                                                                                  20 32 30 32 32 00 00 )                            //  2022..
  .custom instance void [mscorlib]System.Reflection.AssemblyTrademarkAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.GuidAttribute::.ctor(string) = ( 01 00 24 63 34 31 64 30 62 37 31 2D 61 34 65 62   // ..$c41d0b71-a4eb
                                                                                                  2D 34 33 62 31 2D 39 63 62 39 2D 64 61 37 66 61   // -43b1-9cb9-da7fa
                                                                                                  36 38 31 61 37 61 35 00 00 )                      // 681a7a5..
  .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = ( 01 00 07 31 2E 30 2E 30 2E 30 00 00 )             // ...1.0.0.0..
  .custom instance void [mscorlib]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 1C 2E 4E 45 54 46 72 61 6D 65 77 6F 72 6B   // ....NETFramework
                                                                                                        2C 56 65 72 73 69 6F 6E 3D 76 34 2E 37 2E 32 01   // ,Version=v4.7.2.
                                                                                                        00 54 0E 14 46 72 61 6D 65 77 6F 72 6B 44 69 73   // .T..FrameworkDis
                                                                                                        70 6C 61 79 4E 61 6D 65 14 2E 4E 45 54 20 46 72   // playName..NET Fr
                                                                                                        61 6D 65 77 6F 72 6B 20 34 2E 37 2E 32 )          // amework 4.7.2
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module Lesson7.exe
// MVID: {F225F41F-0E4C-4448-930F-330B3F84C8DD}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00020003    //  ILONLY 32BITPREFERRED
// Image base: 0x00840000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit Lesson7.Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Размер кода:       68 (0x44)
    .maxstack  2
    .locals init ([0] string secret)
    IL_0000:  ldstr      "secret"
    IL_0005:  stloc.0
    IL_0006:  ldstr      "Enter password:"
    IL_000b:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0010:  call       string [mscorlib]System.Console::ReadLine()
    IL_0015:  ldloc.0
    IL_0016:  call       bool [mscorlib]System.String::op_Equality(string,
                                                                   string)
    IL_001b:  brtrue.s  IL_0029

    IL_001d:  ldstr      "Welcome!"
    IL_0022:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0027:  br.s       IL_0033

    IL_0029:  ldstr      "Incorrect password!"
    IL_002e:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0033:  ldstr      bytearray (0A 00 1D 04 30 04 36 04 3C 04 38 04 42 04 35 04   // ....0.6.<.8.B.5.
                                    20 00 3B 04 4E 04 31 04 43 04 4E 04 20 00 3A 04   //  .;.N.1.C.N. .:.
                                    3B 04 30 04 32 04 38 04 48 04 43 04 20 00 34 04   // ;.0.2.8.H.C. .4.
                                    3B 04 4F 04 20 00 32 04 4B 04 45 04 3E 04 34 04   // ;.O. .2.K.E.>.4.
                                    30 04 )                                           // 0.
    IL_0038:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_003d:  call       valuetype [mscorlib]System.ConsoleKeyInfo [mscorlib]System.Console::ReadKey()
    IL_0042:  pop
    IL_0043:  ret
  } // end of method Program::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Размер кода:       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Program::.ctor

} // end of class Lesson7.Program


// =============================================================

// *********** ДИЗАССЕМБЛИРОВАНИЕ ЗАВЕРШЕНО ***********************
// ВНИМАНИЕ: создан файл ресурсов Win32 E:\my_work\!GeekBrains\1_Intro2CS\HomeWork\Lesson7\bin\Release\patched.res
