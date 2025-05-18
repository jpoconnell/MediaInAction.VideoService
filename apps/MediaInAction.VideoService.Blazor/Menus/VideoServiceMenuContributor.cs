using System.Threading.Tasks;
using MediaInAction.VideoService.Localization;
using MediaInAction.VideoService.Permissions;
using Volo.Abp.UI.Navigation;

namespace MediaInAction.Blazor.Menus;

public class VideoServiceMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<VideoServiceResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                "VideoService.Home",
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );

        var bookStoreMenu = new ApplicationMenuItem(
            "BooksStore",
            l["Menu:VideoService"],
            icon: "fa fa-book"
        );

        context.Menu.AddItem(bookStoreMenu);

        //CHECK the PERMISSION
        if (await context.IsGrantedAsync(VideoServicePermissions.Seriess.Default))
        {
            bookStoreMenu.AddItem(new ApplicationMenuItem(
                "BooksStore.Books",
                l["Menu:Books"],
                url: "/books"
            ));
        }

        if (await context.IsGrantedAsync(VideoServicePermissions.Seriess.Default))
        {
            bookStoreMenu.AddItem(new ApplicationMenuItem(
                "BooksStore.Authors",
                l["Menu:Authors"],
                url: "/authors"
            ));
        }

    }
}
