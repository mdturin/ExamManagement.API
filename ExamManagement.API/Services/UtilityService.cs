using ExamManagement.API.DTOs;

namespace ExamManagement.API.Services;

public class UtilityService
{
    public bool ValidRegisterRequest(UserDto request)
    {
        return request.Name != string.Empty &&
            request.Email != string.Empty &&
            request.Password != string.Empty;
    }

    public bool ValidLoginRequest(UserLoginDto request)
    {
        return request.Email != string.Empty &&
            request.Password != string.Empty;
    }
}
