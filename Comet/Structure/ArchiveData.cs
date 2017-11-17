namespace Comet.Structure
{
    #region Namespace

    using System;

    #endregion

    public struct ArchiveData
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public DateTimeOffset LastWriteTime { get; set; }

        public Bytes Size { get; set; }

        public Bytes CompressedSize { get; set; }
    }
}