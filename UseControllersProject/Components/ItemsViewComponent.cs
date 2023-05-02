using Microsoft.AspNetCore.Mvc;

namespace UseMVCProject.Components
{
    public class ItemsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            //return Content("SomeText");
            return
                View("Components/Items/_ItemsViewComponent.cshtml", items);
        }

        public static Task<List<string>> GetItemsAsync()
        {
            return Task.FromResult(new List<string> { "Item 1", "Item 2", "Item 3" });
        }
    }
}