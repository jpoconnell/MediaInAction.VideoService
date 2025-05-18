using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService;

public class TestData : ISingletonDependency
{
    public string CurrentUserEmail { get; set; } = "galip.erdem@volosoft.com";
    public Guid CurrentUserId { get; set; } = Guid.NewGuid();
    public string CurrentUserName { get; set; } = "Galip T. ERDEM";
    public Guid TestUserId { get; set; } = Guid.NewGuid();
    public string TestUserEmail { get; set; } = "test@user.com";
    public string TestUserName { get; set; } = "Test User";
    
    public string Filename1 { get; set; } = "Test FBI";
    public string Filename2 { get; set; } = "Test Law and Order";
    public string Filename3 { get; set; } = "Test The Lincoln Layer";
    public string Filename4 { get; set; } = "Test SWAT";
    
    public string Server { get; set; } = "local";
    
    public string Directory1 { get; set; } = "/uncompressed";
    public string Directory2 { get; set; } = "/media/mutimrdei";
    public string Directory3 { get; set; } = "Test FBI";
    public string Directory4 { get; set; } = "Test FBI";

    public ListType List1 { get; set; } = ListType.Compressed;
    public ListType List2 { get; set; } = ListType.Uncompressed;
    public ListType List3 { get; set; } = ListType.Current;
    
    public string Extn1 { get; set; } = ".mkv";
    public string Extn2 { get; set; } = ".mp4";
}