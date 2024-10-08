﻿using System;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService;

public class TestData : ISingletonDependency
{
    public string CurrentUserEmail { get; set; } = "galip.erdem@volosoft.com";
    public Guid CurrentUserId { get; set; } = Guid.NewGuid();
    public string CurrentUserName { get; set; } = "Galip T. ERDEM";
    public Guid TestUserId { get; set; } = Guid.NewGuid();
    public string TestUserEmail { get; set; } = "test@user.com";
    public string TestUserName { get; set; } = "Test User";
    
    public string ShowName1 { get; set; } = "Test FBI";
    public string ShowName2 { get; set; } = "Test Law and Order";
    public string ShowName3 { get; set; } = "Test The Lincoln Layer";
    public string ShowName4 { get; set; } = "Test SWAT";
    public int ShowYear1 { get; set; } = 2020;
    public int ShowYear2 { get; set; } = 2023;
    public int ShowYear3 { get; set; } = 2016;
    public int ShowYear4 { get; set; } = 2010;
 
    public int SeasonNum1 { get; set; } = 1;
    public int SeasonNum2 { get; set; } = 2;
    public int EpisodeNum1 { get; set; } = 1;
    public int EpisodeNum2 { get; set; } = 2;
    
    public string ShowSlug1 { get; set; } = "2001";
    public string ShowSlug2 { get; set; } = "the-lincoln-lawyer";
    
    public string MovieSlug1 { get; set; } = "2001-a-space-odyssey";
    public string MovieSlug2 { get; set; } = "the-lincoln-lawyer";
    public string MovieSlug3 { get; set; } = "no-hard-feelings";
    public string MovieName1 { get; set; } = "2001";
    public string MovieName2 { get; set; } = "The Lincoln Lawyer";
    public string MovieName3 { get; set; } = "No Hard Feelings";
    public string MovieName4 { get; set; } = "Test Movie";
    public int MovieYear1 { get; set; } = 2020;
    public int MovieYear2 { get; set; } = 2018;
    public int MovieYear3 { get; set; } = 2016;
    public int MovieYear4 { get; set; } = 2010;
}