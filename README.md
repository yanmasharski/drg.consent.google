# DRG Consent — Google UMP

Google UMP (User Messaging Platform) adapter for `drg.consent`.

## Assembly

`DRG.Consent.Google` — `ConsentPlatformGoogle`

## Key types

- **`ConsentPlatformGoogle`** — `IConsentPlatform` implementation using the Google UMP SDK. Requests consent information, shows the form if required, and maps the result to `ConsentState`.

## Usage

```csharp
var consent = new ConsentPlatformGoogle(logger);
locator.Register<IConsentPlatform>(consent);
```

## External requirement

Install the **Google Mobile Ads Unity plugin** in the consuming project. UMP APIs (`GoogleMobileAds.Ump.Api`) ship inside that plugin.

**Unity package ID:** **`com.google.ads.mobile`**

This plugin is **not** listed on [Google APIs for Unity — archive](https://developers.google.com/unity/archive). Use the flows Google documents—for example OpenUPM with the `com.google` scope or a `.unitypackage` from GitHub—as described in [AdMob Unity quick start](https://developers.google.com/admob/unity/quick-start).

**Recommendation:** For Google Unity packages published on [Google APIs for Unity — archive](https://developers.google.com/unity/archive), prefer `.tgz` entries in `Packages/manifest.json`. Mobile Ads (`com.google.ads.mobile`) follows the AdMob distribution paths linked above.

## Dependencies

**Declared in `package.json` (bundled with this package):**

- `com.drg.consent`
- `com.drg.core`

**Must be added separately in the host project:**

- `com.google.ads.mobile` — Google Mobile Ads Unity plugin (includes UMP).

## Install

```
https://github.com/yanmasharski/drg.consent.google.git#0.9.0
```
