# BootstrapComboBox for ASP.NET Core - How to implement a Cascading Combo Boxes scenario
This example demonstrates how to implement a Cascading Combo Boxes scenario using a Bootstrap-based component for ASP.NET Core: [BootstrapComboBox](https://demos.devexpress.com/aspnetcore-bootstrap/Editors-ComboBox).

## Steps to implement:
1. Create two Partial Views: one for the Category combo box and the other for the Product combo box (whose data source depends on selection in the Category combo box).
2. Add these partial views to the necessary view
3. Add BootstrapComboBox to the "CategoryComboBoxPartialView" Partial View and handle its client-side SelectedIndexChanged event. It's necessary to perform a callback to the Product combo box in this event handler:

```csharp 
@(Html.DevExpress()
      .BootstrapComboBox("CategoryID")
      ...
      .Bind(ViewData["Category"])
      .ClientSideEvents(cs => cs.SelectedIndexChanged("function(s, e) { ProductID.PerformCallback({ categoryChanged: true }); }"))
)
```
4. Add BootstrapComboBox to the "ProductComboBoxPartialView" Partial View and handle its client-side BeginCallback event. Pass the required data for filtering (the Category combo-box value in this case) to the Product combo-box callback's action method:

```csharp 
@(Html.DevExpress()
      .BootstrapComboBox("ProductID")
      .Routes(routes => routes.MapRoute(r => r
          .Action("ProductComboBoxPartialView")
          .Controller("Home")))
	  ...
      .Bind(ViewData["Product"])
      .ClientSideEvents(cs => cs.BeginCallback("function(s, e) { e.customArgs['Category'] = CategoryID.GetValue(); e.customArgs['Product'] = s.GetValue(); }"))
)
```
5. Specify the "ProductComboBoxPartialView" action method and set the data source based on passed data: 

```csharp  
public IActionResult ProductComboBoxPartialView(int? Category, int? Product, bool? categoryChanged)
{
		if (Category != null)
				ViewData["Product"] = NorthwindContext.Products.Where(m => m.CategoryID == Category);
		else
                ViewData["Product"] = NorthwindContext.Products;
		...
}
``` 

