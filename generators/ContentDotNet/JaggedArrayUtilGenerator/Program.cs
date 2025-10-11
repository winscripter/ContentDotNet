namespace JaggedArrayUtilGenerator
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Reflection;

    class Program
    {
        const int MaxDimensions = 6;
        const string NamespaceName = "ContentDotNet.Security";
        const string ClassName = "JaggedArrayUtil";
        const string OutputFile = "JaggedArrayUtil.g.cs";

        static void Main()
        {
            var compileUnit = new CodeCompileUnit();
            var codeNamespace = new CodeNamespace(NamespaceName);
            compileUnit.Namespaces.Add(codeNamespace);

            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));

            var jaggedUtilClass = new CodeTypeDeclaration(ClassName)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Class | TypeAttributes.Public
            };
            jaggedUtilClass.StartDirectives.Add(
                new CodeRegionDirective(CodeRegionMode.Start, "\nstatic"));
            jaggedUtilClass.EndDirectives.Add(
                new CodeRegionDirective(CodeRegionMode.End, String.Empty));
            jaggedUtilClass.Attributes |= MemberAttributes.Static;

            codeNamespace.Types.Add(jaggedUtilClass);

            for (int dims = 1; dims <= MaxDimensions; dims++)
            {
                var method = GenerateJaggedArrayMethod(dims);
                jaggedUtilClass.Members.Add(method);
            }

            using var writer = new StreamWriter(OutputFile, false);
            var provider = new CSharpCodeProvider();
            var options = new CodeGeneratorOptions
            {
                BracingStyle = "C",
                IndentString = "    "
            };

            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);

            Console.WriteLine($"Generated {OutputFile}");
        }

        static CodeMemberMethod GenerateJaggedArrayMethod(int dimensions)
        {
            var method = new CodeMemberMethod
            {
                Name = "AllocateJaggedArray",
                Attributes = MemberAttributes.Public | MemberAttributes.Static,
                TypeParameters = { new CodeTypeParameter("T") }
            };

            string jaggedType = "T" + RepeatString("[]", dimensions);
            method.ReturnType = new CodeTypeReference(jaggedType);

            // Parameters
            method.Parameters.Add(new CodeParameterDeclarationExpression("this Configuration", "cfg"));
            for (int i = 0; i < dimensions; i++)
                method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), $"size{i}"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "elementSize"));

            // Body: Memory Allocation
            string sizeExpr = string.Join(" * ", GenerateSizeNames(dimensions));
            method.Statements.Add(new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("cfg"), "MemoryAllocator"),
                    "RequestMemory",
                    [new CodeSnippetExpression($"elementSize * {sizeExpr}")]
                )
            ));

            // Body: Root declaration
            var jaggedArrayDecl = new CodeSnippetStatement($"        {jaggedType} array = new T[size0]{string.Concat(Enumerable.Repeat("[]", dimensions - 1))};");
            method.Statements.Add(jaggedArrayDecl);

            // Nested loops
            string arrayPath = "array";
            string indent = "        ";
            for (int i = 0; i < dimensions - 1; i++)
            {
                string idx = $"i{i}";
                method.Statements.Add(new CodeSnippetStatement($"{indent}for (int {idx} = 0; {idx} < size{i}; {idx}++) {{"));
                indent += "    ";
                arrayPath += $"[{idx}]";
                string innerAlloc = $"{indent}{arrayPath} = new T[size{i + 1}]{(RepeatString("[]", dimensions - 2 - i))};";
                method.Statements.Add(new CodeSnippetStatement(innerAlloc));
            }

            for (int i = 0; i < dimensions - 1; i++)
            {
                method.Statements.Add(new CodeSnippetStatement(indent + "}"));
                indent = indent[^4..];
            }

            // Return statement
            method.Statements.Add(new CodeSnippetStatement("        return array;"));

            return method;
        }

        static string[] GenerateSizeNames(int dims)
        {
            var names = new string[dims];
            for (int i = 0; i < dims; i++)
                names[i] = $"size{i}";
            return names;
        }

        private static string RepeatString(string str, int nTimes)
        {
            return string.Concat(Enumerable.Repeat(str, nTimes));
        }
    }

    // Stub for Configuration and MemoryAllocator for compilation
    public class Configuration
    {
        public MemoryAllocator MemoryAllocator { get; } = new MemoryAllocator();
    }

    public class MemoryAllocator
    {
        public void RequestMemory(int bytes)
        {
            // Stub
        }
    }
}
