namespace CleanArchitecture.Domain.Options;

public class AuthOption
{
    public AuthSetting Google { get; set; }
    public AuthSetting Twitter { get; set; }
    public AuthSetting Facebook { get; set; }
}

public class AuthSetting
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}