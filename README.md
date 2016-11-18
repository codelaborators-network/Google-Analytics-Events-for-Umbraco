# Google Analytics Events for Umbraco
![Google Analytics Events for Umbraco Logo](https://github.com/codelaborators-network/Google-Analytics-Events-for-Umbraco/blob/master/umbraco-ga-event-tracking.jpg?raw=true "Google Analytics Events for Umbraco Logo")

A package for adding manageable GA Event Tracking to your Umbraco site.

## Install

1. Install the Google Analytics Events for Umbraco package (we are still working on the first release)
2. Add the Following code to your master template (order is important)

```javascript

@Html.IncludeGoogleAnalytics("insert key here") // Only needed if you have not already included Google Analytics
@Html.IncludeGaEventTracking()

```

### Please read the [Documentation Website](https://github.com/codelaborators-network/Google-Analytics-Events-for-Umbraco) for more in depth guidelines

