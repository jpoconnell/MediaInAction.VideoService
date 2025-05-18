using System;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.DelugeService;

public class TestData : ISingletonDependency
{
    public string CurrentUserEmail { get; set; } = "galip.erdem@volosoft.com";
    public Guid CurrentUserId { get; set; } = Guid.NewGuid();
    public string CurrentUserName { get; set; } = "Galip T. ERDEM";
    public Guid TestUserId { get; set; } = Guid.NewGuid();
    public string TestUserEmail { get; set; } = "test@user.com";
    public string TestUserName { get; set; } = "Test User";
    
    public string TorrentName1 { get; set; } = "Test FBI";
    public string TorrentName2 { get; set; } = "Test Law and Order";
    public string TorrentName3 { get; set; } = "Test The Lincoln Layer";
    public string TorrentName4 { get; set; } = "Test SWAT";
    
    public string Hash1 { get; set; } = "test-fbi";
    public string Hash2 { get; set; } = "test-law-and-order";
    public string Hash3 { get; set; } = "test-the-Lincoln-lawyer";
    public string Hash5 { get; set; } = "test-swat";
    
    
    public string EmbySeriesId1 { get; set; } = "1";
}