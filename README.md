# Umbraco-GA-Event-Tracking

A package for adding manageable GA Event Tracking to your Umbraco site

## Setup

1. Download and Install [Archetype](https://our.umbraco.org/projects/backoffice-extensions/archetype) via the package install or manually
2. Install the Umbraco-GA-Event-Tracking package (we are still working on the first release)
3. Add the "Event Analytics" Document Type via composition to the root document type if your Umbraco website
4. Add the Following code to your master template (order is important)

```javascript

@Html.IncludeGoogleAnalytics("insert key here") // Only needed if you have not already included Google Analytics
@Html.IncludeGaEventTracking()

```

## Contributers

The package is contained in a separate project in the Visual Studio solution. 

Building this project will automatically copy over all necessary files to the Demo website which you can use to test.

The login for the demo site is: 

User: admin@test.com
Password: password123

## More is coming soon!
