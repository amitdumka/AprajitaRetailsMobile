namespace AprajitaRetails.Mobile.Helpers
{
    public interface ISave
    {
        public Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}