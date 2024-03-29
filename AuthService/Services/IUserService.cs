﻿using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
        
        Task<List<GroupDto>> GetGroups();

        Task<UserDto> AddUser(UserDto data);

        Task<GroupDto> AddGroup(CreateGroupDto data);

        Task<string> AddRole(string data);

        Task<List<UserDto>> AddUsersToGroup(int GroupId, List<UserDto> data);

        Task<List<UserDto>> GetUsersByGroupId(int Id);
    }
}
