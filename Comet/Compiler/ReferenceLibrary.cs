namespace Comet.Compiler.CodeExpressions
{
    #region Namespace

    using System.CodeDom;

    #endregion

    public class ReferenceLibrary
    {
        #region Events

        /// <summary>A type reference for the Comet class.</summary>
        /// <returns>
        ///     <see cref="CodeTypeReferenceExpression" />
        /// </returns>
        public static CodeTypeReferenceExpression CometType()
        {
            return new CodeTypeReferenceExpression("Comet");
        }

        public static CodeTypeReferenceExpression InstallerType()
        {
            return new CodeTypeReferenceExpression("CometInstall");
        }

        /// <summary>A type reference for the System.Console class.</summary>
        /// <returns>
        ///     <see cref="CodeTypeReferenceExpression" />
        /// </returns>
        public static CodeTypeReferenceExpression SystemConsoleType()
        {
            return new CodeTypeReferenceExpression("System.Console");
        }

        #endregion
    }
}