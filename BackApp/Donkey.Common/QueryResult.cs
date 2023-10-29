using System.Collections.Generic;

namespace Donkey.Common
{
    /// <summary>
    /// Response
    /// </summary>
    /// <typeparam name="TResult">Data type</typeparam>
    public class QueryResult<TResult> where TResult : class
    {
        /// <summary>
        /// List
        /// </summary>
        public IList<TResult> Items { get; set; }

        /// <summary>
        /// Count        
        /// </summary>
        public long? Count { get; set; }
    }
}
