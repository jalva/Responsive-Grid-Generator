# Responsive-Grid-Generator
If you ever need to declare responsive layouts on the server-side (for example using a CMS) then this component will give you just that.
Using the Responsive Grid Generator you can use a single ASP.Net Core View Component which will render any custom responsive grid layout.
This responsive grid layout view component will accept (as its model) a list of child partial views that will serve as the content rendered
inside the gird's responsive columns. 

This component uses a simple declarative approach to define the responsive grid, using a query string (name-value pair) syntax. 
The responsive grid is based on the Bootstrap 2.
The layout metadata describes the pattern in which to render your custom content (partial views) inside the responsive grid.
In order to declare the responsive grid using this component, you will need to provide the layout metadata for each of the 4 screen sizes (Bootstrap 2 breakpoints: Lg, Md, Sm and Xs).

The key in the key-value pair represents the row number and the value represents a CSV list of columns and their associated widths. For example: **0=8,4** represents a row that has 2 columns, one with width '8' and the second with width '4' (using the 12 virtual column-grid used in Bootstrap 2). The row can define from 1 up to 12 columns. The sum of the CSV values in each pair cannot exceed 12. For example, **0=8,8** is incorrect since 8+8=16 which is greater than 12.

For example, in order to render the following layout, assuming you have 9 partial views to display:

![responsive layout](https://github.com/jalva/Responsive-Grid-Generator/blob/master/responsive-grid-layout-example.PNG)
      
You would use the following syntax to describe such a grid layout: 

**0=12&1=6,6&2=4,4,4**

The above query string describes the grid layout shown in the screenshot. Let's break it down into the query string components:

* 0=12 - first row has one content column which is full width (col-*-12)
* 1=6,6 - second row has two content columns which are of equal width (col-*-6)
* 2=4,4,4 - third row has three content columns which are of equal width (col-*-4)

So the above layout query string describes a layout pattern consisting of 3 rows with the content columns described above. 
Such a query string has to be defined for all 4 screen sizes (Lg, Md, Sm and Xs). Each screen size can have a different layout query string defining a different responsive grid for every screen size (any number of rows, any number of columns). 
You only need to specify the unique rows in the layout pattern query string. 
For example, the above layout **0=12&1=6,6&2=4,4,4** defines a pattern of 3 rows, each having a different number of columns.
This 3 row pattern will repeat itself on a list of partial views of any length. 

If you want the above layout to have all columns stacked on top of each other for any given screen size (for example, for Xs) then use the following query string syntax: **0=12** . This means that the pattern consists of one row with one full width content column. 


The [following file](https://github.com/jalva/Responsive-Grid-Generator/blob/master/WebAppWithGridGenerator/Views/Home/Index.cshtml) contains the code that renders the above layout using the **'GridContainer'** view component, passing to it the responsive layout query strings (for all 4 screen sizes) as well as the partial views to render along with their corresponding view models:

```
@await Component.InvokeAsync("GridContainer", new GridLayoutContainer_ViewModel
            {
                Lg_Row_Indx_To_Col_Sizes_Csv = "0=12&1=6,6&2=4,4,4", // for large screen sizes use the pattern: 1 row with 1 column (col-lg-12), 1 row with 2 columns of equal width (col-lg-6), 1 row with 3 columns of equal width (col-lg-4)
                Md_Row_Indx_To_Col_Sizes_Csv = "0=4,6,2&1=4,8", // for medium screen sizes use the pattern: 1 row with 3 columns (col-md-4 and col-md-6 and col-md-2), 1 row with 2 columns (col-md-4 and col-md-8)
                Sm_Row_Indx_To_Col_Sizes_Csv = "0=6,6", // for small screen sizes use the pattern: 1 row with 2 columns of equal width (col-sm-6)
                Xs_Row_Indx_To_Col_Sizes_Csv = "0=12", // for xsmall screen sizes use the pattern: 1 row with 1 column (col-xs-12)
                
                ColumnPartialsWithModels = new List<Tuple<string, object>>
                {
                    new Tuple<string, object>("Shared/Partials/Card", new Card_ViewModel
                    {
                        ImgUrl = "/images/profile.png",
                        Title = "Title 1",
                        Subtitle = "Subtitle 1",
                        DescriptionParagraphs = new List<string>
                        {
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas in justo ligula. Nulla facilisi. Morbi non tellus vel augue commodo condimentum ac vitae metus.",
                            "Proin vel ipsum vel erat porta porttitor. Sed urna libero, placerat et aliquet eget, facilisis quis ante. Pellentesque congue laoreet mattis. "
                        },
                        DetailsUrl = "#"
                    }),
                    new Tuple<string, object>("Shared/Partials/Card", new Card_ViewModel
                    {
                        ImgUrl = "/images/profile.png",
                        Title = "Title 2",
                        Subtitle = "Subtitle 2",
                        DescriptionParagraphs = new List<string>
                        {
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas in justo ligula. Nulla facilisi. Morbi non tellus vel augue commodo condimentum ac vitae metus.",
                            "Proin vel ipsum vel erat porta porttitor. Sed urna libero, placerat et aliquet eget, facilisis quis ante. Pellentesque congue laoreet mattis. "
                        },
                        DetailsUrl = "#"
                    }),
                    ... rest of the custom views to render inside the responsive grid
```
