Generic Datatables
==================

The Generic Datatables Project is an attempt at creating a flexible, generic component to create a datatable from any given IQueryable<T>
This datatable uses the excellent [datatables](http://www.datatables.net/) library

Technologies used:

- .Net MVC 4 (But it should work under MVC 3 as well)
- C# 5
- .Net 4
- Entity Framework 5

There are two remarkable things about this library:

1. It doesn't use dynamic expressions. 
2. Every Linq query this library makes is supported by Linq To Entities, making it highly performant. 

Notes:

There's still no graceful fallback in place (try executing the linq query against the database, if that fails load the data in memory and execute it in-memory). 
This means that if you try to use some exotic Linq query, it will probably come smacking you in the face.

Show me the goodies!
--------------------

It looks like this:

![Example](http://i.imgur.com/u3dbkyA.png?1)

How does it work?
-----------------

# Step 1: The html

The idea is simple: create a datatable, give it a unique name (There will be a session variable called 'datatables' that contains a dictionary of datatable configurations. In this dictionary, configurations will be saved with this names as the key)
and configure the properties that should be shown. Currently supported datatypes include int?, decimal?, double?, DateTime?, Timespan? and of course their non-nullable equivalents.
You need to give the properties names which should correspond with the javascript mDataProps (see Step 4). Don't worry, I don't use these for anything else so you can actually name them whatever you want.

Lastly, you can add a last column with custom html. The best way I've found to pass this in is through the use of a razor helper, but you could technically also use a partial.

The signature of the LastColumn() function is: 

	IHtmlString LastColumn(string lastColumnHeader, Func<TEntity, IHtmlString> lastColumnHtml);

Anyway, this is how the HtmlHelper works:
	
	@(Html.Datatable<Person>("personDatatable")
			.Property("Id", person => person.Id)
			.Property("Name", p => p.Name.ToLower())
			.Property("Birthday", p => p.Birthday, "dd/MM/yyyy")
			.Property("Address", p => p.Address.Street + " " + p.Address.HouseNumber)
			.Property("Time", p => p.Time, @"hh\:mm\:ss")
			.LastColumn("Actions", PersonButtons))
			
	@helper PersonButtons(Person person) {
		<a href="@Url.Action("PersonForm", new { id = person.Id})" class="btn btn-primary btn-edit">Edit</a>
		<a href="@person.Id" class="btn btn-danger">Delete</a>
	}

# Step 2: The model binder

Just like most other attempts at making a generic server-side wrapper for datatables, I've provided a C# class to map the incoming ajax requests and a model binder that does the heavy lifting for you.
All you need to do is register the model binder, put the following somewhere in Global.asax (or better, in a separate ModelBindersConfig in the App Start folder): 

	ModelBinders.Binders.Add(typeof(DatatableParam), new DatatableParamConverter());
	
# Step 3: The Controller

The idea of server-side processing with a datatable is that your MVC Controller accepts the incoming ajax requests, parses it, fetches the data from somewhere and applies the filtering, sorting, etc. specified in the request object.
However, because the library does most of the heavy work, you only have to glue some specific stuff together. In the PeopleController:

	public JsonResult Datatable(DatatableParam param)
	{
		var people = context.People.Include(p => p.Address); // fetch the data from somewhere
		var sessionObject = Session.GetDatatableProperties<Person>(param.DatatableId); // use a custom extension method on the Session object to get the relevant session object
		var parser = new DatatableParser<Person>(people, sessionObject); // make a parser and pass it the data and session object
		return parser.Parse(param).ToJson(); // have the parser parse the request parameters and return the Json Result
	}
		
# Step 4: The Javascript

Somewhere along the way I considered generating the necessary javascript with the HtmlHelper as well. I wanted to keep full control of the javascript though, so in the end I decided against it.
This means you need to write the following javascript to get the datatable to actually do something:

     $("#personDatatable").dataTable({
        bProcessing: true,
        bServerSide: true,
        sAjaxSource: "People/Datatable", // this links to your controller method from Step 3
        fnServerParams: function (aoData) {
            aoData.push({ name: "datatableId", value: "personDatatable" }); // add the name of the datatable to the ajax request. This should be equal to the unique name you gave it in Step 1
        },
        aoColumns: [ // You can configure, per property, if it should be sortable, etc. Make sure the names equal the names you gave it in step 1.
                    { mDataProp: 'Id' },
                    { mDataProp: 'Name' },
                    { mDataProp: 'Birthday' },
                    { mDataProp: 'Address' },
                    { mDataProp: 'Time' },
                    { mDataProp: 'Actions', bSearchable: false, bSortable: false }
            ],
        fnDrawCallback: function () {
            bindButton($(".btn-edit"));
        }
    });

# That's it!

If the stars align, it should work. Be sure to check out the demo page in the source (Views/People/Index).
There I've added a search filter per property, applied the Twitter Bootstrap theme and added add/edit/delete functionalities.
I also smacked the show/hide columns plugin on it, and also the infinite scrolling plugin (although it's not enabled by default)
I should make a simple, basic demo page someday. Shoot me a mail and maybe I will. :-)

Did you test this?
------------------

I actually have a suite of tests, however they are limited in quantity and they could definitely use additions. 
Note that the tests also use the database, since the project heavily relies on SqlFunctions. It was the only way to get Linq to Entities to play nice with crazy expressions, such as datetime formatting.

This is cool! Can I help?
-------------------------

Sure you can! Well, maybe, shoot me an email and we can discuss it. :-)

Are you sure this is really producing valid queries, and isn't loading the entire table in memory?
--------------------------------------------------------------------------------------------------

Yep! Here's some (crazy) queries that are generated:

Searching for '12' in the Birthday field (which is a DateTime formatted as dd/MM/yyyy) and also 'g' in the Name field (which is a simple string)

	SELECT TOP (10) 
	[Filter1].[Id1] AS [Id], 
	[Filter1].[Name] AS [Name], 
	[Filter1].[Birthday] AS [Birthday], 
	[Filter1].[Time] AS [Time], 
	[Filter1].[AddressId] AS [AddressId], 
	[Filter1].[Id2] AS [Id1], 
	[Filter1].[Street] AS [Street], 
	[Filter1].[HouseNumber] AS [HouseNumber], 
	[Filter1].[PostalCode] AS [PostalCode], 
	[Filter1].[City] AS [City]
	FROM ( SELECT 	[Extent1].[Id] AS [Id1], 
					[Extent1].[Name] AS [Name], 
					[Extent1].[Birthday] AS [Birthday], 
					[Extent1].[Time] AS [Time], 
					[Extent1].[AddressId] AS [AddressId], 
					[Extent2].[Id] AS [Id2], 
					[Extent2].[Street] AS [Street], 
					[Extent2].[HouseNumber] AS [HouseNumber], 
					[Extent2].[PostalCode] AS [PostalCode], 
					[Extent2].[City] AS [City], 
					row_number() OVER (ORDER BY [Extent1].[Id] ASC) AS [row_number]
		FROM  [dbo].[People] AS [Extent1]
		INNER JOIN [dbo].[Addresses] AS [Extent2] ON [Extent1].[AddressId] = [Extent2].[Id]
		WHERE 
			(( CAST(CHARINDEX(LOWER(N'g'), LOWER(LOWER([Extent1].[Name]))) AS int)) > 0) 
		AND 
			(( CAST(CHARINDEX(LOWER(N'12'), LOWER(N'' + REPLACE(STR( CAST( DATEPART(day, [Extent1].[Birthday]) AS float)), N' ', N'') + N'/' + REPLACE(STR( CAST( DATEPART(month, [Extent1].[Birthday]) AS float)), N' ', N'') + N'/' + REPLACE(STR( CAST( DATEPART(year, [Extent1].[Birthday]) AS float)), N' ', N''))) AS int)) > 0)
	)  AS [Filter1]
	WHERE [Filter1].[row_number] > 0
	ORDER BY [Filter1].[Id1] ASC
	
Searching for '20' in a 'Timespan' field and for 'T' in the address field (which is a navigation property of Person)

	SELECT TOP (10) 
	[Filter1].[Id1] AS [Id], 
	[Filter1].[Name] AS [Name], 
	[Filter1].[Birthday] AS [Birthday], 
	[Filter1].[Time] AS [Time], 
	[Filter1].[AddressId] AS [AddressId], 
	[Filter1].[Id2] AS [Id1], 
	[Filter1].[Street] AS [Street], 
	[Filter1].[HouseNumber] AS [HouseNumber], 
	[Filter1].[PostalCode] AS [PostalCode], 
	[Filter1].[City] AS [City]
	FROM ( SELECT 	[Extent1].[Id] AS [Id1], 
					[Extent1].[Name] AS [Name], 
					[Extent1].[Birthday] AS [Birthday], 
					[Extent1].[Time] AS [Time], 
					[Extent1].[AddressId] AS [AddressId], 
					[Extent2].[Id] AS [Id2], 
					[Extent2].[Street] AS [Street], 
					[Extent2].[HouseNumber] AS [HouseNumber], 
					[Extent2].[PostalCode] AS [PostalCode], 
					[Extent2].[City] AS [City], 
					row_number() OVER (ORDER BY [Extent1].[Time] ASC) AS [row_number]
		FROM  [dbo].[People] AS [Extent1]
		INNER JOIN [dbo].[Addresses] AS [Extent2] ON [Extent1].[AddressId] = [Extent2].[Id]
		WHERE 
			(( CAST(CHARINDEX(LOWER(N'20'), LOWER(N'' + REPLACE(STR( CAST( DATEPART(hour, [Extent1].[Time]) AS float)), N' ', N'') + N'\' + N':' + REPLACE(STR( CAST( DATEPART(minute, [Extent1].[Time]) AS float)), N' ', N'') + N'\' + N':' + REPLACE(STR( CAST( DATEPART(second, [Extent1].[Time]) AS float)), N' ', N''))) AS int)) > 0)
		AND 
			(( CAST(CHARINDEX(LOWER(N'T'), LOWER([Extent2].[Street] + N' ' + [Extent2].[HouseNumber])) AS int)) > 0)
	)  AS [Filter1]
	WHERE [Filter1].[row_number] > 0
	ORDER BY [Filter1].[Time] ASC

