# SiteMapNetCore

This library is intended as a **drop-in replacement for the `System.Web.SiteMap` functionality** found in .NET Framework, for use in **.NET 8** projects.

See the official .NET Framework documentation here: [System.Web.SiteMap](https://learn.microsoft.com/en-us/dotnet/api/system.web.sitemap?view=netframework-4.8.1).

---

## Installation

Install via NuGet:

```bash
dotnet add package SiteMapNetCore
```

```package reference
<PackageReference Include="SiteMapNetCore" Version="1.0.0" />
```
---

## Usage

To register the SiteMap in your **Dependency Injection (DI)** container:

```csharp
builder.Services.AddSiteMap();
```

By default, this will look for a `Web.sitemap` file in your project's **content root**.

You can also provide a **custom path** to your sitemap file:

```csharp
builder.Services.AddSiteMap("Config/Web.sitemap");
```

---

## Features

* Supports **.NET 8**
* Provides **SiteMap** and **SiteMapNode** classes similar to the original System.Web implementation.
* Easy integration via DI with optional custom sitemap file path.

---


💡 **Tip:** Ensure your sitemap file is included in your project output, or provide the correct path to `AddSiteMap`.
