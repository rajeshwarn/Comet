namespace Comet.Compiler.CodeExpressions
{
    #region Namespace

    using System.CodeDom;

    #endregion

    public class MethodInvoke
    {
        #region Events

        /// <summary>Method: Execute comet method.</summary>
        /// <param name="method">The method.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression ExecuteCometMethod(string method)
        {
            return new CodeMethodInvokeExpression(ReferenceTypes.CometType(), method);
        }

        /// <summary>Invoke the method expression.</summary>
        /// <param name="nameSpace">The namespace.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression InvokeMethod(CodeTypeReferenceExpression nameSpace, string methodName)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(nameSpace, methodName));
        }

        /// <summary>Invoke the method expression.</summary>
        /// <param name="nameSpace">The namespace.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression InvokeMethod(string nameSpace, string methodName)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(nameSpace), methodName));
        }

        /// <summary>Method: ReadLine.</summary>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression ReadLineExpression()
        {
            return new CodeMethodInvokeExpression(ReferenceTypes.SystemConsoleType(), "ReadLine");
        }

        /// <summary>Method: WriteLine.</summary>
        /// <param name="message">The message to write.</param>
        /// <returns>
        ///     <see cref="CodeMethodInvokeExpression" />
        /// </returns>
        public static CodeMethodInvokeExpression WriteLineExpression(string message)
        {
            return new CodeMethodInvokeExpression(ReferenceTypes.SystemConsoleType(), "WriteLine", new CodePrimitiveExpression(message));
        }

        #endregion
    }
}