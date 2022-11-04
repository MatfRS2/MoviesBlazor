namespace BlazorMoviesApp.Services
{
    public class OsvezenjeService:IOsvezenjeService
    {
        public event Action OsvezenjeJeZahtevano;
        public void ZahtevajOsvezenje()
        {
            OsvezenjeJeZahtevano?.Invoke();
        }
    }
}
