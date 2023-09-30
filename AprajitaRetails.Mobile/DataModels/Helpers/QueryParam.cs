public enum Order
{ Asc, Desc }

namespace AprajitaRetails.Mobile.DataModels
{
    public class QueryParam
    {
        public int Id { get; set; }

        /// <summary>
        /// Use this when ID is of string type
        /// </summary>
        public string Ids { get; set; }

        public List<string> Command { get; set; }
        public List<string> Query { get; set; }

        public Order Order { get; set; }
        public List<string> Filters { get; set; }

        public string StoreId { get; set; }
    }
}