namespace Authorization
{
    /// <summary>
    /// Represents an user using our applications.
    /// </summary>
    public class User
    {
        public Guid Id { get; }

        private readonly IDictionary<Guid, Role> _roles;

        public User(Guid id, IEnumerable<Role> roles)
        {
            Id = id;
            _roles = roles?.DistinctBy(r => r.Id).ToDictionary(r => r.Id, r=> r) ?? new Dictionary<Guid, Role>();
        }

        /// <summary>
        /// The permissions of the user.
        /// </summary>
        public IReadOnlyList<string> Permissions => _roles.SelectMany(r => r.Value.Permissions).Distinct().ToList().AsReadOnly();

        /// <summary>
        /// Add a role to the current user.
        /// </summary>
        public void AddRole(Role role)
        {
            if (role!= null && _roles.ContainsKey(role.Id))
            {
                _roles.Add(role.Id, role);  
            }
        }

        /// <summary>
        /// To remove a role to the current user.
        /// </summary>
        /// <param name="roleId">The role identifier</param>
        public void RemoveRole(Guid roleId)
        {
            if (_roles.ContainsKey(roleId))
            {
                _roles.Remove(roleId);
            }
        }

        /// <summary>
        /// To check if the user has a given permission.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns>True if the user has the permission.</returns>
        public bool IsAllowed(string permission)
        {
            foreach(var role in _roles.Values)
            {
                if (role.HasPermission(permission)) 
                    return true;
            }
            return false;
        }
    }
}
