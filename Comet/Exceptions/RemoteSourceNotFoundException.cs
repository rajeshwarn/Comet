namespace Comet.Exceptions
{
    #region Namespace

    using System;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>The remote source was not found exception.</summary>
    [Serializable]
    public class RemoteSourceNotFoundException : Exception
    {
        #region Variables

        private string _remoteSource;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="RemoteSourceNotFoundException" /> class.</summary>
        public RemoteSourceNotFoundException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="RemoteSourceNotFoundException" /> class.</summary>
        /// <param name="message">The message.</param>
        public RemoteSourceNotFoundException(string message) : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="RemoteSourceNotFoundException" /> class.</summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected RemoteSourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _remoteSource = info.GetString("_remoteSource");
        }

        #endregion

        #region Properties

        /// <summary>The remote source.</summary>
        public string RemoteSource
        {
            get
            {
                return _remoteSource;
            }

            set
            {
                _remoteSource = value;
            }
        }

        #endregion

        #region Events

        /// <summary>Get the object data.</summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("_remoteSource", _remoteSource);
        }

        #endregion
    }
}