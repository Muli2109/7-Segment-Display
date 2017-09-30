namespace _7_Segment_Anzeige.Exceptions
{
    #region [ REFERENCES ]

    using System;

    #endregion [ REFERENCES ]

    public class ExceptionDictionaryNotFound : Exception
    {
        public ExceptionDictionaryNotFound(string exception)
        {
            throw new Exception(exception);
        }
    }
}