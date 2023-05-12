using ExamManagement.API.Models;

namespace ExamManagement.API.Services;

public class UtilityService
{
    public bool ValidRegisterRequest(UserDto request)
    {
        return request.Name != string.Empty &&
            request.Email != string.Empty &&
            request.Password != string.Empty;
    }
}
