using Authorization.Exceptions;
using Authorization.Repositories;

namespace Authorization
{
    /// <summary>
    /// Represents a role of permissions.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// The role identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The name of the role.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The permissions in the role.
        /// </summary>
        public IReadOnlyList<string> Permissions => _permissions.ToList().AsReadOnly();

        public readonly HashSet<string> _permissions;

        /// <summary>
        /// Initialize a new instance of a Role.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name"Name of the role</param>
        /// <param name="permissions">The list of permissions</param>
        public Role(Guid id, string name, IEnumerable<string> permissions)
        {
            Id = id;
            Name = name;
            _permissions = new HashSet<string>(permissions ?? Array.Empty<string>());
        }

        /// <summary>
        /// Create a role and check its validity
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name">Name of the role</param>
        /// <param name="permissions">The list of permissions</param>
        /// <exception cref="RoleValidationException">When the role isn't valid.</exception>
        public static Role New(string name, IEnumerable<string> permissions, IRoleRepository roleRepository)
        {
            var role = new Role(Guid.NewGuid(), name, permissions);
            role.Validate(roleRepository);
            return role;
        }

        /// <summary>
        /// Add some permissions to the group.
        /// </summary>
        /// <param name="permissions">Permissions to add.</param>
        public void AddPermissions(IEnumerable<string> permissions)
        {
            foreach (string permission in permissions ?? Array.Empty<string>())
            {
                _permissions.Add(permission);
            }
        }

        /// <summary>
        /// To remove a permission to the role
        /// </summary>
        /// <param name="permission">The permission to remove.</param>
        /// <exception cref="RoleValidationException">When no permission in the role.</exception>
        public void RemovePermissions(string permission)
        {
            if (_permissions.Count <= 1 && _permissions.Contains(permission))
                throw new RoleValidationException(Id, new string[]{ "At least 1 permission is required." } );

             _permissions.Remove(permission);
        }

        /// <summary>
        /// To check if the role has a given permission.
        /// </summary>
        /// <param name="permission">The permission</param>
        /// <returns>True if permission exists, else false.</returns>
        public bool HasPermission(string permission) => _permissions.Contains(permission);

        private void Validate(IRoleRepository roleRepository)
        {
            var errors = new List<string>();

            if (_permissions.Count == 0) errors.Add("At least 1 permission is required.");
            if (roleRepository.GetRoleByName(Name) is not null) errors.Add("Role name must be unique.");

            if (errors.Any())
                throw new RoleValidationException(Id, errors);
        }
    }
}
