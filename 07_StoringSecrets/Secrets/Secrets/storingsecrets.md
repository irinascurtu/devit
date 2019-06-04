#### Goal & objective
- to be able to use secrets.json 
- understand azurekeyvault
- Talk about Hsts(), and different security middlewars
#### Steps:
- walktrough slides
- short demo
- make them download a repo,

##### Lab:

### Security Headers
#### HTTP Strict Transport Security (HSTS)
```
app.UseHsts(new HstsOptions
        {
            Seconds = 30 * 24 * 60 * 60,
            IncludeSubDomains = false,
            Preload = false
        });
```

#### Public Key Pinning Extension for HTTP (HPKP)

```
app.UseHpkp(hpkp =>
        {
            hpkp.UseMaxAgeSeconds(7 * 24 * 60 * 60)
                .AddSha256Pin("nrmpk4ZI3wbRBmUZIT5aKAgP0LlKHRgfA2Snjzeg9iY=")
                .SetReportOnly()
                .ReportViolationsTo("/hpkp-report");
        });
```

#### X-Content-Type-Options

#### Content-Security-Policy (CSP)
It is an effective measure to protect your site from XSS attacks. By whitelisting sources of approved content, you can prevent the browser from loading malicious assets.
Recommended values: "Content-Security-Policy: default-src 'self'; img-src 'self'; style-src 'self' 'unsafe-inline'; font-src 'self'; script-src 'self' 'unsafe-inline'; connect-src 'self';"
#### X-Download-Options
It instruct the browser not to open a download directly in the browser, but instead to provide only the ‘Save’ option.
Recommended value “ X-Download-Options: noopen”

##### Tool to check online security: 
```
kestrel configuration
--addusersecrets<startup> or generate a guid
-addusersecrets<guid> -


 <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
    <UserSecretsId>34cf16c9-01cd-46f0-ab5f-93da75a4ab69</UserSecretsId>
  </PropertyGroup>


  curent directory vs path
```