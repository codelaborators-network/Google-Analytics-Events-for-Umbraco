<%@ Control Language="C#" AutoEventWireup="true" Inherits="UmbracoGAEventTracking.GAEventTracking.BackOffice.Installer" %>
<h2>Google Analytics Events for Umbraco has been successfully installed!
</h2>

<p>
    You are nearly up and running.
</p>
<p>
    All you need to do now is include the following code AFTER your Google Analytics registration script.
</p>
<pre>
    @Html.IncludeGaEventTracking()
</pre>

<p>
    Need some more help?
    <a href="https://github.com/codelaborators-network/Google-Analytics-Events-for-Umbraco">Check out the documentation
    </a>
</p>

<h3>Pre-installed Events
</h3>

<p>
    We have installed some commonly used events for you to see how the package works and speed things up.
</p>

<p>
    You can safely delete all this content if you wish to rather add your own.
</p>