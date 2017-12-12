namespace Comet.Compiler
{
    #region Namespace

    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.CSharp;

    #endregion

    public class CodeDomCompiler
    {
        #region Events

        /// <summary>Build the CSharp code into an executable.</summary>
        /// <param name="references">The references.</param>
        /// <param name="sources">The source code.</param>
        /// <param name="output">The output file.</param>
        /// <param name="embeddedResources">The embedded Resources.</param>
        /// <returns>The <see cref="CompilerResults" />.</returns>
        public static CompilerResults Build(List<string> references, string[] sources, string output, List<string> embeddedResources)
        {
            CSharpCodeProvider _codeProvider = ConstructCodeProvider();

            CompilerParameters _compilerParameters = new CompilerParameters
                {
                    GenerateExecutable = true,
                    OutputAssembly = output,
                    TreatWarningsAsErrors = false

                    // CompilerOptions = "/target:winexe"
                };

            foreach (string _reference in references)
            {
                _compilerParameters.ReferencedAssemblies.Add(_reference);
            }

            foreach (string _embeddedResource in embeddedResources)
            {
                _compilerParameters.EmbeddedResources.Add(_embeddedResource);
            }

            return _codeProvider.CompileAssemblyFromSource(_compilerParameters, sources);
        }

        /// <summary>Build the code unit into an executable.</summary>
        /// <param name="codeCompileUnit">The code Compile Unit.</param>
        /// <param name="references">The references.</param>
        /// <param name="output">The output file.</param>
        /// <returns>The <see cref="CompilerResults" />.</returns>
        public static CompilerResults Build(CodeCompileUnit codeCompileUnit, List<string> references, string output)
        {
            CSharpCodeProvider _codeProvider = ConstructCodeProvider();

            CompilerParameters _compilerOptions = new CompilerParameters(references.ToArray(), output);
            _compilerOptions.GenerateExecutable = true;

            return _codeProvider.CompileAssemblyFromDom(_compilerOptions, codeCompileUnit);
        }

        /// <summary>CreateInstallerCode the CSharp code provider.</summary>
        /// <param name="frameworkVersion">The framework version.</param>
        /// <returns>
        ///     <see cref="CSharpCodeProvider" />
        /// </returns>
        public static CSharpCodeProvider ConstructCodeProvider(string frameworkVersion = "v4.0")
        {
            CSharpCodeProvider _codeProvider = new CSharpCodeProvider(new Dictionary<string, string>
                {
                    {
                        "CompilerVersion", frameworkVersion
                    }
                });

            return _codeProvider;
        }

        /// <summary>Reads a compile unit to source text.</summary>
        /// <param name="codeCompileUnit">The compile unit.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string GenerateSource(CodeCompileUnit codeCompileUnit)
        {
            string _tempFilename = Path.GetTempFileName();

            CSharpCodeProvider _provider = ConstructCodeProvider();

            CodeGeneratorOptions _generatorOptions = new CodeGeneratorOptions
                {
                    // Keep the braces on the line following the statement or declaration that they are associated with.
                    BracingStyle = "C",
                    IndentString = "    ",
                    BlankLinesBetweenMembers = true
                };

            // Build the source file name with the appropriate language extension.
            string _sourceFile = Path.GetTempPath() + @"\";
            if (_provider.FileExtension[0] == '.')
            {
                _sourceFile = _tempFilename + _provider.FileExtension;
            }
            else
            {
                _sourceFile = _tempFilename + "." + _provider.FileExtension;
            }

            IndentedTextWriter _indentedTextWriter = new IndentedTextWriter(new StreamWriter(_sourceFile, false), "    ");

            _provider.GenerateCodeFromCompileUnit(codeCompileUnit, _indentedTextWriter, _generatorOptions);
            _indentedTextWriter.Close();

            string _sourceCode = File.ReadAllText(_sourceFile);
            File.Delete(_tempFilename);
            return _sourceCode;
        }

        public void CombineStream(string[] sources, string destinationFileName)
        {
            using (Stream _destinationStream = File.OpenWrite(destinationFileName))
            {
                foreach (string _source in sources)
                {
                    using (Stream sourceStream = File.OpenRead(_source))
                    {
                        sourceStream.CopyTo(_destinationStream);
                    }
                }
            }
        }

        #endregion
    }
}