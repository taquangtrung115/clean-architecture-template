namespace CleanArchitectureTemplate.Application.ClaimUser;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}