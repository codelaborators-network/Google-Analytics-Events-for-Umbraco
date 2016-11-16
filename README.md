# Umbraco Google Analytics Events Tracking

A package for adding manageable GA Event Tracking to your Umbraco site.

## Install

1. Install the Umbraco-GA-Event-Tracking package (we are still working on the first release)
2. Add the Following code to your master template (order is important)

```javascript

@Html.IncludeGoogleAnalytics("insert key here") // Only needed if you have not already included Google Analytics
@Html.IncludeGaEventTracking()

```

## How to Use

### Coming soon

## Contributers

The package is contained in a separate project in the Visual Studio solution. 

Building this project will automatically copy over all necessary files to the Demo website which you can use to test.

The login for the demo site is: 

User: admin@test.com
Password: password123

Please contact either Paul or Luke before putting in a PR

## More is coming soon!
