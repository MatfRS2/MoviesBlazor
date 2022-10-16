namespace BlazorMoviesApp.Services
{
    public interface IOsvezenjeService
    {
        event Action OsvezenjeJeZahtevano;
        void ZahtevajOsvezenje();
    }
}
