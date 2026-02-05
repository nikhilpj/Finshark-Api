namespace api.Interfaces
{
    public interface IstockRepository
    {
        public Task<List<Stock>> GetAllAsync();
    }
}
