using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories

{

    public interface IJwtFactory
    {
        string GeneratedEncodedToken(User user);
    }
}
