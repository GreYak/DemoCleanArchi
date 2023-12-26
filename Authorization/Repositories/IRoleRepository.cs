namespace Authorization.Repositories
{
    /// <summary>
    /// Manage the <see cref="Role"/> persistancy.
    /// </summary>
    public interface IRoleRepository
    {
        Role GetRoleByName(string name);
    }
}
