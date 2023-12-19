namespace P7CreateRestApi.Repositories
{
    public interface IJwtFactory
    {
        string GeneratedEncodedToken(string userId, string userName, IList<string> roles);
    }
}