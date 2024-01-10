namespace JWT.GeneratorBlazor.Layout;

public partial class MainLayout
{
    private Sidebar sidebar = default!;
    private IEnumerable<NavItem> navItems = default!;

    private async Task<SidebarDataProviderResult> SidebarDataProvider(SidebarDataProviderRequest request)
    {
        if (navItems is null)
            navItems = GetNavItems();

        return await Task.FromResult(request.ApplyTo(navItems));
    }

    private IEnumerable<NavItem> GetNavItems()
    {
        navItems = new List<NavItem>
        {
#if DEBUG
            new NavItem { Id = "1", Href = "/", IconName = IconName.HouseDoorFill, Text = "Home", Match=NavLinkMatch.All},
#else
            new NavItem { Id = "1", Href = "/JWT.GeneratorBlazor", IconName = IconName.HouseDoorFill, Text = "Home", Match=NavLinkMatch.All},
#endif
        };

        return navItems;
    }
}
