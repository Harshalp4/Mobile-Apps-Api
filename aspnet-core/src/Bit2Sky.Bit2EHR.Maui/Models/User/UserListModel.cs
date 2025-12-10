using Abp.AutoMapper;
using Bit2Sky.Bit2EHR.Authorization.Users.Dto;

namespace Bit2Sky.Bit2EHR.Maui.Models.User;

[AutoMapFrom(typeof(UserListDto))]
public class UserListModel : UserListDto
{
    public string Photo { get; set; }

    public string FullName => Name + " " + Surname;
}