using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.Application.ClaimUser;

// class UserContext có nhiệm vụ lấy ra thông tin người dùng từ trong HttpContext  
public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        // lấy thông tin user từ HttpContext nếu k có thì throw ra exception 
        var user = httpContextAccessor?.HttpContext?.User ?? throw new InvalidOperationException("User context is not present");

        // kiểm tra thuộc tính Identity của user có null và nếu có danh tính thì đã được xác thực chưa 
        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null; 
        }

        // bước lấy thông tin cụ thể của claim trong User 
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        
        // các claim có thể thêm vào xử lý tùy 
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateOfBirthString == null 
            ? (DateOnly?)null 
            : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

        // Nhét vào CurrentUser và trả ra 
        return new CurrentUser(userId, email, roles, nationality, dateOfBirth);
    }
}