namespace myRideApp.Identity.Api;

public static class IdentityApi
{
    public static RouteGroupBuilder MapIdentityApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/auth");

        api.MapPost("/login", LoginAsync);
        api.MapPost("/register", RegisterAsync);
        api.MapGet("/email/confirm", ConfirmEmailAsync);
        api.MapPost("/password-forgot", ForgotPasswordAsync);
        api.MapPost("/password-reset", ResetPasswordAsync);
        api.MapPost("/disable-user", DisableUserAsync);
        api.MapPost("/token/refresh", RefreshTokenAsync);
        api.MapPost("/logout", LogoutAsync);
        return api;
    }

    private static async Task<Results<Created, BadRequest<IEnumerable<IdentityError>>>> RegisterAsync(
        [FromBody] UserRegister request,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IPublishEvents publishEvents,
        IConfiguration config)
    {
        var user = new AppUser { UserName = request.Name, Email = request.Email, State = UserState.Enabled };
        var result = await userManager.CreateAsync(user, request.Password!);

        if (!result.Succeeded)
        { 
            return TypedResults.BadRequest(result.Errors);
        }

        if (!await roleManager.RoleExistsAsync(request.Role!))
        {
            await roleManager.CreateAsync(new IdentityRole(request.Role!));
        }

        await userManager.AddToRoleAsync(user, request.Role!);

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = $"{config["App:BaseUrl"]}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        // TODO: Send confirmationLink via email

        await publishEvents.PublishAsync(
            new RiderProfileUpdatedIntegrationEvent(
                new Guid(user.Id),
                request.Name!,
                request.Email!,
                "en-US",
                DateTime.UtcNow
            )
        , "Rider");

        return TypedResults.Created($"{confirmationLink}");
    }

    private static async Task<Results<Ok<TokenRequest>, UnauthorizedHttpResult>> LoginAsync(
        [FromBody] LoginRequest request,
        UserManager<AppUser> userManager,
        ITokenService tokenService,
        IdentityContext identityContext,
        ILogger<Program> logger
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return TypedResults.Unauthorized();
        }

        if (user!.State == UserState.Disabled || user.State == UserState.Locked)
        {
            logger.LogInformation($"User {user.Id} is disabled");
            return TypedResults.Unauthorized();
        }

        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenService.GenerateAccessToken(user, roles);
        var refreshToken = tokenService.GenerateRefreshToken();

        identityContext.RefreshTokens!.Add(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            AppUser = user,
        });

        await identityContext.SaveChangesAsync();

        logger.LogInformation($"User LogIn successful. Token: {accessToken}");
        
        return TypedResults.Ok(new TokenRequest
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }


    private static async Task<Results<Ok, BadRequest<IEnumerable<IdentityError>>, NotFound>> LogoutAsync(IdentityContext identityContext, [FromBody] RefreshRequest request)
    {
        var token = await identityContext.RefreshTokens!.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);
        if (token == null || token.IsRevoked)
        {
            return TypedResults.NotFound();
        }
        token.IsRevoked = true;
        await identityContext.SaveChangesAsync();

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound, BadRequest<IEnumerable<IdentityError>>>> ResetPasswordAsync(
     IdentityContext identityContext,
     [FromBody] ResetPasswordRequest request,
     UserManager<AppUser> userManager
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return TypedResults.NotFound();
        }
        var result = await userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(request.ResetCode), request.NewPassword);
        return result.Succeeded ? TypedResults.Ok() : TypedResults.BadRequest(result.Errors);
    }

    private static async Task<Results<Ok<string>, NotFound<string>>> DisableUserAsync(
        [FromBody] UserEmailRequest request,
        UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user == null)
        {
            return TypedResults.NotFound("User not found.");
        }

        user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // Effectively disables login
        user.State = UserState.Disabled;
        await userManager.UpdateAsync(user);

        return TypedResults.Ok("User disabled.");

    }
    private static async Task<Results<Ok<TokenRequest>, UnauthorizedHttpResult>> RefreshTokenAsync(
        [FromBody] RefreshRequest request,
        ITokenService tokenService,
        IdentityContext context,
        UserManager<AppUser> userManager
    )
    {
        var storedToken = await context.RefreshTokens!
            .Include(rt => rt.AppUser)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

        if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
        {
            return TypedResults.Unauthorized();
        }

        var roles = await userManager.GetRolesAsync(storedToken.AppUser!);
        var newAccessToken = tokenService.GenerateAccessToken(storedToken.AppUser!, roles);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        storedToken.IsRevoked = true;
        context.RefreshTokens!.Add(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = storedToken.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            AppUser = storedToken.AppUser,
        });

        await context.SaveChangesAsync();

        return TypedResults.Ok(new TokenRequest
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    private static async Task<Results<Ok<string>, BadRequest<string>>> ForgotPasswordAsync(
        [FromBody] UserEmailRequest request,
        UserManager<AppUser> userManager,
        IConfiguration config
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user == null || !await userManager.IsEmailConfirmedAsync(user))
        {
            return TypedResults.BadRequest("Invalid user or email not confirmed.");
        }
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"{config["App:BaseUrl"]}/reset-password?email={request.Email}&token={Uri.EscapeDataString(token)}";

        // TODO: Send resetLink via email
        Console.WriteLine($"Password reset link: {resetLink}");

        return TypedResults.Ok("Password reset link sent.");
    }

    private static async Task<Results<Ok<string>, NotFound<string>, BadRequest<string>>> ConfirmEmailAsync(
        [FromQuery] string userId,
        [FromQuery] string token,
        UserManager<AppUser> userManager
    )
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return TypedResults.NotFound("User not found.");
        }
        var result = await userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded 
            ? TypedResults.Ok("Email confirmed.") 
            : TypedResults.BadRequest("Invalid token.");
    }

}