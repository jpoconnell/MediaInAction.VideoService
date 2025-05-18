using System;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class TestData : ISingletonDependency
{
    public string CurrentUserEmail { get; set; } = "galip.erdem@volosoft.com";
    public Guid CurrentUserId { get; set; } = Guid.NewGuid();
    public string CurrentUserName { get; set; } = "Galip T. ERDEM";
    public Guid TestUserId { get; set; } = Guid.NewGuid();
    public string TestUserEmail { get; set; } = "test@user.com";
    public string TestUserName { get; set; } = "Test User";
    
    public string SeriesName1 { get; set; } = "FBI";
    public string SeriesName2 { get; set; } = "Law and Order";
    public string SeriesName3 { get; set; } = "The Lincoln Layer";
    public string SeriesName4 { get; set; } = "SWAT";
    public int SeriesYear1 { get; set; } = 2020;
    public int SeriesYear2 { get; set; } = 2023;
    public int SeriesYear3 { get; set; } = 2016;
    public int SeriesYear4 { get; set; } = 2010;
 
    public string MovieName1 { get; set; } = "2001";
    public string MovieName2 { get; set; } = "No Hard Feelings";
    public string MovieName3 { get; set; } = "The Lincoln";
    public string MovieName4 { get; set; } = "Test Movie";
    public int MovieYear1 { get; set; } = 2020;
    public int MovieYear2 { get; set; } = 2018;
    public int MovieYear3 { get; set; } = 2016;
    public int MovieYear4 { get; set; } = 2010;
    // episodes
    public string EpisodeName1 { get; set; } = "Pilot";
    public string EpisodeName2 { get; set; } = "First Show";
    public string EpisodeName3 { get; set; } = "Second Show";
  
    public string EpisodeName34 { get; set; } = "Third Show";
    
    public int EpisodeSeason1  { get; set; } = 1;
    public int EpisodeSeason2 { get; set; } = 2;
    public int EpisodeSeason3  { get; set; } = 3;
    public int EpisodeSeason4  { get; set; } = 4;
        
    public int EpisodeNum1  { get; set; } = 1;
    public int EpisodeNum2 { get; set; } = 2;
    public int EpisodeNum3  { get; set; } = 3;
    public int EpisodeNum4  { get; set; } = 4;
    public string Slug1 { get; set; } = "fbi";
    public string Slug2 { get; set; } = "law-and-order";

    //
    public string ToBeMapped1Alias = "fbi";
    public string ToBeMapped2Alias = "law and order";
}