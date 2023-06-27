using API.Contracts;
using API.DTOs.Roles;
using API.Models;

namespace API.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleDto>? GetRole()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return null; // No roles found
            }

            var toDto = roles.Select(role =>
                                                new RoleDto
                                                {
                                                    GUID = role.GUID,
                                                    Name = role.Name
                                                }).ToList();

            return toDto; // Universities found
        }

        public RoleDto? GetRole(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return null; // Role not found
            }

            var toDto = new RoleDto
            {
                GUID = role.GUID,
                Name = role.Name
            };

            return toDto; // Universities found
        }

        public RoleDto? CreateRole(NewRoleDto newRoleDto)
        {
            var role = new Role
            {
                GUID = new Guid(),
                Name = newRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdRole = _roleRepository.Create(role);
            if (createdRole is null)
            {
                return null; // Role not created
            }

            var toDto = new RoleDto
            {
                GUID = createdRole.GUID,
                Name = createdRole.Name
            };

            return toDto; // Role created
        }

        public int UpdateRole(RoleDto updateRoleDto)
        {
            var isExist = _roleRepository.IsExist(updateRoleDto.GUID);
            if (!isExist)
            {
                return -1; // Role not found
            }

            var getRole = _roleRepository.GetByGuid(updateRoleDto.GUID);

            var role = new Role
            {
                GUID = updateRoleDto.GUID,
                Name = updateRoleDto.Name,
                ModifiedDate = DateTime.Now,
                CreatedDate = getRole!.CreatedDate
            };


            var isUpdate = _roleRepository.Update(role);
            if (!isUpdate)
            {
                return 0; // Role not updated
            }

            return 1;

        }

        public int DeleteRole(Guid guid)
        {
            var isExist = _roleRepository.IsExist(guid);
            if (!isExist)
            {
                return -1; // Role not found
            }

            var role = _roleRepository.GetByGuid(guid);
            var isDelete = _roleRepository.Delete(role!.GUID);
            if (!isDelete)
            {
                return 0; // Role not deleted
            }

            return 1;
        }
    }
}
