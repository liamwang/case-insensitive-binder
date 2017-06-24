# Case-Insensitive Dynamic Model Binder

This is an ASP.NET Core dynamic model binder which implements binding dynamic type with case-insensitive properties.

## Installation

At present the code is pre-release but when ready it will be available on Nuget.

## Manual build

If you prefer, you can compile CaseInsensitiveDynamicModelBinder yourself, you'll need:

* Visual Studio 2015 with Update 3 (or above), or Visual Studio Code.
* The .NET Core 1.0 SDK Installer.

To clone it locally click the "Clone or dowload" button above or run the following git commands.

```bash
git clone https://github.com/liamwang/CaseInsensitiveDynamicModelBinder.git
```

## Configuration

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options => options.InsertCaseInsensitiveDynamicModelBinder());
}
```

## Use in Controller

```csharp
[HttpPut]
public IActionResult Put([Insensitive] dynamic data)
{
    string value1 = data.A;
    string value2 = data.a;

    bool b = value1 == value2; // true

    return Ok();
}
```

## Sample Preview

![Sample Preview](https://user-images.githubusercontent.com/5000396/27509536-18f34ddc-5932-11e7-8f64-e1314b750726.gif)

