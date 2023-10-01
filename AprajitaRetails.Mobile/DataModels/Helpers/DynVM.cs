namespace AprajitaRetails.Mobile.DataModels
{
    //TODO: Move to Shared Project
    public class DynVM
    {
        public string StoreId { get; set; }

        public string DisplayMember { get; set; }
        public string DisplayData { get; set; }

        public string ValueMember { get; set; }
        public string ValueData { get; set; }
        public int ValueIntData { get; set; }

        public string BoolMember { get; set; }
        public bool BoolValue { get; set; }
    }
}